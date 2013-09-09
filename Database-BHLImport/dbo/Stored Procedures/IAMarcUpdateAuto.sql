
-- IAMarcUpdateAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAMarc

CREATE PROCEDURE IAMarcUpdateAuto

@MarcID INT,
@ItemID INT,
@Leader VARCHAR(200)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAMarc]

SET

	[ItemID] = @ItemID,
	[Leader] = @Leader,
	[LastModifiedDate] = getdate()

WHERE
	[MarcID] = @MarcID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcID],
		[ItemID],
		[Leader],
		[CreatedDate],
		[LastModifiedDate]

	FROM [dbo].[IAMarc]
	
	WHERE
		[MarcID] = @MarcID
	
	RETURN -- update successful
END

