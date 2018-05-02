CREATE TABLE [dbo].[Booking] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [DateFrom] DATETIME2 (7) NOT NULL,
    [DateTo]   DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED ([Id] ASC)
);

