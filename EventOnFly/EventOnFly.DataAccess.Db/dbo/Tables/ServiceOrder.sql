CREATE TABLE [dbo].[ServiceOrder] (
    [ServiceId] INT NOT NULL,
    [BookingId] INT NOT NULL,
    CONSTRAINT [PK_ServiceOrder] PRIMARY KEY CLUSTERED ([ServiceId] ASC, [BookingId] ASC),
    CONSTRAINT [FK_ServiceOrder_Booking_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [dbo].[Booking] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ServiceOrder_Service_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [AK_ServiceOrder_BookingId_ServiceId] UNIQUE NONCLUSTERED ([BookingId] ASC, [ServiceId] ASC)
);

