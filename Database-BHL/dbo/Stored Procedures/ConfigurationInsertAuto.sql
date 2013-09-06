
-- ConfigurationInsertAuto PROCEDURE
-- Generated 6/10/2011 4:56:25 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Configuration

CREATE PROCEDURE ConfigurationInsertAuto

@ConfigurationID INT,
@ConfigurationName NVARCHAR(50),
@ConfigurationValue NVARCHAR(1000)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Configuration]
(
	[ConfigurationID],
	[ConfigurationName],
	[ConfigurationValue]
)
VALUES
(
	@ConfigurationID,
	@ConfigurationName,
	@ConfigurationValue
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ConfigurationInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ConfigurationID],
		[ConfigurationName],
		[ConfigurationValue]	

	FROM [dbo].[Configuration]
	
	WHERE
		[ConfigurationID] = @ConfigurationID
	
	RETURN -- insert successful
END

