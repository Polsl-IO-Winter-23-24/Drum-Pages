USE [master]
GO
/****** Object:  User [##MS_PolicyEventProcessingLogin##]    Script Date: 18.12.2023 23:54:14 ******/
CREATE USER [##MS_PolicyEventProcessingLogin##] FOR LOGIN [##MS_PolicyEventProcessingLogin##] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [##MS_AgentSigningCertificate##]    Script Date: 18.12.2023 23:54:14 ******/
CREATE USER [##MS_AgentSigningCertificate##] FOR LOGIN [##MS_AgentSigningCertificate##]
GO
/****** Object:  Table [dbo].[Komentarze]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Komentarze](
	[komentarzId] [int] IDENTITY(1,1) NOT NULL,
	[lekcjaId] [int] NULL,
	[autorId] [int] NULL,
	[kursId] [int] NULL,
	[tresc] [text] NULL,
	[ocena] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[komentarzId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kursy]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kursy](
	[kursId] [int] IDENTITY(1,1) NOT NULL,
	[tytul] [nvarchar](255) NULL,
	[opis] [text] NULL,
	[autorId] [int] NULL,
	[trudnosc] [int] NULL,
	[ocena] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[kursId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lekcje]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lekcje](
	[lekcjaId] [int] IDENTITY(1,1) NOT NULL,
	[tytul] [nvarchar](255) NULL,
	[tresc] [text] NULL,
	[kursId] [int] NULL,
	[wideoURL] [nvarchar](255) NULL,
	[ocena] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[lekcjaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sprzet]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sprzet](
	[idSprzetu] [int] IDENTITY(1,1) NOT NULL,
	[nazwa] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idSprzetu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SprzetyUzytkownik]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SprzetyUzytkownik](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uzytkownikId] [int] NOT NULL,
	[idSprzetu] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Style]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Style](
	[idStylu] [int] IDENTITY(1,1) NOT NULL,
	[opis] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idStylu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StyleUzytkownik]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StyleUzytkownik](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uzytkownikId] [int] NOT NULL,
	[stylId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Uzytkownicy]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Uzytkownicy](
	[uzytkownikId] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[haslo] [varchar](50) NOT NULL,
	[imie] [varchar](50) NOT NULL,
	[nazwisko] [varchar](50) NOT NULL,
	[wiek] [int] NOT NULL,
	[rodzajKonta] [varchar](50) NOT NULL,
	[umiejetnosci] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[uzytkownikId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Watki]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Watki](
	[idWatku] [int] IDENTITY(1,1) NOT NULL,
	[temat] [varchar](50) NOT NULL,
	[dataUtworzenia] [date] NOT NULL,
	[uzytkownikId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idWatku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wpisy]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wpisy](
	[idWpisu] [int] IDENTITY(1,1) NOT NULL,
	[zawartosc] [varchar](50) NOT NULL,
	[dataUtworzenia] [date] NOT NULL,
	[idWatku] [int] NOT NULL,
	[uzytkownikId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idWpisu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wydarzenia]    Script Date: 18.12.2023 23:54:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wydarzenia](
	[idWydarzenia] [int] IDENTITY(1,1) NOT NULL,
	[nazwa] [varchar](50) NOT NULL,
	[data] [date] NOT NULL,
	[opis] [text] NOT NULL,
	[lokalizacja] [text] NOT NULL,
	[organizatorId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idWydarzenia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Uzytkownicy]    Script Date: 18.12.2023 23:54:14 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Uzytkownicy] ON [dbo].[Uzytkownicy]
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Uzytkownicy] ADD  DEFAULT ('amator') FOR [rodzajKonta]
GO
ALTER TABLE [dbo].[Watki] ADD  CONSTRAINT [DF_Watki_dataUtworzenia]  DEFAULT (getdate()) FOR [dataUtworzenia]
GO
ALTER TABLE [dbo].[Wpisy] ADD  CONSTRAINT [DF_Wpisy_dataUtworzenia]  DEFAULT (getdate()) FOR [dataUtworzenia]
GO
ALTER TABLE [dbo].[Komentarze]  WITH CHECK ADD FOREIGN KEY([autorId])
REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO
ALTER TABLE [dbo].[Komentarze]  WITH CHECK ADD FOREIGN KEY([kursId])
REFERENCES [dbo].[Kursy] ([kursId])
GO
ALTER TABLE [dbo].[Komentarze]  WITH CHECK ADD FOREIGN KEY([lekcjaId])
REFERENCES [dbo].[Lekcje] ([lekcjaId])
GO
ALTER TABLE [dbo].[Kursy]  WITH CHECK ADD FOREIGN KEY([autorId])
REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO
ALTER TABLE [dbo].[Lekcje]  WITH CHECK ADD FOREIGN KEY([kursId])
REFERENCES [dbo].[Kursy] ([kursId])
GO
ALTER TABLE [dbo].[SprzetyUzytkownik]  WITH CHECK ADD FOREIGN KEY([idSprzetu])
REFERENCES [dbo].[Sprzet] ([idSprzetu])
GO
ALTER TABLE [dbo].[SprzetyUzytkownik]  WITH CHECK ADD FOREIGN KEY([uzytkownikId])
REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO
ALTER TABLE [dbo].[StyleUzytkownik]  WITH CHECK ADD FOREIGN KEY([stylId])
REFERENCES [dbo].[Style] ([idStylu])
GO
ALTER TABLE [dbo].[StyleUzytkownik]  WITH CHECK ADD FOREIGN KEY([uzytkownikId])
REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO
ALTER TABLE [dbo].[Watki]  WITH CHECK ADD FOREIGN KEY([uzytkownikId])
REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO
ALTER TABLE [dbo].[Wpisy]  WITH CHECK ADD FOREIGN KEY([idWatku])
REFERENCES [dbo].[Watki] ([idWatku])
GO
ALTER TABLE [dbo].[Wpisy]  WITH CHECK ADD FOREIGN KEY([uzytkownikId])
REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO
ALTER TABLE [dbo].[Wydarzenia]  WITH CHECK ADD FOREIGN KEY([organizatorId])
REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO
