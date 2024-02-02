USE [master]
GO
/****** Object:  Table [dbo].[Kursy]    Script Date: 02.02.2024 21:12:05 ******/
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
/****** Object:  Table [dbo].[Lekcje]    Script Date: 02.02.2024 21:12:05 ******/
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
ALTER TABLE [dbo].[Kursy]  WITH CHECK ADD FOREIGN KEY([autorId])
REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO
ALTER TABLE [dbo].[Kursy]  WITH CHECK ADD FOREIGN KEY([autorId])
REFERENCES [dbo].[Uzytkownicy] ([uzytkownikId])
GO
ALTER TABLE [dbo].[Lekcje]  WITH CHECK ADD FOREIGN KEY([kursId])
REFERENCES [dbo].[Kursy] ([kursId])
GO
ALTER TABLE [dbo].[Lekcje]  WITH CHECK ADD FOREIGN KEY([kursId])
REFERENCES [dbo].[Kursy] ([kursId])
GO
