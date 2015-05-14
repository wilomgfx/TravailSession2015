USE H15_PROJET_E03
GO


IF OBJECT_ID('dbo.view_VueEnsembleAgence') IS NOT NULL
DROP VIEW dbo.view_VueEnsembleAgence
GO

CREATE VIEW dbo.view_VueEnsembleAgence
AS
(
	SELECT a.AgenceId, a.Nom AS 'Agence', a.NumTel, a.Adresse AS 'Adresse Agence', ag.AgentId, ag.Nom AS 'Agent', s.SeanceId, s.DateSeance, s.Client AS 'Client Séance', s.Photographe, f.Nom AS 'Forfait', f.Prix, s.Statut, s.Extras, p.ProprieteId, p.Adresse, p.Ville, p.Client AS 'Propriétaire'
	FROM Agences.Agence a
	INNER JOIN Agences.Agent ag
	ON a.AgenceId = ag.AgenceId
	INNER JOIN Seance.Seance s
	ON ag.AgentId = s.AgentId
	INNER JOIN Seance.Forfait f
	ON s.ForfaitId = f.ForfaitId
	INNER JOIN Proprietes.Propriete p
	ON s.ProprieteId = p.ProprieteId
)
GO