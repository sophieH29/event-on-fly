CREATE TYPE [dbo].[PropValueMap] AS TABLE (
    [Identifier]    VARCHAR (255)  NOT NULL,
    [ValueType]     VARCHAR (255)  NOT NULL,
    [BooleanVal]    BIT            NULL,
    [StringVal]     NVARCHAR (MAX) NULL,
    [MinIntegerVal] INT            NULL,
    [MaxIntegerVal] INT            NULL,
    [MinFloatVal]   FLOAT (53)     NULL,
    [MaxFloatVal]   FLOAT (53)     NULL,
    [MinDateVal]    DATETIME       NULL,
    [MaxDateVal]    DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Identifier] ASC));

