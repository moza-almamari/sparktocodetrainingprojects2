/*
CREATE DATABASE CompanyDB;
GO

USE CompanyDB;
GO
*/

CREATE TABLE Department
(
    Dnumber INT PRIMARY KEY,
    Dname VARCHAR(50) NOT NULL UNIQUE,
    Mgr_ssn CHAR(9),
    Mgr_start_date DATE
);
