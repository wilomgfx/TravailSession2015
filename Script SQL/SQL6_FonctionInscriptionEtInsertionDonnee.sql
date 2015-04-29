USE [MonProgramme]
Go




IF OBJECT_ID('Inscriptions.ufnInitialiserDateFinInscription') IS NOT NULL DROP FUNCTION Inscriptions.ufnInitialiserDateFinInscription;
GO

CREATE FUNCTION Inscriptions.ufnInitialiserDateFinInscription
(
  @DateDebut date,
  @PeriodeInscriptionEnMois int
)
RETURNS DATE
AS
	BEGIN
	
	DECLARE @DateFinInscription Date
	SET @DateFinInscription =  DATEADD(Month,@PeriodeInscriptionEnMois,@DateDebut)

	RETURN @DateFinInscription
	END
	GO




	ALTER TABLE Inscriptions.Inscription
add dateFinInscription as Inscriptions.ufnInitialiserDateFinInscription(DateInscription,PeriodeInscription);
GO



INSERT INTO [Clients].[Client] (Prenom, Nom, Adresse, NumTel, CodePostal, Poid, Age, Taille, Sexe) VALUES ('Bob', 'LeBricoleur','69 DesgrosMembres','5149859874','J8B2Z1','100', '35','190','M');

INSERT INTO [Inscriptions].[Inscription] VALUES(CURRENT_TIMESTAMP, 3,1);
