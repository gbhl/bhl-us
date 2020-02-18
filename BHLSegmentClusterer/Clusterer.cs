using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DocumentResolver;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOBOT.BHL.SegmentClusterer
{
    public class Clusterer
    {
        BHLProvider provider = null;
        int _segmentStatusNew = 10;
        int _segmentStatusPublished = 20;

        public Clusterer()
        {
            provider = new BHLProvider();
        }

        public void Cluster(int? itemID = null)
        {
            List<Segment> newSegments = provider.SegmentSelectByStatusID(_segmentStatusNew);
            List<Segment> publishedSegments = provider.SegmentSelectByStatusID(_segmentStatusPublished);
            Dictionary<int, Segment> segmentDictionary = GetSegmentDictionary(newSegments, publishedSegments);
            Dictionary<int, Segment> segments = GetSegmentsToCluster(newSegments, itemID);

            // Set up the resolver
            Dictionary<string, string> resolverDictionary = GetResolverDictionary(segmentDictionary);
            MOBOT.BHL.DocumentResolver.DocumentResolver documentResolver = new MOBOT.BHL.DocumentResolver.DocumentResolver(
                resolverDictionary, MOBOT.BHL.DocumentResolver.DocumentResolver.EngineType.BayesTFIDF);

            foreach (KeyValuePair<int, Segment> kvp in segments)
            {
                // Make sure we haven't already clustered the current segment while processing the other new segments
                if ((kvp.Value.SegmentClusterId ?? 0) == 0)
                {
                    List<ResolutionResult> resolutionResults = ResolveSegment(documentResolver, kvp.Key.ToString(),
                        kvp.Value.Title, kvp.Value.Date, kvp.Value.Authors);

                    int? clusterID = null;
                    foreach (ResolutionResult resolutionResult in resolutionResults)
                    {
                        // If the resolution results indicates a matching segment and the DOIs match
                        // (or are not available for comparison), then cluster the segments together.
                        if (resolutionResult.Match ?? false)
                        {
                            if (DOICompare(kvp.Value, segmentDictionary[Convert.ToInt32(resolutionResult.Key)]))
                            {
                                // Found a match... cluster it with the segment being evaluated, creating a new cluster 
                                // record if necessary
                                if ((clusterID ?? 0) == 0)
                                {
                                    clusterID = segmentDictionary[Convert.ToInt32(resolutionResult.Key)].SegmentClusterId;
                                    if ((clusterID ?? 0) == 0)
                                    {
                                        // Add a new segment cluster
                                        clusterID = InsertSegmentCluster();
                                    }
                                }

                                // Add the segment(s) to the cluster
                                if ((kvp.Value.SegmentClusterId ?? 0) == 0)
                                    InsertSegmentClusterSegment(kvp.Key, (int)clusterID);
                                if ((segmentDictionary[Convert.ToInt32(resolutionResult.Key)].SegmentClusterId ?? 0) == 0)
                                    InsertSegmentClusterSegment(Convert.ToInt32(resolutionResult.Key), (int)clusterID);

                                // Update the dictionary entries with the new cluster ID
                                segmentDictionary[kvp.Key].SegmentClusterId = clusterID;
                                segmentDictionary[Convert.ToInt32(resolutionResult.Key)].SegmentClusterId = clusterID;
                                segments[kvp.Key].SegmentClusterId = clusterID;
                                if (segments.ContainsKey(Convert.ToInt32(resolutionResult.Key)))
                                    segments[Convert.ToInt32(resolutionResult.Key)].SegmentClusterId = clusterID;
                            }
                        }

                        // Log result to DB
                        InsertSegmentResolutionLog(kvp.Key, resolutionResult);
                    }
                }

                // Change segment status from New to Published
                UpdateSegmentStatus(kvp.Key);
            }            
        }

        /// <summary>
        /// Resolve the new segment against the existing list of segments
        /// </summary>
        /// <param name="documentResolver"></param>
        /// <param name="kvp"></param>
        /// <returns></returns>
        private static List<ResolutionResult> ResolveSegment(MOBOT.BHL.DocumentResolver.DocumentResolver documentResolver, 
            string key, string title, string date, string authors)
        {
            string segment = string.Format("{0} {1} {2}", title, date, authors);
            List<ResolutionResult> resolutionResults = documentResolver.Resolve(segment, true);

            // Only consider results with a resolution score of at least 0.5.  Also, the resolver may 
            // match the segment against itself... filter that result out here.
            List<ResolutionResult> limitedResults =
                (from resolutionResult
                     in resolutionResults
                 where resolutionResult.Score >= 0.5
                 && resolutionResult.Key != key
                 orderby resolutionResult.Score descending
                 select resolutionResult).ToList();

            return limitedResults;
        }

        /// <summary>
        /// Determine if the DOI of two segments match.
        /// </summary>
        /// <remarks>
        /// If one or both segments do NOT have a DOI, then return true (assume a match).  
        /// If both segments have DOIs, then compare them.
        /// </remarks>
        /// <param name="segment"></param>
        /// <param name="matchingSegment"></param>
        /// <returns></returns>
        private bool DOICompare(Segment segment, Segment matchingSegment)
        {
            bool isMatch = true;

            if (!string.IsNullOrWhiteSpace(segment.DOIName) && !string.IsNullOrWhiteSpace(matchingSegment.DOIName))
            {
                isMatch = (segment.DOIName == matchingSegment.DOIName);
            }

            return isMatch;
        }

        /// <summary>
        /// Convert new and published segment lists into a single dictionary of segments.
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, Segment> GetSegmentDictionary(List<Segment> newSegments, List<Segment> publishedSegments)
        {
            Dictionary<int, Segment> dictionary = new Dictionary<int, Segment>();
            foreach (Segment segment in newSegments)
            {
                dictionary.Add(segment.SegmentID, segment);
            }
            foreach (Segment segment in publishedSegments)
            {
                dictionary.Add(segment.SegmentID, segment);
            }

            return dictionary;
        }

        /// <summary>
        /// Convert the segment dictionary into a form acceptable to the DocumentResolver
        /// </summary>
        /// <param name="segmentDictionary"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetResolverDictionary(Dictionary<int, Segment> segmentDictionary)
        {
            Dictionary<string, string> resolverDictionary = new Dictionary<string, string>();

            foreach (KeyValuePair<int, Segment> kvp in segmentDictionary)
            {
                string segment = string.Format("{0} {1} {2}", kvp.Value.Title, kvp.Value.Date, kvp.Value.Authors);
                resolverDictionary.Add(kvp.Key.ToString(), segment);
            }

            return resolverDictionary;
        }

        /// <summary>
        /// Convert the list of new, unclustered segments into a dictionary.
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, Segment> GetSegmentsToCluster(List<Segment> newSegments, int? itemID = null)
        {
            Dictionary<int, Segment> dictionary = new Dictionary<int, Segment>();
            foreach (Segment segment in newSegments)
            {
                // If no item was specified, or if the segment's item matches the specified item, then include
                // the segment in the dictionary.
                if (((itemID ?? 0) == 0) || (itemID ?? 0) == (segment.ItemID ?? -1))
                    dictionary.Add(segment.SegmentID, segment);
            }
            return dictionary;
        }

        /// <summary>
        /// Create a new segment cluster, returning its identifier.
        /// </summary>
        /// <returns></returns>
        private int InsertSegmentCluster()
        {
            SegmentCluster cluster = provider.SegmentClusterInsertAuto(1);
            return cluster.SegmentClusterID;
        }

        /// <summary>
        /// Add the specified segment to the specified cluster
        /// </summary>
        /// <param name="segmentID"></param>
        /// <param name="segmentClusterID"></param>
        /// <returns></returns>
        private void InsertSegmentClusterSegment(int segmentID, int segmentClusterID)
        {
            provider.SegmentClusterSegmentAuto(segmentID, segmentClusterID, 1);
        }

        /// <summary>
        /// Log the resolution result
        /// </summary>
        private void InsertSegmentResolutionLog(int segmentID, ResolutionResult resolutionResult)
        {
            provider.SegmentResolutionLogInsertAuto(segmentID, Convert.ToInt32(resolutionResult.Key), resolutionResult.Score, 1);
        }

        /// <summary>
        /// Change the status of the segment from New to Published
        /// </summary>
        /// <param name="segmentID"></param>
        private void UpdateSegmentStatus(int segmentID)
        {
            provider.SegmentUpdateStatus(segmentID, _segmentStatusPublished);
        }
    }
}
