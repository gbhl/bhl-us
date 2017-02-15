
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[MarcSubFieldSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[MarcSubFieldSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.MarcSubField
-- Do not modify the contents of this procedure.
-- Generated 2/15/2017 3:15:23 PM

CREATE PROCEDURE [dbo].[MarcSubFieldSelectAuto]

@MarcSubFieldID INT

AS 

SET NOCOUNT ON

SELECT	
	[MarcSubFieldID],
	[MarcDataFieldID],
	[Code],
	[Value],
	[CreationDate],
	[LastModifiedDate]
FROM	
	[dbo].[MarcSubField]
WHERE	
	[MarcSubFieldID] = @MarcSubFieldID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MarcSubFieldSelectAuto. No information was selected.', 16, 1)
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

