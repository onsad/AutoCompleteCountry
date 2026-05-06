IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_SCHEMA = 'dbo' 
        AND TABLE_NAME = 'Country')
BEGIN
    CREATE TABLE [dbo].[Country] (
          CountryId int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
          CountryName varchar(255) NOT NULL,
          CountryCode varchar(255) NOT NULL,
          Currency varchar(255),
        );
END
GO