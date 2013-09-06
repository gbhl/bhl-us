
-- TitleAssociationTypeSelectAuto PROCEDURE
-- Generated 5/6/2009 2:57:04 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleAssociationType

CREATE PROCEDURE TitleAssociationTypeSelectAuto

@TitleAssociationTypeID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleAssociationTypeID],
	[TitleAssociationName],
	[MARCTag],
	[MARCIndicator2],
	[TitleAssociationLabel]

FROM [dbo].[TitleAssociationType]

WHERE
	[TitleAssociationTypeID] = @TitleAssociationTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociationTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

