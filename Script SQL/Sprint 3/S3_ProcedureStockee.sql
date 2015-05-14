USE H15_PROJET_E03
GO

IF OBJECT_ID('dbo.usp_ProduireRapportAgence') IS NOT NULL
	DROP PROCEDURE dbo.usp_ProduireRapportAgence
GO

CREATE PROCEDURE dbo.usp_ProduireRapportAgence
@agenceId int, @annee int, @mois int
AS
BEGIN
	SELECT Agence, NumTel, [Adresse Agence], AgentId, Agent, SeanceId, DateSeance, [Client Séance], Photographe, Forfait, Prix, Statut, Extras, Adresse, Ville, Propriétaire
	FROM [dbo].[view_VueEnsembleAgence] v
	WHERE AgenceId = @agenceId AND YEAR(DateSeance) = @annee AND MONTH(DateSeance) = @mois
	ORDER BY Agence, Agent, DateSeance DESC, Statut, Ville
END
GO

IF OBJECT_ID('dbo.usp_ProduireRapportAgent') IS NOT NULL
	DROP PROCEDURE dbo.usp_ProduireRapportAgent
GO

CREATE PROCEDURE dbo.usp_ProduireRapportAgent
@agentId int, @annee int
AS
BEGIN
	SELECT DateSeance, [Client Séance], Photographe, Forfait, Statut, Extras, Adresse, Ville, Propriétaire, Prix
	FROM [dbo].[view_VueEnsembleAgence] v
	WHERE AgentId = @agentId AND YEAR(DateSeance) = @annee
	ORDER BY Agent, DateSeance DESC, Statut, Ville
END
GO