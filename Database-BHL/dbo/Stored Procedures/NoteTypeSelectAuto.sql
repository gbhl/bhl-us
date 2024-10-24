
-- NoteTypeSelectAuto PROCEDURE
-- Generated 2/27/2015 2:20:32 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for NoteType

CREATE PROCEDURE [dbo].[NoteTypeSelectAuto]

@NoteTypeID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NoteTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
