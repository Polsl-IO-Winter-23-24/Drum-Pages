﻿USE [master]
GO

CREATE TABLE [dbo].[Komentarze] (
  [komentarzId] int NOT NULL,
  [lekcjaId] int,
  [autorId] int,
  [kursId] int,
  [tresc] text,
  [ocena] int,
  PRIMARY KEY ([komentarzId])
)
GO

CREATE TABLE [dbo].[Kursy] (
  [kursId] int NOT NULL,
  [tytul] nvarchar(255),
  [opis] text,
  [autorId] int,
  [trudnosc] int,
  [ocena] int,
  PRIMARY KEY ([kursId])
)
GO

CREATE TABLE [dbo].[Lekcje] (
  [lekcjaId] int NOT NULL,
  [tytul] nvarchar(255),
  [tresc] text,
  [kursId] int,
  [wideoURL] nvarchar(255),
  [ocena] int,
  PRIMARY KEY ([lekcjaId])
)
GO

CREATE TABLE [dbo].[Sprzet] (
  [idSprzetu] int NOT NULL,
  [nazwa] varchar(50) NOT NULL,
  PRIMARY KEY ([idSprzetu])
)
GO

CREATE TABLE [dbo].[SprzetyUzytkownik] (
  [id] int NOT NULL,
  [uzytkownikId] int NOT NULL,
  [idSprzetu] int NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [dbo].[Watki] (
  [idWatku] int NOT NULL,
  [temat] varchar(50) NOT NULL,
  [dataUtworzenia] date NOT NULL,
  [uzytkownikId] int NOT NULL,
  PRIMARY KEY ([idWatku])
)
GO

CREATE TABLE [dbo].[Wydarzenia] (
  [idWydarzenia] int NOT NULL,
  [nazwa] varchar(50) NOT NULL,
  [data] date NOT NULL,
  [opis] text NOT NULL,
  [lokalizacja] text NOT NULL,
  [organizatorId] int NOT NULL,
  PRIMARY KEY ([idWydarzenia])
)
GO

CREATE TABLE [dbo].[Wpisy] (
  [idWpisu] int NOT NULL,
  [zawartosc] varchar(50) NOT NULL,
  [dataUtworzenia] date NOT NULL,
  [idWatku] int NOT NULL,
  [uzytkownikId] int NOT NULL,
  PRIMARY KEY ([idWpisu])
)
GO

CREATE TABLE [dbo].[Style] (
  [idStylu] int NOT NULL,
  [opis] varchar(50) NOT NULL,
  PRIMARY KEY ([idStylu])
)
GO

CREATE TABLE [dbo].[StyleUzytkownik] (
  [id] int NOT NULL,
  [uzytkownikId] int NOT NULL,
  [stylId] int NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [dbo].[Uzytkownicy] (
  [uzytkownikId] int NOT NULL IDENTITY(1, 1),
  [login] varchar(50) NOT NULL,
  [haslo] varchar(50) NOT NULL,
  [imie] varchar(50) NOT NULL,
  [nazwisko] varchar(50) NOT NULL,
  [wiek] int NOT NULL,
  [rodzajKonta] varchar(50) NOT NULL DEFAULT 'amator',
  [umiejetnosci] int NOT NULL,
  PRIMARY KEY ([uzytkownikId])
)
GO

CREATE UNIQUE INDEX [IX_Uzytkownicy] ON [dbo].[Uzytkownicy] ("login")
GO

ALTER TABLE [dbo].[Lekcje] ADD FOREIGN KEY ([kursId]) REFERENCES [dbo].[Kursy] ([kursId])
GO

ALTER TABLE [dbo].[Kursy] ADD FOREIGN KEY ([autorId]) REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO

ALTER TABLE [dbo].[Komentarze] ADD FOREIGN KEY ([autorId]) REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO

ALTER TABLE [dbo].[Komentarze] ADD FOREIGN KEY ([lekcjaId]) REFERENCES [dbo].[Lekcje] ([lekcjaId])
GO

ALTER TABLE [dbo].[Komentarze] ADD FOREIGN KEY ([kursId]) REFERENCES [dbo].[Kursy] ([kursId])
GO

ALTER TABLE [dbo].[SprzetyUzytkownik] ADD FOREIGN KEY ([uzytkownikId]) REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO

ALTER TABLE [dbo].[StyleUzytkownik] ADD FOREIGN KEY ([uzytkownikId]) REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO

ALTER TABLE [dbo].[StyleUzytkownik] ADD FOREIGN KEY ([stylId]) REFERENCES [dbo].[Style] ([idStylu])
GO

ALTER TABLE [dbo].[SprzetyUzytkownik] ADD FOREIGN KEY ([idSprzetu]) REFERENCES [dbo].[Sprzet] ([idSprzetu])
GO

ALTER TABLE [dbo].[Wpisy] ADD FOREIGN KEY ([idWatku]) REFERENCES [dbo].[Watki] ([idWatku])
GO

ALTER TABLE [dbo].[Wpisy] ADD FOREIGN KEY ([uzytkownikId]) REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO

ALTER TABLE [dbo].[Watki] ADD FOREIGN KEY ([uzytkownikId]) REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO

ALTER TABLE [dbo].[Wydarzenia] ADD FOREIGN KEY ([organizatorId]) REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO

INSERT [dbo].[Sprzet] ([idSprzetu], [nazwa]) VALUES (0, N'admin')
INSERT [dbo].[Sprzet] ([idSprzetu], [nazwa]) VALUES (1, N'trojkat')
INSERT [dbo].[Sprzet] ([idSprzetu], [nazwa]) VALUES (2, N'bebny')
GO
INSERT [dbo].[Style] ([idStylu], [opis]) VALUES (0, N'admin')
INSERT [dbo].[Style] ([idStylu], [opis]) VALUES (1, N'rock')
INSERT [dbo].[Style] ([idStylu], [opis]) VALUES (2, N'jazz')
INSERT [dbo].[Style] ([idStylu], [opis]) VALUES (3, N'blues')
GO
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'admin', N'admin', N'admin', N'admin', 100, N'admin', 2)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'user123', N'user123', N'Pawel', N'Nowak', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'user3333', N'user333', N'Pawel', N'Nowak', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'user33334', N'user3334', N'Pawel', N'Nowak', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'uzytkownik3', N'uzytkownik3', N'ImieU3', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'ffuzytkownik3', N'fffuzytkownik3', N'ImieU3', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'ssffuzytkownik3', N'ssfffuzytkownik3', N'ImieU3', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'fssffuzytkownik3', N'fssfffuzytkownik3', N'ImieU3', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'user45', N'user45', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'user455', N'user455', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'user4155', N'user4515', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'user6545', N'user5645', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'user36545', N'user56435', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'auser36545', N'auser56435', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'aduser36545', N'auser56435', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'us36545', N'us56435', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'us365ss45', N'us56435', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'prz1', N'prz1', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'pSrz1', N'pSrz1', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'pddSrz1', N'pddSrz1', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'paddSrz1', N'paddSrz1', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (N'paddsssSrz1', N'paddSrz1', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
