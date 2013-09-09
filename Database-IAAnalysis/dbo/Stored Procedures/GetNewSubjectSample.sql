CREATE PROCEDURE [dbo].[GetNewSubjectSample]

AS
BEGIN

SET NOCOUNT ON

DECLARE @SampleDate DATETIME

-- If we don't already have a sample from today, then get a new sample
SET @SampleDate = CONVERT(DATETIME, CONVERT(NVARCHAR(12), GETDATE(), 101))

IF (NOT EXISTS(SELECT TOP 1 * FROM SubjectSample WHERE SampleDate = @SampleDate))
BEGIN
	-- Eliminate plurals ('s' and 'es' at end of subjects) from sample
	INSERT INTO SubjectSample (TagText, [Count], SampleDate)
--	SELECT '%' + CASE WHEN RIGHT(TagText, 1) = 's' THEN LEFT(TagText, LEN(TagText) - 1) ELSE TagText END + '%' AS TagText, [Count], @SampleDate
--	FROM (
--		SELECT CASE WHEN RIGHT(TagText, 2) = 'es' THEN LEFT(TagText, LEN(TagText) - 2) ELSE TagText END AS TagText, [Count]
--		FROM (
			SELECT	TagText, COUNT(*) AS [Count], @SampleDate
			FROM	BHL.dbo.TitleTag
			WHERE	MarcDataFieldTag = '650' 
			AND		MarcSubFieldCode = 'a'
			GROUP BY TagText HAVING COUNT(*) >= 5
--			) x
--		) y
END

PRINT 'DON''T FORGET:  Manually review the output of this procedure (check the SubjectSample table)'

END
