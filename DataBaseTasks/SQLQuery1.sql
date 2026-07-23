/*
CREATE DATABASE CompanyDB;
GO

USE CompanyDB;
GO

CREATE TABLE Department
(
    Dnumber INT PRIMARY KEY,
    Dname VARCHAR(50) NOT NULL UNIQUE,
    Mgr_ssn CHAR(9),
    Mgr_start_date DATE
); 
CREATE TABLE Employee
(
    Ssn CHAR(9) PRIMARY KEY,
    Fname VARCHAR(20) NOT NULL,
    Minit CHAR(1),
    Lname VARCHAR(20) NOT NULL,
    Bdate DATE,
    Address VARCHAR(100),
    Sex CHAR(1) ,
    Salary DECIMAL(10,2) CHECK (Salary > 0),
    Super_ssn CHAR(9),
    Dno INT NOT NULL
); 

CREATE TABLE Dept_Locations
(
    Dnumber INT,
    Dlocation VARCHAR(50),

    PRIMARY KEY (Dnumber, Dlocation)
); 
CREATE TABLE Project
(
    Pnumber INT PRIMARY KEY,
    Pname VARCHAR(50) NOT NULL UNIQUE,
    Plocation VARCHAR(50),
    Dnum INT NOT NULL
); 
CREATE TABLE Works_On
(
    Essn CHAR(9),
    Pno INT,
    Hours DECIMAL(4,1),

    PRIMARY KEY (Essn, Pno)
); 
CREATE TABLE Dependent
(
    Essn CHAR(9),
    Dependent_name VARCHAR(30),
    Sex CHAR(1),
    Bdate DATE,
    Relationship VARCHAR(20),

    PRIMARY KEY (Essn, Dependent_name)
); */

ALTER TABLE Employee
ADD CONSTRAINT FK_Employee_Department
FOREIGN KEY (Dno)
REFERENCES Department(Dnumber); 

ALTER TABLE Employee
ADD CONSTRAINT FK_Employee_Supervisor
FOREIGN KEY (Super_ssn)
REFERENCES Employee(Ssn); 

ALTER TABLE Department
ADD CONSTRAINT FK_Department_Manager
FOREIGN KEY (Mgr_ssn)
REFERENCES Employee(Ssn);

ALTER TABLE Dept_Locations
ADD CONSTRAINT FK_Location_Department
FOREIGN KEY (Dnumber)
REFERENCES Department(Dnumber); 

ALTER TABLE Project
ADD CONSTRAINT FK_Project_Department
FOREIGN KEY (Dnum)
REFERENCES Department(Dnumber); 

ALTER TABLE Works_On
ADD CONSTRAINT FK_WorksOn_Employee
FOREIGN KEY (Essn)
REFERENCES Employee(Ssn); 

ALTER TABLE Works_On
ADD CONSTRAINT FK_WorksOn_Project
FOREIGN KEY (Pno)
REFERENCES Project(Pnumber); 

ALTER TABLE Dependent
ADD CONSTRAINT FK_Dependent_Employee
FOREIGN KEY (Essn)
REFERENCES Employee(Ssn); 
