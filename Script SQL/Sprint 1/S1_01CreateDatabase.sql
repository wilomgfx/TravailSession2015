USE master;

CREATE DATABASE GestionPhotoImmobilier
	ON PRIMARY(
		NAME='GestionPhotoImmobilier',
		FILENAME='C:\Espace Labo\SQLData\GestionPhotoImmobilier.mdf',
		SIZE=10MB,
		MAXSIZE=20,
		FILEGROWTH=10%
		)
	LOG ON(
		NAME='MonProgramme_log',
		FILENAME='C:\Espace Labo\SQLLog\GestionPhotoImmobilier_log.ldf',
		SIZE=10MB,
		MAXSIZE=200,
		FILEGROWTH=20%
		);