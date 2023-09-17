USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name = 'n5now_db')
BEGIN
	DROP DATABASE n5now_db
END
GO

CREATE DATABASE n5now_db
GO

USE n5now_db
GO

CREATE TABLE PermissionTypes
(
	Id INT IDENTITY(1,1),
	Description VARCHAR(250),
	CONSTRAINT PK_PermissionTypes PRIMARY KEY (Id)
)

CREATE TABLE Permissions
(
	Id INT IDENTITY(1,1),
	EmployeeName VARCHAR(125),
	EmployeeLastName VARCHAR(125),
	PermissionTypeId INT,
	PermissionDate DATETIME,
	CONSTRAINT PK_Permissions PRIMARY KEY (Id),
	CONSTRAINT FK_PermissionTypes FOREIGN KEY (PermissionTypeId)
	REFERENCES PermissionTypes(Id)
)