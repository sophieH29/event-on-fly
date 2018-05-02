CREATE TABLE [dbo].[ServiceOrderPropertyValue] (
    [ServiceId]       INT NOT NULL,
    [BookingId]       INT NOT NULL,
    [PropertyId]      INT NOT NULL,
    [PropertyValueId] INT NULL,
    CONSTRAINT [PK_ServiceOrderPropertyValue] PRIMARY KEY CLUSTERED ([ServiceId] ASC, [BookingId] ASC, [PropertyId] ASC),
    CONSTRAINT [FK_ServiceOrderPropertyValue_Property_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [dbo].[Property] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ServiceOrderPropertyValue_PropertyValue_PropertyValueId] FOREIGN KEY ([PropertyValueId]) REFERENCES [dbo].[PropertyValue] ([Id]),
    CONSTRAINT [FK_ServiceOrderPropertyValue_ServiceOrder_ServiceId_BookingId] FOREIGN KEY ([ServiceId], [BookingId]) REFERENCES [dbo].[ServiceOrder] ([ServiceId], [BookingId]) ON DELETE CASCADE,
    CONSTRAINT [AK_ServiceOrderPropertyValue_BookingId_PropertyId_ServiceId] UNIQUE NONCLUSTERED ([BookingId] ASC, [PropertyId] ASC, [ServiceId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderPropertyValue_PropertyId]
    ON [dbo].[ServiceOrderPropertyValue]([PropertyId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderPropertyValue_PropertyValueId]
    ON [dbo].[ServiceOrderPropertyValue]([PropertyValueId] ASC);

