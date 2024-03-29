SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------

CREATE PROCEDURE [dbo].[ItemRelationshipInsertAuto]

@RelationshipID INT OUTPUT,
@ParentID INT,
@ChildID INT,
@SequenceOrder INT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ItemRelationship]
( 	[ParentID],
	[ChildID],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@ParentID,
	@ChildID,
	@SequenceOrder,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @RelationshipID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemRelationshipInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

GO
