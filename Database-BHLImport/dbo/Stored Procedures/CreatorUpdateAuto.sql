
-- CreatorUpdateAuto PROCEDURE
-- Generated 4/4/2008 9:03:06 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for Creator

CREATE PROCEDURE CreatorUpdateAuto

@CreatorID INT,
@ImportStatusID INT,
@ImportSourceID INT,
@CreatorName NVARCHAR(255),
@FirstNameFirst NVARCHAR(255),
@SimpleName NVARCHAR(255),
@DOB NVARCHAR(50),
@DOD NVARCHAR(50),
@Biography NTEXT,
@CreatorNote NVARCHAR(255),
@MARCDataFieldTag NVARCHAR(3),
@MARCCreator_a NVARCHAR(450),
@MARCCreator_b NVARCHAR(450),
@MARCCreator_c NVARCHAR(450),
@MARCCreator_d NVARCHAR(450),
@MARCCreator_Full NVARCHAR(450),
@ExternalCreationDate DATETIME,
@ExternalLastModifiedDate DATETIME,
@ProductionDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[Creator]

SET

	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[CreatorName] = @CreatorName,
	[FirstNameFirst] = @FirstNameFirst,
	[SimpleName] = @SimpleName,
	[DOB] = @DOB,
	[DOD] = @DOD,
	[Biography] = @Biography,
	[CreatorNote] = @CreatorNote,
	[MARCDataFieldTag] = @MARCDataFieldTag,
	[MARCCreator_a] = @MARCCreator_a,
	[MARCCreator_b] = @MARCCreator_b,
	[MARCCreator_c] = @MARCCreator_c,
	[MARCCreator_d] = @MARCCreator_d,
	[MARCCreator_Full] = @MARCCreator_Full,
	[ExternalCreationDate] = @ExternalCreationDate,
	[ExternalLastModifiedDate] = @ExternalLastModifiedDate,
	[ProductionDate] = @ProductionDate,
	[LastModifiedDate] = getdate()

WHERE
	[CreatorID] = @CreatorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CreatorUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

