
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[MarcControlInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[MarcControlInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.MarcControl
-- Do not modify the contents of this procedure.
-- Generated 2/15/2017 3:14:49 PM

CREATE PROCEDURE dbo.MarcControlInsertAuto

@MarcControlID INT OUTPUT,
@MarcID INT,
@Tag NCHAR(3),
@Value NVARCHAR(2000)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[MarcControl]
( 	[MarcID],
	[Tag],
	[Value],
	[CreationDate],
	[LastModifiedDate] )
VALUES
( 	@MarcID,
	@Tag,
	@Value,
	getdate(),
	getdate() )

SET @MarcControlID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MarcControlInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

