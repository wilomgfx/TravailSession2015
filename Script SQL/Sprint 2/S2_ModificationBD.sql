USE H15_PROJET_E03;
GO

CREATE SCHEMA Proprietes;
GO

Create table Proprietes.Photo
(
	PhotoId int NOT NULL,
	TypeFichier nvarchar(4),
	Chemin nvarchar(255),
	ProprieteId int NOT NULL
)
GO


Create table Proprietes.Propriete
(
	ProprieteId int NOT NULL IDENTITY,
	Client nvarchar(50),
	Adresse nvarchar(235),
	Ville nvarchar(50)
)
Go

ALTER TABLE Proprietes.Propriete
	ADD CONSTRAINT PK_Propriete_ProprieteId
	PRIMARY KEY (ProprieteId);
GO

ALTER TABLE Proprietes.Photo
	ADD CONSTRAINT PK_Photo_PhotoId_ProprieteId
	PRIMARY KEY (PhotoId, ProprieteId),
	CONSTRAINT FK_Propriete_ProprieteId
	FOREIGN KEY (ProprieteId)
	REFERENCES Proprietes.Propriete(ProprieteId)
GO	

Create schema Agences;
Go

Create Table Agences.Agent
(
	AgentId int NOT NULL identity,
	Nom nvarchar(50)
)
GO

ALTER TABLE Agences.Agent
ADD CONSTRAINT PK_Agences_Agent_AgentId
PRIMARY KEY (AgentId)
GO

ALTER table Seance.Seance
DROP COLUMN Agent

GO

ALTER table Seance.Seance
ADD AgentId int
GO

Alter table Seance.Seance
Add constraint FK_Seance_AgentId
Foreign Key (AgentId)
REFERENCES Agences.Agent(AgentId)


ALTER table Seance.Seance
ADD ProprieteId int
GO

Alter table Seance.Seance
Add constraint FK_Seance_ProprieteId
Foreign Key (ProprieteId)
REFERENCES Proprietes.Propriete(ProprieteId)

GO


--PHOTOS
GO
ALTER TABLE Proprietes.Photo
ADD photoPrise bit NOT NULL
Go	

ALTER TABLE Proprietes.Photo
ADD CONSTRAINT DF_Photo_PhotoPrise
DEFAULT 'false' FOR photoPrise

GO



