﻿CREATE TABLE [dbo].[UsersAdresses] (
    [ADRESS_ID]        INT           IDENTITY (1, 1) NOT NULL,
    [STREET_NAME]      NVARCHAR (25) NOT NULL,
    [STREET_NUMBER]    NVARCHAR (5)  NOT NULL,
    [POSSESION_NUMBER] INT           NULL,
    PRIMARY KEY CLUSTERED ([ADRESS_ID] ASC)
);

