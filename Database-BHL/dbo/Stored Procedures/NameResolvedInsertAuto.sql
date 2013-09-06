
-- NameResolvedInsertAuto PROCEDURE
-- Generated 12/10/2012 3:05:47 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for NameResolved

CREATE PROCEDURE NameResolvedInsertAuto

@NameResolvedID INT OUTPUT,
@ResolvedNameString NVARCHAR(100),
@CanonicalNameString NVARCHAR(100),
@IsPreferred SMALLINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[NameResolved]
(
	[ResolvedNameString],
	[CanonicalNameString],
	[IsPreferred],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@ResolvedNameString,
	@CanonicalNameString,
	@IsPreferred,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @NameResolvedID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameResolvedInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[NameResolvedID],
		[ResolvedNameString],
		[CanonicalNameString],
		[IsPreferred],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[NameResolved]
	
	WHERE
		[NameResolvedID] = @NameResolvedID
	
	RETURN -- insert successful
END

