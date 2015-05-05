USE GestionPhotoImmobilier;
GO

IF OBJECT_ID('RDV.trg_RDVConfirme') IS NOT NULL
	DROP TRIGGER RDV.trg_RDVConfirme
GO

-- QUAND LE RENDEZ-VOUS EST PRIS, LE STATUT DE LA SÉANCE DEVIENT CONFIRMÉ

CREATE TRIGGER RDV.trg_RDVConfirme
ON RDV.RDV
AFTER UPDATE
AS
BEGIN
	DECLARE @idSeance int
	DECLARE @statut bit

	SELECT @idSeance = i.SeanceId, @statut = i.Confirmer
	FROM inserted i

	IF(@statut = 0)
	BEGIN
		UPDATE Seance.Seance
		SET Statut = 'Reportee'
		WHERE SeanceId = @idSeance
	END
	IF(@statut = 1)
	BEGIN
		UPDATE Seance.Seance
		SET Statut = 'Confirmee'
		WHERE SeanceId = @idSeance
	END
END
GO
