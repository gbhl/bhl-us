
-- TitleKeywordSelectAuto PROCEDURE
-- Generated 5/3/2012 1:28:21 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleKeyword

CREATE PROCEDURE TitleKeywordSelectAuto

@TitleKeywordID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleKeywordSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


