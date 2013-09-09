
-- TitleAssociationSelectAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleAssociation

CREATE PROCEDURE TitleAssociationSelectAuto

@TitleAssociationID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleAssociationID],
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[MARCTag],
	[MARCIndicator2],
	[Title],
	[Section],
	[Volume],
	[Heading],
	[Publication],
	[Relationship],
	[Active],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

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

