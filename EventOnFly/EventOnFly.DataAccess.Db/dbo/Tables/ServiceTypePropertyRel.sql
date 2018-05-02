CREATE TABLE [dbo].[ServiceTypePropertyRel] (
    [ServiceType] INT NOT NULL,
    [PropertyId]  INT NOT NULL,
    CONSTRAINT [PK_ServiceTypePropertyRel] PRIMARY KEY CLUSTERED ([ServiceType] ASC, [PropertyId] ASC),
    CONSTRAINT [FK_ServiceTypePropertyRel_Property_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [dbo].[Property] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [AK_ServiceTypePropertyRel_PropertyId_ServiceType] UNIQUE NONCLUSTERED ([PropertyId] ASC, [ServiceType] ASC)
);

