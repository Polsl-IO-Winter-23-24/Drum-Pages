USE [master]
GO
/****** Object:  Table [dbo].[Kursy]    Script Date: 02.02.2024 21:25:45 ******/
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
/****** Object:  Table [dbo].[Lekcje]    Script Date: 02.02.2024 21:25:45 ******/
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
SET IDENTITY_INSERT [dbo].[Kursy] ON 

INSERT [dbo].[Kursy] ([kursId], [tytul], [opis], [autorId], [trudnosc], [ocena]) VALUES (1, N'How do i drum?', N'How do i drum?', 1, 1, 1)
INSERT [dbo].[Kursy] ([kursId], [tytul], [opis], [autorId], [trudnosc], [ocena]) VALUES (2, N'Chad Smith', N'example', 1, 2, 3)
INSERT [dbo].[Kursy] ([kursId], [tytul], [opis], [autorId], [trudnosc], [ocena]) VALUES (3, N'Matt Cameron', N'try', 1, 4, 2)
INSERT [dbo].[Kursy] ([kursId], [tytul], [opis], [autorId], [trudnosc], [ocena]) VALUES (4, N'Sweet Child O'' Mine', N'Guns N Roses', 1, 2, 4)
INSERT [dbo].[Kursy] ([kursId], [tytul], [opis], [autorId], [trudnosc], [ocena]) VALUES (5, N'Rock Beginner', N'SDSA', 1, 1, 4)
INSERT [dbo].[Kursy] ([kursId], [tytul], [opis], [autorId], [trudnosc], [ocena]) VALUES (6, N'Funk Beginner', N'fds', 1, 1, 5)
INSERT [dbo].[Kursy] ([kursId], [tytul], [opis], [autorId], [trudnosc], [ocena]) VALUES (7, N'Pop Beginner', N'1', 1, 1, 5)
INSERT [dbo].[Kursy] ([kursId], [tytul], [opis], [autorId], [trudnosc], [ocena]) VALUES (8, N'Jazz Beginner', N'fdf', 1, 1, 5)
SET IDENTITY_INSERT [dbo].[Kursy] OFF
GO
SET IDENTITY_INSERT [dbo].[Lekcje] ON 

INSERT [dbo].[Lekcje] ([lekcjaId], [tytul], [tresc], [kursId], [wideoURL], [ocena]) VALUES (1, N'nuda', N'Witam!
W tej lekcji....', 1, N'https://www.youtube.com/embed/jSmhFRvhnbc?list=RD2a7_yCxMgZc', 1)
INSERT [dbo].[Lekcje] ([lekcjaId], [tytul], [tresc], [kursId], [wideoURL], [ocena]) VALUES (2, N'fsdfsdf', N'Hello!
Sike frfr ngl', 1, N'https://www.youtube.com/embed/MqazV4hbu8E?list=RD2a7_yCxMgZc', 1)
INSERT [dbo].[Lekcje] ([lekcjaId], [tytul], [tresc], [kursId], [wideoURL], [ocena]) VALUES (3, N'dasda', N'Hello there...
General kenobi', 1, N'https://www.youtube.com/embed/sZrTJesvJeo', 1)
SET IDENTITY_INSERT [dbo].[Lekcje] OFF
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
