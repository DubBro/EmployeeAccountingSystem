USE EmployeeAccountingDb
GO

INSERT INTO Company (Name, Description) VALUES ('Microsoft', 'Microsoft Corporation is an American multinational technology corporation headquartered in Redmond, Washington. ' + 
                                                                'Microsoft ranked No. 14 in the 2022 Fortune 500 rankings of the largest United States corporations by total revenue. ' + 
                                                                'It is considered one of the Big Five American information technology companies, ' + 
                                                                'alongside Alphabet (parent company of Google), Amazon, Apple, and Meta.')

INSERT INTO Position (Name) VALUES ('Team Lead'), ('Architect'), ('.NET Developer'), ('SQL Developer'), ('C++ Developer')

INSERT INTO Department (Name) VALUES ('C++ Team'), ('.NET Team'), ('Database Team')

INSERT INTO Employee 
    (FirstName, LastName, MiddleName, BirthDate, Country, City, Address, Phone, HireDate, Salary, DepartmentId, PositionId) 
VALUES
    ('Jonas', 'Clay', 'Clark', '19900710', 'USA', 'New-York', NULL, NULL, '20101022', 10000, 1, 2),
    ('Haven', 'Barton', 'Jordon', '20000131', 'Canada', NULL, NULL, NULL, '20200507', 3000, 2, 3),
    ('Mohamed', 'Woods', 'Jared', '20010408', NULL, NULL, NULL, NULL, '20231210', 1000, 2, 3),
    ('Tristan', 'Costa', 'Francis', '19951122', 'Spain', 'Madrid', NULL, NULL, '20180212', 5000, 3, 4),
    ('Adolph', 'Kim', 'Ace', '19990225', NULL, NULL, NULL, NULL, '20190110', 4000, 1, 5)