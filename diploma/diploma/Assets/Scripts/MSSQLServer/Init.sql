CREATE DATABASE SQL_CONTEST;
go
USE SQL_CONTEST;
go
CREATE LOGIN SQL_CONTEST_USER WITH PASSWORD = 'Password123';
go
CREATE USER SQL_CONTEST_USER FOR LOGIN SQL_CONTEST_USER;
go
GRANT CREATE TABLE TO SQL_CONTEST_USER;
go
