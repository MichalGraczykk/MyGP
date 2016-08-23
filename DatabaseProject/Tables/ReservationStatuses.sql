CREATE TABLE [dbo].[ReservationStatuses] (
    [STATUS_ID] INT           IDENTITY (1, 1) NOT NULL,
    [NAME]      NVARCHAR (15) NOT NULL,
    PRIMARY KEY CLUSTERED ([STATUS_ID] ASC)
);