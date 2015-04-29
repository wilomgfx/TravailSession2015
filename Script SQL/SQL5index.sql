USE [MonProgramme];
GO

CREATE NONCLUSTERED INDEX IX_But_DateDebutBut
On [Buts].[But]
(
	[DateDebutBut]
)
GO
CREATE NONCLUSTERED INDEX IX_But_ClientID
On [Buts].[But]
(
	[ClientID]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueBut_DateDebut
On [Buts].[HistoriqueBut]
(
	[DateDebut]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueBut_DateFin
On [Buts].[HistoriqueBut]
(
	[DateFin]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueBut_Client
On [Buts].[HistoriqueBut]
(
	[ClientID]
)
GO
CREATE NONCLUSTERED INDEX IX_Client_NomPrenom
On [Clients].[Client]
(
	[Nom],
	[Prenom]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueCirconferenceMembre_ClientID
On [Clients].[HistoriqueCirconferenceMembre]
(
	[ClientID]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueCirconferenceMembre_DateMesurePrise
On [Clients].[HistoriqueCirconferenceMembre]
(
	[DateMesurePrise]
)
GO
CREATE NONCLUSTERED INDEX IX_Entraineur_NomPrenom
On [Entraineurs].[Entraineur]
(
	[Nom],
	[Prenom]
)
GO
CREATE NONCLUSTERED INDEX IX_Entraineur_DateEmbauche
On [Entraineurs].[Entraineur]
(
	[DateEmbauche]
)
GO
CREATE NONCLUSTERED INDEX IX_Entraineur_DateDepart
On [Entraineurs].[Entraineur]
(
	[DateDepart]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueInscription_DateInscription
On [Inscriptions].[HistoriqueInscription]
(
	[DateInscription]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueInscription_ClientID
On [Inscriptions].[HistoriqueInscription]
(
	[ClientID]
)
GO
CREATE NONCLUSTERED INDEX Inscription_ClientID
On [Inscriptions].[Inscription]
(
	[ClientID]
)
GO
CREATE NONCLUSTERED INDEX IX_Inscription_DateInscription
On [Inscriptions].[Inscription]
(
	[DateInscription]
)
GO
CREATE NONCLUSTERED INDEX IX_Exercice_Nom
On [Programmes].[Exercice]
(
	[Nom]
)
GO
CREATE NONCLUSTERED INDEX IX_Exercice_RegionCorporelle
On [Programmes].[Exercice]
(
	[RegionCorporelleSollicite]
)
GO
CREATE NONCLUSTERED INDEX IX_SerieExercice_NombreRepetition
On [Programmes].[SerieExercice]
(
	[NombreRepetition]
)
GO
CREATE NONCLUSTERED INDEX IX_SerieExercice_NumMachine
On [Programmes].[SerieExercice]
(
	[NumMachine]
)
GO
CREATE NONCLUSTERED INDEX IX_Exercice_ButID
On [Programmes].[Exercice]
(
	[ButID]
)
GO
/*
CREATE NONCLUSTERED INDEX IX_HistoriqueProgramme_DateDebut
On [Programmes].[HistoriqueProgramme]
(
	[DateDebut]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueProgramme_DateFin
On [Programmes].[HistoriqueProgramme]
(
	[DateFin]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueProgramme_HistoriqueBut
On [Programmes].[HistoriqueProgramme]
(
	[HistoriqueButID]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueProgramme_NombreRepetition
On [Programmes].[HistoriqueSerieExercice]
(
	[NombreRepetition]
)
GO*/
CREATE NONCLUSTERED INDEX IX_Programme_DateDebut
On [Programmes].[Programme]
(
	[DateDebut]
)
GO
CREATE NONCLUSTERED INDEX IX_Programme_DateFin
On [Programmes].[Programme]
(
	[DateFin]
)
GO
CREATE NONCLUSTERED INDEX IX_Programme_ClientID
On [Programmes].[Programme]
(
	[ClientID]
)
GO
CREATE NONCLUSTERED INDEX IX_Serie_ProgrammeID
On [Programmes].[Serie]
(
	[ProgrammeID]
)
GO

/*
CREATE NONCLUSTERED INDEX IX_HistoriqueRencontre_DateRencontre
On [Rencontres].[HistoriqueRencontre]
(
	[DateRencontre]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueRencontre_EntraineurID
On [Rencontres].[HistoriqueRencontre]
(
	[EntraineurID]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueRencontre_ClientID
On [Rencontres].[HistoriqueRencontre]
(
	[ClientID]
)
GO*/
CREATE NONCLUSTERED INDEX IX_Rencontre_DateRencontre
On [Rencontres].[Rencontre]
(
	[DateRencontre]
)
GO
CREATE NONCLUSTERED INDEX IX_Rencontre_Entraineur
On [Rencontres].[Rencontre]
(
	[EntraineurID]
)
GO
CREATE NONCLUSTERED INDEX IX_Rencontre_Client
On [Rencontres].[Rencontre]
(
	[ClientID]
)
GO
/*
CREATE NONCLUSTERED INDEX IX_HistoriqueVisite_DateVisite
On [Visites].[HistoriqueVisite]
(
	[DateVisite]
)
GO
CREATE NONCLUSTERED INDEX IX_HistoriqueVisite_ClientID
On [Visites].[HistoriqueVisite]
(
	[ClientID]
)
GO*/
CREATE NONCLUSTERED INDEX IX_Visite_DateVisite
On [Visites].[Visite]
(
	[DateVisite]
)
GO
CREATE NONCLUSTERED INDEX IX_Visite_Client
On [Visites].[Visite]
(
	[ClientID]
)
GO