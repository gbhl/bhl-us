
-- KeywordSelectAuto PROCEDURE
-- Generated 5/3/2012 1:28:21 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Keyword

CREATE PROCEDURE KeywordSelectAuto

@KeywordID INT

AS 

SET NOCOUNT ON

SELECT 

	[KeywordID],
	[Keyword],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[Keyword]

WHERE
	[KeywordID] = @KeywordID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure KeywordSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


