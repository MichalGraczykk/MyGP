CREATE TABLE [dbo].[Items] (
    [ITEM_ID]       INT            IDENTITY (1, 1) NOT NULL,
    [NAME]          NVARCHAR (50)  NOT NULL,
    [DESCRPTION]    NVARCHAR (MAX) NULL,
    [PHOTO]         NVARCHAR (400) NULL,
    [PRICE_PER_DAY] DECIMAL (9, 2) NOT NULL,
    [STATE_ID]      INT            NULL,
    PRIMARY KEY CLUSTERED ([ITEM_ID] ASC),
    CONSTRAINT [FK_Items_ToTable_States] FOREIGN KEY ([STATE_ID]) REFERENCES [dbo].[States] ([STATE_ID])
);