USE H15_PROJET_E03;
GO

Create table Seance.Seance
(
	SeanceId int identity,
	DateSeance datetime,
	Agent nvarchar(50),
	Photographe nvarchar(50),
	Client nvarchar(50),
	Forfait nvarchar(50),
	Commentaire nvarchar(375),
	Statut nvarchar(50)
)


Create table Rdv.Rdv
(
	RdvId int identity,
	Confirmer bit not null,
	Client nvarchar(50),
	Photographe nvarchar(50)
)


