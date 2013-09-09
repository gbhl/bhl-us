
-- CreatorInsertAuto PROCEDURE
-- Generated 4/4/2008 9:03:06 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Creator

CREATE PROCEDURE CreatorInsertAuto

@CreatorID INT OUTPUT,
@ImportStatusID INT,
@ImportSourceID INT = null,
@CreatorName NVARCHAR(255),
@FirstNameFirst NVARCHAR(255) = null,
@SimpleName NVARCHAR(255) = null,
@DOB NVARCHAR(50) = null,
@DOD NVARCHAR(50) = null,
@Biography NTEXT = null,
@CreatorNote NVARCHAR(255) = null,
@MARCDataFieldTag NVARCHAR(3) = null,
@MARCCreator_a NVARCHAR(450) = null,
@MARCCreator_b NVARCHAR(450) = null,
@MARCCreator_c NVARCHAR(450) = null,
@MARCCreator_d NVARCHAR(450) = null,
@MARCCreator_Full NVARCHAR(450) = null,
@ExternalCreationDate DATETIME = null,
@ExternalLastModifiedDate DATETIME = null,
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Creator]
(
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
)
VALUES
(
	@ImportStatusID,
	@ImportSourceID,
	@CreatorName,
	@FirstNameFirst,
	@SimpleName,
	@DOB,
	@DOD,
	@Biography,
	@CreatorNote,
	@MARCDataFieldTag,
	@MARCCreator_a,
	@MARCCreator_b,
	@MARCCreator_c,
	@MARCCreator_d,
	@MARCCreator_Full,
	@ExternalCreationDate,
	@ExternalLastModifiedDate,
	@ProductionDate,
	getdate(),
	getdate()
)

SET @CreatorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CreatorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- insert successful
END

