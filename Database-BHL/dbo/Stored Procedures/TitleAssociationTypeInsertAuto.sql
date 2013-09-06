
-- TitleAssociationTypeInsertAuto PROCEDURE
-- Generated 5/6/2009 2:57:04 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleAssociationType

CREATE PROCEDURE TitleAssociationTypeInsertAuto

@TitleAssociationTypeID INT OUTPUT,
@TitleAssociationName NVARCHAR(60),
@MARCTag NVARCHAR(20),
@MARCIndicator2 NCHAR(1),
@TitleAssociationLabel NVARCHAR(30)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleAssociationType]
(
	[TitleAssociationName],
	[MARCTag],
	[MARCIndicator2],
	[TitleAssociationLabel]
)
VALUES
(
	@TitleAssociationName,
	@MARCTag,
	@MARCIndicator2,
	@TitleAssociationLabel
)

SET @TitleAssociationTypeID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociationTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

