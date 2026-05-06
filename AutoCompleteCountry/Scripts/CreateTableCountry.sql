CREATE TABLE Country (
  CountryId int IDENTITY(1, 1) PRIMARY KEY NOT NULL,
  CountryName varchar(255) NOT NULL,
  CountryCode varchar(255) NOT NULL,
  Currency varchar(255),
);