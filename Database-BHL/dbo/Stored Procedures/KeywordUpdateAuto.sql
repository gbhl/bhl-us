
-- KeywordUpdateAuto PROCEDURE
-- Generated 5/3/2012 1:28:21 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Keyword

CREATE PROCEDURE KeywordUpdateAuto

@KeywordID INT,
@Keyword NVARCHAR(50),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Keyword]

SET

	[Keyword] = @Keyword,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[KeywordID] = @KeywordID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure KeywordUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END


