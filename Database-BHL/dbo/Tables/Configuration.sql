CREATE TABLE [dbo].[Configuration] (
    [ConfigurationID]    INT             NOT NULL,
    [ConfigurationName]  NVARCHAR (50)   NOT NULL,
    [ConfigurationValue] NVARCHAR (1000) NOT NULL,
    CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED ([ConfigurationID] ASC)
);

