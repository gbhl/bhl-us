CREATE PROCEDURE dbo.DOIPrefixSelectAll
AS
BEGIN

	SELECT	DOIPrefixID,
			Prefix,
			OriginalRegistrant,
			AllowNew,
			CreationDate,
			LastModifiedDate,
			CreationUserID,
			LastModifiedUserID
	FROM	dbo.DOIPrefix

END
GO
