CREATE TABLE [dbo].[Property] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [DefaultValueId] INT            NULL,
    [DefaultVaueId]  INT            NULL,
    [Name]           NVARCHAR (MAX) NULL,
    [PropertyType]   INT            NOT NULL,
    CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Property_PropertyValue_DefaultVaueId] FOREIGN KEY ([DefaultVaueId]) REFERENCES [dbo].[PropertyValue] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Property_DefaultVaueId]
    ON [dbo].[Property]([DefaultVaueId] ASC);

