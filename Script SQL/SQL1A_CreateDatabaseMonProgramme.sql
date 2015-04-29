-- utilisez master
USE master;
--  créez la base de données ExempleX, 
--      sur PRIMARY,(ON PRIMARY) il y aura le fichier de données,
--      	avec le nom (NAME) 'ExempleX', 
--      	le nom de fichier  (FILENAME) 'C:\Espace Labo\SQLData\ExempleX.mdf' ,
--       	de grandeur initiale(SIZE) de 10MB, 
--			pouvant grandir (MAXSIZE) jusqu'à 20 sans intervention
--       	et avec un taux de croissance prévu  (FILEGROWTH) de 10%
--      le log quant à lui (LOG ON) aura les informations suivantes:
--        	avec le nom (NAME) 'ExempleX_log', 
--      	le nom de fichier  (FILENAME) 'C:\Espace Labo\SQLLog\ExempleX_log.ldf' 
--       	de grandeur initiale(SIZE) de 10MB, 
--			pouvant grandir (MAXSIZE) jusqu'à 200 sans intervention
--       	et avec un taux de croissance prévu  (FILEGROWTH) de 20%

CREATE DATABASE MonProgramme
	ON PRIMARY(
		NAME='MonProgramme',
		FILENAME='C:\Espace Labo\SQLData\MonProgramme.mdf',
		SIZE=10MB,
		MAXSIZE=20,
		FILEGROWTH=10%
		)
	LOG ON(
		NAME='MonProgramme_log',
		FILENAME='C:\Espace Labo\SQLLog\MonProgramme_log.ldf',
		SIZE=10MB,
		MAXSIZE=200,
		FILEGROWTH=20%
		);


