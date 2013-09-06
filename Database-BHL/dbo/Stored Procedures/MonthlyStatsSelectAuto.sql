
-- MonthlyStatsSelectAuto PROCEDURE
-- Generated 10/29/2008 10:12:36 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for MonthlyStats

CREATE PROCEDURE MonthlyStatsSelectAuto

@Year INT,
@Month INT,
@InstitutionName NVARCHAR(255),
@StatType NVARCHAR(100)

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MonthlyStatsSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

