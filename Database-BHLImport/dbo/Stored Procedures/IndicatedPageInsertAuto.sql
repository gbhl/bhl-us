
-- IndicatedPageInsertAuto PROCEDURE
-- Generated 1/16/2008 1:54:48 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IndicatedPage

CREATE PROCEDURE IndicatedPageInsertAuto

@IndicatedPageID INT OUTPUT,
@BarCode NVARCHAR(40),
@FileNamePrefix NVARCHAR(200),
@SequenceOrder INT = null,
@Sequence SMALLINT = null,
@ImportStatusID INT,
@ImportSourceID INT = null,
@PagePrefix NVARCHAR(20) = null,
@PageNumber NVARCHAR(20) = null,
@Implied BIT,
@ExternalCreationDate DATETIME = null,
@ExternalLastModifiedDate DATETIME = null,
@ExternalCreationUser INT = null,
@ExternalLastModifiedUser INT = null,
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IndicatedPage]
(
	[BarCode],
	[FileNamePrefix],
	[SequenceOrder],
	[Sequence],
	[ImportStatusID],
	[ImportSourceID],
	[PagePrefix],
	[PageNumber],
	[Implied],
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
	@BarCode,
	@FileNamePrefix,
	@SequenceOrder,
	@Sequence,
	@ImportStatusID,
	@ImportSourceID,
	@PagePrefix,
	@PageNumber,
	@Implied,
	@ExternalCreationDate,
	@ExternalLastModifiedDate,
	@ExternalCreationUser,
	@ExternalLastModifiedUser,
	@ProductionDate,
	getdate(),
	getdate()
)

SET @IndicatedPageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IndicatedPageInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[IndicatedPageID],
		[BarCode],
		[FileNamePrefix],
		[SequenceOrder],
		[Sequence],
		[ImportStatusID],
		[ImportSourceID],
		[PagePrefix],
		[PageNumber],
		[Implied],
		[ExternalCreationDate],
		[ExternalLastModifiedDate],
		[ExternalCreationUser],
		[ExternalLastModifiedUser],
		[ProductionDate],
		[CreatedDate],
		[LastModifiedDate]	

	FROM [dbo].[IndicatedPage]
	
	WHERE
		[IndicatedPageID] = @IndicatedPageID
	
	RETURN -- insert successful
END

