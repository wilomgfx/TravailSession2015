USE GestionPhotoImmobilier;
GO

ALTER TABLE Rdv.Rdv
ADD SeanceId int NOT NULL,
CONSTRAINT FK_RDV_SeanceID
FOREIGN KEY (SeanceId)
REFERENCES Seance.Seance(SeanceId)
GO  


Alter table Rdv.Rdv
Alter COLUMN Confirmer bit not null

Go