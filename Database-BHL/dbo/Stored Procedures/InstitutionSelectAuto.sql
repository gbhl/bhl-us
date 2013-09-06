
-- InstitutionSelectAuto PROCEDURE
-- Generated 10/15/2009 3:36:51 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Institution

CREATE PROCEDURE InstitutionSelectAuto

@InstitutionCode NVARCHAR(10) /* Code for Institution providing assistance. */

AS 

SET NOCOUNT ON

SELECT 

	[InstitutionCode],
	[InstitutionName],
	[Note],
	[InstitutionUrl],
	[BHLMemberLibrary]

FROM [dbo].[Institution]

WHERE
	[InstitutionCode] = @InstitutionCode

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

