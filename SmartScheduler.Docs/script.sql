SET NOCOUNT ON
CREATE TABLE TempMaleFirstNames
(
	ID INT IDENTITY(1,1),
	Name VARCHAR(50)
)

CREATE TABLE TempMaleLastNames
(
	ID INT IDENTITY(1,1),
	Name VARCHAR(50)
)

CREATE TABLE TempMaleMiddleNames
(
	ID INT IDENTITY(1,1),
	Name VARCHAR(50)
)

CREATE TABLE TempFemaleFirstNames
(
	ID INT IDENTITY(1,1),
	Name VARCHAR(50)
)

CREATE TABLE TempFemaleLastNames
(
	ID INT IDENTITY(1,1),
	Name VARCHAR(50)
)

CREATE TABLE TempFemaleMiddleNames
(
	ID INT IDENTITY(1,1),
	Name VARCHAR(50)
)

INSERT INTO TempMaleFirstNames(Name)
VALUES
('�����'),
('�����'),
('�������'),
('���������'),
('���'),
('������'),
('�����'),
('���'),
('�����'),
('�����'),
('������'),
('������'),
('�����'),
('����'),
('������'),
('�����'),
('�����'),
('���������'),
('������'),
('���������')

INSERT INTO TempFemaleFirstNames(Name)
VALUES
('����������'),
('������'),
('�����'),
('������'),
('��������'),
('�����'),
('���������'),
('���������'),
('������'),
('�������'),
('�����'),
('����'),
('����'),
('������'),
('������'),
('�����'),
('������'),
('����'),
('������'),
('���')

INSERT INTO TempMaleLastNames(Name)
VALUES
('�����'),
('���������'),
('��������'),
('��������'),
('������'),
('���������'),
('���������'),
('��������'),
('�����'),
('������'),
('����������'),
('�������'),
('�������'),
('�����'),
('�������'),
('�����'),
('������'),
('�������'),
('����'),
('����������')

INSERT INTO TempFemaleLastNames(Name)
VALUES
('�������'),
('��������'),
('������'),
('����������'),
('������������'),
('������'),
('�����'),
('����������'),
('���������'),
('�����'),
('���������'),
('�����'),
('���������'),
('���������'),
('�����'),
('��������'),
('�������'),
('�����'),
('��������'),
('���������')


INSERT INTO TempMaleMiddleNames(Name)
VALUES
('���������'),
('���������'),
('����������'),
('�������������'),
('�������'),
('���������'),
('��������'),
('����������'),
('���������'),
('���������'),
('����������'),
('���������'),
('��������'),
('��������'),
('�����������'),
('���������'),
('���������'),
('�������������'),
('���������'),
('�������������')


INSERT INTO TempFemaleMiddleNames(Name)
VALUES
('���㳿���'),
('��������'),
('���������'),
('������������'),
('�������'),
('���������'),
('��������'),
('��������'),
('��������'),
('��������'),
('��������'),
('��������'),
('�������'),
('�������'),
('���������'),
('�������'),
('��������'),
('������������'),
('��������'),
('������������')
--SELECT * FROM TempFirstNames
--SELECT * FROM TempLastNames
--SELECT * FROM TempMiddleNames

WHILE(((SELECT (MAX(Student.studentId) - MIN(Student.studentId))
		FROM Student)) < 5000) OR ((SELECT MAX(Student.studentId)
		FROM Student) IS NULL)
BEGIN
	IF (CONVERT(int, FLOOR(RAND() * 10)) % 2 = 0)
		INSERT INTO Student(name, surname, middlename, enteringYear)
		VALUES
		(
			(SELECT TOP 1 Name
			FROM TempMaleFirstNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			(SELECT TOP 1 Name
			FROM TempMaleLastNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			(SELECT TOP 1 Name
			FROM TempMaleMiddleNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			CONVERT(int, FLOOR(RAND() * 10000)) % 20 + 1996
		)
	ELSE
		INSERT INTO Student(name, surname, middlename, enteringYear)
		VALUES
		(
			(SELECT TOP 1 Name
			FROM TempFemaleFirstNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			(SELECT TOP 1 Name
			FROM TempFemaleLastNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			(SELECT TOP 1 Name
			FROM TempFemaleMiddleNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			CONVERT(int, FLOOR(RAND() * 10000)) % 20 + 1996
		)
	INSERT INTO [USER](login, password, regDate, lastActivityDate)
	SELECT TOP 1 'user' + CONVERT(VARCHAR(50), studentId), 
				 CONVERT(VARCHAR(15), studentId) + CONVERT(VARCHAR(15), studentId),
				 DATEADD(YEAR, -1 * (2016 - enteringYear), '2016-09-01'), 
				 SYSDATETIME()
	FROM Student
	ORDER BY studentId DESC
	UPDATE Student
	Set Student.userId = (SELECT TOP 1 userId
						 FROM [User]
						 ORDER BY userID DESC)
	WHERE studentId = (SELECT MAX(studentId) FROM Student)
END

--DATEADD(DAY, ABS(CHECKSUM(NEWID()) % 3650), '2000-01-01')

INSERT INTO [USER](login, password, regDate, lastActivityDate)
VALUES
(
	'admin',
	'admin',
	'1996-01-01',
	SYSDATETIME()
)

INSERT INTO Administator(name, surname, userId)
VALUES
(
	'Major',
	'Pupok',
	(SELECT TOP 1 userId from [USER] ORDER BY userId DESC)
)

INSERT INTO Rank(rank)
VALUES
('������'),
('��������'),
('������� ��������'),
('��������'),
('��������')

WHILE(((SELECT (MAX(Teacher.teacherId) - MIN(Teacher.teacherId))
		FROM Teacher)) < 50) OR ((SELECT MAX(Teacher.teacherId)
		FROM Teacher) IS NULL)
BEGIN
	IF (CONVERT(int, FLOOR(RAND() * 10)) % 2 = 0)
		INSERT INTO Teacher(name, surname, middlename, enteringYear, rankid)
		VALUES
		(
			(SELECT TOP 1 Name
			FROM TempMaleFirstNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			(SELECT TOP 1 Name
			FROM TempMaleLastNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			(SELECT TOP 1 Name
			FROM TempMaleMiddleNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			CONVERT(int, FLOOR(RAND() * 10000)) % 20 + 1996,
			(SELECT TOP 1 rankid
			FROM Rank
			WHERE rankid = CONVERT(int, FLOOR(RAND() * 10))%(SELECT COUNT(rankid) FROM Rank) + 
															(SELECT MIN(rankid) from RANK))
		)
	ELSE
		INSERT INTO Teacher(name, surname, middlename, enteringYear, rankid)
		VALUES
		(
			(SELECT TOP 1 Name
			FROM TempFemaleFirstNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			(SELECT TOP 1 Name
			FROM TempFemaleLastNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			(SELECT TOP 1 Name
			FROM TempFemaleMiddleNames
			WHERE ID = CONVERT(int, FLOOR(RAND() * 100))%20 + 1),
			CONVERT(int, FLOOR(RAND() * 10000)) % 20 + 1996,
			(SELECT TOP 1 rankid
			FROM Rank
			WHERE rankid = CONVERT(int, FLOOR(RAND() * 10))%(SELECT COUNT(rankid) FROM Rank) + 
															(SELECT MIN(rankid) from RANK))
		)
	INSERT INTO [USER](login, password, regDate, lastActivityDate)
	SELECT TOP 1 'teacher' + CONVERT(VARCHAR(50), teacherid), 
				  CONVERT(VARCHAR(15), teacherId) + CONVERT(VARCHAR(15), teacherId), 
				  DATEADD(YEAR, -1 * (2016 - enteringYear) , '2016-09-01'),
				  SYSDATETIME()
	FROM Teacher
	ORDER BY teacherId DESC
	UPDATE Teacher
	Set userId = (SELECT TOP 1 userId
						 FROM [User]
						 ORDER BY userID DESC)
	WHERE teacherId = (SELECT MAX(teacherId) FROM Teacher)
END

INSERT INTO [Group](name)
VALUES
('ϲ-11'),
('ϲ-21'),
('ϲ-31'),
('ϲ-41'),
('ϲ-12'),
('ϲ-22'),
('ϲ-32'),
('ϲ-42'),
('ϲ-13'),
('ϲ-23'),
('ϲ-33'),
('ϲ-43'),
('ϲ-14'),
('ϲ-24'),
('ϲ-34'),
('ϲ-44')

DECLARE @COUNTER INT
SET @COUNTER = (SELECT MIN(studentid) FROM Student)

WHILE ((SELECT COUNT(studentsInGroupsId) FROM StudentsInGroups) < (SELECT COUNT(studentId) FROM Student)
		OR (SELECT COUNT(studentsInGroupsId) FROM StudentsInGroups) IS NULL)
BEGIN
	INSERT INTO StudentsInGroups(groupId, studentId)
	VALUES
	(
		CONVERT(int, FLOOR(RAND() * 2000))%(SELECT COUNT(groupId) FROM [Group]) + 
										  (SELECT MIN(groupId) FROM [Group]),
		@COUNTER
	)
	SET @COUNTER = @COUNTER + 1
END

SET @COUNTER = 1101
WHILE (@COUNTER < 5521)
BEGIN
	INSERT INTO Auditory(number)
	VALUES
	(@COUNTER)
	SET @COUNTER = @COUNTER + 1
	IF (RIGHT(@COUNTER, 2) > 20)
	BEGIN
		SET @COUNTER = @COUNTER - 20 + 100 
	END

	IF (LEFT(RIGHT(@COUNTER, 3), 1) > 5)
	BEGIN
		SET @COUNTER = @COUNTER - 500 + 1000
	END
END

INSERT INTO Subject(name, credits)
VALUES
('Գ����� ���������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('��������� ���� (�� ���������� ������������)', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('��''�����-��������� �������������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('�������� ����', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('������� ������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('����� �� ������� ����������� ������������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('Գ����', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('���������� ����''������� �����]', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('�������� ���������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('������-������� �������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('������������� � ��������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('�������� ��������� �������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('������� �������� ���������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('���������� ��������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('������ ���������� �������������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('������������� ������������� ������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('������ �������� �������� ����������� ������������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('������������� ��� �������� ��������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('������������� ��� �������� ��������', CONVERT(int, FLOOR(RAND() * 10))%6 + 1),
('������ ������� �����', CONVERT(int, FLOOR(RAND() * 10))%6 + 1)

DROP TABLE TempMaleFirstNames
DROP TABLE TempMaleLastNames
DROP TABLE TempMaleMiddleNames

DROP TABLE TempFemaleFirstNames
DROP TABLE TempFemaleLastNames
DROP TABLE TempFemaleMiddleNames

SELECT TOP 20 * FROM [User]
WHERE [login] LIKE 'user%'
SELECT TOP 20 * FROM Student
SELECT * FROM [Group]
SELECT TOP 20 * FROM StudentsInGroups
SELECT TOP 20 * FROM Administator
SELECT TOP 20 * FROM Rank
SELECT TOP 20 * FROM Teacher
SELECT TOP 20 * FROM [User]
WHERE [login] LIKE 'teacher%'
SELECT TOP 20 * FROM Subject
SELECT TOP 50 * FROM Auditory


--DELETE FROM StudentsInGroups
--DELETE FROM Student
--DELETE FROM Administator
--DELETE FROM Teacher
--DELETE FROM Rank
--DELETE FROM [User]
--DELETE FROM [Group]
--DELETE FROM Auditory
--DELETE FROM Subject

SET NOCOUNT OFF