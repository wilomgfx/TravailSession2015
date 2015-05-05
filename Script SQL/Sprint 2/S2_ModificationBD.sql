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

Create table Proprietes.Propriete
(
	ProprieteId int,
	Client nvarchar(50),
	Adresse nvarchar(235),
	Ville nvarchar(50)
)


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



	



