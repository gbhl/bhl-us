
-- AuthorNameUpdateAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AuthorName

CREATE PROCEDURE AuthorNameUpdateAuto

@AuthorNameID INT,
@AuthorID INT,
@FullName NVARCHAR(300),
@LastName NVARCHAR(150),
@FirstName NVARCHAR(150),
@FullerForm NVARCHAR(150),
@IsPreferredName SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[AuthorName]

SET

	[AuthorID] = @AuthorID,
	[FullName] = @FullName,
	[LastName] = @LastName,
	[FirstName] = @FirstName,
	[FullerForm] = @FullerForm,
	[IsPreferredName] = @IsPreferredName,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[AuthorNameID] = @AuthorNameID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorNameUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AuthorNameID],
		[AuthorID],
		[FullName],
		[LastName],
		[FirstName],
		[FullerForm],
		[IsPreferredName],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[AuthorName]
	
	WHERE
		[AuthorNameID] = @AuthorNameID
	
	RETURN -- update successful
END


