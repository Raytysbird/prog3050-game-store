CREATE TABLE Review (
review_id int IDENTITY(1,1) PRIMARY KEY,
asp_user_id NVARCHAR (450) NOT NULL,
title varchar(255) NULL, 
review varchar(255) NULL, 
rating int NULL, 
game_id int NULL, 
isApproved bit NULL,
FOREIGN KEY (asp_user_id) REFERENCES [AspNetUsers](Id),
FOREIGN KEY (game_id) REFERENCES [Game](game_id)
);