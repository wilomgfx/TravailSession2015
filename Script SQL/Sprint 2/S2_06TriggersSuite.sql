--quand les photos sont prises (statut : réalisée)
If OBJECT_ID('Seance.trg_PhotosPrise') IS NOT NULL DROP TRIGGER Proprietes.trg_PhotosPrise;
Go

CREATE TRIGGER Seance.trg_PhotosPrise
ON Seance.Seance
AFTER UPDATE
AS
BEGIN
	DECLARE @SeanceId as int = (Select SeanceId FROM (Select TOP 1 * FROM inserted ORDER BY SeanceId DESC) AS I);
	DECLARE @photoDisponible as bit = (SELECT photoDisponible FROM inserted)
	DECLARE @oldStatut as nvarchar(50) = (SELECT statut FROM deleted)
	
	IF UPDATE(photoDisponible)
	BEGIN
		IF @photoDisponible = 'true'
		BEGIN
			UPDATE Seance.Seance
			SET Statut='réalisé'
			WHERE SeanceId = @SeanceId;
		END
		ELSE
		BEGIN
			UPDATE Seance.Seance
			SET Statut = @oldStatut
			WHERE SeanceId = @SeanceId
		END
	END
	
  END 
GO

--TEST--
UPDATE Seance.Seance
SET [photoDisponible] = 1
WHERE [SeanceId] = 1009;





--CREATE TRIGGER Proprietes.trg_PhotosPrise
--ON Proprietes.Photo
--AFTER INSERT
--AS
--BEGIN
--	DECLARE @SeanceId int
--	DECLARE @ProprieteId as int = (SELECT ProprieteId FROM inserted)

--	SET @SeanceId = (SELECT SeanceId FROM Seance.Seance WHERE @ProprieteId = ProprieteId) 

--	UPDATE Seance.Seance
--	SET Statut='Réalisée'
--	WHERE SeanceId = @SeanceId;

--  END 
--GO
--TESTS


--quand les photos sont disponibles pour l’agent (statut : livrée)
If OBJECT_ID('Proprietes.trg_PhotosLivree') IS NOT NULL DROP TRIGGER Proprietes.trg_PhotosLivree;
Go


CREATE TRIGGER Proprietes.trg_PhotosLivree
ON Proprietes.Photo
AFTER INSERT
AS
BEGIN
	DECLARE @SeanceId int
	DECLARE @ProprieteId as int = (SELECT ProprieteId FROM inserted)

	SET @SeanceId = (SELECT SeanceId FROM Seance.Seance WHERE @ProprieteId = ProprieteId) 

	UPDATE Seance.Seance
	SET Statut='Livrée'
	WHERE SeanceId = @SeanceId;

  END 
GO
--TEST--
--INSERT INTO [Proprietes].[Photo]
--VALUES('jpeg','C:\\Espace Labo',1003)



--ON Seance.Seance
--AFTER UPDATE
--AS
--BEGIN
--	DECLARE @SeanceId as int = (Select SeanceId FROM (Select TOP 1 * FROM inserted ORDER BY SeanceId DESC) AS I);
--	DECLARE @photoDisponible as bit = (SELECT photoDisponible FROM inserted)
--	DECLARE @oldStatut as nvarchar(50) = (SELECT statut FROM deleted)
	
--	IF UPDATE(photoDisponible)
--	BEGIN
--		IF @photoDisponible = 'true'
--		BEGIN
--			UPDATE Seance.Seance
--			SET Statut='Livrée'
--			WHERE SeanceId = @SeanceId;
--		END
--		ELSE
--		BEGIN
--			UPDATE Seance.Seance
--			SET Statut = @oldStatut
--			WHERE SeanceId = @SeanceId
--		END
--	END
	
--  END 
--GO

--TESTS
--INSERT INTO Seance.Seance(DateSeance,Photographe,Client,Forfait,Commentaire,AgentId,ProprieteId)
--VALUES('20150508','BoB','Jorge','goodshit',':D',1,1)
--UPDATE Seance.Seance
--SET photosPrise = 'true'
--WHERE SeanceId = 1