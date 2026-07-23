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
); */
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
