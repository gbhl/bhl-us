
CREATE PROCEDURE [dbo].[ImportErrorSelectRecent]

@NumErrors INT = 10

AS
BEGIN

SET NOCOUNT ON

SELECT	ImportErrorID,
		KeyType,
		KeyValue,
		ErrorDate,
		Number,
		Severity,
		State,
		[Procedure],
		Line,
		[Message]
FROM	dbo.ImportError
WHERE	DATEDIFF(day, ErrorDate, GETDATE()) <= @NumErrors
ORDER BY
		ImportErrorID DESC

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportErrorSelectRecent. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END

