
-- NameResolvedUpdateAuto PROCEDURE
-- Generated 12/10/2012 3:05:47 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for NameResolved

CREATE PROCEDURE NameResolvedUpdateAuto

@NameResolvedID INT,
@ResolvedNameString NVARCHAR(100),
@CanonicalNameString NVARCHAR(100),
@IsPreferred SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[NameResolved]

SET

	[ResolvedNameString] = @ResolvedNameString,
	[CanonicalNameString] = @CanonicalNameString,
	[IsPreferred] = @IsPreferred,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[NameResolvedID] = @NameResolvedID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameResolvedUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

