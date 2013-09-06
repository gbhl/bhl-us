
-- MonthlyStatsUpdateAuto PROCEDURE
-- Generated 10/29/2008 10:12:36 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for MonthlyStats

CREATE PROCEDURE MonthlyStatsUpdateAuto

@Year INT,
@Month INT,
@InstitutionName NVARCHAR(255),
@StatType NVARCHAR(100),
@StatValue INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[MonthlyStats]

SET

	[Year] = @Year,
	[Month] = @Month,
	[InstitutionName] = @InstitutionName,
	[StatType] = @StatType,
	[StatValue] = @StatValue,
	[LastModifiedDate] = getdate()

WHERE
	[Year] = @Year AND
	[Month] = @Month AND
	[InstitutionName] = @InstitutionName AND
	[StatType] = @StatType
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MonthlyStatsUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[Year],
		[Month],
		[InstitutionName],
		[StatType],
		[StatValue],
		[CreationDate],
		[LastModifiedDate]

	FROM [dbo].[MonthlyStats]
	
	WHERE
		[Year] = @Year AND 
		[Month] = @Month AND 
		[InstitutionName] = @InstitutionName AND 
		[StatType] = @StatType
	
	RETURN -- update successful
END

