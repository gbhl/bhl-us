
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[MarcSubFieldInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[MarcSubFieldInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.MarcSubField
-- Do not modify the contents of this procedure.
-- Generated 2/15/2017 3:15:23 PM

CREATE PROCEDURE dbo.MarcSubFieldInsertAuto

@MarcSubFieldID INT OUTPUT,
@MarcDataFieldID INT,
@Code NCHAR(1),
@Value NVARCHAR(2000)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[MarcSubField]
( 	[MarcDataFieldID],
	[Code],
	[Value],
	[CreationDate],
	[LastModifiedDate] )
VALUES
( 	@MarcDataFieldID,
	@Code,
	@Value,
	getdate(),
	getdate() )

SET @MarcSubFieldID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.MarcSubFieldInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[MarcSubFieldID],
		[MarcDataFieldID],
		[Code],
		[Value],
		[CreationDate],
		[LastModifiedDate]	
	FROM [dbo].[MarcSubField]
	WHERE
		[MarcSubFieldID] = @MarcSubFieldID
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

