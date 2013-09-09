
-- MarcControlUpdateAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for MarcControl

CREATE PROCEDURE MarcControlUpdateAuto

@MarcControlID INT,
@ItemID INT,
@Tag NCHAR(3),
@Value NVARCHAR(200)

AS 

SET NOCOUNT ON

UPDATE [dbo].[MarcControl]

SET

	[ItemID] = @ItemID,
	[Tag] = @Tag,
	[Value] = @Value

WHERE
	[MarcControlID] = @MarcControlID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcControlUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcControlID],
		[ItemID],
		[Tag],
		[Value],
		[CreationDate]

	FROM [dbo].[MarcControl]
	
	WHERE
		[MarcControlID] = @MarcControlID
	
	RETURN -- update successful
END

