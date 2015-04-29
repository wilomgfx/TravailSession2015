USE MonProgramme;
GO
CREATE TABLE Inscriptions.Inscription
(
	InscriptionID int NOT NULL IDENTITY,
	DateInscription datetime not null DEFAULT current_timestamp,
	PeriodeInscription int not null DEFAULT 3,
	ClientID int not null
) ON [PRIMARY];
GO
CREATE TABLE Inscriptions.HistoriqueInscription
(
	HistoriqueInscriptionID int NOT NULL IDENTITY,
	DateInscription datetime not null,
	PeriodeInscription int not null,
	ClientID int not null
) ON [PRIMARY];
GO

CREATE TABLE Visites.Visite
(
	VisiteID int NOT NULL IDENTITY,
	DateVisite datetime not null,
	ClientID int NOT NULL
) ON [PRIMARY];
GO
/*
CREATE TABLE Visites.HistoriqueVisite
(
	HistoriqueVisiteID int NOT NULL IDENTITY,
	DateVisite datetime not null,
	ClientID int NOT NULL
) ON [PRIMARY];
GO*/

CREATE TABLE Clients.Client
(
	ClientID int NOT NULL IDENTITY,
	Prenom varchar(50) NOT NULL,	
	Nom varchar(50) NOT NULL,
	Adresse varchar(50) not null,
	NumTel varchar(11),
	CodePostal varchar(6) not null,
	Poid int not null,
	Age int not null,
	Taille int not null,
	Sexe varchar(15) not null,
	DateMesurePrise Date null DEFAULT current_timestamp,
	CirconferenceBras int null,
	CirconferenceJambe int null,
	CirconferenceAbdominal int null,
) ON [PRIMARY];
GO

CREATE TABLE Clients.HistoriqueCirconferenceMembre
(
	HistoriqueCirconferenceMembreID int NOT NULL IDENTITY,
	Poid int not null,
	Age int not null,
	Taille int not null,	
	CirconferenceBras int null,
	CirconferenceJambe int null,
	CirconferenceAbdominal int null,
	DateMesurePrise datetime not null,
	ClientID int NOT NULL
) ON [PRIMARY];
GO

CREATE TABLE Rencontres.Rencontre
(
	RencontreID int NOT NULL IDENTITY,
	DateRencontre datetime NOT NULL,	
	Commentaire varchar(500) NULL,
	EntraineurID int not null,
	ClientID int not null
) ON [PRIMARY];
GO
/*
CREATE TABLE Rencontres.HistoriqueRencontre
(
	HistoriqueRencontreID int NOT NULL IDENTITY,
	DateRencontre datetime NOT NULL,	
	Commentaire varchar(500) NULL,
	EntraineurID int not null,
	ClientID int not null
) ON [PRIMARY];
GO*/

CREATE TABLE Entraineurs.Entraineur 
(
	EntraineurID int NOT NULL IDENTITY,
	Prenom varchar(50) NOT NULL,	
	Nom varchar(50) NOT NULL,
	Adresse varchar(50) not null,
	NumTel varchar(11) null,
	CodePostal varchar(6) not null,
	DateEmbauche datetime not null,
	DateDepart datetime null,
) ON [PRIMARY];
GO

CREATE TABLE Buts.But 
(
	ButID int NOT NULL IDENTITY,
	But varchar(50) NULL,
	DateDebutBut datetime null,
	DateFinBut datetime null,
	ClientID int not null
) ON [PRIMARY];
GO

CREATE TABLE Buts.HistoriqueBut 
(
	HistoriqueButID int NOT NULL IDENTITY,
	But varchar(50) NULL,
	DateDebut datetime NOT null,
	DateFin datetime NOT null,
	ClientID int not null
) ON [PRIMARY];
GO

CREATE TABLE Programmes.Programme 
(
	ProgrammeID int NOT NULL IDENTITY,	
	DateDebut datetime null,
	DateFin datetime null,
	Frequence int not null,
	ClientID int not null
) ON [PRIMARY];
GO
/*
CREATE TABLE Programmes.SerieExercice
(
	SerieExerciceID int NOT NULL IDENTITY,
	NombreRepetition int not null, 
	TempsRepos int null,
	TempsEchauffement int null,
	TempsRetourAuCalme int null,
	ProgrammeID int null
) ON [PRIMARY];
GO

CREATE TABLE Programmes.Exercice
(
	ExerciceID int NOT NULL IDENTITY,
	Nom varchar(50) not null,
	DescriptionExercice varchar(500) null,
	RegionCorporelleSollicite varchar(50) not null,
	NombreRepetition int not null,
	TempsReposEntreRepetition int null,
	NumMachine int null,
	NombreDeWatt int null,
	Poid int null,
	Vitesse int null,
	SerieExerciceID int not null,
	ButID int not null
) ON [PRIMARY];
GO*/


CREATE TABLE Programmes.Exercice
(
	ExerciceID int NOT NULL IDENTITY,
	Nom varchar(50) not null,
	DescriptionExercice varchar(500) null,
	RegionCorporelleSollicite varchar(50) not null,
	ButID int not null
) ON [PRIMARY];
GO


CREATE TABLE Programmes.SerieExercice
(
	SerieExerciceID int NOT NULL IDENTITY,
	NombreRepetition int not null, 
	TempsReposEntreRepetition int null,
	NumMachine int null,
	NombreDeWatt int null,
	Poid int null,
	Vitesse int null,
	TempsRepos int null,
	TempsEchauffement int null,
	TempsRetourAuCalme int null,
	ExerciceID int,
	SerieID int
) ON [PRIMARY];
GO
CREATE TABLE Programmes.Serie
(
	SerieID int NOT NULL IDENTITY,
	ProgrammeID int null,
	JourSemaine varchar(10)
	
)











/*
CREATE TABLE Programmes.HistoriqueProgramme
(
	HistoriqueProgrammeID int NOT NULL IDENTITY,
	DateDebut datetime not null,
	DateFin datetime not null,
	Frequence int not null,
	HistoriqueButID int null
) ON [PRIMARY];
GO*/
/*
CREATE TABLE Programmes.HistoriqueSerieExercice
(
	HistoriqueSerieExerciceID int NOT NULL IDENTITY,
	NombreRepetition int  null,
	TempsReposEntreExercice int null,
	TempsEchauffement int not null,
	TempsRetourAuCalme int not null

) ON [PRIMARY];
GO*/
/*
CREATE TABLE Programmes.Programme_SerieExercice
(
	Programme_SerieExerciceID int NOT NULL IDENTITY,
	SerieExerciceID int not null,
	ProgrammeID int not null,
) ON [PRIMARY];
GO*/
/*
CREATE TABLE Programmes.HistoriqurProgramme_HistoriqueSerieExerciceProgramme
(
	HistoriqurProgramme_HistoriqueSerieExerciceProgrammeID int NOT NULL IDENTITY,
	HistoriqueProgrammeID int not null,
	HistoriqueSerieExerciceProgramme int not null,
) ON [PRIMARY];
GO*/
