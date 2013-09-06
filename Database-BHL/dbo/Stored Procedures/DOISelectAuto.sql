
-- DOISelectAuto PROCEDURE
-- Generated 11/11/2011 1:11:27 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for DOI

CREATE PROCEDURE DOISelectAuto

@DOIID INT

AS 

SET NOCOUNT ON

SELECT 

	[DOIID],
	[DOIEntityTypeID],
	[EntityID],
	[DOIStatusID],
	[DOIBatchID],
	[DOIName],
	[StatusDate],
	[StatusMessage],
	[IsValid],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[DOI]

WHERE
	[DOIID] = @DOIID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure DOISelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

