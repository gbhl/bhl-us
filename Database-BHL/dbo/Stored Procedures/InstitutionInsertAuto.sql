
-- InstitutionInsertAuto PROCEDURE
-- Generated 10/15/2009 3:36:51 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Institution

CREATE PROCEDURE InstitutionInsertAuto

@InstitutionCode NVARCHAR(10) /* Code for Institution providing assistance. */,
@InstitutionName NVARCHAR(255) /* Name for the Institution. */,
@Note NVARCHAR(255) = null /* Notes about this Institution. */,
@InstitutionUrl NVARCHAR(255) = null,
@BHLMemberLibrary BIT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Institution]
(
	[InstitutionCode],
	[InstitutionName],
	[Note],
	[InstitutionUrl],
	[BHLMemberLibrary]
)
VALUES
(
	@InstitutionCode,
	@InstitutionName,
	@Note,
	@InstitutionUrl,
	@BHLMemberLibrary
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

