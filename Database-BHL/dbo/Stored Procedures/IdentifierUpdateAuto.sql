
-- IdentifierUpdateAuto PROCEDURE
-- Generated 5/1/2012 2:41:41 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Identifier

CREATE PROCEDURE IdentifierUpdateAuto

@IdentifierID INT,
@IdentifierName NVARCHAR(40),
@IdentifierLabel NVARCHAR(50),
@Display SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Identifier]

SET

	[IdentifierName] = @IdentifierName,
	[IdentifierLabel] = @IdentifierLabel,
	[Display] = @Display,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[IdentifierID] = @IdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[IdentifierID],
		[IdentifierName],
		[IdentifierLabel],
		[Display],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[Identifier]
	
	WHERE
		[IdentifierID] = @IdentifierID
	
	RETURN -- update successful
END


