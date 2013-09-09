
-- IAScandataAltPageNumberInsertAuto PROCEDURE
-- Generated 11/23/2010 11:26:17 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAScandataAltPageNumber

CREATE PROCEDURE IAScandataAltPageNumberInsertAuto

@ScandataAltPageNumberID INT OUTPUT,
@ScandataID INT,
@Sequence INT,
@PagePrefix NVARCHAR(40),
@PageNumber NVARCHAR(20),
@Implied BIT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAScandataAltPageNumber]
(
	[ScandataID],
	[Sequence],
	[PagePrefix],
	[PageNumber],
	[Implied],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@ScandataID,
	@Sequence,
	@PagePrefix,
	@PageNumber,
	@Implied,
	getdate(),
	getdate()
)

SET @ScandataAltPageNumberID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageNumberInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

