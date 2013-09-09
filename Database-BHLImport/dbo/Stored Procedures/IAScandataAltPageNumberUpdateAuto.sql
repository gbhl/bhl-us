
-- IAScandataAltPageNumberUpdateAuto PROCEDURE
-- Generated 11/23/2010 11:26:17 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAScandataAltPageNumber

CREATE PROCEDURE IAScandataAltPageNumberUpdateAuto

@ScandataAltPageNumberID INT,
@ScandataID INT,
@Sequence INT,
@PagePrefix NVARCHAR(40),
@PageNumber NVARCHAR(20),
@Implied BIT

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAScandataAltPageNumber]

SET

	[ScandataID] = @ScandataID,
	[Sequence] = @Sequence,
	[PagePrefix] = @PagePrefix,
	[PageNumber] = @PageNumber,
	[Implied] = @Implied,
	[LastModifiedDate] = getdate()

WHERE
	[ScandataAltPageNumberID] = @ScandataAltPageNumberID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageNumberUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

