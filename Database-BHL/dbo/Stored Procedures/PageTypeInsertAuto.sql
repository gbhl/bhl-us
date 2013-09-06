
-- PageTypeInsertAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for PageType

CREATE PROCEDURE PageTypeInsertAuto

@PageTypeID INT OUTPUT /* Unique identifier for each Page Type record. */,
@PageTypeName NVARCHAR(30) /* Name of a Page Type. */,
@PageTypeDescription NVARCHAR(255) = null /* Description of the Page Type. */

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PageType]
(
	[PageTypeName],
	[PageTypeDescription]
)
VALUES
(
	@PageTypeName,
	@PageTypeDescription
)

SET @PageTypeID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PageTypeID],
		[PageTypeName],
		[PageTypeDescription]	

	FROM [dbo].[PageType]
	
	WHERE
		[PageTypeID] = @PageTypeID
	
	RETURN -- insert successful
END

