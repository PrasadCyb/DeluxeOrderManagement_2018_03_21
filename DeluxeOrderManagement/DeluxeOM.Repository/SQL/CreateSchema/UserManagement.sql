
USE DeluxeOrderManagement
GO

CREATE TABLE Users
(
	UserId INT IDENTITY(1,1) PRIMARY KEY,
	UserName VARCHAR(128) NOT NULL,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	Email VARCHAR(128) NOT NULL,
	PhoneNo VARCHAR(50) NULL,
	Title VARCHAR(50) NULL,
	Active BIT NOT NULL,
	LoginAttempts INT DEFAULT 0 NOT NULL,
	LastLogin DATETIME NULL,
	CreatedDate DATETIME NOT NULL,
	ModifiedDate	DATETIME NOT NULL
)


CREATE TABLE Role
(
	RoleId INT IDENTITY(1,1) PRIMARY KEY,
	RoleName VARCHAR(50) NOT NULL,
	Description VARCHAR(250) NULL,
	Active BIT NOT NULL,
	ModifiedDate DATETIME NOT NULL
)

CREATE TABLE Privilege
(
	PrivId INT IDENTITY(1,1) PRIMARY KEY,
	PrivName VARCHAR(50) NOT NULL,
	Description VARCHAR(250) NULL,
	ModifiedDate DATETIME NOT NULL
)

CREATE TABLE UserRole
(
	UserRoleId INT IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL,
	RoleId INT NOT NULL,
	CONSTRAINT fk_UserRole_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId),
	CONSTRAINT fk_UserRole_RoleId FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);

CREATE TABLE RolePriv
(
	RolePrivId INT IDENTITY(1,1) PRIMARY KEY,
	RoleId INT NOT NULL,
	PrivId INT NOT NULL,
	CONSTRAINT fk_RolePrivilege_RoleId FOREIGN KEY (RoleId) REFERENCES Role(RoleId),
	CONSTRAINT fk_RolePrivilege_PrivId FOREIGN KEY (PrivId) REFERENCES Privilege(PrivId)
);

CREATE TABLE PasswordReset
(
	PasswordResetId INT IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL,
	Email VARCHAR(128) NOT NULL,
	Token VARCHAR(64) NOT NULL,
	ExpireDateTime	DATETIME NOT NULL,
	CONSTRAINT fk_PasswordRest_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE UserPassword
(
	UserPasswordId INT IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL,
	Password VARCHAR(128) NOT NULL,
	PasswordDate DATETIME DEFAULT GETDATE() NOT NULL,
	CreatedDate	DATE DEFAULT GETDATE() NOT NULL,
	CONSTRAINT fk_UserPassword_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

INSERT INTO Users (Username, FirstName, LastName, Email, PhoneNo, Active, CreatedDate, ModifiedDate)
VALUES ('praasdja', 'Prasad', 'Jadhav', 'prasadja@cybage.com', '1234567890', 1, GETDATE(), GETDATE())

INSERT INTO Users (Username, FirstName, LastName, Email, PhoneNo, Active, CreatedDate, ModifiedDate)
VALUES ('analystuser', 'analyst', 'deluxe', 'analyst@deluxe.com', '1234567890', 1, GETDATE(), GETDATE())

INSERT INTO Users (Username, FirstName, LastName, Email, PhoneNo, Active, CreatedDate, ModifiedDate)
VALUES ('manageruser', 'manager', 'deluxe', 'manager@deluxe.com', '1234567890', 1, GETDATE(), GETDATE())

INSERT INTO Users (Username, FirstName, LastName, Email, PhoneNo, Active, CreatedDate, ModifiedDate)
VALUES ('adminuser', 'admin', 'deluxe', 'admin@deluxe.com', '1234567890', 1, GETDATE(), GETDATE())

/*Cybage@123*/
INSERT INTO UserPassword (UserId, Password, CreatedDate)
VALUES (1, 'j5XldmP/m7tFQXJmyIJ57YSk/Z2px5s4J5rW', GETDATE());

/*Cybage@123*/
INSERT INTO UserPassword (UserId, Password, CreatedDate)
VALUES (2, 'j5XldmP/m7tFQXJmyIJ57YSk/Z2px5s4J5rW', GETDATE());

/*Cybage@123*/
INSERT INTO UserPassword (UserId, Password, CreatedDate)
VALUES (3, 'j5XldmP/m7tFQXJmyIJ57YSk/Z2px5s4J5rW', GETDATE());

/*Cybage@123*/
INSERT INTO UserPassword (UserId, Password, CreatedDate)
VALUES (4, 'j5XldmP/m7tFQXJmyIJ57YSk/Z2px5s4J5rW', GETDATE());

INSERT INTO Role (RoleName, Description, Active, ModifiedDate)
VALUES ('Root', 'ROOT System Administration Role', 1, GETDATE());

INSERT INTO Role (RoleName, Description, Active, ModifiedDate)
VALUES ('Analyst', 'Analyst Role', 1, GETDATE());

INSERT INTO Role (RoleName, Description, Active, ModifiedDate)
VALUES ('Manager', 'Manager Role', 1, GETDATE());

INSERT INTO Role (RoleName, Description, Active, ModifiedDate)
VALUES ('Administrator', 'Administrator Role', 1, GETDATE());

INSERT INTO Privilege (PrivName, Description, ModifiedDate)
VALUES('SystemAdmin', 'Privilege allows access to all system functions', GETDATE());

INSERT INTO Privilege (PrivName, Description, ModifiedDate)
VALUES('ReportAdmin', 'Execute and export reports', GETDATE());

INSERT INTO Privilege (PrivName, Description, ModifiedDate)
VALUES('ManagerAdmin', 'Update certain fields in the application eg. Order statuses, enter dates etc', GETDATE());

INSERT INTO RolePriv (RoleId, PrivId) 
VALUES
(
	(SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Analyst'),
	(SELECT TOP 1 PrivId FROM Privilege WHERE PrivName = 'ReportAdmin')
),
(
	(SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Manager'),
	(SELECT TOP 1 PrivId FROM Privilege WHERE PrivName = 'ReportAdmin')
),
(
	(SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Manager'),
	(SELECT TOP 1 PrivId FROM Privilege WHERE PrivName = 'ManagerAdmin')
),
(
	(SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Administrator'),
	(SELECT TOP 1 PrivId FROM Privilege WHERE PrivName = 'ReportAdmin')
),
(
	(SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Administrator'),
	(SELECT TOP 1 PrivId FROM Privilege WHERE PrivName = 'ManagerAdmin')
),
(
	(SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Administrator'),
	(SELECT TOP 1 PrivId FROM Privilege WHERE PrivName = 'SystemAdmin')
)

INSERT INTO UserRole (UserId, RoleId)
VALUES 
(
	(SELECT TOP 1 UserId FROM Users WHERE Username = 'praasdja'),
	(SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Administrator')
),
(
	(SELECT TOP 1 UserId FROM Users WHERE Username = 'analystuser'),
	(SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Analyst')
),
(
	(SELECT TOP 1 UserId FROM Users WHERE Username = 'manageruser'),
	(SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Manager')
),
(
	(SELECT TOP 1 UserId FROM Users WHERE Username = 'adminuser'),
	(SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Administrator')
)