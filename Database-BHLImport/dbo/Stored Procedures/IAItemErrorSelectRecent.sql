
CREATE PROCEDURE [dbo].[IAItemErrorSelectRecent]

@NumErrors INT = 10

AS
BEGIN

SET NOCOUNT ON

SELECT	e.ItemErrorID,
		e.ItemID,
		i.IAIdentifier,
		e.ErrorDate,
		e.Number,
		e.Severity,
		e.State,
		e.[Procedure],
		e.Line,
		e.[Message]
FROM	dbo.IAItemError e INNER JOIN dbo.IAItem i
			ON e.ItemID = i.ItemID
WHERE	DATEDIFF(day, ErrorDate, GETDATE()) <= @NumErrors
ORDER BY
		ItemErrorID DESC

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemErrorSelectRecent. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END

