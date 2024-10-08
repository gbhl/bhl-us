SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InstitutionSelectBySegmentIDAndRole]

@SegmentID int,
@InstitutionRoleName nvarchar(100)

AS

BEGIN

SET NOCOUNT ON

SELECT	i.InstitutionCode,
		i.InstitutionName,
		i.Note,
		i.InstitutionUrl,
		i.BHLMemberLibrary,
		ii.ItemInstitutionID,
		r.InstitutionRoleName,
		r.InstitutionRoleLabel,
		i.CreationDate,
		i.LastModifiedDate,
		i.CreationUserID,
		i.LastModifiedUserID
FROM	dbo.Segment s
		INNER JOIN dbo.ItemInstitution ii ON s.ItemID = ii.ItemID
		INNER JOIN Institution i ON ii.InstitutionCode = i.InstitutionCode
		INNER JOIN InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	s.SegmentID = @SegmentID
AND		r.InstitutionRoleName = @InstitutionRoleName

END


GO
