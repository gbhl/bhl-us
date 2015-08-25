CREATE PROCEDURE [dbo].[SegmentSelectWithoutSubmittedDOI]

@NumberToReturn int = 10

AS

BEGIN

SET NOCOUNT ON

DECLARE @NumToReturn int
SET @NumToReturn = @NumberToReturn

DECLARE @ISSNID int
SELECT @ISSNID = IdentifierID FROM dbo.Identifier WITH (NOLOCK) WHERE IdentifierName = 'ISSN'

DECLARE @TreatmentID int
SELECT @TreatmentID = SegmentGenreID FROM dbo.SegmentGenre WITH (NOLOCK) WHERE GenreName = 'Treatment'

SELECT DISTINCT TOP (@NumToReturn)
		ISNULL(d.DOIID, 0) AS DOIID,
		ISNULL(d.DOIEntityTypeID, 0) AS DOIEntityTypeID,
		s.SegmentID AS EntityID,
		ISNULL(d.DOIStatusID, 0) AS DOIStatusID,
		ISNULL(d.DOIName, '') AS DOIName
FROM	dbo.Segment s WITH (NOLOCK) 
		LEFT JOIN dbo.DOI d WITH (NOLOCK) ON s.SegmentID = d.EntityID	AND d.DOIEntityTypeID = 40 -- Segment
		INNER JOIN dbo.SearchCatalogSegment c WITH (NOLOCK) ON s.SegmentID = c.SegmentID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON s.ItemID = i.ItemID
		LEFT JOIN dbo.Title_Identifier ti WITH (NOLOCK) ON i.PrimaryTitleID = ti.TitleID AND ti.IdentifierID = @ISSNID
		LEFT JOIN dbo.SegmentIdentifier si WITH (NOLOCK) ON s.SegmentID = si.SegmentID AND si.IsContainerIdentifier = 1 AND si.IdentifierID = @ISSNID
WHERE	s.SegmentStatusID = 20 -- Published
AND		(d.DOIStatusID = 10 -- No DOI
OR		d.DOIStatusID = 20 -- DOI Assigned (but not submitted)
OR		d.DOIStatusID = 30 -- Pending Resubmit
OR		d.DOIStatusID = 40 -- Batch ID Assigned
OR		d.DOIID IS NULL)
AND		c.HasLocalContent = 1 -- Make sure that the segment content is held within BHL
AND		s.ContributorCode <> 'USER'  -- No user-contributed segments
AND		(ti.IdentifierValue IS NOT NULL OR si.IdentifierValue IS NOT NULL) -- Make sure that an ISSN exists for the segment container
AND		s.SegmentGenreID <> @TreatmentID	-- Any segment types except treatments

END
