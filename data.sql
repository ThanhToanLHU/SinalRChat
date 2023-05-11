CREATE DATABASE Chat2Fun;
use Chat2Fun;

CREATE TABLE UserData
(
  UID CHAR(8) NOT NULL,
  Email VARCHAR(50) NOT NULL,
  Username VARCHAR(50) NOT NULL,
  Displayname VARCHAR(50) NOT NULL,
  Password CHAR(16) NOT NULL,
  Avatar VARCHAR(max),
  PRIMARY KEY (UID)
);

CREATE TABLE Messages (
    MessageID INT PRIMARY KEY,
    GroupID INT,
    UID CHAR(8),
    MessageText NVARCHAR(MAX) NOT NULL,
    SentDateTime DATETIME,
    FOREIGN KEY (UID) REFERENCES UserData(UID)
);

insert into Messages (MessageID,UID,MessageText) values(1,6843775,'a')
select * from Messages
drop table Messages
drop table UserData
SELECT * FROM Userdata