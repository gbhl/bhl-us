CREATE PROCEDURE [dbo].[DOIInsertQueue]

@DOIEntityTypeID INT,
@EntityID INT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

DECLARE @DOIStatusQueuedID int
SELECT @DOIStatusQueuedID = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'Queued'

DECLARE @DOIStatusErrorID int
SELECT @DOIStatusErrorID = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'Error'

DECLARE @EntityTypeTitleID int, @EntityTypeSegmentID int
SELECT @EntityTypeTitleID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Title'
SELECT @EntityTypeSegmentID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'

DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

IF NOT EXISTS(	SELECT	DOIID 
				FROM	dbo.DOI 
				WHERE	DOIEntityTypeID = @DOIEntityTypeID 
				AND		EntityID = @EntityID
				AND		DOIStatusID = @DOIStatusQueuedID
			) 
BEGIN

	-- Get any BHL DOI that has already been assigned to this entity
	DECLARE @DOIName nvarchar(50)

	-- Look in Title_Identifier and ItemIdentifier first to make sure we pick up BHL-acquired DOIs
	IF (@DOIEntityTypeID = @EntityTypeTitleID)
	BEGIN
		SELECT	@DOIName = ti.IdentifierValue
		FROM	dbo.Title_Identifier ti 
		WHERE	ti.TitleID = @EntityID
		AND		ti.IdentifierID = @DOIIdentifierID 
		AND		SUBSTRING(	ti.IdentifierValue, 1, 
								CASE WHEN CHARINDEX('/', ti.IdentifierValue) > 0 
									THEN CHARINDEX('/', ti.IdentifierValue) - 1 
									ELSE LEN(ti.IdentifierValue) 
								END) IN (SELECT Prefix FROM dbo.DOIPrefix)
	END

	IF (@DOIName IS NULL AND @DOIEntityTypeID = @EntityTypeSegmentID)
	BEGIN
		SELECT @DOIName = ii.IdentifierValue
		FROM	dbo.Segment s 
				LEFT JOIN dbo.ItemIdentifier ii 
					ON s.ItemID = ii.ItemID 
					AND ii.IdentifierID = @DOIIdentifierID 
					AND SUBSTRING(	ii.IdentifierValue, 1, 
								CASE WHEN CHARINDEX('/', ii.IdentifierValue) > 0 
									THEN CHARINDEX('/', ii.IdentifierValue) - 1 
									ELSE LEN(ii.IdentifierValue) 
								END) IN (SELECT Prefix FROM dbo.DOIPrefix)
		WHERE	s.SegmentID = @EntityID
	END

	-- Look in the DOI table last, if we haven't already found a BHL-managed DOI elsewhere
	IF (@DOIName IS NULL)
	BEGIN
		SELECT	@DOIName = d.DOIName
		FROM	dbo.DOI d
		WHERE	d.EntityID = @EntityID
		AND		d.DOIEntityTypeID = @DOIEntityTypeID
		AND		d.DOIStatusID NOT IN (@DOIStatusQueuedID, @DOIStatusErrorID)
	END

	-- Add the new message to the DOI queue
	INSERT INTO [dbo].[DOI]
	( 	[DOIEntityTypeID],
		[EntityID],
		[DOIStatusID],
		[DOIBatchID],
		[DOIName],
		[StatusDate],
		[StatusMessage],
		[IsValid],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID] )
	VALUES
	( 	@DOIEntityTypeID,
		@EntityID,
		@DOIStatusQueuedID,
		'', --DOIBatchID,
		ISNULL(@DOIName, ''),
		getdate(),
		'', --StatusMessage,
		0,  --IsValid,
		getdate(),
		getdate(),
		@CreationUserID,
		@LastModifiedUserID )
END

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.DOIInsertQueue. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- insert successful
END

GO
