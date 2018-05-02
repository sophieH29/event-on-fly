CREATE TABLE [dbo].[PropertyValue] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [BooleanValue] BIT            NULL,
    [FloatValue]   FLOAT (53)     NULL,
    [IntegerValue] INT            NULL,
    [TextValue]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_PropertyValue] PRIMARY KEY CLUSTERED ([Id] ASC)
);

