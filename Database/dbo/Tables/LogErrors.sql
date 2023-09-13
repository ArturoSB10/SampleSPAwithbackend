CREATE TABLE [dbo].[LogErrors] (
    [id]               INT            IDENTITY (1, 1) NOT NULL,
    [error]            NVARCHAR (MAX) NULL,
    [registrationDate] AS             (getdate()),
    PRIMARY KEY CLUSTERED ([id] ASC)
);

