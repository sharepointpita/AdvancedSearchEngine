USE [AdvancedSearchEngine]
GO
/****** Object:  Table [dbo].[App]    Script Date: 14-10-2014 13:07:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[BuildNr] [int] NULL,
	[Version] [decimal](18, 2) NULL,
	[Deployed] [bit] NULL,
	[DocumentId] [int] NULL,
 CONSTRAINT [PK_TableOne] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[App_Technology]    Script Date: 14-10-2014 13:07:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App_Technology](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppId] [int] NOT NULL,
	[TechnologyId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_TableTwo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Document]    Script Date: 14-10-2014 13:07:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNr] [int] NULL,
	[DocumentName] [nvarchar](255) NULL,
	[DocumentUrl] [nvarchar](255) NULL,
	[Note] [text] NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KeyUser]    Script Date: 14-10-2014 13:07:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeyUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[AppId] [int] NULL,
 CONSTRAINT [PK_KeyUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Technology]    Script Date: 14-10-2014 13:07:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Technology](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Tech] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[App] ON 

GO
INSERT [dbo].[App] ([Id], [Name], [BuildNr], [Version], [Deployed], [DocumentId]) VALUES (1, N'TOR', 200, CAST(131.00 AS Decimal(18, 2)), 1, 1)
GO
INSERT [dbo].[App] ([Id], [Name], [BuildNr], [Version], [Deployed], [DocumentId]) VALUES (2, N'DCG', 250, CAST(183.00 AS Decimal(18, 2)), 0, 2)
GO
INSERT [dbo].[App] ([Id], [Name], [BuildNr], [Version], [Deployed], [DocumentId]) VALUES (3, N'SHIFTREPORT', 100, CAST(1.10 AS Decimal(18, 2)), 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[App] OFF
GO
SET IDENTITY_INSERT [dbo].[App_Technology] ON 

GO
INSERT [dbo].[App_Technology] ([Id], [AppId], [TechnologyId], [Deleted]) VALUES (3, 1, 1, 0)
GO
INSERT [dbo].[App_Technology] ([Id], [AppId], [TechnologyId], [Deleted]) VALUES (4, 1, 2, 0)
GO
INSERT [dbo].[App_Technology] ([Id], [AppId], [TechnologyId], [Deleted]) VALUES (5, 2, 3, 0)
GO
INSERT [dbo].[App_Technology] ([Id], [AppId], [TechnologyId], [Deleted]) VALUES (6, 1, 3, 1)
GO
SET IDENTITY_INSERT [dbo].[App_Technology] OFF
GO
SET IDENTITY_INSERT [dbo].[KeyUser] ON 

GO
INSERT [dbo].[KeyUser] ([Id], [Name], [AppId]) VALUES (1, N'Victor', 1)
GO
INSERT [dbo].[KeyUser] ([Id], [Name], [AppId]) VALUES (2, N'Peter', 2)
GO
SET IDENTITY_INSERT [dbo].[KeyUser] OFF
GO
SET IDENTITY_INSERT [dbo].[Technology] ON 

GO
INSERT [dbo].[Technology] ([Id], [Name], [Description], [Deleted]) VALUES (1, N'C#', NULL, NULL)
GO
INSERT [dbo].[Technology] ([Id], [Name], [Description], [Deleted]) VALUES (2, N'VB.NET', NULL, NULL)
GO
INSERT [dbo].[Technology] ([Id], [Name], [Description], [Deleted]) VALUES (3, N'IIS', NULL, NULL)
GO
INSERT [dbo].[Technology] ([Id], [Name], [Description], [Deleted]) VALUES (4, N'Windows Server 2003', NULL, NULL)
GO
INSERT [dbo].[Technology] ([Id], [Name], [Description], [Deleted]) VALUES (5, N'Windows Server 2008', NULL, 1)
GO
INSERT [dbo].[Technology] ([Id], [Name], [Description], [Deleted]) VALUES (6, N'Java', NULL, NULL)
GO
INSERT [dbo].[Technology] ([Id], [Name], [Description], [Deleted]) VALUES (7, N'Ruby on Rails', NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Technology] OFF
GO
