
-- TitleKeywordUpdateAuto PROCEDURE
-- Generated 5/3/2012 1:28:21 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleKeyword

CREATE PROCEDURE TitleKeywordUpdateAuto

@TitleKeywordID INT,
@TitleID INT,
@KeywordID INT,
@MarcDataFieldTag NVARCHAR(50),
@MarcSubFieldCode NVARCHAR(50),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleKeyword]

SET

	[TitleID] = @TitleID,
	[KeywordID] = @KeywordID,
	[MarcDataFieldTag] = @MarcDataFieldTag,
	[MarcSubFieldCode] = @MarcSubFieldCode,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[TitleKeywordID] = @TitleKeywordID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleKeywordUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END


