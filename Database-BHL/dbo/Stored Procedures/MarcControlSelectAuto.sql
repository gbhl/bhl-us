
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[MarcControlSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[MarcControlSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.MarcControl
-- Do not modify the contents of this procedure.
-- Generated 2/15/2017 3:14:49 PM

CREATE PROCEDURE [dbo].[MarcControlSelectAuto]

@MarcControlID INT

AS 

SET NOCOUNT ON

SELECT	
	[MarcControlID],
	[MarcID],
	[Tag],
	[Value],
	[CreationDate],
	[LastModifiedDate]
FROM	
	[dbo].[MarcControl]
WHERE	
	[MarcControlID] = @MarcControlID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MarcControlSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

