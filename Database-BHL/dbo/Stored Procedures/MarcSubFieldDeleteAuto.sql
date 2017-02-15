
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[MarcSubFieldDeleteAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[MarcSubFieldDeleteAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Delete Procedure for dbo.MarcSubField
-- Do not modify the contents of this procedure.
-- Generated 2/15/2017 3:15:23 PM

CREATE PROCEDURE dbo.MarcSubFieldDeleteAuto

@MarcSubFieldID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[MarcSubField]
WHERE	
	[MarcSubFieldID] = @MarcSubFieldID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MarcSubFieldDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

