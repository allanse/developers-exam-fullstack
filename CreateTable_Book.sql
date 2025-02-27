-- INSPAND_Exam.dbo.Book definição

-- Drop table

-- DROP TABLE INSPAND_Exam.dbo.Book;

CREATE TABLE INSPAND_Exam.dbo.Books (
	id bigint IDENTITY(1,1) NOT NULL,
	Title varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Author varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Description varchar(1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CreatedDate datetime NULL,
	LastUpdatedDate datetime NULL,	
	CONSTRAINT Book_UNIQUE UNIQUE (Title)
);