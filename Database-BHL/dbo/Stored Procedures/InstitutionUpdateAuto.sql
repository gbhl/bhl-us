
-- InstitutionUpdateAuto PROCEDURE
-- Generated 10/15/2009 3:36:51 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Institution

CREATE PROCEDURE InstitutionUpdateAuto

@InstitutionCode NVARCHAR(10) /* Code for Institution providing assistance. */,
@InstitutionName NVARCHAR(255) /* Name for the Institution. */,
@Note NVARCHAR(255) /* Notes about this Institution. */,
@InstitutionUrl NVARCHAR(255),
@BHLMemberLibrary BIT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Institution]

SET

	[InstitutionCode] = @InstitutionCode,
	[InstitutionName] = @InstitutionName,
	[Note] = @Note,
	[InstitutionUrl] = @InstitutionUrl,
	[BHLMemberLibrary] = @BHLMemberLibrary

WHERE
	[InstitutionCode] = @InstitutionCode
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[InstitutionCode],
		[InstitutionName],
		[Note],
		[InstitutionUrl],
		[BHLMemberLibrary]

	FROM [dbo].[Institution]
	
	WHERE
		[InstitutionCode] = @InstitutionCode
	
	RETURN -- update successful
END

