CREATE TABLE [dbo].[UserEvent]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	asp_user_id NVARCHAR (450) NOT NULL,
	event_id INT NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	FOREIGN KEY (asp_user_id) REFERENCES [AspNetUsers](Id),
	FOREIGN KEY (event_id) REFERENCES [Events](event_id)

)
