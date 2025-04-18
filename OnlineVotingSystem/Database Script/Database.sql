USE [master]
GO
/****** Object:  Database [OnlineVotingSystem]    Script Date: 04/07/2025 8:26:50 AM ******/
CREATE DATABASE [OnlineVotingSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineVotingSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OnlineVotingSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnlineVotingSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OnlineVotingSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OnlineVotingSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineVotingSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineVotingSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineVotingSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineVotingSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OnlineVotingSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineVotingSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OnlineVotingSystem] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineVotingSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineVotingSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineVotingSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineVotingSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnlineVotingSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnlineVotingSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OnlineVotingSystem] SET QUERY_STORE = OFF
GO
USE [OnlineVotingSystem]
GO
/****** Object:  Table [dbo].[Candidates]    Script Date: 04/07/2025 8:26:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidates](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[ElectionID] [int] NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Candidates] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Elections]    Script Date: 04/07/2025 8:26:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Elections](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Elections] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 04/07/2025 8:26:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04/07/2025 8:26:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 04/07/2025 8:26:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[UserID] [int] NOT NULL,
	[RoleID] [tinyint] NOT NULL,
 CONSTRAINT [PK_UsersRoles_1] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Votes]    Script Date: 04/07/2025 8:26:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Votes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ElectionID] [int] NOT NULL,
	[CandidateID] [int] NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Votes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Candidates] ON 

INSERT [dbo].[Candidates] ([ID], [Name], [Description], [ElectionID], [DateTimeCreated]) VALUES (1, N'Emily Willis', N'great singer', 2, CAST(N'2025-02-22T18:39:29.663' AS DateTime))
INSERT [dbo].[Candidates] ([ID], [Name], [Description], [ElectionID], [DateTimeCreated]) VALUES (2, N'Riley Reid', N'awesome singer', 2, CAST(N'2025-02-22T18:39:29.663' AS DateTime))
INSERT [dbo].[Candidates] ([ID], [Name], [Description], [ElectionID], [DateTimeCreated]) VALUES (3, N'Dil Doe', N'nice singer', 2, CAST(N'2025-02-22T18:39:29.663' AS DateTime))
INSERT [dbo].[Candidates] ([ID], [Name], [Description], [ElectionID], [DateTimeCreated]) VALUES (11, N'C1', N'D1', 9, CAST(N'2025-02-22T18:39:29.663' AS DateTime))
INSERT [dbo].[Candidates] ([ID], [Name], [Description], [ElectionID], [DateTimeCreated]) VALUES (12, N'C2', N'D2', 9, CAST(N'2025-02-22T18:39:29.663' AS DateTime))
INSERT [dbo].[Candidates] ([ID], [Name], [Description], [ElectionID], [DateTimeCreated]) VALUES (13, N'Marie Mel C. Riosa', N'Naka huli ng daga', 10, CAST(N'2025-02-22T18:39:29.663' AS DateTime))
INSERT [dbo].[Candidates] ([ID], [Name], [Description], [ElectionID], [DateTimeCreated]) VALUES (14, N'Joshua C. Magoliman', N'Programmer', 10, CAST(N'2025-02-22T18:39:29.663' AS DateTime))
INSERT [dbo].[Candidates] ([ID], [Name], [Description], [ElectionID], [DateTimeCreated]) VALUES (18, N'C1', N'C1D', 9, CAST(N'2025-02-22T18:39:29.663' AS DateTime))
INSERT [dbo].[Candidates] ([ID], [Name], [Description], [ElectionID], [DateTimeCreated]) VALUES (19, N'C2', N'D2', 9, CAST(N'2025-02-22T18:39:29.663' AS DateTime))
INSERT [dbo].[Candidates] ([ID], [Name], [Description], [ElectionID], [DateTimeCreated]) VALUES (20, N'C3', N'C3', 14, CAST(N'2025-02-22T18:39:29.663' AS DateTime))
SET IDENTITY_INSERT [dbo].[Candidates] OFF
GO
SET IDENTITY_INSERT [dbo].[Elections] ON 

INSERT [dbo].[Elections] ([ID], [Name], [StartDateTime], [EndDateTime], [DateTimeCreated]) VALUES (2, N'Best Singer of the year 2025', CAST(N'2025-02-07T00:00:00.000' AS DateTime), CAST(N'2025-02-14T00:00:00.000' AS DateTime), CAST(N'2025-02-22T18:40:33.147' AS DateTime))
INSERT [dbo].[Elections] ([ID], [Name], [StartDateTime], [EndDateTime], [DateTimeCreated]) VALUES (9, N'E1', CAST(N'2025-02-12T11:14:00.000' AS DateTime), CAST(N'2025-02-27T11:14:00.000' AS DateTime), CAST(N'2025-02-22T18:40:33.147' AS DateTime))
INSERT [dbo].[Elections] ([ID], [Name], [StartDateTime], [EndDateTime], [DateTimeCreated]) VALUES (10, N'Employee of the month - Jan 2025', CAST(N'2025-02-01T13:00:00.000' AS DateTime), CAST(N'2025-02-28T13:00:00.000' AS DateTime), CAST(N'2025-02-22T18:40:33.147' AS DateTime))
INSERT [dbo].[Elections] ([ID], [Name], [StartDateTime], [EndDateTime], [DateTimeCreated]) VALUES (13, N'A1', CAST(N'2024-12-17T19:40:00.000' AS DateTime), CAST(N'2025-01-12T19:40:00.000' AS DateTime), CAST(N'2025-02-22T18:40:33.147' AS DateTime))
INSERT [dbo].[Elections] ([ID], [Name], [StartDateTime], [EndDateTime], [DateTimeCreated]) VALUES (14, N'UPCOMING ETO', CAST(N'2025-03-01T15:46:00.000' AS DateTime), CAST(N'2025-03-31T15:46:00.000' AS DateTime), CAST(N'2025-02-22T18:40:33.147' AS DateTime))
SET IDENTITY_INSERT [dbo].[Elections] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([ID], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([ID], [Name]) VALUES (2, N'Voter')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Username], [Password], [DateTimeCreated]) VALUES (1, N'joshua.magoliman', N'joshua.magoliman', CAST(N'2025-02-22T18:42:46.370' AS DateTime))
INSERT [dbo].[Users] ([ID], [Username], [Password], [DateTimeCreated]) VALUES (2, N'mariemel', N'mariemel', CAST(N'2025-02-22T18:42:46.370' AS DateTime))
INSERT [dbo].[Users] ([ID], [Username], [Password], [DateTimeCreated]) VALUES (3, N'johndoe', N'johndoe', CAST(N'2025-02-22T18:42:46.370' AS DateTime))
INSERT [dbo].[Users] ([ID], [Username], [Password], [DateTimeCreated]) VALUES (4, N'dildoe', N'dildoe', CAST(N'2025-02-22T18:42:46.370' AS DateTime))
INSERT [dbo].[Users] ([ID], [Username], [Password], [DateTimeCreated]) VALUES (5, N'asd', N'asd', CAST(N'2025-02-22T18:42:46.370' AS DateTime))
INSERT [dbo].[Users] ([ID], [Username], [Password], [DateTimeCreated]) VALUES (6, N'qwe', N'qwe', CAST(N'2025-02-22T18:42:46.370' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[UsersRoles] ([UserID], [RoleID]) VALUES (1, 1)
INSERT [dbo].[UsersRoles] ([UserID], [RoleID]) VALUES (1, 2)
INSERT [dbo].[UsersRoles] ([UserID], [RoleID]) VALUES (2, 2)
INSERT [dbo].[UsersRoles] ([UserID], [RoleID]) VALUES (3, 2)
INSERT [dbo].[UsersRoles] ([UserID], [RoleID]) VALUES (4, 2)
INSERT [dbo].[UsersRoles] ([UserID], [RoleID]) VALUES (5, 2)
INSERT [dbo].[UsersRoles] ([UserID], [RoleID]) VALUES (6, 2)
GO
SET IDENTITY_INSERT [dbo].[Votes] ON 

INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (1, 1, 2, 1, CAST(N'2025-02-10T14:52:10.000' AS DateTime))
INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (3, 2, 2, 2, CAST(N'2025-02-10T14:52:27.923' AS DateTime))
INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (4, 3, 2, 1, CAST(N'2025-02-10T16:29:11.670' AS DateTime))
INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (1005, 6, 2, 3, CAST(N'2025-02-11T10:22:38.110' AS DateTime))
INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (1006, 5, 2, 1, CAST(N'2025-02-11T12:01:25.150' AS DateTime))
INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (1025, 1, 9, 12, CAST(N'2025-02-12T11:16:04.370' AS DateTime))
INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (1026, 2, 9, 11, CAST(N'2025-02-12T11:16:40.927' AS DateTime))
INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (1027, 4, 10, 13, CAST(N'2025-02-12T18:54:53.083' AS DateTime))
INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (1028, 6, 10, 14, CAST(N'2025-02-12T18:56:32.437' AS DateTime))
INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (1029, 1, 10, 14, CAST(N'2025-02-12T18:57:40.933' AS DateTime))
INSERT [dbo].[Votes] ([ID], [UserID], [ElectionID], [CandidateID], [DateTimeCreated]) VALUES (1030, 3, 9, 11, CAST(N'2025-02-21T16:05:57.820' AS DateTime))
SET IDENTITY_INSERT [dbo].[Votes] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Elections_Name]    Script Date: 04/07/2025 8:26:50 AM ******/
ALTER TABLE [dbo].[Elections] ADD  CONSTRAINT [UK_Elections_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Roles_Name]    Script Date: 04/07/2025 8:26:50 AM ******/
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [UK_Roles_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Users_Username]    Script Date: 04/07/2025 8:26:50 AM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UK_Users_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Candidates] ADD  CONSTRAINT [DF_Candidates_DateTimeCreated]  DEFAULT (getdate()) FOR [DateTimeCreated]
GO
ALTER TABLE [dbo].[Elections] ADD  CONSTRAINT [DF_Elections_DateTimeCreated]  DEFAULT (getdate()) FOR [DateTimeCreated]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_DateTimeCreated]  DEFAULT (getdate()) FOR [DateTimeCreated]
GO
ALTER TABLE [dbo].[Candidates]  WITH CHECK ADD  CONSTRAINT [FK_Candidates_ElectionID] FOREIGN KEY([ElectionID])
REFERENCES [dbo].[Elections] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Candidates] CHECK CONSTRAINT [FK_Candidates_ElectionID]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_RoleID]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_UserID]
GO
ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [FK_Votes_CandidateID] FOREIGN KEY([CandidateID])
REFERENCES [dbo].[Candidates] ([ID])
GO
ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [FK_Votes_CandidateID]
GO
ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [FK_Votes_ElectionID] FOREIGN KEY([ElectionID])
REFERENCES [dbo].[Elections] ([ID])
GO
ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [FK_Votes_ElectionID]
GO
ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [FK_Votes_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [FK_Votes_UserID]
GO
USE [master]
GO
ALTER DATABASE [OnlineVotingSystem] SET  READ_WRITE 
GO
