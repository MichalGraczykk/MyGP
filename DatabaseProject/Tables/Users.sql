CREATE TABLE [dbo].[Users] (
    [USER_ID]   INT           IDENTITY (1, 1) NOT NULL,
    [LOGIN]     NVARCHAR (20) NOT NULL,
    [PASSWORD]  NVARCHAR (20) NOT NULL,
    [NAME]      NVARCHAR (15) NOT NULL,
    [SURNAME]   NVARCHAR (25) NOT NULL,
    [AGE]       INT           NOT NULL,
    [ADRESS_ID] INT           NOT NULL,
    [ROLE_ID]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([USER_ID] ASC),
    CONSTRAINT [FK_Users_ToTable_UsersAdresses] FOREIGN KEY ([ADRESS_ID]) REFERENCES [dbo].[UsersAdresses] ([ADRESS_ID]),
    CONSTRAINT [FK_Users_ToTable_Roles] FOREIGN KEY ([ROLE_ID]) REFERENCES [dbo].[Roles] ([ROLE_ID])
);