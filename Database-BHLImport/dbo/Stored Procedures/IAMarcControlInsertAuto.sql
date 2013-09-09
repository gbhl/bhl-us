
-- IAMarcControlInsertAuto PROCEDURE
-- Generated 7/8/2013 2:53:08 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAMarcControl

CREATE PROCEDURE IAMarcControlInsertAuto

@MarcControlID INT OUTPUT,
@MarcID INT,
@Tag NCHAR(3),
@Value NVARCHAR(2000)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAMarcControl]
(
	[MarcID],
	[Tag],
	[Value],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@MarcID,
	@Tag,
	@Value,
	getdate(),
	getdate()
)

SET @MarcControlID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcControlInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

