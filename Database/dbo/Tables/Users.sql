CREATE TABLE [dbo].[Users] (
    [id]               INT           IDENTITY (1, 1) NOT NULL,
    [userName]         NVARCHAR (50) NOT NULL,
    [password]         NVARCHAR (50) NOT NULL,
    [isAdmin]          BIT           NOT NULL,
    [registrationDate] AS            (getdate()),
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([userName] ASC)
);

