CREATE TABLE [dbo].[ServicePropertyValue] (
    [ServiceId]        INT            NOT NULL,
    [PropertyId]       INT            NOT NULL,
    [EvaluationScript] NVARCHAR (MAX) NULL,
    [PropertyValueId]  INT            NULL,
    CONSTRAINT [PK_ServicePropertyValue] PRIMARY KEY CLUSTERED ([ServiceId] ASC, [PropertyId] ASC),
    CONSTRAINT [FK_ServicePropertyValue_Property_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [dbo].[Property] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ServicePropertyValue_PropertyValue_PropertyValueId] FOREIGN KEY ([PropertyValueId]) REFERENCES [dbo].[PropertyValue] ([Id]),
    CONSTRAINT [FK_ServicePropertyValue_Service_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [AK_ServicePropertyValue_PropertyId_ServiceId] UNIQUE NONCLUSTERED ([PropertyId] ASC, [ServiceId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_ServicePropertyValue_PropertyValueId]
    ON [dbo].[ServicePropertyValue]([PropertyValueId] ASC);

