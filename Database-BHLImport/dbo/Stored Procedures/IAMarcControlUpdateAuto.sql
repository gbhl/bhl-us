
-- IAMarcControlUpdateAuto PROCEDURE
-- Generated 7/8/2013 2:53:08 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAMarcControl

CREATE PROCEDURE IAMarcControlUpdateAuto

@MarcControlID INT,
@MarcID INT,
@Tag NCHAR(3),
@Value NVARCHAR(2000)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAMarcControl]

SET

	[MarcID] = @MarcID,
	[Tag] = @Tag,
	[Value] = @Value,
	[LastModifiedDate] = getdate()

WHERE
	[MarcControlID] = @MarcControlID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcControlUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcControlID],
		[MarcID],
		[Tag],
		[Value],
		[CreatedDate],
		[LastModifiedDate]

	FROM [dbo].[IAMarcControl]
	
	WHERE
		[MarcControlID] = @MarcControlID
	
	RETURN -- update successful
END

