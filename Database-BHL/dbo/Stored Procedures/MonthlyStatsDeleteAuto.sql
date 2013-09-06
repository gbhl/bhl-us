
-- MonthlyStatsDeleteAuto PROCEDURE
-- Generated 10/29/2008 10:12:36 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for MonthlyStats

CREATE PROCEDURE MonthlyStatsDeleteAuto

@Year INT,
@Month INT,
@InstitutionName NVARCHAR(255),
@StatType NVARCHAR(100)

AS 

DELETE FROM [dbo].[MonthlyStats]

WHERE

	[Year] = @Year AND
	[Month] = @Month AND
	[InstitutionName] = @InstitutionName AND
	[StatType] = @StatType

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MonthlyStatsDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

