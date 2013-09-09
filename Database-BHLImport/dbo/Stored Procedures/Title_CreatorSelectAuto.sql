
-- Title_CreatorSelectAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Title_Creator

CREATE PROCEDURE Title_CreatorSelectAuto

@TitleCreatorID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleCreatorID],
	[CreatorName],
	[MARCCreator_a],
	[MARCCreator_b],
	[MARCCreator_c],
	[MARCCreator_d],
	[CreatorRoleTypeID],
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[Title_Creator]

WHERE
	[TitleCreatorID] = @TitleCreatorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_CreatorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

