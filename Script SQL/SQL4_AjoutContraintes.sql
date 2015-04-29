USE MonProgramme;
GO

ALTER TABLE Inscriptions.Inscription
	ADD CONSTRAINT PK_Inscriptions_Inscription
	PRIMARY KEY (InscriptionID);
GO	

ALTER TABLE Inscriptions.HistoriqueInscription
	ADD CONSTRAINT PK_Inscriptions_HistoriqueInscription
	PRIMARY KEY (HistoriqueInscriptionID);
GO	

ALTER TABLE Clients.Client
	ADD CONSTRAINT PK_Clients_ClientID
	PRIMARY KEY (ClientID);
GO	

ALTER TABLE Clients.HistoriqueCirconferenceMembre
	ADD CONSTRAINT PK_Clients_HistoriqueCirconferenceMembreID
	PRIMARY KEY (HistoriqueCirconferenceMembreID);
GO	

ALTER TABLE Visites.Visite
	ADD CONSTRAINT PK_Visites_VisiteID
	PRIMARY KEY (VisiteID);
GO	

/*
ALTER TABLE Visites.HistoriqueVisite
	ADD CONSTRAINT PK_Visites_HistoriqueVisiteID
	PRIMARY KEY (HistoriqueVisiteID);
GO	*/

ALTER TABLE Rencontres.Rencontre
	ADD CONSTRAINT PK_Rencontres_RencontreID
	PRIMARY KEY (RencontreID);
GO	
/*
ALTER TABLE Rencontres.HistoriqueRencontre
	ADD CONSTRAINT PK_Rencontres_HistoriqueRencontreID
	PRIMARY KEY (HistoriqueRencontreID);	
GO	*/
ALTER TABLE Entraineurs.Entraineur
	ADD CONSTRAINT PK_Entraineurs_EntraineurID
	PRIMARY KEY (EntraineurID);	
GO
ALTER TABLE Buts.But
	ADD CONSTRAINT PK_Buts_ButID
	PRIMARY KEY (ButID);	
GO
ALTER TABLE Buts.HistoriqueBut
	ADD CONSTRAINT PK_Buts_HistoriqueButID
	PRIMARY KEY (HistoriqueButID);	
GO
ALTER TABLE Programmes.Programme
	ADD CONSTRAINT PK_Programmes_ProgrammeID
	PRIMARY KEY (ProgrammeID);	
GO
/*
ALTER TABLE Programmes.Programme_SerieExercice
	ADD CONSTRAINT PK_Programmes_Programme_SerieExerciceID
	PRIMARY KEY (Programme_SerieExerciceID);
GO*/
ALTER TABLE Programmes.SerieExercice
	ADD CONSTRAINT PK_Programmes_SerieExerciceID
	PRIMARY KEY (SerieExerciceID);	
GO
ALTER TABLE Programmes.Exercice
	ADD CONSTRAINT PK_Programmes_ExerciceID
	PRIMARY KEY (ExerciceID);
GO
ALTER TABLE Programmes.Serie
	ADD CONSTRAINT PK_Programmes_SerieID
	PRIMARY KEY (SerieID);
GO
/*
ALTER TABLE Programmes.HistoriqueProgramme
	ADD CONSTRAINT PK_Programmes_HistoriqueProgrammeID
	PRIMARY KEY (HistoriqueProgrammeID);
GO
ALTER TABLE Programmes.HistoriqurProgramme_HistoriqueSerieExerciceProgramme
	ADD CONSTRAINT PK_Programmes_HistoriqurProgramme_HistoriqueSerieExerciceProgrammeID
	PRIMARY KEY (HistoriqurProgramme_HistoriqueSerieExerciceProgrammeID);
GO
ALTER TABLE Programmes.HistoriqueSerieExercice
	ADD CONSTRAINT PK_Programmes_HistoriqueSerieExerciceID
	PRIMARY KEY (HistoriqueSerieExerciceID);
GO*/


ALTER TABLE Inscriptions.Inscription
	ADD CONSTRAINT FK_ProblemesSante_ProblemeSante_ProblemeSanteID
	FOREIGN KEY (ClientID)
	REFERENCES Clients.Client(ClientID);
GO

ALTER TABLE Visites.Visite
	ADD CONSTRAINT FK_Visites_Visite_ClientID
	FOREIGN KEY (ClientID)
	REFERENCES Clients.Client(ClientID);
GO

ALTER TABLE Rencontres.Rencontre
	ADD CONSTRAINT FK_Rencontres_Rencontre_EntraineurID
	FOREIGN KEY (EntraineurID)
	REFERENCES Entraineurs.Entraineur(EntraineurID);
GO
ALTER TABLE Rencontres.Rencontre
	ADD CONSTRAINT FK_Rencontres_Rencontre_ClientID
	FOREIGN KEY (ClientID)
	REFERENCES Clients.Client(ClientID);
GO
ALTER TABLE Buts.But
	ADD CONSTRAINT FK_Buts_But_ClientID
	FOREIGN KEY (ClientID)
	REFERENCES Clients.Client(ClientID);
GO
ALTER TABLE Programmes.Programme
	ADD CONSTRAINT FK_Programmes_Programme_ClientID
	FOREIGN KEY (ClientID)
	REFERENCES Clients.Client(ClientID);
GO
/*
ALTER TABLE Programmes.Exercice
	ADD CONSTRAINT FK_Programmes_Exercice_SerieExerciceID
	FOREIGN KEY (SerieExerciceID)
	REFERENCES Programmes.SerieExercice(SerieExerciceID);
GO*/
/*
ALTER TABLE Programmes.Programme_SerieExercice
	ADD CONSTRAINT FK_Programmes_Programme_SerieExercice_ProgrammeID
	FOREIGN KEY (ProgrammeID)
	REFERENCES Programmes.Programme(ProgrammeID);
GO
ALTER TABLE Programmes.Programme_SerieExercice
	ADD CONSTRAINT FK_Programmes_Programme_SerieExercice_SerieExerciceID
	FOREIGN KEY (SerieExerciceID)
	REFERENCES Programmes.SerieExercice(SerieExerciceID);
GO*/
ALTER TABLE Programmes.SerieExercice
	ADD CONSTRAINT FK_Programmes_SerieExercice_ExerciceID
	FOREIGN KEY (ExerciceID)
	REFERENCES Programmes.Exercice(ExerciceID);
ALTER TABLE Programmes.SerieExercice
	ADD CONSTRAINT FK_Programmes_SerieExercice_SerieID
	FOREIGN KEY (SerieID)
	REFERENCES Programmes.Serie(SerieID);
ALTER TABLE Programmes.Exercice
	ADD CONSTRAINT FK_Programmes_Exercice_ButID
	FOREIGN KEY (ButID)
	REFERENCES Buts.But(ButID);
GO
ALTER TABLE Programmes.Serie
	ADD CONSTRAINT FK_Programmes_Serie_ProgrammeID
	FOREIGN KEY (ProgrammeID)
	REFERENCES Programmes.Programme(ProgrammeID)
GO

/*
ALTER TABLE Programmes.HistoriqueProgramme
	ADD CONSTRAINT FK_Programmes_HistoriqueProgramme_HistoriqueButID
	FOREIGN KEY (HistoriqueButID)
	REFERENCES Buts.HistoriqueBut(HistoriqueButID);
GO*/
/*
ALTER TABLE Programmes.HistoriqurProgramme_HistoriqueSerieExerciceProgramme
	ADD CONSTRAINT FK_Programmes_HistoriqurProgramme_HistoriqueSerieExerciceProgramme_HistoriqueProgrammeID
	FOREIGN KEY (HistoriqueProgrammeID)
	REFERENCES Programmes.HistoriqueProgramme(HistoriqueProgrammeID);
GO
ALTER TABLE Programmes.HistoriqurProgramme_HistoriqueSerieExerciceProgramme
	ADD CONSTRAINT FK_Programmes_HistoriqurProgramme_HistoriqueSerieExerciceProgramme_HistoriqueSerieProgrammeID
	FOREIGN KEY (HistoriqueSerieExerciceProgramme)
	REFERENCES Programmes.HistoriqueSerieExercice(HistoriqueSerieExerciceID);
GO*/

ALTER TABLE Inscriptions.Inscription
	ADD CONSTRAINT FK_Inscriptions_Inscription_ClientID
	FOREIGN KEY (ClientID)
	REFERENCES Clients.Client([ClientID]);
GO

ALTER TABLE [Clients].[Client]
ADD CONSTRAINT CHK_Clients_age CHECK([Age] > 0)
GO

ALTER TABLE [Clients].[Client]
ADD CONSTRAINT CHK_Clients_Poid CHECK(Poid > 0)
GO

ALTER TABLE [Clients].[Client]
ADD CONSTRAINT CHK_Clients_Taille CHECK([Taille] > 0)
GO

ALTER TABLE [Clients].[Client]
ADD CONSTRAINT CHK_Clients_CirconferenceBras CHECK([CirconferenceBras] > 0)
GO

ALTER TABLE [Clients].[Client]
ADD CONSTRAINT CHK_Client_CirconferenceJambe CHECK([CirconferenceJambe] > 0)
GO

ALTER TABLE [Clients].[Client]
ADD CONSTRAINT CHK_Client_CirconferenceAbdominal CHECK([CirconferenceAbdominal] > 0)
GO

ALTER TABLE [Inscriptions].[Inscription]
ADD CONSTRAINT CHK_Inscription_PeriodeInscription CHECK([PeriodeInscription] > 0)
GO

ALTER TABLE [Rencontres].[Rencontre]
ADD CONSTRAINT CHK_Rencontre_DateRencontre CHECK([DateRencontre] >= current_Timestamp )
GO


ALTER TABLE [Entraineurs].[Entraineur]
ADD CONSTRAINT CHK_Entraineur_DateEmbauche CHECK([DateEmbauche] > 0)
GO

ALTER TABLE [Entraineurs].[Entraineur]
ADD CONSTRAINT CHK_Entraineur_DateDepart CHECK([DateEmbauche] > 0)
GO

ALTER TABLE [Buts].[But]
ADD CONSTRAINT CHK_But_DateDebutBut CHECK([DateDebutBut] > 0)
GO

ALTER TABLE [Programmes].[Programme]
ADD CONSTRAINT CHK_Programme_DateDebut CHECK([DateDebut] > 0)
GO

ALTER TABLE [Programmes].[Programme]
ADD CONSTRAINT CHK_Programme_DateFin CHECK([DateFin] > 0)
GO

ALTER TABLE [Programmes].[Programme]
ADD CONSTRAINT CHK_Programme_Frequence CHECK([Frequence] > 0)
GO

ALTER TABLE [Programmes].[SerieExercice]
ADD CONSTRAINT CHK_SerieExercice_NombreRepetition CHECK([NombreRepetition] > 0)
GO

ALTER TABLE [Programmes].[SerieExercice]
ADD CONSTRAINT CHK_SerieExercice_TempsRepos CHECK([TempsRepos] > 0)
GO

ALTER TABLE [Programmes].[SerieExercice]
ADD CONSTRAINT CHK_SerieExercice_TempsEchauffement CHECK([TempsEchauffement] > 0)
GO

ALTER TABLE [Programmes].[SerieExercice]
ADD CONSTRAINT CHK_SerieExercice_TempsRetourAuCalme CHECK([TempsRetourAuCalme] > 0)
GO

ALTER TABLE [Programmes].[SerieExercice]
ADD CONSTRAINT CHK_Exercice_NombreDeWatt CHECK([NombreDeWatt] > 0)
GO

ALTER TABLE [Programmes].[SerieExercice]
ADD CONSTRAINT CHK_Exercice_Poid CHECK([Poid] > 0)
GO

ALTER TABLE [Programmes].[SerieExercice]
ADD CONSTRAINT CHK_Exercice_Vitesse CHECK([Vitesse] > 0)
GO
