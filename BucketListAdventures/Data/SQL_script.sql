CREATE TABLE destinations (
Id INT AUTO_INCREMENT PRIMARY KEY,
name VARCHAR (80),
location VARCHAR (80),
description VARCHAR (255)
);

CREATE TABLE userprofiles (
name longtext,
username varchar(255) NOT NULL,
address longtext,
interests longtext,
PRIMARY KEY (`username`)
);
