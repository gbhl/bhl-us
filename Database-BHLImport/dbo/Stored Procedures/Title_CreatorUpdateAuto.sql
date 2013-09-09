
-- Title_CreatorUpdateAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Title_Creator

CREATE PROCEDURE Title_CreatorUpdateAuto

@TitleCreatorID INT,
@CreatorName NVARCHAR(255),
@MARCCreator_a NVARCHAR(450),
@MARCCreator_b NVARCHAR(450),
@MARCCreator_c NVARCHAR(450),
@MARCCreator_d NVARCHAR(450),
@CreatorRoleTypeID INT,
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT,
@ExternalCreationDate DATETIME,
@ExternalLastModifiedDate DATETIME,
@ExternalCreationUser INT,
@ExternalLastModifiedUser INT,
@ProductionDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[Title_Creator]

SET

	[CreatorName] = @CreatorName,
	[MARCCreator_a] = @MARCCreator_a,
	[MARCCreator_b] = @MARCCreator_b,
	[MARCCreator_c] = @MARCCreator_c,
	[MARCCreator_d] = @MARCCreator_d,
	[CreatorRoleTypeID] = @CreatorRoleTypeID,
	[ImportKey] = @ImportKey,
	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[ExternalCreationDate] = @ExternalCreationDate,
	[ExternalLastModifiedDate] = @ExternalLastModifiedDate,
	[ExternalCreationUser] = @ExternalCreationUser,
	[ExternalLastModifiedUser] = @ExternalLastModifiedUser,
	[ProductionDate] = @ProductionDate,
	[LastModifiedDate] = getdate()

WHERE
	[TitleCreatorID] = @TitleCreatorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_CreatorUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

