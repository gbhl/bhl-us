
-- TitleKeywordInsertAuto PROCEDURE
-- Generated 5/3/2012 1:28:21 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleKeyword

CREATE PROCEDURE TitleKeywordInsertAuto

@TitleKeywordID INT OUTPUT,
@TitleID INT,
@KeywordID INT,
@MarcDataFieldTag NVARCHAR(50) = null,
@MarcSubFieldCode NVARCHAR(50) = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleKeyword]
(
	[TitleID],
	[KeywordID],
	[MarcDataFieldTag],
	[MarcSubFieldCode],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@TitleID,
	@KeywordID,
	@MarcDataFieldTag,
	@MarcSubFieldCode,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @TitleKeywordID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleKeywordInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleKeywordID],
		[TitleID],
		[KeywordID],
		[MarcDataFieldTag],
		[MarcSubFieldCode],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[TitleKeyword]
	
	WHERE
		[TitleKeywordID] = @TitleKeywordID
	
	RETURN -- insert successful
END


