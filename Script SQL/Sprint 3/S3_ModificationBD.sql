USE [H15_PROJET_E03]

ALTER TABLE [Seance].[Seance]
ADD  Facture bit not null default 'false'

Go

ALTER TABLE [Seance].[Seance]
ADD  RVersion rowversion NOT NULL
