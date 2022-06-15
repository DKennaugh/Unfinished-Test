CREATE TABLE [dbo].[Gear_Types]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] VARCHAR NOT NULL
)

CREATE TABLE [dbo].[Engine_Types]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] VARCHAR NOT NULL
)

CREATE TABLE [dbo].[Body_Types]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] VARCHAR NOT NULL
)

CREATE TABLE [dbo].[Cars]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] VARCHAR NOT NULL, 
	[Body_Type] INT FOREIGN KEY REFERENCES Body_Types(Id) NOT NULL,
	[Engine_Type] INT FOREIGN KEY REFERENCES Engine_Types(Id),
	[Gear_Type] INT FOREIGN KEY REFERENCES Gear_Types(Id) NOT NULL,
	[Price] FLOAT NOT NULL
);