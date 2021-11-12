CREATE PROCEDURE dbo.SegmentSelectPagination

@SegmentID int

AS 

SELECT	s.SegmentID AS ItemID,
		s.BarCode,
		I.ItemStatusID,
		s.PaginationStatusID,
		s.PaginationStatusDate,
		s.PaginationStatusUserID,
		U.FirstName + ' ' + U.LastName AS PaginationUserName,
		PS.PaginationStatusName
FROM	dbo.Item I
		INNER JOIN dbo.Segment s ON i.ItemID = s.ItemID
		LEFT OUTER JOIN dbo.PaginationStatus PS ON PS.PaginationStatusID = s.PaginationStatusID
		LEFT OUTER JOIN dbo.AspNetUsers U ON U.Id = s.PaginationStatusUserID
WHERE	s.SegmentID = @SegmentID

GO
