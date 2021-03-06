USE [master]
GO
/****** Object:  Database [MusicGO]    Script Date: 2/13/2020 6:07:45 PM ******/
CREATE DATABASE [MusicGO]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MusicGO', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\MusicGO.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MusicGO_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\MusicGO_log.ldf' , SIZE = 3136KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MusicGO] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MusicGO].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MusicGO] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MusicGO] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MusicGO] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MusicGO] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MusicGO] SET ARITHABORT OFF 
GO
ALTER DATABASE [MusicGO] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MusicGO] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MusicGO] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MusicGO] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MusicGO] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MusicGO] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MusicGO] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MusicGO] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MusicGO] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MusicGO] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MusicGO] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MusicGO] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MusicGO] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MusicGO] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MusicGO] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MusicGO] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MusicGO] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MusicGO] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MusicGO] SET  MULTI_USER 
GO
ALTER DATABASE [MusicGO] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MusicGO] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MusicGO] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MusicGO] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MusicGO] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MusicGO', N'ON'
GO
ALTER DATABASE [MusicGO] SET QUERY_STORE = OFF
GO
USE [MusicGO]
GO
/****** Object:  Table [dbo].[Binnacle]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Binnacle](
	[BinnacleID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Binnacle] PRIMARY KEY CLUSTERED 
(
	[BinnacleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dictionary]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dictionary](
	[DictionaryID] [uniqueidentifier] NOT NULL,
	[LanguageID] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Key] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Dictionary] PRIMARY KEY CLUSTERED 
(
	[DictionaryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DVV]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DVV](
	[DVVID] [uniqueidentifier] NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[DVVHash] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_DVV] PRIMARY KEY CLUSTERED 
(
	[DVVID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[LanguageID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[PermissionID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsGroup] [bit] NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionGroup]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionGroup](
	[PermissionGroupID] [uniqueidentifier] NOT NULL,
	[ParentID] [uniqueidentifier] NOT NULL,
	[ChildID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PermissionGroup] PRIMARY KEY CLUSTERED 
(
	[PermissionGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Blocked] [bit] NOT NULL,
	[Password] [nvarchar](32) NOT NULL,
	[ArtistName] [nvarchar](50) NULL,
	[DVH] [nvarchar](32) NULL,
	[ImgKey] [nvarchar](max) NULL,
	[LanguageID] [uniqueidentifier] NOT NULL,
	[ContractID] [uniqueidentifier] NULL,
	[Playbacks] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermission](
	[PermissionUserID] [uniqueidentifier] NOT NULL,
	[PermissionID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserPermission_1] PRIMARY KEY CLUSTERED 
(
	[PermissionUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'94e053b7-4d89-449f-8255-003b2ef232cf', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-09T20:57:57.677' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4352552a-0dc2-43ef-8022-05851fc77f35', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:01:06.057' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7c633eb6-40c6-4817-a499-06b0fa68f3ce', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T15:32:24.253' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ef2d7973-2192-4ef4-bdef-096f96e6a56c', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T12:26:49.807' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8e0c2d52-ae1a-4442-8ba3-0b79eae91576', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T12:49:54.483' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a1610c3a-6a91-4104-b09a-0c94bf09f512', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T13:06:04.763' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9a3fd67a-66d0-4085-8744-0de34407de91', N'L', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-02T18:46:50.417' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'bf319645-6f1e-465b-89ae-104e0ff9594d', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-02T19:02:07.323' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a28d2379-1ab8-436c-b82f-1612d563a55b', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T15:02:46.027' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd60d68c4-d559-45d3-8753-17498fe1827a', N'L', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-02T17:16:36.560' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9a708fcb-e146-46a9-977e-26e98c6b6f25', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T14:42:00.450' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e11e2cf5-36a4-45b9-9ebf-2af82c09e566', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T12:10:05.407' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b75962e8-44af-4d16-a6b3-2fa12f0486eb', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:01:50.973' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd51a86e0-1b88-4563-93e6-3178258780c7', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:24:25.183' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7552eb25-b08c-4d05-9527-31f123877a08', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T16:38:51.730' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'777182cb-e2ab-490b-9782-348ce9b69c9c', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T11:54:24.563' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'56a03837-65fa-4267-b1ef-3752705f434d', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T01:26:07.533' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'fc25fdfb-def5-450e-9b42-40cc427b27e5', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:22:55.557' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2cf0ae17-a4d5-4489-a6c5-433407545b79', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T14:40:48.093' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b03e9503-e8e6-4dca-90d2-446baaeb4071', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:16:54.913' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6a2a6cd4-6753-41ad-b203-4750e6c4617c', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T12:18:22.797' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5612dfb3-0c98-4b38-99b8-595ac6a74c56', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T12:24:03.487' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'558e2904-199f-4995-9ea4-597b0df23b00', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T14:24:45.003' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9696aa20-f7a0-41f0-b360-6241c429d0ff', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T12:25:04.293' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a1550ece-3b02-43a9-81b1-6da56d80d0c9', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T19:54:11.473' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'eff28fe2-1464-44c8-84e7-6f745491e536', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:25:18.863' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'56713936-cbcd-4010-b208-730be0830cde', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T00:57:24.160' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'df28d0d6-74a2-46b1-937c-77e997043ce6', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:47:07.143' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6b9e48ed-9652-4f18-a91b-7be5d811eca0', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T17:09:50.913' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'02cccec5-8559-4aa8-a112-7fd187c70b31', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T01:26:07.533' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b85bd7ee-6382-4880-a08b-823d6e39b8b3', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-10T16:08:21.780' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4ea7db84-1eea-4eaa-bb8f-872c3464e41e', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T11:57:42.233' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9ebcc952-8511-4e32-aceb-905b18bad65d', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T01:01:54.980' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f5a5bbd4-239a-47bd-8c52-95f03667b8b5', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T12:12:39.897' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'52837cd2-ac02-470b-b281-a88efa845fb1', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:38:22.300' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6f0b5bef-3765-4b5c-b9bc-aa8983b4f4fb', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T14:37:44.970' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'dbb0dadc-2e1e-4209-9674-aab1edb3c1a9', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T01:02:09.423' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'0f057ae4-9326-4529-a526-ae6d72a013af', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:13:32.463' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7b32ca4a-a069-4943-85f6-b00f0c4587b1', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:42:13.693' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'519991a4-dbf5-4d1d-bb6f-b0983dc2eae6', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T15:48:59.487' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'386da6ab-6adb-4e71-8674-b3d8847dfa12', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-10T16:42:14.613' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd99c6e62-7ae9-48b3-9e33-d1ff993129d8', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:15:33.417' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2abab78b-23cc-4727-a6d9-d2e857346617', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T10:27:28.777' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4f782f2f-0a3d-4cbb-abb3-d49854679f6b', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:21:33.970' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8b1d1dfe-801d-4f2c-b637-d75673e8bd12', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:11:38.470' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9c8a1347-4459-4d9a-a9d9-da6d5c4476d4', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-09T19:15:34.210' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6dab3fe2-fc8e-4fcf-870e-e239a09d0686', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-10T01:04:05.027' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'096fdb81-f28e-4858-a6e1-e481af3e1b46', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:02:28.947' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'232b01ab-e4ae-44f4-b62c-e7363636d8fc', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T15:49:38.550' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'3adcec68-0490-4ed1-8edf-ea8af5112b10', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:19:10.707' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'736bf4a3-0b08-4fbd-ad8d-eba4d604389d', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T15:35:42.790' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'eea3c512-49b3-40fa-bb3f-ec22aae187b1', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:39:28.503' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'248cdd19-bf8c-4221-bc8e-ecb3cb83806b', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T15:09:10.907' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f786aa3d-8ac1-4c9a-a0bb-f3f8f49ddc31', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T00:52:10.953' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f1e9668d-cc05-4a28-b11f-fc168d3f4e34', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T17:09:13.143' AS DateTime))
INSERT [dbo].[Dictionary] ([DictionaryID], [LanguageID], [Value], [Key]) VALUES (N'8bf48864-13d1-4409-a862-0842a655f89c', N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', N'hola mundo', N'helloworld')
INSERT [dbo].[Dictionary] ([DictionaryID], [LanguageID], [Value], [Key]) VALUES (N'93b9d6af-c1e1-4741-87a0-33b204e53976', N'1c11e8f2-1b83-4476-9f27-54da779c64c6', N'Hello World', N'helloworld')
INSERT [dbo].[Dictionary] ([DictionaryID], [LanguageID], [Value], [Key]) VALUES (N'6a77f871-50b7-420f-87cc-6fe9b88ce3b2', N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', N'Cerrar Sesión', N'logout')
INSERT [dbo].[Dictionary] ([DictionaryID], [LanguageID], [Value], [Key]) VALUES (N'e739728d-ed57-4f41-9495-a0d803856569', N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', N'Ingresar', N'login')
INSERT [dbo].[Dictionary] ([DictionaryID], [LanguageID], [Value], [Key]) VALUES (N'281ca9b7-ee62-4660-8535-a752a2fc881b', N'1c11e8f2-1b83-4476-9f27-54da779c64c6', N'login', N'login')
INSERT [dbo].[Dictionary] ([DictionaryID], [LanguageID], [Value], [Key]) VALUES (N'4276f6f7-c14a-4855-bc5e-e18e6424e602', N'1c11e8f2-1b83-4476-9f27-54da779c64c6', N'logout', N'logout')
INSERT [dbo].[DVV] ([DVVID], [TableName], [DVVHash]) VALUES (N'027ea870-8717-4ab2-b896-f9411ab3d13b', N'UserDAL', N'ded008fb554d6383a3aa6c045791f1ff')
INSERT [dbo].[Language] ([LanguageID], [Name], [Code]) VALUES (N'1c11e8f2-1b83-4476-9f27-54da779c64c6', N'English', N'en')
INSERT [dbo].[Language] ([LanguageID], [Name], [Code]) VALUES (N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', N'Español', N'es')
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'0d072b21-119d-48e2-ad23-1280eab671d8', N'DeletePermission', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'078e46f0-3065-4272-bd28-1295ca302346', N'Songs', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'd247a163-0802-44e2-a135-19fa11d658b8', N'SuperAdmin', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'e4c115da-f568-4843-9116-276f0e947ec7', N'AddSong', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'3cc71b2c-7f92-4e34-8dba-3d9f74369a49', N'ModifySong', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'4382837c-0782-46cf-a1e5-56e64922b97e', N'Login', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'b2de0226-c029-417c-9355-5f0facda8f39', N'CreatePermission', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'e4a76a8d-a0bd-484a-ae28-665a642ee024', N'Permissions', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'2768a7a9-5119-4f44-853c-6fd96785bc9f', N'Artist', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'1f2c411b-6415-47d9-90a9-6fed4fe0cdc6', N'ModifyPermission', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'a1494ad9-0aab-42d2-ab0d-7faf2410bd8f', N'DeleteSong', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'0380110f-4d1c-467a-9952-8d54ee3cf215', N'Admin', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'8190e0ab-98a7-45cb-8110-90f0e8343714', N'Play', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'317622bc-0b29-4734-9d7e-ec2a20aa015f', N'User', 1)
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'438820d2-3be6-4d3d-a55a-054e4c5a0544', N'e4a76a8d-a0bd-484a-ae28-665a642ee024', N'b2de0226-c029-417c-9355-5f0facda8f39')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'2be25689-d62e-4fb9-b2ff-408d0e71532f', N'2768a7a9-5119-4f44-853c-6fd96785bc9f', N'078e46f0-3065-4272-bd28-1295ca302346')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'f16c6754-cdc8-466d-897c-550c632d2ee0', N'd247a163-0802-44e2-a135-19fa11d658b8', N'4382837c-0782-46cf-a1e5-56e64922b97e')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'7b3481d3-e96d-4be1-85d2-59d753e1e5d5', N'078e46f0-3065-4272-bd28-1295ca302346', N'8190e0ab-98a7-45cb-8110-90f0e8343714')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'9f342156-0faa-4aad-8dcb-64a21153aa07', N'078e46f0-3065-4272-bd28-1295ca302346', N'e4c115da-f568-4843-9116-276f0e947ec7')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'ed695c94-7568-493b-9a9b-6c607dc05ffd', N'2768a7a9-5119-4f44-853c-6fd96785bc9f', N'4382837c-0782-46cf-a1e5-56e64922b97e')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'15196028-416e-4499-8ab0-788eba4a3b6d', N'078e46f0-3065-4272-bd28-1295ca302346', N'3cc71b2c-7f92-4e34-8dba-3d9f74369a49')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'af2e2029-f4b6-4c6c-93c9-7cb320b60759', N'317622bc-0b29-4734-9d7e-ec2a20aa015f', N'4382837c-0782-46cf-a1e5-56e64922b97e')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'ef6c3adf-f87e-49bc-bfc2-7fa648227388', N'e4a76a8d-a0bd-484a-ae28-665a642ee024', N'1f2c411b-6415-47d9-90a9-6fed4fe0cdc6')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'ab8fe338-a420-4f5f-ba8c-910285da3f60', N'd247a163-0802-44e2-a135-19fa11d658b8', N'078e46f0-3065-4272-bd28-1295ca302346')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'8becfede-5a84-40f3-b295-c95233c816cd', N'd247a163-0802-44e2-a135-19fa11d658b8', N'e4a76a8d-a0bd-484a-ae28-665a642ee024')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'21468593-f471-49b9-a08e-d81ea9f28dbd', N'0380110f-4d1c-467a-9952-8d54ee3cf215', N'4382837c-0782-46cf-a1e5-56e64922b97e')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'cbef5950-321e-4812-9c82-d8abd7897510', N'078e46f0-3065-4272-bd28-1295ca302346', N'a1494ad9-0aab-42d2-ab0d-7faf2410bd8f')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'20e22ea0-cf5e-463a-a310-f7a184a3db08', N'e4a76a8d-a0bd-484a-ae28-665a642ee024', N'0d072b21-119d-48e2-ad23-1280eab671d8')
INSERT [dbo].[User] ([UserID], [Name], [LastName], [UserName], [Email], [Blocked], [Password], [ArtistName], [DVH], [ImgKey], [LanguageID], [ContractID], [Playbacks]) VALUES (N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', N'Ezequiel', N'Fernandez', N'Ezefer', N'Ezequielf10@hotmail.com', 0, N'81dc9bdb52d04dc20036dbd8313ed055', NULL, N'0c0074ad522c26e115d28fb94091a7ed', NULL, N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', NULL, 0)
INSERT [dbo].[User] ([UserID], [Name], [LastName], [UserName], [Email], [Blocked], [Password], [ArtistName], [DVH], [ImgKey], [LanguageID], [ContractID], [Playbacks]) VALUES (N'e723f631-7508-4bf9-b437-45bdbdc20913', N'Andres', N'Perez', N'Andy', N'andres@hotmail.com', 0, N'81dc9bdb52d04dc20036dbd8313ed055', NULL, N'7ec4921c9bc9f7f32bafebed53e9b756', NULL, N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', NULL, 0)
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'd5db467c-0547-4c43-b02c-37a9ad42bc07', N'8190e0ab-98a7-45cb-8110-90f0e8343714', N'e723f631-7508-4bf9-b437-45bdbdc20913')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'd7868461-6b64-42cc-a750-4139e8447c0c', N'8190e0ab-98a7-45cb-8110-90f0e8343714', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'c76e8e38-f60a-44b7-95c0-4e56e90539a3', N'4382837c-0782-46cf-a1e5-56e64922b97e', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'c5fceca2-c9f7-48b3-80de-5571d0b5a668', N'4382837c-0782-46cf-a1e5-56e64922b97e', N'e723f631-7508-4bf9-b437-45bdbdc20913')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'712cec14-e2f8-4a87-980e-8146da91a8b5', N'd247a163-0802-44e2-a135-19fa11d658b8', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'87a9fd1c-063a-4b66-8303-8f3f27ccfe3b', N'078e46f0-3065-4272-bd28-1295ca302346', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'73922821-c390-42e6-936e-ab49c0372e26', N'3cc71b2c-7f92-4e34-8dba-3d9f74369a49', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'1fd8b0fe-03b4-4f64-864b-bbe99163a8a2', N'e4c115da-f568-4843-9116-276f0e947ec7', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'9d89b8cf-8393-46aa-a7ba-cc7b29d4d13f', N'a1494ad9-0aab-42d2-ab0d-7faf2410bd8f', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'978290d2-786b-4b57-a765-d2cd2eaac668', N'e4a76a8d-a0bd-484a-ae28-665a642ee024', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'06faa595-bc14-4bc6-8696-d8b44be9715b', N'1f2c411b-6415-47d9-90a9-6fed4fe0cdc6', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'dc99bbc5-5fe4-4b04-a64d-ddf311e80f3e', N'b2de0226-c029-417c-9355-5f0facda8f39', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'b4c8af00-3e5d-4b7c-a740-f7dd1f47bce6', N'317622bc-0b29-4734-9d7e-ec2a20aa015f', N'e723f631-7508-4bf9-b437-45bdbdc20913')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'a6846b83-45fe-43b0-8b29-fbc0ac13e38c', N'0d072b21-119d-48e2-ad23-1280eab671d8', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
ALTER TABLE [dbo].[Binnacle]  WITH CHECK ADD  CONSTRAINT [FK_Binnacle_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Binnacle] CHECK CONSTRAINT [FK_Binnacle_User]
GO
ALTER TABLE [dbo].[Dictionary]  WITH CHECK ADD  CONSTRAINT [FK_Dictionary_Language] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Language] ([LanguageID])
GO
ALTER TABLE [dbo].[Dictionary] CHECK CONSTRAINT [FK_Dictionary_Language]
GO
ALTER TABLE [dbo].[PermissionGroup]  WITH CHECK ADD  CONSTRAINT [FK_PermissionGroup_Permission] FOREIGN KEY([ChildID])
REFERENCES [dbo].[Permission] ([PermissionID])
GO
ALTER TABLE [dbo].[PermissionGroup] CHECK CONSTRAINT [FK_PermissionGroup_Permission]
GO
ALTER TABLE [dbo].[PermissionGroup]  WITH CHECK ADD  CONSTRAINT [FK_PermissionGroup_PermissionGroup] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Permission] ([PermissionID])
GO
ALTER TABLE [dbo].[PermissionGroup] CHECK CONSTRAINT [FK_PermissionGroup_PermissionGroup]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Language] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Language] ([LanguageID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Language]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_Permission] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[Permission] ([PermissionID])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_Permission]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_User]
GO
/****** Object:  StoredProcedure [dbo].[AddBinnacle]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddBinnacle]
	@UserID uniqueidentifier, @Description nvarchar(max)
AS
BEGIN
	insert into Binnacle
	(BinnacleID,
	UserID,
	Description,
	Date)
	values
	(NEWID(),
	@UserID,
	@Description,
	GetDate())
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetBinnacle]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetBinnacle]
   @DateFrom date, @DateTo date, @UserName nvarchar
AS
BEGIN
	SELECT b.BinnacleID,b.UserID,b.Description,b.Date from Binnacle b left join [User] u on
	u.UserID = b.UserID where u.UserName = IIF(@UserName IS NULL, UserName, @UserName ) and
	b.Date > IIF(@DateFrom IS NULL, Date, @DateFrom) and 
	b.Date < IIF(@DateTo IS NULL, Date, @DateTo) order by u.UserName asc	
END
GO
/****** Object:  StoredProcedure [dbo].[GetDVV]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetDVV]
AS
BEGIN
	SELECT DVV.DVVID, DVV.DVVHash, DVV.TableName  FROM DVV
END
GO
/****** Object:  StoredProcedure [dbo].[GetLanguageById]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLanguageById]
	@languageID uniqueidentifier
AS
BEGIN

	SELECT l.LanguageID, l.Name,l.Code, d.[Key],d.Value from Dictionary d left join Language l
	on l.LanguageID = d.LanguageID 
	where l.LanguageID = @languageID
END
GO
/****** Object:  StoredProcedure [dbo].[GetPermissions]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPermissions]
AS
BEGIN
	
	SELECT p.PermissionID, p.Name, p.IsGroup ,pg.ParentID as 'ParentID', pparent.Name as 'ParentName' FROM Permission p
	left join PermissionGroup pg on pg.ChildID = p.PermissionID 
	left join permission pparent on pparent.PermissionID = pg.ParentID
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByUserName]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserByUserName]
	@UserName nvarchar(50)
AS
BEGIN
	
	SELECT u.UserID ,u.UserName, u.Name, u.LastName, u.ArtistName, u.Email, u.Password,
	u.Blocked, u.ImgKey, u.ContractID, u.Playbacks, l.LanguageID, l.Code, l.Name as 'LanguageName'
	from [User] u left join Language l on l.LanguageID = u.LanguageID where u.UserName = @UserName
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserPermissions]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserPermissions]
	@UserID UniqueIdentifier
AS
BEGIN

SELECT p.PermissionID, p.Name, p.IsGroup into #results FROM Permission p
      join UserPermission up on up.PermissionID = p.PermissionID and up.UserID = @UserID 

SELECT p.PermissionID, p.Name, p.IsGroup, pparent.PermissionID 'ParentID', pparent.Name as 'ParentName' FROM #results p
	  left join PermissionGroup pg on p.PermissionID = pg.ChildID
	  left join permission pparent on pparent.PermissionID = pg.ParentID
	  where pparent.PermissionID in (select r.PermissionID from #results r) or pparent.PermissionID is null

	  drop table #results
	   
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN
	SELECT u.UserID ,u.UserName, u.Name, u.LastName, u.ArtistName, u.Email, u.Password,
	u.Blocked, u.ImgKey, u.ContractID, u.Playbacks
	from [User] u
END
GO
/****** Object:  StoredProcedure [dbo].[SetDVV]    Script Date: 2/13/2020 6:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SetDVV]
	 @TableName nvarchar(max), @DVVHash nvarchar(max)
AS
BEGIN
IF NOT EXISTS(SELECT 1 FROM DVV WHERE DVV.TableName = @TableName)
    INSERT INTO DVV(DVVID,TableName,DVVHash)
    VALUES(NEWID(),@TableName,@DVVHash)
ELSE
    UPDATE DVV
    SET DVVHash = @DVVHash
    WHERE TableName = @TableName
END
GO
USE [master]
GO
ALTER DATABASE [MusicGO] SET  READ_WRITE 
GO
