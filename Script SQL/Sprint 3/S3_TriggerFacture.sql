USE [H15_PROJET_E03];
GO

If OBJECT_ID('Seance.trg_Facture') IS NOT NULL DROP TRIGGER Seance.trg_Facture;
Go


CREATE TRIGGER Seance.trg_Facture
ON Seance.Seance
AFTER UPDATE
AS
BEGIN
	DECLARE @SeanceId as int = (Select i.SeanceId FROM inserted i);
	DECLARE @factureDisponible as bit = (SELECT Facture FROM inserted)
	DECLARE @oldStatut as nvarchar(50) = (SELECT statut FROM deleted)
	
	IF UPDATE(Facture)
	BEGIN
		IF @factureDisponible = 'true'
		BEGIN
			UPDATE Seance.Seance
			SET Statut='Facturée'
			WHERE SeanceId = @SeanceId;
		END
	END	
  END 
GO