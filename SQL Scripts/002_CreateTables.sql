USE EmployeeAccountingDb
GO

CREATE TABLE Company(
	Id INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	Name VARCHAR(50) NOT NULL UNIQUE,
	Description VARCHAR(500) NOT NULL,
)

CREATE TABLE Department(
	Id INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	Name VARCHAR(50) NOT NULL UNIQUE,
	Description VARCHAR(255),
)

CREATE TABLE Position(
	Id INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	Name VARCHAR(50) NOT NULL UNIQUE,
	Description VARCHAR(255),
)

CREATE TABLE Employee(
	Id INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
	MiddleName VARCHAR(50) NOT NULL,
	BirthDate DATETIME NOT NULL,
	Country VARCHAR(50),
	City VARCHAR(50),
	Address VARCHAR(50),
	Phone VARCHAR(50),
	HireDate DATETIME NOT NULL,
	Salary DECIMAL NOT NULL,
	DepartmentId INT NOT NULL FOREIGN KEY REFERENCES Department(Id),
	PositionId INT NOT NULL FOREIGN KEY REFERENCES Position(Id),
)
GO