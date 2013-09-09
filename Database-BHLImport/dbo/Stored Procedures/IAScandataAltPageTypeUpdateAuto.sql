
-- IAScandataAltPageTypeUpdateAuto PROCEDURE
-- Generated 11/23/2010 11:26:17 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAScandataAltPageType

CREATE PROCEDURE IAScandataAltPageTypeUpdateAuto

@ScandataAltPageTypeID INT,
@ScandataID INT,
@PageType NVARCHAR(50)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAScandataAltPageType]

SET

	[ScandataID] = @ScandataID,
	[PageType] = @PageType,
	[LastModifiedDate] = getdate()

WHERE
	[ScandataAltPageTypeID] = @ScandataAltPageTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ScandataAltPageTypeID],
		[ScandataID],
		[PageType],
		[CreatedDate],
		[LastModifiedDate]

	FROM [dbo].[IAScandataAltPageType]
	
	WHERE
		[ScandataAltPageTypeID] = @ScandataAltPageTypeID
	
	RETURN -- update successful
END

