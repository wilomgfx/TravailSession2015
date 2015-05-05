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

IF OBJECT_ID('Seance.trg_RDVReporte') IS NOT NULL
	DROP TRIGGER Seance.trg_RDVReporte
GO

CREATE TRIGGER Seance.trg_RDVReporte
ON Seance.Seance
AFTER UPDATE
AS
BEGIN
	DECLARE @idSeance int
	DECLARE @dateNouvelle DATETIME
	DECLARE @dateVieille DATETIME

	SELECT @idSeance = i.SeanceId, @dateNouvelle = i.DateSeance
	FROM inserted i

	SELECT @dateVieille = d.DateSeance
	FROM deleted d
	
	IF((YEAR(@dateNouvelle) != YEAR(@dateVieille)) OR (MONTH(@dateNouvelle) != MONTH(@dateVieille)) OR (DAY(@dateNouvelle) != DAY(@dateVieille)))
	BEGIN
		DECLARE @hourVieille int = DATEPART(HOUR, @dateVieille)
		DECLARE @minuteVieille int = DATEPART(MINUTE, @dateVieille)
		DECLARE @hourNouvelle int = DATEPART(HOUR, @dateNouvelle)
		DECLARE @minuteNouvelle int = DATEPART(MINUTE, @dateNouvelle)

		IF((@hourVieille != @hourNouvelle) OR (@minuteVieille != @minuteNouvelle))
		BEGIN
			UPDATE Seance.Seance
			SET Statut = 'Reportee'
			WHERE SeanceId = @idSeance
		END
	END

END
GO
