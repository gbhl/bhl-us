
-- IAMarcInsertAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAMarc

CREATE PROCEDURE IAMarcInsertAuto

@MarcID INT OUTPUT,
@ItemID INT,
@Leader VARCHAR(200)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAMarc]
(
	[ItemID],
	[Leader],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@ItemID,
	@Leader,
	getdate(),
	getdate()
)

SET @MarcID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

