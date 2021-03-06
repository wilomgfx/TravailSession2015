USE [master]
GO
/****** Object:  Database [GestionPhotoImmobilier]    Script Date: 2015-05-06 10:55:49 ******/
CREATE DATABASE [GestionPhotoImmobilier]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GestionPhotoImmobilier', FILENAME = N'C:\Espace Labo\SQLData\GestionPhotoImmobilier.mdf' , SIZE = 10240KB , MAXSIZE = 20480KB , FILEGROWTH = 10%)
 LOG ON 
( NAME = N'MonProgramme_log', FILENAME = N'C:\Espace Labo\SQLLog\GestionPhotoImmobilier_log.ldf' , SIZE = 10240KB , MAXSIZE = 204800KB , FILEGROWTH = 20%)
GO
ALTER DATABASE [GestionPhotoImmobilier] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GestionPhotoImmobilier].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GestionPhotoImmobilier] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET ARITHABORT OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET  MULTI_USER 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GestionPhotoImmobilier] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GestionPhotoImmobilier] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [GestionPhotoImmobilier]
GO
/****** Object:  Schema [Rdv]    Script Date: 2015-05-06 10:55:49 ******/
CREATE SCHEMA [Rdv]
GO
/****** Object:  Schema [Seance]    Script Date: 2015-05-06 10:55:49 ******/
CREATE SCHEMA [Seance]
GO
/****** Object:  Table [Rdv].[Rdv]    Script Date: 2015-05-06 10:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Rdv].[Rdv](
	[RdvId] [int] IDENTITY(1,1) NOT NULL,
	[Confirmer] [bit] NOT NULL,
	[Client] [nvarchar](50) NULL,
	[Photographe] [nvarchar](50) NULL,
	[SeanceId] [int] NOT NULL,
 CONSTRAINT [PK_Rdv_RdvId] PRIMARY KEY CLUSTERED 
(
	[RdvId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Seance].[Seance]    Script Date: 2015-05-06 10:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Seance].[Seance](
	[SeanceId] [int] IDENTITY(1,1) NOT NULL,
	[DateSeance] [datetime] NULL,
	[Agent] [nvarchar](50) NULL,
	[Photographe] [nvarchar](50) NULL,
	[Client] [nvarchar](50) NULL,
	[Forfait] [nvarchar](50) NULL,
	[Commentaire] [nvarchar](375) NULL,
	[Statut] [nvarchar](50) NULL,
 CONSTRAINT [PK_Seance_SeanceId] PRIMARY KEY CLUSTERED 
(
	[SeanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [Rdv].[Rdv]  WITH CHECK ADD  CONSTRAINT [FK_RDV_SeanceID] FOREIGN KEY([SeanceId])
REFERENCES [Seance].[Seance] ([SeanceId])
GO
ALTER TABLE [Rdv].[Rdv] CHECK CONSTRAINT [FK_RDV_SeanceID]
GO
/****** Object:  Trigger [Rdv].[trg_RDVConfirme]    Script Date: 2015-05-06 10:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- QUAND LE RENDEZ-VOUS EST PRIS, LE STATUT DE LA SÉANCE DEVIENT CONFIRMÉ

CREATE TRIGGER [Rdv].[trg_RDVConfirme]
ON [Rdv].[Rdv]
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
		SET Statut = 'Reportee'
		WHERE SeanceId = @idSeance
	END
	IF(@statut = 1)
	BEGIN
		UPDATE Seance.Seance
		SET Statut = 'Confirmee'
		WHERE SeanceId = @idSeance
	END
END

GO
/****** Object:  Trigger [Seance].[trg_RDVReporte]    Script Date: 2015-05-06 10:55:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [Seance].[trg_RDVReporte]
ON [Seance].[Seance]
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
			SET Statut = 'Reportee'
			WHERE SeanceId = @idSeance
		END
	END

END

GO
USE [master]
GO
ALTER DATABASE [GestionPhotoImmobilier] SET  READ_WRITE 
GO
