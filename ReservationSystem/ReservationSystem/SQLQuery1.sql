CREATE TABLE Brand (
Id INT IDENTITY(1,1) PRIMARY KEY,
[Name] VARCHAR(50) NOT NULL
);

INSERT INTO Brand([Name]) VALUES ('Food Corp');

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
INSERT INTO Customer VALUES ('Jared', 'Hendrix', '1234567890', 'jimmyscousinjared@gmail.com');
INSERT INTO Customer VALUES ('Randy', 'Hendrix', '1234567890', 'notrelatedtojimmy@gmail.com');
INSERT INTO Customer VALUES ('Sasha', 'Jones', '1234567890', 'sjone221@gmail.com');
INSERT INTO Customer VALUES ('Roselina', 'Ortega', '1234567890', 'RosiOs78@gmail.com');

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

INSERT INTO [Location] VALUES ('Hubert''s St. Johnsbury', 'St. Johnsbury', 'Vermont', 10, '09:00:00', '19:00:00', 1);
INSERT INTO [Location] VALUES ('Bob''s Fish Mart', 'Sydney', 'Australia', 18, '09:00:00', '19:00:00', 1);
INSERT INTO [Location] VALUES ('The Hungry Hunter Springvale', 'Springvale', 'Mondstadt', 40, '10:00:00', '20:00:00', 1);

SELECT * FROM [Location];

CREATE TABLE Reservation(
Id INT IDENTITY(1,1) PRIMARY KEY,
[Length] FLOAT NOT NULL,
PartySize INT NOT NULL,
ReservationTime DATETIME NOT NULL,
CustomerId INT FOREIGN KEY REFERENCES Customer(Id) ON UPDATE CASCADE ON DELETE CASCADE,
LocationId INT FOREIGN KEY REFERENCES [Location](Id) ON UPDATE CASCADE ON DELETE CASCADE
);

INSERT INTO Reservation VALUES (1, 2,'2022-12-24 10:00:00', 2, 1);
INSERT INTO Reservation VALUES (1, 2,'2022-12-24 10:00:00', 1, 2);
INSERT INTO Reservation VALUES (1, 4,'2022-12-24 10:00:00', 3, 3);
INSERT INTO Reservation VALUES (1, 2,'2022-12-24 10:00:00', 4, 2);
INSERT INTO Reservation VALUES (1, 2,'2022-12-31 18:00:00', 1, 3);
INSERT INTO Reservation VALUES (1, 2,'2022-12-25 12:00:00', 2, 1);
INSERT INTO Reservation VALUES (1, 1,'2022-12-24 10:00:00', 5, 2);
INSERT INTO Reservation VALUES (1, 2,'2022-12-26 17:00:00', 4, 3);
INSERT INTO Reservation VALUES (1, 2,'2022-12-24 12:00:00', 1, 1);
INSERT INTO Reservation VALUES (1, 15,'2022-12-24 10:00:00', 5, 3);

--DROP TABLE Reservation;

INSERT INTO Reservation([Length], PartySize, ReservationTime, CustomerId, LocationId) VALUES (1, 1, '2022-02-14', 1, 1);

SELECT * FROM Reservation;