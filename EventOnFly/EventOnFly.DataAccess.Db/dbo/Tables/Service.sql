CREATE TABLE [dbo].[Service] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [ServiceType] INT            NOT NULL,
    [State]       INT            DEFAULT ((2)) NOT NULL,
    [UserId]      INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED ([Id] ASC)
);

