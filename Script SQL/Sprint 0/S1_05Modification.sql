USE GestionPhotoImmobilier;
GO

ALTER TABLE Rdv.Rdv
ADD SeanceId int NULL,
CONSTRAINT FK_RDV_SeanceID
FOREIGN KEY (RdvId)
REFERENCES Seance.Seance(SeanceId)
GO  
