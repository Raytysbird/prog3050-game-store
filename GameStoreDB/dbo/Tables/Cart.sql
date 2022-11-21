
CREATE TABLE [dbo].[Cart] 
(cart_id int IDENTITY NOT NULL,
credit_card_id int NOT NULL,
total_cost float(15) NULL, 
state_of_order varchar(255) NULL,
user_id nvarchar(450) NOT NULL,
CONSTRAINT [FKUser 75428] FOREIGN KEY ([user_id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
CONSTRAINT [FKCredit 786863] FOREIGN KEY ([credit_card_id]) REFERENCES [dbo].[Credit Card Info] ([credit_card_id]),
PRIMARY KEY (cart_id));