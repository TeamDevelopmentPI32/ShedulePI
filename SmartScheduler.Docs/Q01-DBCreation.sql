CREATE DATABASE SmartScheduler;
GO
USE [SmartScheduler]
CREATE TABLE [User]
(
userId INT IDENTITY(1,1) PRIMARY KEY,
[login] VARCHAR(50) NOT NULL,
[password] VARCHAR(50) NOT NULL,
regDate DATE NOT NULL,
lastActivityDate DATE
);

CREATE TABLE [Rank]
(
rankid INT IDENTITY(1,1) PRIMARY KEY,
[rank] VARCHAR(50) NOT NULL,
);

CREATE TABLE Student
(
studentId INT IDENTITY(1,1) PRIMARY KEY,
[name] VARCHAR(50) NOT NULL,
surname VARCHAR(50) NOT NULL,
middlename VARCHAR(50),
enteringYear int NOT NULL,
userId int FOREIGN KEY REFERENCES [User](userId)
);

CREATE TABLE Teacher
(
teacherId INT IDENTITY(1,1) PRIMARY KEY,
[name] VARCHAR(50) NOT NULL,
surname VARCHAR(50) NOT NULL,
middlename VARCHAR(50),
enteringYear int NOT NULL,
rankid int FOREIGN KEY REFERENCES [Rank](rankid),
userId int FOREIGN KEY REFERENCES [User](userId)
);

CREATE TABLE Administator
(
administratorId INT IDENTITY(1,1) PRIMARY KEY,
[name] VARCHAR(50) NOT NULL,
surname VARCHAR(50) NOT NULL,
userId int FOREIGN KEY REFERENCES [User](userId)
);

CREATE TABLE [Group]
(
groupId INT IDENTITY(1,1) PRIMARY KEY,
[name] VARCHAR(50) NOT NULL,

);

CREATE TABLE StudentsInGroups
(
studentsInGroupsId INT IDENTITY(1,1) PRIMARY KEY,
groupId int FOREIGN KEY REFERENCES [Group](groupId),
studentId int FOREIGN KEY REFERENCES Student(studentId)
);

CREATE TABLE Auditory 
(
auditoryId INT IDENTITY(1,1) PRIMARY KEY,
number int NOT NULL
);

CREATE TABLE Subject
(
subjectId INT IDENTITY(1,1) PRIMARY KEY,
[name] VARCHAR(50) NOT NULL,
credits float NOT NULL
);

CREATE TABLE Schedule
(
scheduleId INT IDENTITY(1,1) PRIMARY KEY,
[number] int,
[date] date,
);

CREATE TABLE ScheduleItem
(
scheduleItemId INT IDENTITY(1,1) PRIMARY KEY,
scheduleYear INT NOT NULL,
scheduleId INT FOREIGN KEY REFERENCES Schedule(scheduleId),
teacherId INT FOREIGN KEY REFERENCES Teacher(teacherId),
groupId int FOREIGN KEY REFERENCES [Group](groupId),
auditoryId int FOREIGN KEY REFERENCES Auditory(auditoryId),
subjectId int FOREIGN KEY REFERENCES [Subject](subjectId),
);



