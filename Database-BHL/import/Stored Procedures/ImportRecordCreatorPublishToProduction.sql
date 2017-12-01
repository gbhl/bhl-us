CREATE PROCEDURE [import].[ImportRecordCreatorPublishToProduction]

@ImportFileID int,
@UserID int

AS

BEGIN

SET NOCOUNT ON

BEGIN TRY

	CREATE TABLE #tmpRecordCreator
		(
		FullName nvarchar(300) NOT NULL,
		FirstName nvarchar(150) NOT NULL,
		LastName nvarchar(150) NOT NULL,
		StartYear nvarchar(25) NOT NULL,
		EndYear nvarchar(25) NOT NULL,
		AuthorType nvarchar(50) NOT NULL
		)

	-- Get status IDs
	DECLARE @ImportRecordNewID int
	SELECT @ImportRecordNewID = ImportRecordStatusID FROM import.ImportRecordStatus WHERE StatusName = 'New'
	IF (@ImportRecordNewID IS NULL) RAISERROR('ImportRecordStatus -New- not found', 0, 1)

	-- Get the creators to be created
	INSERT	#tmpRecordCreator
	SELECT DISTINCT c.FullName, c.FirstName, c.LastName, c.StartYear, c.EndYear, c.AuthorType
	FROM	import.ImportRecordCreator c
			INNER JOIN import.ImportRecord r ON c.ImportRecordID = r.ImportRecordID
	WHERE	r.ImportFileID = @ImportFileID
	AND		r.ImportRecordStatusID = @ImportRecordNewID
	AND		c.AuthorID IS NULL

	-- Add the new creators to production (making note of the new AuthorIDs)
	DECLARE @NewAuthorID int
	DECLARE @FullName nvarchar(300)
	DECLARE @FirstName nvarchar(150)
	DECLARE @LastName nvarchar(150)
	DECLARE @StartYear nvarchar(25)
	DECLARE @EndYear nvarchar(25)
	DECLARE @AuthorType nvarchar(50)

	DECLARE	curInsert CURSOR 
	FOR	SELECT	FullName, FirstName, LastName, StartYear, EndYear, AuthorType 
		FROM	#tmpRecordCreator 
		
	OPEN curInsert
	FETCH NEXT FROM curInsert INTO @FullName, @FirstName, @LastName, @StartYear, @EndYear, @AuthorType

	WHILE (@@fetch_status <> -1)
	BEGIN
		IF (@@fetch_status <> -2)
		BEGIN
			BEGIN TRAN

			-- Insert a new Author record into the production database
			INSERT INTO dbo.Author (AuthorTypeID, StartDate, EndDate, IsActive, CreationUserID, LastModifiedUserID)
			VALUES (CASE WHEN @AuthorType = 'corporate' THEN 2 ELSE 1 END, @StartYear, @EndYear, 1, @UserID, @UserID)
						
			-- Save the ID of the newly inserted author record
			SELECT @NewAuthorID = SCOPE_IDENTITY()
				
			-- Insert a new AuthorName record
			INSERT INTO dbo.AuthorName (AuthorID, FullName, FirstName, LastName, IsPreferredName, CreationUserID, LastModifiedUserID)
			VALUES (@NewAuthorID, @FullName, @FirstName, @LastName, 1, @UserID, @UserID)

			-- Preserve the production identifier
			UPDATE	import.ImportRecordCreator
			SET		AuthorID = @NewAuthorID
			WHERE	FullName = @FullName
			AND		FirstName = @FirstName
			AND		LastName = @LastName
			AND		StartYear = @StartYear
			AND		EndYear = @EndYear
			AND		AuthorType = @AuthorType

			COMMIT TRAN
		END
		FETCH NEXT FROM curInsert INTO @FullName, @FirstName, @LastName, @StartYear, @EndYear, @AuthorType
	END

	CLOSE curInsert
	DEALLOCATE curInsert

END TRY
BEGIN CATCH
	DECLARE @ErrMsg NVARCHAR(4000)
	DECLARE @ErrSeverity INT
	DECLARE @ErrState INT
	
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrSeverity = ERROR_SEVERITY(),
			@ErrState = ERROR_STATE()

	IF @@TRANCOUNT > 0 ROLLBACK TRAN

	RAISERROR (@ErrMsg, @ErrSeverity, @ErrState)
END CATCH

END
