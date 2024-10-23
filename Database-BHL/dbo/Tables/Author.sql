CREATE TABLE [dbo].[Author] (
    [AuthorID]           INT            IDENTITY (1, 1) NOT NULL,
    [AuthorTypeID]       INT            NULL,
    [StartDate]          NVARCHAR (25)  CONSTRAINT [DF_Author_DOB] DEFAULT ('') NOT NULL,
    [EndDate]            NVARCHAR (25)  CONSTRAINT [DF_Author_DOD] DEFAULT ('') NOT NULL,
    [Numeration]         NVARCHAR (300) CONSTRAINT [DF_Author_Numeration] DEFAULT ('') NOT NULL,
    [Title]              NVARCHAR (200) CONSTRAINT [DF_Author_Title] DEFAULT ('') NOT NULL,
    [GenerationalSuffix] NVARCHAR (50)  CONSTRAINT [DF_Author_GenerationalSuffix] DEFAULT ('') NOT NULL,
    [Unit]               NVARCHAR (300) CONSTRAINT [DF_Author_Unit] DEFAULT ('') NOT NULL,
    [Location]           NVARCHAR (200) CONSTRAINT [DF_Author_Location] DEFAULT ('') NOT NULL,
	[Note]               NVARCHAR (MAX) CONSTRAINT [DF_Author_Note] DEFAULT('') NOT NULL,
    [IsActive]           SMALLINT       CONSTRAINT [DF_Author_IsActive] DEFAULT ((1)) NOT NULL,
    [RedirectAuthorID]   INT            NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_Author_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_Author_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT            CONSTRAINT [DF_Author_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT            CONSTRAINT [DF_Author_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED ([AuthorID] ASC),
    CONSTRAINT [FK_Author_Author] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Author] ([AuthorID]),
    CONSTRAINT [FK_Author_AuthorType] FOREIGN KEY ([AuthorTypeID]) REFERENCES [dbo].[AuthorType] ([AuthorTypeID]),
    CONSTRAINT [CK_Author_GenerationalSuffix] CHECK ([GenerationalSuffix] IN ('Jr.', 'Sr.', 'III', ''))
);
GO

CREATE NONCLUSTERED INDEX [IX_Author_IsActive] 
	ON [dbo].[Author]([IsActive] ASC)
	INCLUDE ([AuthorID],[StartDate],[EndDate]); 
GO

CREATE NONCLUSTERED INDEX IX_Author_StartDateEndDate
	ON dbo.Author (StartDate, EndDate)
	INCLUDE (AuthorID)
GO
