--Please execute the below scripts as part of userInterests table creation

ALTER TABLE userprofiles ADD `AirLineCode` varchar(10) NOT NULL;

UPDATE userprofiles
SET airlinecode = 'STL';
