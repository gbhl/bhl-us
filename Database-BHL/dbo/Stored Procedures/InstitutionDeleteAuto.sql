
-- InstitutionDeleteAuto PROCEDURE
-- Generated 10/15/2009 3:36:51 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Institution

CREATE PROCEDURE InstitutionDeleteAuto

@InstitutionCode NVARCHAR(10) /* Code for Institution providing assistance. */

AS 

DELETE FROM [dbo].[Institution]

WHERE

	[InstitutionCode] = @InstitutionCode

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

