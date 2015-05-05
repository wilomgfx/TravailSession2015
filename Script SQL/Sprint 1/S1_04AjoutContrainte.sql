Use GestionPhotoImmobilier;
Go

ALTER TABLE Seance.Seance
	ADD CONSTRAINT PK_Seance_SeanceId
	PRIMARY KEY (SeanceID);
GO	

ALTER TABLE Rdv.Rdv
	ADD CONSTRAINT PK_Rdv_RdvId
	PRIMARY KEY (RdvId);
GO	