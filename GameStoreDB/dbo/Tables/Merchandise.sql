CREATE TABLE Merchandise 
(merchandise_id int IDENTITY NOT NULL,
game_id INT NULL,
name varchar(255) NOT NULL, 
description varchar(255) NULL, 
price float(10) NOT NULL,
PRIMARY KEY (merchandise_id),
FOREIGN KEY (game_id) REFERENCES [Game]([game_id])
);
