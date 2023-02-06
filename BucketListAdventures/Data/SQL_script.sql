--Please execute the below scripts as part of destinations and userprofiles table creation

CREATE TABLE destinations (
Id INT AUTO_INCREMENT PRIMARY KEY,
name VARCHAR (300),
location VARCHAR (300),
description VARCHAR (3000)
);

CREATE TABLE userprofiles (
name longtext,
username varchar(255) NOT NULL,
address longtext,
interests longtext,
PRIMARY KEY (`username`)
);
