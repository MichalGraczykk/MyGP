﻿CREATE TABLE [dbo].[States] (
    [STATE_ID]   INT           IDENTITY (1, 1) NOT NULL,
    [STATE_NAME] NVARCHAR (15) NOT NULL,
    PRIMARY KEY CLUSTERED ([STATE_ID] ASC)
);