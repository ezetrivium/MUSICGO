USE [master]
GO
/****** Object:  Database [MusicGO]    Script Date: 7/20/2020 10:07:59 AM ******/
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
/****** Object:  Table [dbo].[Binnacle]    Script Date: 7/20/2020 10:07:59 AM ******/
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
/****** Object:  Table [dbo].[Contract]    Script Date: 7/20/2020 10:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract](
	[ContractID] [uniqueidentifier] NOT NULL,
	[HireDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[ServiceID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED 
(
	[ContractID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dictionary]    Script Date: 7/20/2020 10:07:59 AM ******/
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
/****** Object:  Table [dbo].[DVV]    Script Date: 7/20/2020 10:07:59 AM ******/
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
/****** Object:  Table [dbo].[Language]    Script Date: 7/20/2020 10:07:59 AM ******/
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
/****** Object:  Table [dbo].[Permission]    Script Date: 7/20/2020 10:07:59 AM ******/
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
/****** Object:  Table [dbo].[PermissionGroup]    Script Date: 7/20/2020 10:07:59 AM ******/
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
/****** Object:  Table [dbo].[Service]    Script Date: 7/20/2020 10:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/20/2020 10:07:59 AM ******/
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
	[BlockedDateTime] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 7/20/2020 10:07:59 AM ******/
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
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4ecdb538-c6c2-4242-ba4e-0031304c19d3', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T07:08:30.847' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'94e053b7-4d89-449f-8255-003b2ef232cf', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-09T20:57:57.677' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'442b2b7a-ad8c-42bd-ba75-0141392b4eeb', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-12T12:22:02.020' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'84afefa8-93df-4a89-9f81-015bf1ff87ee', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T23:03:22.743' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e8f177d4-b10d-46bf-8de6-039c436cf98b', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-15T09:04:28.080' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'bcb7c1f0-c975-4811-bc28-042cb2ea00bb', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T22:42:14.490' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd2a2cfb5-6511-4513-9228-04eefb55a5e9', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T07:08:30.800' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e884e7a9-9011-4cfe-8df6-0515b8025963', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T02:40:08.013' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4352552a-0dc2-43ef-8022-05851fc77f35', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:01:06.057' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5237f435-72e6-4953-95b3-0597a51a91d3', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T07:08:30.840' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7c633eb6-40c6-4817-a499-06b0fa68f3ce', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T15:32:24.253' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4678758a-416e-4976-a9d7-06ea77e58012', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T06:05:26.313' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8878c179-1c1f-43a2-98df-08be45f0c444', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-17T02:34:24.483' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ef2d7973-2192-4ef4-bdef-096f96e6a56c', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T12:26:49.807' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'938628a1-f9f7-41ef-9820-099df6bb6309', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T01:46:24.483' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e55e6eb9-995f-4106-9e0c-0a259629a19f', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T16:25:42.720' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'eb1d77e2-f92d-416f-b152-0b51c9ec6aaf', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-15T22:59:00.970' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8e0c2d52-ae1a-4442-8ba3-0b79eae91576', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T12:49:54.483' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5ac37d71-6175-424d-8198-0c03fb8a1024', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T02:32:34.117' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a1610c3a-6a91-4104-b09a-0c94bf09f512', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T13:06:04.763' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd34eab84-649f-4f93-b68b-0dacdd583640', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-12T12:22:02.020' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9a3fd67a-66d0-4085-8744-0de34407de91', N'L', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-02T18:46:50.417' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd5618fed-9f54-480e-b6c0-0e97c9562d82', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T05:42:17.247' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b1dccb42-5bdc-4a1c-8013-0faeff763e0b', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-18T16:41:09.653' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'bf319645-6f1e-465b-89ae-104e0ff9594d', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-02T19:02:07.323' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'39d87cfa-c9d9-49a8-a813-11f2024dd718', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T05:44:41.143' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4a451c9f-7899-4942-b094-13e16173a2da', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T05:12:36.610' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5defef18-245b-4ee8-8371-141fefc4c106', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T06:03:03.367' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'40a63039-8e82-4627-a015-157c7183d52c', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T15:41:49.967' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a28d2379-1ab8-436c-b82f-1612d563a55b', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T15:02:46.027' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd60d68c4-d559-45d3-8753-17498fe1827a', N'L', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-02T17:16:36.560' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f74fe5f0-0e9d-40ad-b05a-17c88bea8291', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T07:41:10.157' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd98e5825-6b9c-44ff-bf04-1808d67705dd', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T03:03:14.117' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'0e54da03-4d51-42af-9617-1880b28b87e9', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-15T09:07:19.160' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'cb9a70fc-d3e5-4c7b-937c-1883b16218fd', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-15T08:59:50.397' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'bc46e9f7-b1d7-4455-8ce4-1945b3c03fdb', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T06:03:03.383' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5d330676-b1ec-406b-818a-196dee47171a', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T06:05:26.297' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2996b447-5555-426a-b0a6-19875bf9391d', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-12T12:41:39.940' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9a80f6f7-6e0e-4170-beeb-19adf0ee6fd2', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T16:36:52.597' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'51a12a5f-771d-4aa0-99f9-1a60206121fe', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T16:36:52.567' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9385517e-f3a5-425d-baef-1a6f2d59c982', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T07:22:51.873' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'722129ec-8d8c-4b7e-85b7-1c20ac7e7d77', N'Login', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-06-03T22:59:01.177' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'207d3086-ebcb-4787-9d06-1c3985f84148', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T15:41:49.970' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'80fd01b4-4253-4ebb-9118-1e02e6e8811a', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T22:48:16.673' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'82401e33-3154-4699-bddc-1e540fced8de', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-15T22:59:00.970' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'85b20375-fad9-4dda-a6dd-1e8f36e511c5', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T03:03:14.160' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e3327e7a-70e9-40f2-abb6-1ec2a8e7bf0f', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T23:04:16.530' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'c853b9b5-9fb6-4433-96af-1eca19901505', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-12T03:11:01.370' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e07990cc-96f6-4bd3-8e70-21711179c8f7', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-12T03:12:36.910' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8864f2cf-e3c9-4265-b79b-220f762a7c57', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T01:30:49.547' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'c225da3a-d188-405c-8798-2241c546caac', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T01:46:24.433' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'211b5880-aa93-47fd-8bff-23bd42432028', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T15:41:49.973' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9a708fcb-e146-46a9-977e-26e98c6b6f25', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T14:42:00.450' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'86556f7b-3a3c-4bb8-a51a-27d41a5741bb', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T05:30:26.280' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'30323501-c6df-41e6-9298-299b0e3f010e', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-17T02:34:24.477' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'71d7781e-62a5-4575-a584-29de22d85959', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T05:44:41.127' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'cd53522d-5d07-417a-b867-2a67d10b7804', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T07:22:51.830' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e11e2cf5-36a4-45b9-9ebf-2af82c09e566', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T12:10:05.407' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd60a8bda-df63-473b-b1f2-2becf4c48823', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T05:44:41.150' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6a9e62db-0512-45d3-9721-2caaf4656746', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T06:56:58.860' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9389bd1d-d750-41f9-b639-2cc0177b5cf7', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T23:04:01.680' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'74f61e4a-bed7-4c68-a223-2ddda8661f01', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-15T13:39:04.160' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a42ef07e-6574-450b-af96-2f5f180c827f', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T07:22:51.880' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'bda5a3dd-59c7-48bd-80ce-2f7eeab88f1a', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T10:41:11.307' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b75962e8-44af-4d16-a6b3-2fa12f0486eb', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:01:50.973' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'14fbebf1-8a3d-4c34-9e6f-2fbcb6e83667', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-18T16:08:23.717' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f703e621-8b47-4093-8e4a-309f7319886c', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T02:40:08.020' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'64a817f2-5fd9-4db6-b707-310636e8ab05', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:18:26.673' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8c556941-91e5-4e68-aaa6-31194d1411c2', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-12T12:27:22.147' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd51a86e0-1b88-4563-93e6-3178258780c7', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:24:25.183' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'056e5902-fb62-4dfe-a8bd-318098a45262', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T16:39:58.597' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'82c1e630-9058-418e-b006-31991aeea0e7', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T22:42:51.467' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7552eb25-b08c-4d05-9527-31f123877a08', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T16:38:51.730' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a82edf0c-2053-4533-a1f1-324d9d1a1d30', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-12T03:14:13.037' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'865de1a9-dc60-40ad-9931-33898cde3cd4', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T07:38:35.657' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9d9207f6-f847-48d1-b6ad-343735ae9b25', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-15T09:04:28.080' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'777182cb-e2ab-490b-9782-348ce9b69c9c', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T11:54:24.563' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9d2d0db2-44f6-4f44-a7e6-34a19c5b9599', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T00:07:46.900' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'68502762-2e7d-4e6e-bd01-372784b54c01', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-06-15T13:07:30.407' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'56a03837-65fa-4267-b1ef-3752705f434d', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T01:26:07.533' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'85cebef6-f544-4c11-9af5-381c013b2c04', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T06:56:58.910' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd420198b-612e-4404-8bb2-3841d5acab7c', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-15T22:59:00.973' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'24dcbb9c-4b59-4c3d-9c5b-38552ee267f3', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-08T14:45:00.113' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e5de3d79-661c-403c-89b6-3a279fdfc6b0', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-15T22:48:07.857' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2eb62bce-ae39-49f6-9731-3bf04443484c', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T06:56:58.913' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b47de16f-abf3-43f5-9a94-3ce61502951d', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T16:25:42.713' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'bc9c6a4d-f95e-42f7-a178-3d92a7cdbbe2', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T15:45:18.150' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'40c7c158-8c2a-4668-b96c-3e72020304b9', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T23:07:20.717' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9288a869-3fe5-41c5-93f5-3ff9f496a571', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T06:03:43.407' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4bc5ec31-70e2-426c-ba50-3ffa1bbf3bf3', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-03T23:22:35.133' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f5087da4-fa8d-4e36-9a49-402db8bd6c60', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T05:40:58.123' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'1b513d04-c099-4932-9282-40737888c23a', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T02:45:26.957' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'fc25fdfb-def5-450e-9b42-40cc427b27e5', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:22:55.557' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5a0e87ad-0707-4bc7-be5d-40fc9f8850a4', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-12T03:14:13.033' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2cf0ae17-a4d5-4489-a6c5-433407545b79', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T14:40:48.093' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e71ea20f-06ad-4650-9d1e-43a9a17a35c7', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T05:44:41.147' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ad2539af-01c8-4316-b3a2-43b738ffaa3e', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-03T23:24:55.020' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b03e9503-e8e6-4dca-90d2-446baaeb4071', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:16:54.913' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7bfbecec-6c8b-4d31-924d-455c8f7165ed', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T05:30:26.263' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'236f7ef0-69b8-488e-afb6-4570817b8347', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T15:45:18.190' AS DateTime))
GO
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'584d8564-d862-495e-a79a-45ac9a574143', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T07:31:17.707' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6a2a6cd4-6753-41ad-b203-4750e6c4617c', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T12:18:22.797' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'57f5c7e5-7ac8-4fa5-bc36-47ab966b669f', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T16:39:58.600' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'c26ebac9-d838-41a7-bbd1-48199516f65a', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-15T09:07:19.160' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a38c1c27-66d5-4d78-ba23-48990387ba86', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T16:25:42.713' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd113195b-a6e2-413c-942a-48f095566bad', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-06-12T02:04:28.643' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'800522e1-faeb-4a18-8f07-4992bd748228', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T06:07:39.660' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'84f9ae00-b524-4c34-a59c-49b5da769a85', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-12T12:27:22.153' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ab17024b-fe91-4652-81e1-4cb0837b6619', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-12T03:12:36.907' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5f2cd0b3-65ee-4761-afa3-4ce345490242', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T05:57:19.167' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ba695e7f-9b58-44be-b71d-4e4ce49d9558', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T06:41:18.167' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a432709d-e346-462c-84a4-4ec41cc468e0', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T07:43:13.930' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2d1572f5-5345-4d66-8d75-50b368d674d0', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-17T02:34:24.370' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ce7b12f3-d962-4377-9b20-5230be72f12c', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-15T22:59:00.960' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4f7acd41-b093-4815-a130-53279a2bb666', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T02:45:26.950' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'03d34ec9-017f-4c9d-a018-561f16cbbbc0', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-12T12:41:39.937' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'00c71446-d97c-4df0-884e-57b8b564fff8', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-12T03:11:01.373' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5612dfb3-0c98-4b38-99b8-595ac6a74c56', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T12:24:03.487' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'08f5af09-10bd-49c3-9479-5975675a33f8', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T05:57:19.183' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'558e2904-199f-4995-9ea4-597b0df23b00', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T14:24:45.003' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'02054899-28ac-46df-b101-599830524c64', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-15T08:59:22.610' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e45f76c0-2333-4646-b49f-5afb9934f5ed', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T15:45:18.183' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e60e7ce7-db41-488e-866d-5ef92e526d5c', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T05:12:36.617' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'c7ab42c6-c882-43c8-93e0-5f25dba3aea7', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T00:07:46.910' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6c5cc976-df32-4b59-afb1-6231a68aa89b', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T23:09:12.767' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9696aa20-f7a0-41f0-b360-6241c429d0ff', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T12:25:04.293' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4f3b4147-0221-4fab-8b4f-63b206d36e2e', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:34:15.617' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'78611667-3a9f-4cb9-881a-64210cd28bd0', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T22:55:17.700' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'35f0b183-d87d-40ff-98f0-645d2f69042c', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T07:31:17.660' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'903ebc79-d331-4f26-9749-64a27b8f5a0d', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-01T23:56:30.240' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ce65af76-b891-42c0-993b-6611c916ff3b', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-18T16:55:23.963' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'eb42d018-7be1-4bbd-ae7a-67326bca0396', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-15T08:59:50.393' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd0357fc7-e80a-4905-a53e-69b15065d6b1', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-15T22:48:07.857' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'947990bb-0819-4504-b681-6a912af24120', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T06:03:03.390' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'745ade47-5dc9-4330-8309-6bd06f7eb784', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-12T03:14:13.040' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a1550ece-3b02-43a9-81b1-6da56d80d0c9', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T19:54:11.473' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'eff28fe2-1464-44c8-84e7-6f745491e536', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:25:18.863' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'fccacd5b-f0db-4f35-992b-70dd4ccb0812', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T06:56:58.917' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'56713936-cbcd-4010-b208-730be0830cde', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T00:57:24.160' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7419e871-83f8-4cb2-b922-73e0ca4db69c', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T02:32:34.120' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ebfe05a8-b71c-4441-91aa-75209dedfdd3', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T07:41:10.160' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd64f966a-4a69-4edb-8b7a-754afe7f9346', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T06:41:18.170' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6f50e85a-33a5-4494-a18e-76121eb807aa', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-15T09:04:28.073' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6a5146c9-d549-4a84-9e68-7664d178cc70', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T06:05:26.310' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f7642ace-1357-4721-914f-772e5311069f', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:34:20.257' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9bbf7b73-e715-4dd9-b426-77a2522f5985', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T02:45:26.970' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b832997c-d5d6-4b33-a195-77d81ef30d0b', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T05:42:17.250' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'df28d0d6-74a2-46b1-937c-77e997043ce6', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:47:07.143' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'1ed49fbf-b749-4cc5-86c1-7890b4152a84', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T07:43:13.910' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'dcf33be7-bb8f-4045-8d35-7adc8881a7f0', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T16:25:42.667' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'33f04e9b-06bc-4fb9-9a80-7b063c504d63', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T15:41:49.927' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6b9e48ed-9652-4f18-a91b-7be5d811eca0', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T17:09:50.913' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'75490b57-11f4-4320-9f64-7cc40bf8ba14', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-12T12:27:22.153' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e37a72a6-dea9-4919-b84f-7ccd73e1fe90', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T23:18:11.177' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'82c773b9-15b7-4b60-a4a7-7ceb711c5733', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T23:18:07.693' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'02cccec5-8559-4aa8-a112-7fd187c70b31', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T01:26:07.533' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'548a64f9-0290-4011-9595-822ba597c442', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T05:42:17.250' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b85bd7ee-6382-4880-a08b-823d6e39b8b3', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-10T16:08:21.780' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'fb56f044-fdcb-4b80-9a7b-824f4844ebfc', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-15T22:48:07.843' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b4261e31-3d2f-4dba-9a6a-82e5e5ef4064', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-15T13:39:04.187' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'30c10572-7c79-4425-8ae4-834f1e0012ba', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-12T12:27:22.150' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'437a0b18-6d12-41bb-bf03-8490dd26a8f7', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T05:40:58.163' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e76136aa-a07d-4824-9ffc-84ec1945b6a0', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T23:10:26.690' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ce95908b-4368-4fcc-94b5-85107bdf4518', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T01:46:24.480' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'811a200c-5a6a-4a76-9d0f-85bf55127014', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T03:08:40.863' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e9bfec73-e133-4d9c-99d2-863001b0b287', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T03:08:40.870' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'1bb4af17-c7ed-45e6-98ee-86f4442901ce', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T02:32:34.123' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4ea7db84-1eea-4eaa-bb8f-872c3464e41e', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T11:57:42.233' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'971b8037-2175-446a-8989-8a293080e916', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T02:45:26.907' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'68ae749f-7930-4c99-8739-8b086a918126', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T13:13:37.343' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'dea5cbba-43d0-4a16-850c-8e4cc507050f', N'Login', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-06-03T22:42:50.993' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'00616691-9074-48d1-9fdb-8edfb45b4992', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-15T22:48:07.857' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ab56584f-2efb-4fef-afce-900067c4cf8c', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T07:43:13.927' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9ebcc952-8511-4e32-aceb-905b18bad65d', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T01:01:54.980' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'fb1d69cd-30e6-4933-ae11-9208388375eb', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T06:07:39.643' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'447ab208-4229-409a-9010-932da34d87d4', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T07:41:10.127' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'0d4cb2e9-b7ca-4aa8-85af-93f362aa0162', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:08:28.017' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7fd4b38e-5ae5-44e9-a164-94a25c5ed5a1', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T07:38:35.650' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f5a5bbd4-239a-47bd-8c52-95f03667b8b5', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T12:12:39.897' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'516b27a3-1c65-4a31-9d19-964ff996a774', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T07:41:10.150' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2c5a5fb9-47d4-4d62-ba5d-9708454ac6e6', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T23:22:28.250' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8a2c43ba-84b9-47e3-8c3f-9771e25518bc', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T16:39:58.557' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'75bf3c25-072b-4c3d-9795-995a871f6977', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T22:44:32.620' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f93af847-e898-4f3c-bd51-99af4315ecfe', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-12T03:11:01.373' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'c05cd5f4-24aa-4da6-9bf4-9b8780b35090', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:08:08.500' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b24796e7-6cdb-431c-8620-9c6fe8b7b5ce', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T07:38:35.653' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'150fe996-3c78-449f-9cf5-9d4242cac40c', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:47:23.037' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'adba12e4-dd70-4509-b1dd-9ddaef8e00d6', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-15T09:07:19.120' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'325e9d17-7bf2-4419-b556-9e940fd9fcb8', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-12T03:11:01.320' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'3ad9502b-74e1-443c-b4be-9eb9688c2a90', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T23:09:53.687' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6462e6d3-b795-471b-b6ae-a0611b74f5de', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T01:30:49.593' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7f85bbf9-7eb9-445a-84ea-a0895659ec49', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-08T11:31:38.553' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a006d1ce-6d37-45e1-b3a5-a093527c5e71', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T03:08:40.867' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e478361e-e348-4f40-bfb2-a3429ddfb99f', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T23:22:31.857' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'1343ace7-7d5c-4e36-aa97-a369a807a5f6', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T23:04:01.697' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f28d388a-07a6-40c3-ab61-a3cce7bb3105', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T03:08:40.823' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'402fcb26-a421-4419-bfde-a4167bbe6e82', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-15T08:59:22.640' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ea6024ef-6877-4f43-b3af-a45ba45a7f14', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T07:43:13.933' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'52837cd2-ac02-470b-b281-a88efa845fb1', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:38:22.300' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'161b5bf4-e3ef-44f9-b831-a931d456ecad', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T02:32:34.073' AS DateTime))
GO
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6f0b5bef-3765-4b5c-b9bc-aa8983b4f4fb', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T14:37:44.970' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'dbb0dadc-2e1e-4209-9674-aab1edb3c1a9', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T01:02:09.423' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8494b2d3-71a9-4654-9485-ab58c19f99fc', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T16:39:58.600' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6373647e-0628-42fa-bc8a-ad5052b74d85', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T00:07:46.903' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'0f057ae4-9326-4529-a526-ae6d72a013af', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:13:32.463' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6b75379b-e59d-4041-9b4b-ae7632e163fd', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T01:30:49.597' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5582245c-ca32-4867-91bd-aec0d954c9b1', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T03:03:14.150' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7b32ca4a-a069-4943-85f6-b00f0c4587b1', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:42:13.693' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'519991a4-dbf5-4d1d-bb6f-b0983dc2eae6', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T15:48:59.487' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e31b7f82-2bad-42d2-ad54-b1d31d0130ce', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-01T23:56:31.443' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'36412fd2-5c1b-4ec8-8131-b1e1e5482b88', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-18T23:26:58.737' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ccf01428-752c-4f25-9538-b2cf1f91806a', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-15T09:07:19.157' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7131d546-6538-440c-a02b-b337cc3a40f9', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T13:16:18.363' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'386da6ab-6adb-4e71-8674-b3d8847dfa12', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-10T16:42:14.613' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9e5121e7-382f-4bb8-a8be-b52b664c0812', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T07:31:17.700' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'c20cbd59-5820-4a0d-9d8f-b543029f7144', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T05:40:58.170' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9e139145-a603-4950-9fb8-b76f757659a6', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-15T13:39:04.190' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b4e2c319-d1df-4ab0-a917-b8b39de22b67', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-08T11:44:50.753' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'0345d0a8-600e-4b2d-a961-b9b356da23c0', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T06:41:18.160' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'cc6e7ab6-dbb6-4f35-a00a-ba41df0200fa', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T03:03:14.147' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2a6dcd4f-6407-485b-8e97-bb99e0afd3e3', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T06:07:39.663' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'c6a8e964-d02c-46fb-a8c7-bc85de7acbe0', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T05:40:58.167' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'28e4f4c2-ee68-47b6-a2b3-bd78245b60e0', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-08T15:03:54.257' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'a21e68c4-8bd8-4163-a3d3-be8f41e04171', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T01:46:24.487' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5da4a59e-9e5e-4df1-bac6-c016239c6942', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-15T13:39:04.187' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f60ca24e-3e7f-4fd9-8324-c0e284ff846e', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-12T12:41:39.930' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'82dcd75f-843f-4215-b814-c0ffa8851900', N'Login', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-06-03T23:04:16.507' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'3dc330e9-7fa7-436f-8e66-c38a0c6da7af', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-15T09:04:28.080' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'75dc808f-c768-41c5-b379-c3978d334b72', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T00:07:46.857' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'13f22404-b5a1-440f-8804-c3c49ba7be83', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:08:28.453' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ea40f585-5940-4cd4-be60-c40c38b1b969', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-18T16:34:57.100' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ff78cb6a-08b6-449f-b717-c4292ecbc36b', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T06:03:43.430' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'022f4fb1-fba0-485a-aab0-c4d91918e6c7', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-12T03:14:13.010' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b9acb3be-778e-4e7d-9ed1-c54c857ef6c6', N'Login', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-06-03T22:42:14.400' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6a0347eb-60f1-42ac-b1bc-c771c9820b20', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T01:30:49.600' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'05c0dcc3-fd99-4bea-a8e8-cab88362edc1', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-12T12:22:02.020' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'69601ae4-2528-4265-bdd5-cb4da19867b5', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T07:31:17.700' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'195e99f3-0b62-4962-a0e0-cc2b8923216b', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:18:08.453' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'3e5701ec-5597-4df0-b7ed-cc79157c5bc9', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:11:03.213' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e5dec90f-9f37-4e3d-9066-ccc081af1cd1', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T23:04:27.890' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'c291d14f-58d4-4892-b609-cdff71f1d610', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-03T02:11:03.260' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b7d6a53d-4e8d-4f91-b31b-ce6b84281278', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T02:40:08.017' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'08203b9e-c0d8-4883-b353-d16c04868929', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T06:41:18.087' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd99c6e62-7ae9-48b3-9e33-d1ff993129d8', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:15:33.417' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd817a675-c2ee-4bed-829a-d2282b6243cb', N'Login', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-06-03T22:43:23.027' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'88efe4ff-ce28-4e7b-ace2-d25a48c15a3c', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T05:30:26.280' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2abab78b-23cc-4727-a6d9-d2e857346617', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-13T10:27:28.777' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'617a55fd-74f1-47d8-9ab2-d482d370de2b', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T06:05:26.317' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'4f782f2f-0a3d-4cbb-abb3-d49854679f6b', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:21:33.970' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7da93a40-b898-4617-863f-d57ff3abca82', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T05:12:30.090' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'0af61d21-6e90-4a81-a508-d5aefcc438d9', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T23:14:12.087' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ad2c2238-fdeb-4e8c-b1d8-d6f80b70b638', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-18T16:28:47.820' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8b1d1dfe-801d-4f2c-b637-d75673e8bd12', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:11:38.470' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9f33aea6-0c7b-4c5b-a548-d8670705b33c', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T05:57:19.190' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'b4030887-c6df-4ced-b963-d8d0b5453212', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-17T02:34:24.480' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9c8a1347-4459-4d9a-a9d9-da6d5c4476d4', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-09T19:15:34.210' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'42e62d7e-2e71-4cc9-80ee-db7f2fe1c020', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T22:48:09.600' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2af036cf-84cc-475e-a802-db974bd56183', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T06:03:03.380' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9efe8b8a-bbe2-43df-89f9-dd1cb3069ec8', N'UpdatePassword', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-03T02:22:49.773' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'0d09b12f-8238-41c2-86e4-e10894fd49dd', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T05:30:26.273' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f9c6094e-a5d9-41aa-b2b8-e12dda28dfc0', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-15T08:59:22.643' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'de85c2fd-6fca-482d-beda-e22324426ad2', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-11T02:40:07.963' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8008567a-f18c-47db-8381-e227ab8d7710', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T05:57:19.187' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6dab3fe2-fc8e-4fcf-870e-e239a09d0686', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-10T01:04:05.027' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'64cd58d8-837a-486f-b956-e2bd5921a339', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-11T16:36:52.597' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd4ca3cbe-2b3f-4a27-ba59-e2c192ca872f', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T23:14:01.773' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'5ed74223-4cc9-4fd0-a215-e2e59420bb03', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-03T02:22:05.107' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e511a6e4-3949-4fec-9f5c-e3c4c354dbbd', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-12T03:12:36.910' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'096fdb81-f28e-4858-a6e1-e481af3e1b46', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:02:28.947' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'd88d4380-df56-407b-a32c-e4e2db77001f', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-11T15:45:18.187' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ce0d957f-76b8-4b2d-8ebf-e5639014dd0b', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-11T16:36:52.600' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'8e07e190-7d25-4c7b-a022-e6881e8b984c', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T07:38:35.633' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'232b01ab-e4ae-44f4-b62c-e7363636d8fc', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T15:49:38.550' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'0221e71d-355d-4989-9741-e7d5a4388e7a', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T06:03:43.427' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'79e36fa2-ad3e-4560-a951-e829108a93aa', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T07:08:30.843' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6b54a745-5769-4e46-a263-e8bb1f0d005c', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T05:12:33.950' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'3adcec68-0490-4ed1-8edf-ea8af5112b10', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T13:19:10.707' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'736bf4a3-0b08-4fbd-ad8d-eba4d604389d', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T15:35:42.790' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'62009a4e-db53-4693-ac92-ebdd57cc072d', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T23:03:22.760' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'eea3c512-49b3-40fa-bb3f-ec22aae187b1', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T16:39:28.503' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'248cdd19-bf8c-4221-bc8e-ecb3cb83806b', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-08T15:09:10.907' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'725e1e70-1638-4c3a-b0aa-eccf578d0361', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-15T08:59:50.400' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'c5035b56-6ee8-4f88-92c9-ef29e8ac359b', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T22:43:34.967' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'1bae3490-41a4-4755-bba4-ef847c962a96', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-15T08:59:22.643' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'aedc8929-23cc-4ce0-b476-f02fe308ba7a', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-12T12:22:01.983' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'bd8edf4c-c534-4c56-938f-f0a911208b95', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-07T07:22:51.877' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'69ca3ff0-d3c1-4399-9b8e-f14282278996', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-15T08:59:50.380' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ab54da2d-0644-4602-bd37-f257318deb0a', N'Login', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-06-03T22:55:17.683' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'332d25a0-b5c9-48a5-a623-f2b1dd9970da', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T22:44:31.960' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'e38e567c-26da-458b-a5a7-f328ada981c0', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:08:08.133' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'ba70cc74-b12a-4fd1-9357-f35d3392e4c0', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-02T00:18:26.253' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'27b037a9-4267-448f-a271-f3878e1d4cd5', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-12T03:12:36.887' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f786aa3d-8ac1-4c9a-a0bb-f3f8f49ddc31', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-03T00:52:10.953' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'2935404b-c444-48bb-a059-f6773ef67caf', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-03T23:04:27.880' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'c0ac584a-68fd-442b-a414-f73b451a6b47', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-03T22:59:01.190' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'96966b3f-59ca-456e-8d1b-f7708ac03d76', N'Email to Reset Password', N'e723f631-7508-4bf9-b437-45bdbdc20913', CAST(N'2020-05-18T16:24:13.430' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'9ce32852-e914-45a2-9880-f89c7555eb11', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-06-07T05:42:17.230' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'7fec64b4-5a68-444d-bb94-f942df61f0e4', N'El número de registro modificado es: 0', NULL, CAST(N'2020-06-12T12:41:39.940' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'6fb54fe6-0377-4227-b59e-f95e206cc242', N'El número de registro modificado es: 1', NULL, CAST(N'2020-06-07T06:07:39.667' AS DateTime))
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'f1e9668d-cc05-4a28-b11f-fc168d3f4e34', N'Login', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', CAST(N'2020-02-11T17:09:13.143' AS DateTime))
GO
INSERT [dbo].[Binnacle] ([BinnacleID], [Description], [UserID], [Date]) VALUES (N'78ba3c07-93a9-4742-a410-fc9c70982b3c', N'Se encontro inconsistencia en base de datos en: UserDAL', NULL, CAST(N'2020-06-07T06:03:43.423' AS DateTime))
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
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'3fdb0c54-933a-4eb7-b761-579ef1bbb2e6', N'UpdateUser', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'b2de0226-c029-417c-9355-5f0facda8f39', N'CreatePermission', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'e4a76a8d-a0bd-484a-ae28-665a642ee024', N'Permissions', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'2768a7a9-5119-4f44-853c-6fd96785bc9f', N'Artist', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'1f2c411b-6415-47d9-90a9-6fed4fe0cdc6', N'ModifyPermission', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'747302ab-7eb6-4b4e-bc51-7948843a4ad1', N'DeleteUser', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'a1494ad9-0aab-42d2-ab0d-7faf2410bd8f', N'DeleteSong', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'8696d7f0-4880-41c7-8fac-80f6f6b682dc', N'Users', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'0380110f-4d1c-467a-9952-8d54ee3cf215', N'Admin', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'b40dc25e-6d6f-4a51-af47-8ec44f0c81b6', N'AddUser', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'8190e0ab-98a7-45cb-8110-90f0e8343714', N'Play', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'317622bc-0b29-4734-9d7e-ec2a20aa015f', N'User', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'253fdf6c-dad8-4add-80a6-f07f0afccef4', N'GetUsers', 0)
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'438820d2-3be6-4d3d-a55a-054e4c5a0544', N'e4a76a8d-a0bd-484a-ae28-665a642ee024', N'b2de0226-c029-417c-9355-5f0facda8f39')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'd25b7f90-90a9-4d0f-93c6-1684616f8731', N'd247a163-0802-44e2-a135-19fa11d658b8', N'8696d7f0-4880-41c7-8fac-80f6f6b682dc')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'2be25689-d62e-4fb9-b2ff-408d0e71532f', N'2768a7a9-5119-4f44-853c-6fd96785bc9f', N'078e46f0-3065-4272-bd28-1295ca302346')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'7b3481d3-e96d-4be1-85d2-59d753e1e5d5', N'078e46f0-3065-4272-bd28-1295ca302346', N'8190e0ab-98a7-45cb-8110-90f0e8343714')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'9f342156-0faa-4aad-8dcb-64a21153aa07', N'078e46f0-3065-4272-bd28-1295ca302346', N'e4c115da-f568-4843-9116-276f0e947ec7')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'15196028-416e-4499-8ab0-788eba4a3b6d', N'078e46f0-3065-4272-bd28-1295ca302346', N'3cc71b2c-7f92-4e34-8dba-3d9f74369a49')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'ef6c3adf-f87e-49bc-bfc2-7fa648227388', N'e4a76a8d-a0bd-484a-ae28-665a642ee024', N'1f2c411b-6415-47d9-90a9-6fed4fe0cdc6')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'9960f581-ddd6-4502-8b1d-7ffe115f6cf9', N'8696d7f0-4880-41c7-8fac-80f6f6b682dc', N'b40dc25e-6d6f-4a51-af47-8ec44f0c81b6')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'ab8fe338-a420-4f5f-ba8c-910285da3f60', N'd247a163-0802-44e2-a135-19fa11d658b8', N'078e46f0-3065-4272-bd28-1295ca302346')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'fd26fd4b-6457-4578-92cd-a7b8e60dddc9', N'8696d7f0-4880-41c7-8fac-80f6f6b682dc', N'253fdf6c-dad8-4add-80a6-f07f0afccef4')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'48e20eff-f14a-4149-a481-af16a1c1b1da', N'0380110f-4d1c-467a-9952-8d54ee3cf215', N'253fdf6c-dad8-4add-80a6-f07f0afccef4')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'9a293b28-2b2e-4acd-afb0-c6aade39e1e6', N'8696d7f0-4880-41c7-8fac-80f6f6b682dc', N'747302ab-7eb6-4b4e-bc51-7948843a4ad1')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'8becfede-5a84-40f3-b295-c95233c816cd', N'd247a163-0802-44e2-a135-19fa11d658b8', N'e4a76a8d-a0bd-484a-ae28-665a642ee024')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'cbef5950-321e-4812-9c82-d8abd7897510', N'078e46f0-3065-4272-bd28-1295ca302346', N'a1494ad9-0aab-42d2-ab0d-7faf2410bd8f')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'44937d4f-07f2-4a26-868f-dc740cd912e6', N'8696d7f0-4880-41c7-8fac-80f6f6b682dc', N'3fdb0c54-933a-4eb7-b761-579ef1bbb2e6')
INSERT [dbo].[PermissionGroup] ([PermissionGroupID], [ParentID], [ChildID]) VALUES (N'20e22ea0-cf5e-463a-a310-f7a184a3db08', N'e4a76a8d-a0bd-484a-ae28-665a642ee024', N'0d072b21-119d-48e2-ad23-1280eab671d8')
INSERT [dbo].[User] ([UserID], [Name], [LastName], [UserName], [Email], [Blocked], [Password], [ArtistName], [DVH], [ImgKey], [LanguageID], [ContractID], [Playbacks], [BlockedDateTime]) VALUES (N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', N'Ezequiel', N'Fernandez', N'Ezefer', N'ezequielf10@hotmail.com', 0, N'81dc9bdb52d04dc20036dbd8313ed055', NULL, N'0c0074ad522c26e115d28fb94091a7ed', NULL, N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', NULL, 0, NULL)
INSERT [dbo].[User] ([UserID], [Name], [LastName], [UserName], [Email], [Blocked], [Password], [ArtistName], [DVH], [ImgKey], [LanguageID], [ContractID], [Playbacks], [BlockedDateTime]) VALUES (N'e723f631-7508-4bf9-b437-45bdbdc20913', N'Andres', N'Perez', N'Andy', N'ezequielf10@hotmail.com', 0, N'81dc9bdb52d04dc20036dbd8313ed055', NULL, N'7ec4921c9bc9f7f32bafebed53e9b756', NULL, N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', NULL, 0, NULL)
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'712cec14-e2f8-4a87-980e-8146da91a8b5', N'd247a163-0802-44e2-a135-19fa11d658b8', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'067329bd-5a46-4c1a-8be8-d6cba6672f6e', N'4382837c-0782-46cf-a1e5-56e64922b97e', N'e723f631-7508-4bf9-b437-45bdbdc20913')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'e81504c4-bf2f-43ac-97d0-f52ed42c07ef', N'4382837c-0782-46cf-a1e5-56e64922b97e', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'b4c8af00-3e5d-4b7c-a740-f7dd1f47bce6', N'0380110f-4d1c-467a-9952-8d54ee3cf215', N'e723f631-7508-4bf9-b437-45bdbdc20913')
ALTER TABLE [dbo].[Binnacle]  WITH CHECK ADD  CONSTRAINT [FK_Binnacle_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Binnacle] CHECK CONSTRAINT [FK_Binnacle_User]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_Contract_Service] FOREIGN KEY([ContractID])
REFERENCES [dbo].[Service] ([ServiceID])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_Service]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_Contract_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_User]
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
/****** Object:  StoredProcedure [dbo].[AddBinnacle]    Script Date: 7/20/2020 10:08:00 AM ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 7/20/2020 10:08:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteUser]
	@userID uniqueidentifier
AS
BEGIN
    DELETE FROM [dbo].[User] 
      WHERE UserID = @userID
END
GO
/****** Object:  StoredProcedure [dbo].[GetBinnacle]    Script Date: 7/20/2020 10:08:00 AM ******/
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
/****** Object:  StoredProcedure [dbo].[GetDVV]    Script Date: 7/20/2020 10:08:00 AM ******/
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
/****** Object:  StoredProcedure [dbo].[GetLanguageById]    Script Date: 7/20/2020 10:08:00 AM ******/
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
/****** Object:  StoredProcedure [dbo].[GetPermissions]    Script Date: 7/20/2020 10:08:00 AM ******/
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
/****** Object:  StoredProcedure [dbo].[GetUserByUserName]    Script Date: 7/20/2020 10:08:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserByUserName]
	@UserName nvarchar(50)
AS
BEGIN
	SELECT u.UserID ,u.UserName, u.Name, u.LastName, u.ArtistName, u.Email, u.Password,
	u.Blocked, u.ImgKey, u.ContractID, c.HireDate, c.ExpirationDate, c.ServiceID, s.Name as 'ServiceName', s.Description,
	s.Price, u.Playbacks, l.LanguageID, l.Code, l.Name as 'LanguageName'
	from [User] u left join Language l on l.LanguageID = u.LanguageID 
	left join Contract c on c.ContractID = u.ContractID 
	left join Service s on s.ServiceID = c.ServiceID
	where u.UserName = @UserName
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserPermissions]    Script Date: 7/20/2020 10:08:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserPermissions]
	@UserID UniqueIdentifier
AS
BEGIN

SELECT p.PermissionID, p.Name, p.IsGroup FROM Permission p
      join UserPermission up on up.PermissionID = p.PermissionID and up.UserID = @UserID 

--SELECT p.PermissionID, p.Name, p.IsGroup, pparent.PermissionID 'ParentID', pparent.Name as 'ParentName' FROM #results p
--	  left join PermissionGroup pg on p.PermissionID = pg.ChildID
--	  left join permission pparent on pparent.PermissionID = pg.ParentID
--	  where pparent.PermissionID in (select r.PermissionID from #results r) or pparent.PermissionID is null

--	  drop table #results
	   
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 7/20/2020 10:08:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN
	SELECT u.UserID ,u.UserName, u.Name, u.LastName, u.ArtistName, u.Email, u.Password,
	u.Blocked, u.ImgKey, u.ContractID, u.Playbacks, u.DVH
	from [User] u
END
GO
/****** Object:  StoredProcedure [dbo].[SetDVV]    Script Date: 7/20/2020 10:08:00 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SetUserPermission]    Script Date: 7/20/2020 10:08:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SetUserPermission]
	@userID uniqueidentifier, @permissionID uniqueidentifier
AS
BEGIN

	INSERT INTO [dbo].[UserPermission]
           ([PermissionUserID]
           ,[PermissionID]
           ,[UserID])
     VALUES
           (newid()
           ,@permissionID
           ,@userID)
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 7/20/2020 10:08:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUser]
		@userID uniqueidentifier, @name nvarchar(50), @lastName nvarchar(50), @userName nvarchar(50),
		@email nvarchar(50) = null, @blocked bit , @password nvarchar(32), @artistName nvarchar(50) = null,
		@imgKey nvarchar(max) = null, @languageID uniqueidentifier, @contractID uniqueidentifier = null,
		@playbacks int

AS
BEGIN
UPDATE [dbo].[User]
   SET [Name] = @name
      ,[LastName] = @lastName
      ,[UserName] = @userName
      ,[Email] = @email
      ,[Blocked] = @blocked
      ,[Password] = @password
      ,[ArtistName] = @artistName
      ,[ImgKey] = @imgKey
      ,[LanguageID] = @languageID
      ,[ContractID] = @contractID
      ,[Playbacks] = @playbacks
	  ,[BlockedDateTime] = iif(@blocked = 1,GETDATE(),NULL)
 WHERE [UserID] = @userID
END
GO
USE [master]
GO
ALTER DATABASE [MusicGO] SET  READ_WRITE 
GO
