CREATE TABLE [dbo].[PropValues] (
    [VALUE_ID]    INT           IDENTITY (1, 1) NOT NULL,
    [VALUE]       NVARCHAR (20) NOT NULL,
    [PROPERTY_ID] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([VALUE_ID] ASC),
    CONSTRAINT [FK_PropValues_ToTable_Properties] FOREIGN KEY ([PROPERTY_ID]) REFERENCES [dbo].[Properties] ([PROPERTY_ID])
);