
-- Page_PageTypeSelectAuto PROCEDURE
-- Generated 1/16/2008 1:54:48 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Page_PageType

CREATE PROCEDURE Page_PageTypeSelectAuto

@PagePageTypeID INT

AS 

SET NOCOUNT ON

SELECT 

	[PagePageTypeID],
	[BarCode],
	[FileNamePrefix],
	[SequenceOrder],
	[PageTypeID],
	[ImportStatusID],
	[ImportSourceID],
	[Verified],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[Page_PageType]

WHERE
	[PagePageTypeID] = @PagePageTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Page_PageTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

