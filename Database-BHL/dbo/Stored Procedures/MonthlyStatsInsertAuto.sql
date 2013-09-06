
-- MonthlyStatsInsertAuto PROCEDURE
-- Generated 10/29/2008 10:12:36 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for MonthlyStats

CREATE PROCEDURE MonthlyStatsInsertAuto

@Year INT,
@Month INT,
@InstitutionName NVARCHAR(255),
@StatType NVARCHAR(100),
@StatValue INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[MonthlyStats]
(
	[Year],
	[Month],
	[InstitutionName],
	[StatType],
	[StatValue],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@Year,
	@Month,
	@InstitutionName,
	@StatType,
	@StatValue,
	getdate(),
	getdate()
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MonthlyStatsInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

