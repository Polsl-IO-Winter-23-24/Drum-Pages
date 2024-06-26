USE [master]
GO
/****** Object:  Table [dbo].[Sprzet]    Script Date: 17.12.2023 13:29:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sprzet](
	[idSprzetu] [int] NOT NULL,
	[nazwa] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Sprzet] PRIMARY KEY CLUSTERED 
(
	[idSprzetu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SprzetyUzytkownik]    Script Date: 17.12.2023 13:29:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SprzetyUzytkownik](
	[id] [int] NOT NULL,
	[userId] [int] NOT NULL,
	[idSprzetu] [int] NOT NULL,
 CONSTRAINT [PK_SprzetyUzytkownik] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Style]    Script Date: 17.12.2023 13:29:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Style](
	[idStylu] [int] NOT NULL,
	[opis] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Style] PRIMARY KEY CLUSTERED 
(
	[idStylu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StyleUzytkownik]    Script Date: 17.12.2023 13:29:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StyleUzytkownik](
	[id] [int] NOT NULL,
	[userId] [int] NOT NULL,
	[stylId] [int] NOT NULL,
 CONSTRAINT [PK_StyleUzytkownik] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Uzytkownicy]    Script Date: 17.12.2023 13:29:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Uzytkownicy](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[haslo] [varchar](50) NOT NULL,
	[imie] [varchar](50) NOT NULL,
	[nazwisko] [varchar](50) NOT NULL,
	[wiek] [int] NOT NULL,
	[rodzajKonta] [varchar](50) NOT NULL,
	[umiejetnosci] [int] NOT NULL,
 CONSTRAINT [PK_Uzytkownicy] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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
SET IDENTITY_INSERT [dbo].[Uzytkownicy] ON 

INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (1, N'admin', N'admin', N'admin', N'admin', 100, N'admin', 2)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (2, N'user123', N'user123', N'Pawel', N'Nowak', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (4, N'user3333', N'user333', N'Pawel', N'Nowak', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (6, N'user33334', N'user3334', N'Pawel', N'Nowak', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (8, N'uzytkownik3', N'uzytkownik3', N'ImieU3', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (10, N'ffuzytkownik3', N'fffuzytkownik3', N'ImieU3', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (12, N'ssffuzytkownik3', N'ssfffuzytkownik3', N'ImieU3', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (14, N'fssffuzytkownik3', N'fssfffuzytkownik3', N'ImieU3', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (15, N'user45', N'user45', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (17, N'user455', N'user455', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (18, N'user4155', N'user4515', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (19, N'user6545', N'user5645', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (20, N'user36545', N'user56435', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (21, N'auser36545', N'auser56435', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (22, N'aduser36545', N'auser56435', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (24, N'us36545', N'us56435', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (26, N'us365ss45', N'us56435', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (27, N'prz1', N'prz1', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (29, N'pSrz1', N'pSrz1', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (31, N'pddSrz1', N'pddSrz1', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (32, N'paddSrz1', N'paddSrz1', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
INSERT [dbo].[Uzytkownicy] ([userId], [login], [haslo], [imie], [nazwisko], [wiek], [rodzajKonta], [umiejetnosci]) VALUES (33, N'paddsssSrz1', N'paddSrz1', N'ImieU45', N'NaziwskoU3', 20, N'Pro', 3)
SET IDENTITY_INSERT [dbo].[Uzytkownicy] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Uzytkownicy]    Script Date: 17.12.2023 13:29:06 ******/
ALTER TABLE [dbo].[Uzytkownicy] ADD  CONSTRAINT [IX_Uzytkownicy] UNIQUE NONCLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Uzytkownicy] ADD  CONSTRAINT [DF_Uzytkownicy_rodzajKonta]  DEFAULT ('amator') FOR [rodzajKonta]
GO
ALTER TABLE [dbo].[SprzetyUzytkownik]  WITH CHECK ADD  CONSTRAINT [FK_SprzetyUzytkownik_Sprzet1] FOREIGN KEY([idSprzetu])
REFERENCES [dbo].[Sprzet] ([idSprzetu])
GO
ALTER TABLE [dbo].[SprzetyUzytkownik] CHECK CONSTRAINT [FK_SprzetyUzytkownik_Sprzet1]
GO
ALTER TABLE [dbo].[SprzetyUzytkownik]  WITH CHECK ADD  CONSTRAINT [FK_SprzetyUzytkownik_Uzytkownicy] FOREIGN KEY([userId])
REFERENCES [dbo].[Uzytkownicy] ([userId])
GO
ALTER TABLE [dbo].[SprzetyUzytkownik] CHECK CONSTRAINT [FK_SprzetyUzytkownik_Uzytkownicy]
GO
ALTER TABLE [dbo].[StyleUzytkownik]  WITH CHECK ADD  CONSTRAINT [FK_StyleUzytkownik_Style] FOREIGN KEY([stylId])
REFERENCES [dbo].[Style] ([idStylu])
GO
ALTER TABLE [dbo].[StyleUzytkownik] CHECK CONSTRAINT [FK_StyleUzytkownik_Style]
GO
ALTER TABLE [dbo].[StyleUzytkownik]  WITH CHECK ADD  CONSTRAINT [FK_StyleUzytkownik_Uzytkownicy] FOREIGN KEY([userId])
REFERENCES [dbo].[Uzytkownicy] ([userId])
GO
ALTER TABLE [dbo].[StyleUzytkownik] CHECK CONSTRAINT [FK_StyleUzytkownik_Uzytkownicy]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Table to store basic user info
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Uzytkownicy'
GO
