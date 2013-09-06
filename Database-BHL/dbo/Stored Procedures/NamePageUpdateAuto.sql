
-- NamePageUpdateAuto PROCEDURE
-- Generated 10/29/2012 3:17:36 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for NamePage

CREATE PROCEDURE NamePageUpdateAuto

@NamePageID INT,
@NameID INT,
@PageID INT,
@NameSourceID INT,
@IsFirstOccurrence SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[NamePage]

SET

	[NameID] = @NameID,
	[PageID] = @PageID,
	[NameSourceID] = @NameSourceID,
	[IsFirstOccurrence] = @IsFirstOccurrence,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[NamePageID] = @NamePageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NamePageUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[NamePageID],
		[NameID],
		[PageID],
		[NameSourceID],
		[IsFirstOccurrence],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[NamePage]
	
	WHERE
		[NamePageID] = @NamePageID
	
	RETURN -- update successful
END

