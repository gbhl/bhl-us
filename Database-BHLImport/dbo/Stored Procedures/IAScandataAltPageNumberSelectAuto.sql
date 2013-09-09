
-- IAScandataAltPageNumberSelectAuto PROCEDURE
-- Generated 11/23/2010 11:26:17 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAScandataAltPageNumber

CREATE PROCEDURE IAScandataAltPageNumberSelectAuto

@ScandataAltPageNumberID INT

AS 

SET NOCOUNT ON

SELECT 

	[ScandataAltPageNumberID],
	[ScandataID],
	[Sequence],
	[PagePrefix],
	[PageNumber],
	[Implied],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAScandataAltPageNumber]

WHERE
	[ScandataAltPageNumberID] = @ScandataAltPageNumberID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageNumberSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

