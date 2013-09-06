
-- TitleAssociationTypeUpdateAuto PROCEDURE
-- Generated 5/6/2009 2:57:04 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for TitleAssociationType

CREATE PROCEDURE TitleAssociationTypeUpdateAuto

@TitleAssociationTypeID INT,
@TitleAssociationName NVARCHAR(60),
@MARCTag NVARCHAR(20),
@MARCIndicator2 NCHAR(1),
@TitleAssociationLabel NVARCHAR(30)

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleAssociationType]

SET

	[TitleAssociationName] = @TitleAssociationName,
	[MARCTag] = @MARCTag,
	[MARCIndicator2] = @MARCIndicator2,
	[TitleAssociationLabel] = @TitleAssociationLabel

WHERE
	[TitleAssociationTypeID] = @TitleAssociationTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociationTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleAssociationTypeID],
		[TitleAssociationName],
		[MARCTag],
		[MARCIndicator2],
		[TitleAssociationLabel]

	FROM [dbo].[TitleAssociationType]
	
	WHERE
		[TitleAssociationTypeID] = @TitleAssociationTypeID
	
	RETURN -- update successful
END

