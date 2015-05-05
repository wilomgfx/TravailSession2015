USE GestionPhotoImmobilier;
GO


--quand l’agent demande une séance (statut : demandé)

If OBJECT_ID('Seance.trg_SeanceConfirme') IS NOT NULL DROP TRIGGER Seance.trg_SeanceConfirme;
Go


CREATE TRIGGER Seance.trg_SeanceConfirme
ON Seance.Seance
FOR INSERT
AS
BEGIN
	DECLARE @SeanceId as int = (Select SeanceId FROM (Select TOP 1 * FROM inserted ORDER BY SeanceId DESC) AS I);
	
	UPDATE Seance.Seance
	SET Statut='Demandee'
	WHERE SeanceId = @SeanceId;
	
  END 
GO
