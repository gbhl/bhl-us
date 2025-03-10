SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemRelationshipUpdateAuto]

@RelationshipID INT,
@ParentID INT,
@ChildID INT,
@SequenceOrder INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemRelationship]
SET
	[ParentID] = @ParentID,
	[ChildID] = @ChildID,
	[SequenceOrder] = @SequenceOrder,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[RelationshipID] = @RelationshipID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemRelationshipUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[RelationshipID],
		[ParentID],
		[ChildID],
		[SequenceOrder],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [dbo].[ItemRelationship]
	WHERE
		[RelationshipID] = @RelationshipID
	
	RETURN -- update successful
END

GO
