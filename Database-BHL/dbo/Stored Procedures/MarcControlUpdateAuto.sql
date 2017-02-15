
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[MarcControlUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[MarcControlUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.MarcControl
-- Do not modify the contents of this procedure.
-- Generated 2/15/2017 3:14:49 PM

CREATE PROCEDURE dbo.MarcControlUpdateAuto

@MarcControlID INT,
@MarcID INT,
@Tag NCHAR(3),
@Value NVARCHAR(2000)

AS 

SET NOCOUNT ON

UPDATE [dbo].[MarcControl]
SET
	[MarcID] = @MarcID,
	[Tag] = @Tag,
	[Value] = @Value,
	[LastModifiedDate] = getdate()
WHERE
	[MarcControlID] = @MarcControlID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MarcControlUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[MarcControlID],
		[MarcID],
		[Tag],
		[Value],
		[CreationDate],
		[LastModifiedDate]
	FROM [dbo].[MarcControl]
	WHERE
		[MarcControlID] = @MarcControlID
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

