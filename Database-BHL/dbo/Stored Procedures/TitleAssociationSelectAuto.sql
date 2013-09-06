
-- TitleAssociationSelectAuto PROCEDURE
-- Generated 2/4/2011 2:43:35 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleAssociation

CREATE PROCEDURE TitleAssociationSelectAuto

@TitleAssociationID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleAssociationID],
	[TitleID],
	[TitleAssociationTypeID],
	[Title],
	[Section],
	[Volume],
	[Active],
	[AssociatedTitleID],
	[CreationDate],
	[LastModifiedDate],
	[Heading],
	[Publication],
	[Relationship],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[TitleAssociation]

WHERE
	[TitleAssociationID] = @TitleAssociationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociationSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

