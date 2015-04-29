USE [MonProgramme];
GO
INSERT INTO [Clients].[Client] (Prenom, Nom, Adresse, NumTel, CodePostal, Poid, Age, Taille, Sexe) VALUES ('Rejean', 'LeGrand','44 DesPoneyx','4504419985','J3V5W4','120', '30','180','M');
GO
INSERT INTO [Entraineurs].[Entraineur](Prenom, Nom, Adresse, NumTel, CodePostal, DateEmbauche, DateDepart) VALUES('Jean', 'Traine','69 Sainte-Catherine Laval','5149874561', 'Z9C7A3',CURRENT_TIMESTAMP, null);
GO
INSERT INTO [Rencontres].[Rencontre] (DateRencontre, Commentaire, EntraineurID, ClientID) VALUES(CURRENT_TIMESTAMP,null,1,1);
GO
INSERT INTO [Programmes].[Programme] VALUES(CURRENT_TIMESTAMP,'20160202',3,1);
/*
INSERT INTO [Programmes].[SerieExercice](NombreRepetition, TempsRepos, TempsEchauffement, TempsRetourAuCalme, ProgrammeID) VALUES(10,30,15,15,1);

INSERT INTO [Buts].[But] VALUES('Perdre du poid', CURRENT_TIMESTAMP, null, 1);

INSERT INTO [Programmes].[Exercice] (Nom, DescriptionExercice, RegionCorporelleSollicite, NombreRepetition, TempsReposEntreRepetition, NumMachine, NombreDeWatt, Poid, Vitesse, SerieExerciceID, ButID)
VALUES('Pompe',null, 'pectoraux', 50, 2, null, null, null, null, 1, 1);*/
INSERT INTO [Buts].[But] VALUES('Perdre du poid', CURRENT_TIMESTAMP, null, 1);
INSERT INTO [Programmes].[Serie] (ProgrammeID, JourSemaine) VALUES (1,'Jeudi');
INSERT INTO [Programmes].[Exercice] (Nom, DescriptionExercice, RegionCorporelleSollicite, ButID) VALUES ('Pompe',null,'Pectoraux',1);
INSERT INTO [Programmes].[SerieExercice] (NombreRepetition, TempsReposEntreRepetition, NumMachine, NombreDeWatt, Poid, Vitesse, TempsRepos, TempsEchauffement, TempsRetourAuCalme, ExerciceID, SerieID)
VALUES(3,10,null,null,null,null,10,10,10,1,1)


INSERT INTO [Visites].[Visite] VALUES(CURRENT_TIMESTAMP, 1);

/*
1) En tant que client   je veux avoir accès à mes séries d’exercices. 
	-Un programme doit comporter des séries d’exercices*/

	/*
	SELECT [SerieExerciceID], [NombreRepetition],[TempsRepos],[TempsEchauffement],[TempsRetourAuCalme]
	FROM [Programmes].[SerieExercice] se
	INNER JOIN [Programmes].[Programme] p
	ON se.[ProgrammeID] = p.[ProgrammeID]
	INNER JOIN [Clients].[Client] c
	ON c.[ClientID] = p.[ClientID]*/



	IF OBJECT_ID('dbo.view_ProgrammeClient') IS NOT NULL DROP VIEW dbo.view_ProgrammeClient
	GO
	create view dbo.view_ProgrammeClient
	AS
	SELECT se.SerieExerciceID, NombreRepetition, TempsReposEntreRepetition, NumMachine, NombreDeWatt, Poid, Vitesse, TempsRepos, TempsEchauffement, TempsRetourAuCalme, ExerciceID, ClientID, s.SerieID
	FROM [Programmes].[SerieExercice] se
	INNER JOIN [Programmes].[Serie] s
	ON se.[SerieID] = s.[SerieID]
	INNER JOIN [Programmes].[Programme] p
	ON s.[ProgrammeID] = p.[ProgrammeID]
	GO
	IF OBJECT_ID('dbo.proc_SerieExercice') IS NOT NULL DROP PROCEDURE dbo.proc_SerieExercice

	GO
	CREATE PROCEDURE dbo.proc_SerieExercice
	@ClientID int
	AS
	BEGIN
		
		SELECT se.SerieExerciceID, NombreRepetition, TempsReposEntreRepetition, NumMachine, NombreDeWatt, Poid, Vitesse, TempsRepos, TempsEchauffement, TempsRetourAuCalme, ExerciceID, SerieID
	FROM dbo.view_ProgrammeClient se	
	WHERE [ClientID] = @ClientID
	END
	GO
	
	EXEC dbo.proc_SerieExercice @ClientID =1;

/*
2) En tant que client  je veux  avoir accès facilement à mon programme.
	-Un client doit avoir un programme*/
	GO
	CREATE PROCEDURE dbo.proc_Programme
	@ClientID int
	AS
	BEGIN

	SELECT [ProgrammeID], [DateDebut], [DateFin], [Frequence]
	FROM [Programmes].[Programme] p
	INNER JOIN [Clients].[Client] c
	ON c.[ClientID] = p.[ClientID]
	WHERE c.[ClientID] = @ClientID
	END
	GO
	
	EXEC dbo.proc_Programme @ClientID =1;

/*
3) En tant que client  je veux pouvoir voir mon but afin de rester motivé.
	-Un client doit avoir un but*/

	GO
	CREATE PROCEDURE dbo.But
	@ClientID int
	AS
	BEGIN

	SELECT [ButID], [But], [DateDebutBut], [DateFinBut]
	FROM [Buts].[But] b
	INNER JOIN [Clients].[Client] c
	ON b.[ClientID] = c.[ClientID]
	WHERE c.[ClientID] = 1

	END
	GO
	
	EXEC dbo.But @ClientID =1;
	GO

	/*
4) En tant que client  je veux  pouvoir m’inscrire.
	-Un client doit pouvoir avoir une inscription*/

	INSERT INTO [Inscriptions].[Inscription] VALUES(CURRENT_TIMESTAMP, 3,2);
	GO

	/*
5) En tant que client  je veux avoir un entraineur.
	-Un Client doit pouvoir prendre rendez-vous avec un entraineur*/

	INSERT INTO [Rencontres].[Rencontre] VALUES(DATEADD(day,1,CURRENT_TIMESTAMP), null,1,2);
	GO
	/*
6) En tant qu’entraineur  je veux voir la liste de mes clients	
-Un entraineur doit avoir des clients*/

GO
CREATE FUNCTION dbo.ufn_listeClientEntraineur
(
	@EntaineurID int
)
RETURNS TABLE
AS
	return(		
		SELECT c.ClientID, c.Prenom, c.Nom, c.Adresse, c.NumTel, c.CodePostal, c.Poid, c.Age, c.Taille, c.Sexe, c.DateMesurePrise, c.CirconferenceBras, c.CirconferenceJambe, c.CirconferenceAbdominal
		FROM [Clients].[Client] c
		INNER JOIN [Rencontres].[Rencontre] r
		ON c.[ClientID] = r.[ClientID]
		INNER JOIN [Entraineurs].[Entraineur] e
		ON r.[EntraineurID] = e.[EntraineurID]
		WHERE e.[EntraineurID] = @EntaineurID
		)
GO
Select * From dbo.ufn_listeClientEntraineur(1);

	/*
7) En tant que client je veux  pouvoir changer de buts afin de rester motivé.
-Un client doit pouvoir modifier ses buts.*/

WITH butClientAChanger as ( Select ButID as 'ButID', But as 'But', DateDebutBut as 'DateDebutBut', DateFinBut as 'DateFinBut', clientID	as 'ClientID'
							From [Buts].[But]
							where ClientID = 1)

INSERT INTO  [Buts].[HistoriqueBut] (But,DateDebut, DateFin, ClientID) SELECT But, DateDebutBut, CURRENT_TIMESTAMP ,ClientID
from butClientAChanger;
GO
INSERT INTO [Buts].[But] (But, DateDebutBut, DateFinBut, ClientID) VALUES ('Gagner du poid', CURRENT_TIMESTAMP, null, 1)
GO

/*
8) En tant que client  je veux pouvoir visualiser quels muscles sont sollicités dans mes exercices.
	-Les exercices doivent montrer quelles régions corporelles sont sollicitées*/	

	GO
	CREATE PROCEDURE dbo.proc_MuscleSolliciteClient
	@ClientID int
	AS
	BEGIN
		
		Select e.[Nom], e.[RegionCorporelleSollicite]
	FROM [Programmes].[Exercice] e
	INNER JOIN [Programmes].[SerieExercice] se
	ON e.[ExerciceID] = se.[ExerciceID]
	INNER JOIN [Programmes].[Serie] s
	ON s.[SerieID] = se.[SerieID]
	INNER JOIN [Programmes].[Programme] p
	ON p.[ProgrammeID] = s.[ProgrammeID]
	WHERE [ClientID] = @ClientID

	END
	GO
	
	EXEC dbo.proc_SerieExercice @ClientID =1;




	/*
9) En tant qu’entraineur  je veux voir la liste de certains de mes clients	
-Un entraineur doit avoir des clients*/ 
	
	IF OBJECT_ID('dbo.proc_VoirCertainClientSelonEntraineur') IS NOT NULL DROP PROCEDURE dbo.proc_VoirCertainClientSelonEntraineur
	GO
	CREATE PROCEDURE dbo.proc_VoirCertainClientSelonEntraineur
	@ListeClient nvarchar(500),
	@EntraineurID nvarchar(10)
	AS 
    BEGIN
		Declare @SQLSTRING nvarchar(600);
		Set @SQLSTRING = N'SELECT c.ClientID, c.Prenom, c.Nom, c.Adresse, c.NumTel, c.CodePostal, c.Poid, c.Age, c.Taille, c.Sexe, c.DateMesurePrise, c.CirconferenceBras, c.CirconferenceJambe, c.CirconferenceAbdominal
		FROM [Clients].[Client] c
		INNER JOIN [Rencontres].[Rencontre] r
		ON c.[ClientID] = r.[ClientID]
		INNER JOIN [Entraineurs].[Entraineur] e
		ON r.[EntraineurID] = e.[EntraineurID]
		WHERE e.[EntraineurID] = '+@EntraineurID+' AND c.ClientID IN (' +@ListeClient+');'
	
	exec sp_executesql @SQLSTRING;

	END
	GO

	EXEC dbo.proc_VoirCertainClientSelonEntraineur @ListeClient = '1,2', @EntraineurID = '1';

	/*
10) En tant qu’entraineur je veux savoir depuis combiend de temps je travail ici.
	Un entraineur doit travailler au gym */


	GO
	CREATE PROCEDURE dbo.proc_TempsTravailEntraineur
	@EntraineurID int
	AS
	BEGIN
		
	SELECT DATEDIFF(MONTH,[DateEmbauche],DATEADD(day,1,CURRENT_TIMESTAMP)) as 'Mois depuis lembauche'
	FROM [Entraineurs].[Entraineur]

	END
	GO
	
	EXEC dbo.proc_TempsTravailEntraineur @EntraineurID =1;

	

	

	/*
11)	En tant que propriétaire, je veux pouvoir vérifier si mes entraineurs sont bons. Ma mesure est que les gens qu’ils entrainent se réinscrivent.
-	Pour chaque entraineur, je peux voir combien de clients se sont réinscris et plus précisément, combien de fois ils se sont réinscrits. Avec une division par sexe.*/

GO
IF OBJECT_ID('dbo.view_InscriptionEntraineurClient') IS NOT NULL DROP VIEW dbo.view_InscriptionEntraineurClient
	GO
	create view dbo.view_InscriptionEntraineurClient
	AS
	SELECT c.[ClientID],(c.[Nom] + ' , ' +c.[Prenom]) AS 'Nom Client',[InscriptionID],r.[EntraineurID], (e.[Nom] + ' , ' + e.[Prenom]) as 'Nom Entraineur', i.[ClientID] as 'ClientIDInscription'
	FROM [Clients].[Client] c
	INNER JOIN [Inscriptions].[Inscription] i
	ON c.[ClientID] = i.[ClientID]
	INNER JOIN [Rencontres].[Rencontre] r
	on c.[ClientID] = r.[ClientID]
	INNER JOIN [Entraineurs].[Entraineur] e
	ON e.[EntraineurID] = r.[EntraineurID]
	GO

IF OBJECT_ID('dbo.ufn_ReinscriptionParEntraineur') IS NOT NULL DROP function dbo.ufn_ReinscriptionParEntraineur
	GO
CREATE FUNCTION dbo.ufn_ReinscriptionParEntraineur
(
 @EntraineurID int
)
RETURNS TABLE
AS
	return(		
			Select [ClientID], [Nom Client], EntraineurID, [Nom Entraineur], count(InscriptionID) as 'Nombre Inscriptions', count(InscriptionID) as 'Inscription par Entraineur'
			FROM dbo.view_InscriptionEntraineurClient
			where 1 = EntraineurID AND ClientIDInscription = [ClientID]
			group by ClientID, [Nom Client], EntraineurID, [Nom Entraineur]
		)
GO


IF OBJECT_ID('dbo.proc_ReinscriptionParEntraineur') IS NOT NULL DROP Procedure dbo.proc_ReinscriptionParEntraineur
GO
CREATE PROCEDURE dbo.proc_ReinscriptionParEntraineur
AS
BEGIN
	DECLARE @nombreEntraineur int;
	SET @nombreEntraineur = (Select count( DISTINCT  [EntraineurID])
							FROM  [Entraineurs].[Entraineur]);

	DECLARE @nombreEntraineurAfficher int;
	SET @nombreEntraineurAfficher = 0;
	DECLARE @TABLE table(
	ClientID int,
	NomClient nvarchar(50),
	EntraineurId int,
	NomEntraineur nvarchar(50),
	NombreInscription int,
	NombreInscriptionPourEntraineur int
	);
	
	WHILE(@nombreEntraineur > @nombreEntraineurAfficher)
	BEGin
	
		Insert into @TABLE
		SELECT * FROM dbo.ufn_ReinscriptionParEntraineur(CAST(@nombreEntraineurAfficher AS INT));
		SET @nombreEntraineurAfficher +=1;
	END

	SELECT * FROM @TABLE;
END
GO



EXEC dbo.proc_ReinscriptionParEntraineur;




