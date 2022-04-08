CREATE TABLE Brand (
Id INT IDENTITY(1,1) PRIMARY KEY,
[Name] VARCHAR(50) NOT NULL
);

INSERT INTO Brand([Name]) VALUES ('Hubert''s Discount Gas Station Sushi');

SELECT * FROM Brand;
--DELETE FROM Brand WHERE Id = 1;

CREATE TABLE Customer(
Id INT IDENTITY(1,1) PRIMARY KEY,
FirstName VARCHAR(20) NOT NULL,
LastName VARCHAR(20) NOT NULL,
Phone VARCHAR(10) NOT NULL,
Email VARCHAR(50)
);

INSERT INTO Customer VALUES ('Jimmy', 'Hendrix', '1234567890', 'jimmyhjeans@gmail.com');

SELECT * FROM Customer;

CREATE TABLE Location(
Id int IDENTITY(1,1) PRIMARY KEY,
[Name] VARCHAR(50) NOT NULL,
City VARCHAR(50) NOT NULL,
[State] VARCHAR(20) NOT NULL,
Capacity INT NOT NULL,
OpenTime TIME NOT NULL,
CloseTime TIME NOT NULL,
BrandId INT FOREIGN KEY REFERENCES Brand(Id)
);

--DROP TABLE Location;

INSERT INTO [Location] VALUES ('Hubert''s St. Johnsbury', 'St. Johnsbury', 'Vermont', 1, '10:00:00', '19:00:00', 1);

SELECT * FROM [Location];

CREATE TABLE Reservation(
Id INT IDENTITY(1,1) PRIMARY KEY,
[Length] FLOAT NOT NULL,
PartySize INT NOT NULL,
ReservationTime DATETIME NOT NULL,
CustomerId INT FOREIGN KEY REFERENCES Customer(Id) ON UPDATE CASCADE ON DELETE CASCADE,
LocationId INT FOREIGN KEY REFERENCES [Location](Id) ON UPDATE CASCADE ON DELETE CASCADE
);

--DROP TABLE Reservation;

INSERT INTO Reservation([Length], PartySize, ReservationTime, CustomerId, LocationId) VALUES (1, 1, '2022-02-14', 1, 1);

SELECT * FROM Reservation;