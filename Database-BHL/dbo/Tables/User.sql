CREATE TABLE [dbo].[User] (
    [UserID]        INT            NOT NULL,
    [FirstName]     NVARCHAR (30)  NULL,
    [LastName]      NVARCHAR (30)  NULL,
    [LastNameFirst] NVARCHAR (62)  NOT NULL,
    [Email]         NVARCHAR (60)  NULL,
    [Password]      NVARCHAR (10)  NULL,
    [UserNote]      NVARCHAR (255) NULL,
    CONSTRAINT [aaaaaUser_PK] PRIMARY KEY CLUSTERED ([UserID] ASC)
);


GO
