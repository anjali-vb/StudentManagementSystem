


CREATE DATABASE StudentDB;
USE StudentDB;



--  Table  Students


CREATE TABLE dbo.Students
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RollNumber INT NOT NULL UNIQUE,
    Name NVARCHAR(30) NOT NULL,
    Maths INT NOT NULL CHECK (Maths BETWEEN 1 AND 100),    ---here the range between marks are given 
    Physics INT NOT NULL CHECK (Physics BETWEEN 1 AND 100),
    Chemistry INT NOT NULL CHECK (Chemistry BETWEEN 1 AND 100),
    English INT NOT NULL CHECK (English BETWEEN 1 AND 100),
    Programming INT NOT NULL CHECK (Programming BETWEEN 1 AND 100)
);


-- Table  Users



CREATE TABLE dbo.Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(200) NOT NULL, -- For demo: store plain password
    Role NVARCHAR(50) NOT NULL, -- here roll is given because an invigilator is looking the codes of each student or Student
    StudentRollNumber INT NULL,
    FOREIGN KEY (StudentRollNumber) REFERENCES dbo.Students(RollNumber)
);

----inserting values to students table
INSERT INTO dbo.Students (RollNumber, Name, Maths, Physics, Chemistry, English, Programming)
VALUES
(1, 'Anjitha Unni', 85, 78, 82, 90, 88),
(2, 'Meenakshi Anoop', 65, 72, 70, 68, 75),
(3, 'Chythra Raj', 92, 89, 94, 91, 95),
(4, 'Aiswarya VK', 75, 80, 77, 70, 82);

--Inserting to user table

INSERT INTO dbo.Users (Username, Password, Role, StudentRollNumber)
VALUES
('admin', 'admin123', 'Invigilator', NULL),
('Anjitha', 'anjitha123', 'Student', 1),
('Meenakshi Anoop', 'meenu123', 'Student', 2),
('Chythra Raj', 'chythra123', 'Student', 3),
('Aiswarya VK', 'aiswarya123', 'Student', 4);



-- stored procedures for students CRUD operations

-- Add Student

CREATE PROCEDURE dbo.sp_AddStudent
    @RollNumber INT,
    @Name NVARCHAR(30),
    @Maths INT,
    @Physics INT,
    @Chemistry INT,
    @English INT,
    @Programming INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Students (RollNumber, Name, Maths, Physics, Chemistry, English, Programming)
    VALUES (@RollNumber, @Name, @Maths, @Physics, @Chemistry, @English, @Programming);
END


-- Get All Students

CREATE PROCEDURE dbo.sp_GetAllStudents
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, RollNumber, Name, Maths, Physics, Chemistry, English, Programming
    FROM dbo.Students
    ORDER BY RollNumber ASC;
END


-- Get Student by Roll

CREATE PROCEDURE dbo.sp_GetStudentByRoll
    @RollNumber INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM dbo.Students
    WHERE RollNumber = @RollNumber;
END


-- Update Student

CREATE PROCEDURE dbo.sp_UpdateStudent
    @RollNumber INT,
    @Name NVARCHAR(30),
    @Maths INT,
    @Physics INT,
    @Chemistry INT,
    @English INT,
    @Programming INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Students
    SET Name = @Name,
        Maths = @Maths,
        Physics = @Physics,
        Chemistry = @Chemistry,
        English = @English,
        Programming = @Programming
    WHERE RollNumber = @RollNumber;
END


-- Delete Student

CREATE PROCEDURE dbo.sp_DeleteStudent
    @RollNumber INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Students WHERE RollNumber = @RollNumber;
END


-- Get Maximum Roll Number

CREATE PROCEDURE dbo.sp_GetMaxRollNumber
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MAX(RollNumber) AS MaxRoll FROM dbo.Students;
END



-- sp for users (Login)


CREATE PROCEDURE dbo.sp_GetUser
    @Username NVARCHAR(100),
    @Password NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Username, Password, Role, StudentRollNumber
    FROM dbo.Users
    WHERE Username = @Username AND Password = @Password;
END



























