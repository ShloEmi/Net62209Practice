--
    -- File generated with SQLiteStudio v3.3.3 on Wed Sep 28 17:26:18 2022
        --
    -- Text encoding used: System
    --
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: ItemData
CREATE TABLE ITEM_DATA (
    ID 						INTEGER  		PRIMARY KEY AUTOINCREMENT UNIQUE, 
    NAME 					STRING, 
    DESCRIPTION 			STRING, 
    CATEGORY_ID 			INTEGER, 
    DATE_ADDED 				DATETIME  		DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE ITEM_CATEGORY (
    ID 						INTEGER  		PRIMARY KEY AUTOINCREMENT UNIQUE, 
    NAME 					STRING, 
    DESCRIPTION 			STRING
);


COMMIT TRANSACTION;
PRAGMA foreign_keys = on;


