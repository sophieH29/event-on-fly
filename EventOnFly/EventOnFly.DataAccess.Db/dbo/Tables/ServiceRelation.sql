CREATE TABLE [dbo].[ServiceRelation] (
    [Service1Id]   INT NOT NULL,
    [Service2Id]   INT NOT NULL,
    [RelationType] INT NOT NULL,
    CONSTRAINT [PK_ServiceRelation] PRIMARY KEY CLUSTERED ([Service1Id] ASC, [Service2Id] ASC),
    CONSTRAINT [FK_ServiceRelation_Service_Service1Id] FOREIGN KEY ([Service1Id]) REFERENCES [dbo].[Service] ([Id]),
    CONSTRAINT [FK_ServiceRelation_Service_Service2Id] FOREIGN KEY ([Service2Id]) REFERENCES [dbo].[Service] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceRelation_Service2Id]
    ON [dbo].[ServiceRelation]([Service2Id] ASC);

