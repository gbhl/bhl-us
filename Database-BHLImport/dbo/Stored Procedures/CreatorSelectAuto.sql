
-- CreatorSelectAuto PROCEDURE
-- Generated 4/4/2008 9:03:06 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for Creator

CREATE PROCEDURE CreatorSelectAuto

@CreatorID INT

AS 

SET NOCOUNT ON

SELECT 

	[CreatorID],
	[ImportStatusID],
	[ImportSourceID],
	[CreatorName],
	[FirstNameFirst],
	[SimpleName],
	[DOB],
	[DOD],
	[Biography],
	[CreatorNote],
	[MARCDataFieldTag],
	[MARCCreator_a],
	[MARCCreator_b],
	[MARCCreator_c],
	[MARCCreator_d],
	[MARCCreator_Full],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[Creator]

WHERE
	[CreatorID] = @CreatorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CreatorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

