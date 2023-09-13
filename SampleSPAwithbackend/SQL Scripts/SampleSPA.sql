CREATE DATABASE SampleSPA;

GO
USE SampleSPA;

GO
CREATE TABLE Users 
(
id INT IDENTITY PRIMARY KEY,
userName NVARCHAR(50) UNIQUE NOT NULL,
[password] NVARCHAR(50) NOT NULL,
isAdmin BIT NOT NULL,
registrationDate AS GETDATE()
);


GO
INSERT INTO Users VALUES ('admin', 'F1tprotr@cker!', 1);

GO
SELECT * FROM Users