--Creation of DB
USE master
GO
-- Create the new database if it does not exist` already
IF NOT EXISTS (
   SELECT [name]
       FROM sys.databases
       WHERE [name] = N'CompanyManager'
)
CREATE DATABASE CompanyManager
GO
USE CompanyManager 
GO
--CREATION OF TABLE WORKERS
CREATE TABLE Workers
(
  WorkerID INT NOT NULL PRIMARY KEY,
  WorkerName NVARCHAR(50) NOT NULL,
  BirthDate Date NOT NULL
)
--CREATION OF TABLE Projects
CREATE TABLE Projects
(
	ProjectID INT NOT NULL PRIMARY KEY,
	ProjectName NVARCHAR(50) NOT NULL,
	CreationDate Date NOT NULL,
	State NVARCHAR(50) NOT NULL,
	ClosingDate Date 
)
--CREATION OF TABLE Positions
CREATE TABLE Positions
(
	PositionID INT NOT NULL PRIMARY KEY,
	Name NVARCHAR(50) NOT NULL
)
--CREATION OF TABLE Tasks
CREATE TABLE Tasks
(
	TaskID INT NOT NULL PRIMARY KEY,
	TaskName NVARCHAR(50) NOT NULL,
	Status NVARCHAR(MAX) NOT NULL,
	Deadline Date NOT NULL,
	IdWorker INT,
	IdProject INT NOT NULL,
	IdPosition INT NOT NULL,
    FOREIGN KEY (IdWorker) REFERENCES Workers (WorkerID),
    FOREIGN KEY (IdProject) REFERENCES Projects (ProjectID),
	FOREIGN KEY (IdPosition) REFERENCES Positions (PositionID),
)
ALTER TABLE Projects
ADD CONSTRAINT CHK_ProjectState CHECK (State = 'Open' OR State='Close');

ALTER TABLE Tasks
ADD CONSTRAINT CHK_TaskStatus CHECK (Status = 'Open' OR Status='Done' OR Status='Accepted' OR Status='Need modifications'); 

INSERT INTO dbo.Projects
VALUES (2, 'TaskManager','20190203', 'Open','20190304'); 
INSERT INTO dbo.Projects
VALUES (1, 'New Project','20190305', 'Close','20190804');
INSERT INTO dbo.Projects
VALUES (3, 'New Site','20190727', 'Open','20191011');  

INSERT INTO dbo.Workers
VALUES (1, 'Iryna','20000422');  
INSERT INTO dbo.Workers
VALUES (2, 'Dima','19990314'); 
INSERT INTO dbo.Workers
VALUES (3, 'Katya','19900516');

INSERT INTO dbo.Positions
VALUES (1, 'Headmaster'); 
INSERT INTO dbo.Positions
VALUES (2, 'Developer');
INSERT INTO dbo.Positions
VALUES (3, 'Designer');

INSERT INTO dbo.Tasks
VALUES (1, 'Web site', 'Open','20190728', 1, 3, 2)

INSERT INTO dbo.Tasks
VALUES (2, 'Web API', 'Closed','20190428', 2, 2, 1)


