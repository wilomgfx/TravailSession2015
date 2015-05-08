USE H15_PROJET_E03;
GO

IF OBJECT_ID('Rdv.trg_RDVConfirme') IS NOT NULL
	DROP TRIGGER Rdv.trg_RDVConfirme
GO

-- QUAND LE RENDEZ-VOUS EST PRIS, LE STATUT DE LA SÉANCE DEVIENT CONFIRMÉ

CREATE TRIGGER Rdv.trg_RDVConfirme
ON Rdv.Rdv
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
		SET Statut = 'Reportée'
		WHERE SeanceId = @idSeance
	END
	IF(@statut = 1)
	BEGIN
		UPDATE Seance.Seance
		SET Statut = 'Confirmée'
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
			SET Statut = 'Reportée'
			WHERE SeanceId = @idSeance
		END
	END

END
GO

--quand l’agent demande une séance (statut : demandé)

If OBJECT_ID('Seance.trg_SeanceDemandee') IS NOT NULL DROP TRIGGER Seance.trg_SeanceDemandee;
Go


CREATE TRIGGER Seance.trg_SeanceDemandee
ON Seance.Seance
FOR INSERT
AS
BEGIN
	DECLARE @SeanceId as int = (Select SeanceId FROM (Select TOP 1 * FROM inserted ORDER BY SeanceId DESC) AS I);
	
	UPDATE Seance.Seance
	SET Statut='Demandée'
	WHERE SeanceId = @SeanceId;
	
  END 
GO