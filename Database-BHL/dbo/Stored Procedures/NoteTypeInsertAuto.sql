
-- NoteTypeInsertAuto PROCEDURE
-- Generated 2/27/2015 2:20:32 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for NoteType

CREATE PROCEDURE [dbo].[NoteTypeInsertAuto]

@NoteTypeID INT OUTPUT,
@NoteTypeName NVARCHAR(50),
@NoteTypeDisplay NVARCHAR(50),
@MarcDataFieldTag NVARCHAR(5),
@MarcIndicator1 NVARCHAR(5),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[NoteType]
(
	[NoteTypeName],
	[NoteTypeDisplay],
	[MarcDataFieldTag],
	[MarcIndicator1],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@NoteTypeName,
	@NoteTypeDisplay,
	@MarcDataFieldTag,
	@MarcIndicator1,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @NoteTypeID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NoteTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END


GO
