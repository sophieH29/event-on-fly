CREATE TABLE [dbo].[ServiceTypeRelation] (
    [ServiceId]      INT            NOT NULL,
    [ServiceType]    INT            NOT NULL,
    [AlowanceScript] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ServiceTypeRelation] PRIMARY KEY CLUSTERED ([ServiceId] ASC, [ServiceType] ASC),
    CONSTRAINT [FK_ServiceTypeRelation_Service_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service] ([Id]) ON DELETE CASCADE
);

