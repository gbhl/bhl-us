
-- MarcControlUpdateAuto PROCEDURE
-- Generated 4/15/2009 3:34:26 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for MarcControl

CREATE PROCEDURE MarcControlUpdateAuto

@MarcControlID INT,
@MarcID INT,
@Tag NCHAR(3),
@Value NVARCHAR(200)

AS 

SET NOCOUNT ON

UPDATE [dbo].[MarcControl]

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
	RAISERROR('An error occurred in procedure MarcControlUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcControlID],
		[MarcID],
		[Tag],
		[Value],
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[MarcControl]
	
	WHERE
		[MarcControlID] = @MarcControlID
	
	RETURN -- update successful
END

