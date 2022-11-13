CREATE TABLE Events (event_id int IDENTITY NOT NULL, name varchar(255) NOT NULL, description varchar(255) NULL, start_date DATETIME NULL, end_date DATETIME NULL, PRIMARY KEY (event_id));
