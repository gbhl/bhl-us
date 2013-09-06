
-- TitleVariantSelectAuto PROCEDURE
-- Generated 2/15/2011 12:02:06 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleVariant

CREATE PROCEDURE TitleVariantSelectAuto

@TitleVariantID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleVariantID],
	[TitleID],
	[TitleVariantTypeID],
	[Title],
	[TitleRemainder],
	[PartNumber],
	[PartName],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[TitleVariant]

WHERE
	[TitleVariantID] = @TitleVariantID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleVariantSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

