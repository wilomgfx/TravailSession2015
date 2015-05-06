USE GestionPhotoImmobilier;
GO

CREATE SCHEMA Proprietes;
GO

Create table Proprietes.Photo
(
	PhotoId int,
	TypeFichier nvarchar(4),
	Chemin nvarchar(255),
	ProprieteId int
)
GO
Create table Proprietes.Propriete
(
	ProprieteId int,
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
	AgentId int identity,
	Nom nvarchar(50)
)


ALTER table Seance.Seance
DROP COLUMN Agent

GO

ALTER table Seance.Seance
ADD Agent int
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


	



