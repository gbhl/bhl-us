CREATE PROCEDURE servlog.ServiceSelectAll

AS
BEGIN
	SET NOCOUNT ON

	SELECT	ServiceID
			,s.[Name]
			,s.[Param]
			,ISNULL(f.[Label], '') AS FrequencyLabel
			,f.IntervalInMinutes
			,[Disabled]
			,s.Display
			,s.CreationDate
			,s.CreationUserID
			,s.LastModifiedDate
			,s.LastModifiedUserID
	FROM	servlog.[Service] s
			LEFT JOIN servlog.Frequency f ON s.FrequencyID = f.FrequencyID
	ORDER BY s.[Name], s.[Param]
END
GO
