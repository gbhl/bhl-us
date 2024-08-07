
-- NoteTypeUpdateAuto PROCEDURE
-- Generated 2/27/2015 2:20:32 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for NoteType

CREATE PROCEDURE [dbo].[NoteTypeUpdateAuto]

@NoteTypeID INT,
@NoteTypeName NVARCHAR(50),
@NoteTypeDisplay NVARCHAR(50),
@MarcDataFieldTag NVARCHAR(5),
@MarcIndicator1 NVARCHAR(5),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[NoteType]

SET

	[NoteTypeName] = @NoteTypeName,
	[NoteTypeDisplay] = @NoteTypeDisplay,
	[MarcDataFieldTag] = @MarcDataFieldTag,
	[MarcIndicator1] = @MarcIndicator1,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[NoteTypeID] = @NoteTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NoteTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[NoteTypeID],
		[NoteTypeName],
		[NoteTypeDisplay],
		[MarcDataFieldTag],
		[MarcIndicator1],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[NoteType]
	
	WHERE
		[NoteTypeID] = @NoteTypeID
	
	RETURN -- update successful
END


GO
