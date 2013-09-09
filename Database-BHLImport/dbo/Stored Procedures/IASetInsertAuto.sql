
-- IASetInsertAuto PROCEDURE
-- Generated 5/27/2008 11:38:08 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IASet

CREATE PROCEDURE IASetInsertAuto

@SetID INT OUTPUT,
@SetSpecification NVARCHAR(200),
@DownloadAll BIT,
@LastDownloadDate DATETIME = null,
@LastFullDownloadDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IASet]
(
	[SetSpecification],
	[DownloadAll],
	[LastDownloadDate],
	[LastFullDownloadDate],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@SetSpecification,
	@DownloadAll,
	@LastDownloadDate,
	@LastFullDownloadDate,
	getdate(),
	getdate()
)

SET @SetID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IASetInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[SetID],
		[SetSpecification],
		[DownloadAll],
		[LastDownloadDate],
		[LastFullDownloadDate],
		[CreatedDate],
		[LastModifiedDate]	

	FROM [dbo].[IASet]
	
	WHERE
		[SetID] = @SetID
	
	RETURN -- insert successful
END

