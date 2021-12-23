CREATE PROCEDURE [dbo].[DOIInsertQueue]

@DOIEntityTypeID INT,
@EntityID INT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

DECLARE @DOIStatusQueuedID int
SELECT @DOIStatusQueuedID = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'Queued'

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
	SELECT @DOIName = COALESCE(ti.IdentifierValue, ii.IdentifierValue, d.DOIName)
	FROM	dbo.DOI d
			LEFT JOIN dbo.Title_Identifier ti 
				ON d.DOIEntityTypeID = @EntityTypeTitleID AND d.EntityID = ti.TitleID AND ti.IdentifierID = @DOIIdentifierID AND ti.IdentifierValue LIKE '%10.59265'
			LEFT JOIN dbo.Segment s 
				ON d.DOIEntityTypeID = @EntityTypeSegmentID AND d.EntityID = s.SegmentID
			LEFT JOIN dbo.ItemIdentifier ii 
				ON s.ItemID = ii.ItemID AND ii.IdentifierID = @DOIIdentifierID AND ii.IdentifierValue LIKE '%10.59265'
	WHERE	d.EntityID = @EntityID
	AND		d.DOIEntityTypeID = @DOIEntityTypeID
	AND		d.DOIStatusID <> @DOIStatusQueuedID

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
		@DOIName,
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
