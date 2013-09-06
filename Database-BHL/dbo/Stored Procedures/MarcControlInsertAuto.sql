
-- MarcControlInsertAuto PROCEDURE
-- Generated 4/15/2009 3:34:26 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for MarcControl

CREATE PROCEDURE MarcControlInsertAuto

@MarcControlID INT OUTPUT,
@MarcID INT,
@Tag NCHAR(3),
@Value NVARCHAR(200)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[MarcControl]
(
	[MarcID],
	[Tag],
	[Value],
	[CreationDate],
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
	RAISERROR('An error occurred in procedure MarcControlInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

