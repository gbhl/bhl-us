
-- MarcControlInsertAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for MarcControl

CREATE PROCEDURE MarcControlInsertAuto

@MarcControlID INT OUTPUT,
@ItemID INT,
@Tag NCHAR(3),
@Value NVARCHAR(200)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[MarcControl]
(
	[ItemID],
	[Tag],
	[Value],
	[CreationDate]
)
VALUES
(
	@ItemID,
	@Tag,
	@Value,
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
		[ItemID],
		[Tag],
		[Value],
		[CreationDate]	

	FROM [dbo].[MarcControl]
	
	WHERE
		[MarcControlID] = @MarcControlID
	
	RETURN -- insert successful
END

