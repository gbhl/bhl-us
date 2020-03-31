CREATE PROCEDURE dbo.InstitutionGroupSelectInstitutions

@InstitutionGroupID int

AS 

SET NOCOUNT ON

SELECT	gi.InstitutionGroupInstitutionID,
		gi.InstitutionGroupID,
		gi.InstitutionCode,
		i.InstitutionName
FROM	dbo.InstitutionGroupInstitution gi
		INNER JOIN dbo.Institution i on gi.InstitutionCode = i.InstitutionCode
WHERE	gi.InstitutionGroupID = @InstitutionGroupID
ORDER BY
		i.InstitutionName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionGroupSelectInstitutions. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
