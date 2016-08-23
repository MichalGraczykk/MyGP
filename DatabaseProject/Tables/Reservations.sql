CREATE TABLE [dbo].[Reservations] (
    [RESERVATION_ID] INT             IDENTITY (1, 1) NOT NULL,
    [DATE_FROM]      DATETIME        NOT NULL,
    [DATE_TO]        DATETIME        NOT NULL,
    [ORDER_DATE]     DATETIME        NOT NULL,
    [OVERALL_PRICE]  DECIMAL (12, 2) NOT NULL,
    [USER_ID]        INT             NOT NULL,
    [ITEM_ID]        INT             NOT NULL,
    [STATUS_ID]      INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([RESERVATION_ID] ASC),
    CONSTRAINT [FK_Reservations_ToTable_Users] FOREIGN KEY ([USER_ID]) REFERENCES [dbo].[Users] ([USER_ID]),
    CONSTRAINT [FK_Reservations_ToTable_Items] FOREIGN KEY ([ITEM_ID]) REFERENCES [dbo].[Items] ([ITEM_ID]),
    CONSTRAINT [FK_Reservations_ToTable_ReservationStatuses] FOREIGN KEY ([STATUS_ID]) REFERENCES [dbo].[ReservationStatuses] ([STATUS_ID])
);