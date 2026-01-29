using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.IO;

namespace MOBOT.BHL.Web2
{
    public partial class ucFeatureBox : System.Web.UI.UserControl
    {

        private string featureType = "support";

        public string FeatureType
        {
            get
            {
                return featureType;
            }
            set
            {
                featureType = value;
            }
        }

        private string _class = "featurebox";
        public string SpecialClass
        {
            get
            {
                return _class;
            }
            set {
                _class = value;
            }
        }

        public string specialID {get; set;}

        protected string title { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (this.featureType.ToUpper())
            {
                case "SUPPORT":
                    if (AppConfig.ShowNewFuture)
                        panNewFuture.Visible = true;
                    else
                        panSupport.Visible = true;
                    break;
                case "BLOG":
                    panBlog.Visible = true;
                    break;
                case "COLLECTION":
                    if (AppConfig.ShowNewFuture)
                    {
                        panSupportLarge.Visible = true;
                    }
                    else
                    {
                        PopulateCollectionBox();
                        panCollection.Visible = true;
                    }
                    break;
                case "FLICKR":
                    panFlickr.Visible = true;
                    PopulateFlickrClass();
                    break;

            }
        }

        protected void PopulateCollectionBox()
        {

            string cacheKey = "FeaturedCollection";
            Collection collection = null;

            if (Cache[cacheKey] != null)
            {
                // Used the cached version of the feed contents
                collection = (Collection)Cache[cacheKey];
            }
            else
            {

                BHLProvider provider = new BHLProvider();
                collection = provider.CollectionSelectFeatured();

                // Cache the featured collection
                if (collection != null)
                {
                    Cache.Add(cacheKey, collection, null, DateTime.Now.AddMinutes(AppConfig.FeaturedCollectionCacheTime),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }

            if (collection == null)
            {
                panCollection.Visible = false;
            }
            else
            {
                string hrefRoot = string.Empty;
                if (collection.HtmlContent == string.Empty) hrefRoot = "/browse";
                lnkFeaturedCollectionImage.HRef = string.Format(lnkFeaturedCollectionImage.HRef,
                    hrefRoot,
                    (collection.CollectionURL == string.Empty ? collection.CollectionID.ToString() : collection.CollectionURL));
                lnkFeaturedCollectionImage.Title = collection.CollectionName;
                imgFeaturedCollection.Src = collection.ImageURL;
                imgFeaturedCollection.Alt = collection.CollectionName;
               
                lnkCollectionButton.HRef = string.Format(lnkCollectionButton.HRef,
                    hrefRoot,
                    (collection.CollectionURL == string.Empty ? collection.CollectionID.ToString() : collection.CollectionURL));
                lnkCollectionButton.Title = collection.CollectionName;
                title = collection.CollectionName;
                //set special class
               


                //lnkFeaturedCollectionName.InnerHtml = collection.CollectionName;
            }

        }

        protected void PopulateFlickrClass()
        {
                string[] flickrThumbList = null;
                string cacheKey = "FlickrThumbnailList";
                if (Cache[cacheKey] != null)
                {
                    // Use cached version
                    flickrThumbList = (string[])Cache[cacheKey];
                }
                else
                {
                    if (File.Exists(Request.PhysicalApplicationPath + "\\flickrthumbs.txt"))
                    {
                        flickrThumbList = File.ReadAllLines(Request.PhysicalApplicationPath + "\\flickrthumbs.txt");
                        Cache.Add(cacheKey, flickrThumbList, null, DateTime.Now.AddMinutes(AppConfig.FlickrThumbListCacheTime),
                            System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                    }
                }

                // Show the flickr thumbnails
                if (flickrThumbList != null)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<ul>\n");
                    foreach (string flickrThumb in flickrThumbList)
                    {
                        string[] thumbDetails = flickrThumb.Split('\t');
                        sb.Append(string.Format(
                            "<li><a href='/page/{0}' title='{1}'><img alt='Flickr image:{1}' src='/images/flickrthumbs/{2}.jpg'></a></li>\n",
                            thumbDetails[0],
                            thumbDetails[2] + " - " + (thumbDetails[3].Length > 0 ? thumbDetails[3] : thumbDetails[4]),
                            thumbDetails[0]));
                    }
                    sb.Append("</ul>\n");
                    flickrList.Text = sb.ToString();

                }

        }


    }
}