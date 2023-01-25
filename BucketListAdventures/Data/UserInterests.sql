--Please execute the below scripts as part of userInterests table creation

ALTER TABLE userprofiles DROP COLUMN interests;

TRUNCATE `bucket_list_adventures`.`userprofiles`;

CREATE TABLE userinterests (
id int NOT NULL AUTO_INCREMENT,
interest longtext NOT NULL,
`userprofileusername` varchar(255) NOT NULL,
PRIMARY KEY (`id`),
CONSTRAINT `FK_UserInterests_UserProfiles_UserProfileUserName` FOREIGN KEY (`userprofileusername`) REFERENCES `userprofiles` (`username`) ON DELETE CASCADE
)
