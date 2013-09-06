
-- NameUpdateAuto PROCEDURE
-- Generated 12/10/2012 3:05:47 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Name

CREATE PROCEDURE NameUpdateAuto

@NameID INT,
@NameSourceID INT,
@NameString NVARCHAR(100),
@IsActive SMALLINT,
@LastModifiedUserID INT,
@NameResolvedID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Name]

SET

	[NameSourceID] = @NameSourceID,
	[NameString] = @NameString,
	[IsActive] = @IsActive,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID,
	[NameResolvedID] = @NameResolvedID

WHERE
	[NameID] = @NameID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[NameID],
		[NameSourceID],
		[NameString],
		[IsActive],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[NameResolvedID]

	FROM [dbo].[Name]
	
	WHERE
		[NameID] = @NameID
	
	RETURN -- update successful
END

