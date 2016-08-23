CREATE TABLE [dbo].[Connectors] (
    [CONNECTION_ID] INT IDENTITY (1, 1) NOT NULL,
    [ITEM_ID]       INT NOT NULL,
    [VALUE_ID]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([CONNECTION_ID] ASC),
    CONSTRAINT [FK_Connectors_ToTable_Items] FOREIGN KEY ([ITEM_ID]) REFERENCES [dbo].[Items] ([ITEM_ID]),
    CONSTRAINT [FK_Connectors_ToTable_PropValues] FOREIGN KEY ([VALUE_ID]) REFERENCES [dbo].[PropValues] ([VALUE_ID])
);