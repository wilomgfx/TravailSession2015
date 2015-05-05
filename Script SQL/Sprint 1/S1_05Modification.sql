USE GestionPhotoImmobilier;
GO

ALTER TABLE Rdv.Rdv
ADD SeanceId int NOT NULL,
CONSTRAINT FK_RDV_SeanceID
FOREIGN KEY (SeanceId)
REFERENCES Seance.Seance(SeanceId)
GO  
