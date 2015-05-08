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
--
ALTER TABLE Proprietes.Propriete
	ADD CONSTRAINT PK_Propriete_ProprieteId
	PRIMARY KEY (ProprieteId);
GO

ALTER TABLE Proprietes.Photo
	ADD CONSTRAINT PK_Photo_PhotoId
	PRIMARY KEY (PhotoId),
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


--Seance Photos
GO
ALTER TABLE Seance.Seance
ADD photoDisponible bit NULL
Go	

ALTER TABLE Seance.Seance
ADD CONSTRAINT DF_Seance_photoDisponible
DEFAULT 'false' FOR photoDisponible

GO











CREATE TABLE Agences.Agence
(
AgenceId int not null identity,
Nom nvarchar(50),
Adresse nvarchar(50),
NumTel nvarchar(12)
)
GO

ALTER TABLE Agences.Agence
ADD CONSTRAINT PK_Agences_AgenceId
PRIMARY KEY (AgenceId)
GO

Create table Seance.Forfait
(
ForfaitId int not null identity,
Nom nvarchar(50),
DescriptionForfait nvarchar(200), 
Prix nvarchar(50)
)
GO

ALTER TABLE Seance.Forfait
ADD CONSTRAINT PK_Seance_Forfait_ForfaitId
PRIMARY KEY (ForfaitId)
GO

ALTER TABLE Agences.Agent
ADD AgenceId int,
CONSTRAINT FK_Agent_AgenceId
FOREIGN KEY (AgenceId)
REFERENCES Agences.Agence(AgenceId)
GO

ALTER TABLE Seance.Seance
DROP COLUMN Forfait
GO

ALTER TABLE Seance.Seance
ADD ForfaitId int,
CONSTRAINT FK_Seance_ForfaitId
FOREIGN KEY (ForfaitId)
REFERENCES Seance.Forfait(ForfaitId)

GO
