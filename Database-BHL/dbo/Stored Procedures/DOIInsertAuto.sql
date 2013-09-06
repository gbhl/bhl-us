
-- DOIInsertAuto PROCEDURE
-- Generated 11/11/2011 1:11:27 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for DOI

CREATE PROCEDURE DOIInsertAuto

@DOIID INT OUTPUT,
@DOIEntityTypeID INT,
@EntityID INT,
@DOIStatusID INT,
@DOIBatchID NVARCHAR(50),
@DOIName NVARCHAR(50),
@StatusDate DATETIME,
@StatusMessage NVARCHAR(1000),
@IsValid SMALLINT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[DOI]
(
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
)
VALUES
(
	@DOIEntityTypeID,
	@EntityID,
	@DOIStatusID,
	@DOIBatchID,
	@DOIName,
	@StatusDate,
	@StatusMessage,
	@IsValid,
	getdate(),
	getdate()
)

SET @DOIID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure DOIInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- insert successful
END

