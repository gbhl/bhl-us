
-- KeywordInsertAuto PROCEDURE
-- Generated 5/3/2012 1:28:21 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Keyword

CREATE PROCEDURE KeywordInsertAuto

@KeywordID INT OUTPUT,
@Keyword NVARCHAR(50),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Keyword]
(
	[Keyword],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@Keyword,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @KeywordID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure KeywordInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END


