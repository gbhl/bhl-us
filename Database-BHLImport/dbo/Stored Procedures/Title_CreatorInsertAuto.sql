
-- Title_CreatorInsertAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Title_Creator

CREATE PROCEDURE Title_CreatorInsertAuto

@TitleCreatorID INT OUTPUT,
@CreatorName NVARCHAR(255),
@MARCCreator_a NVARCHAR(450) = null,
@MARCCreator_b NVARCHAR(450) = null,
@MARCCreator_c NVARCHAR(450) = null,
@MARCCreator_d NVARCHAR(450) = null,
@CreatorRoleTypeID INT,
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT = null,
@ExternalCreationDate DATETIME = null,
@ExternalLastModifiedDate DATETIME = null,
@ExternalCreationUser INT = null,
@ExternalLastModifiedUser INT = null,
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Title_Creator]
(
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
)
VALUES
(
	@CreatorName,
	@MARCCreator_a,
	@MARCCreator_b,
	@MARCCreator_c,
	@MARCCreator_d,
	@CreatorRoleTypeID,
	@ImportKey,
	@ImportStatusID,
	@ImportSourceID,
	@ExternalCreationDate,
	@ExternalLastModifiedDate,
	@ExternalCreationUser,
	@ExternalLastModifiedUser,
	@ProductionDate,
	getdate(),
	getdate()
)

SET @TitleCreatorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_CreatorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- insert successful
END

