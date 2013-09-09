
-- IAScandataAltPageTypeSelectAuto PROCEDURE
-- Generated 11/23/2010 11:26:17 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAScandataAltPageType

CREATE PROCEDURE IAScandataAltPageTypeSelectAuto

@ScandataAltPageTypeID INT

AS 

SET NOCOUNT ON

SELECT 

	[ScandataAltPageTypeID],
	[ScandataID],
	[PageType],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAScandataAltPageType]

WHERE
	[ScandataAltPageTypeID] = @ScandataAltPageTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

