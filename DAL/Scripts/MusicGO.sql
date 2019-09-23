USE [master]
GO
/****** Object:  Database [MusicGO]    Script Date: 22/9/2019 10:40:11 p. m. ******/
CREATE DATABASE [MusicGO]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MusicGO', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MusicGO.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MusicGO_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MusicGO_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MusicGO] SET COMPATIBILITY_LEVEL = 140
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
ALTER DATABASE [MusicGO] SET AUTO_CLOSE ON 
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
ALTER DATABASE [MusicGO] SET  ENABLE_BROKER 
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
ALTER DATABASE [MusicGO] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MusicGO] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MusicGO] SET QUERY_STORE = OFF
GO
USE [MusicGO]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [MusicGO]
GO
/****** Object:  Table [dbo].[Binnacle]    Script Date: 22/9/2019 10:40:11 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dictionary]    Script Date: 22/9/2019 10:40:12 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DVV]    Script Date: 22/9/2019 10:40:12 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 22/9/2019 10:40:12 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 22/9/2019 10:40:12 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionGroup]    Script Date: 22/9/2019 10:40:12 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 22/9/2019 10:40:12 p. m. ******/
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
	[Password] [nvarchar](18) NOT NULL,
	[ArtistName] [nvarchar](50) NULL,
	[DVH] [nvarchar](32) NULL,
	[ImgKey] [nvarchar](max) NULL,
	[LanguageID] [uniqueidentifier] NOT NULL,
	[ContractID] [uniqueidentifier] NULL,
	[Playbacks] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 22/9/2019 10:40:12 p. m. ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Language] ([LanguageID], [Name], [Code]) VALUES (N'1c11e8f2-1b83-4476-9f27-54da779c64c6', N'English', N'en')
INSERT [dbo].[Language] ([LanguageID], [Name], [Code]) VALUES (N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', N'Español', N'es')
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'd247a163-0802-44e2-a135-19fa11d658b8', N'SuperAdmin', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'4382837c-0782-46cf-a1e5-56e64922b97e', N'Login', 0)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'2768a7a9-5119-4f44-853c-6fd96785bc9f', N'Artist', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'0380110f-4d1c-467a-9952-8d54ee3cf215', N'Admin', 1)
INSERT [dbo].[Permission] ([PermissionID], [Name], [IsGroup]) VALUES (N'317622bc-0b29-4734-9d7e-ec2a20aa015f', N'User', 1)
INSERT [dbo].[User] ([UserID], [Name], [LastName], [UserName], [Email], [Blocked], [Password], [ArtistName], [DVH], [ImgKey], [LanguageID], [ContractID], [Playbacks]) VALUES (N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0', N'Ezequiel', N'Fernandez', N'Ezefer', N'Ezequielf10@hotmail.com', 0, N'234234234234', NULL, NULL, NULL, N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', NULL, NULL)
INSERT [dbo].[User] ([UserID], [Name], [LastName], [UserName], [Email], [Blocked], [Password], [ArtistName], [DVH], [ImgKey], [LanguageID], [ContractID], [Playbacks]) VALUES (N'e723f631-7508-4bf9-b437-45bdbdc20913', N'Andres', N'Perez', N'Andy', N'andres@hotmail.com', 0, N'234234234234', NULL, NULL, NULL, N'e3a2b25a-34a3-4009-97e8-fb88c5cb27c1', NULL, NULL)
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'c76e8e38-f60a-44b7-95c0-4e56e90539a3', N'4382837c-0782-46cf-a1e5-56e64922b97e', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
INSERT [dbo].[UserPermission] ([PermissionUserID], [PermissionID], [UserID]) VALUES (N'712cec14-e2f8-4a87-980e-8146da91a8b5', N'd247a163-0802-44e2-a135-19fa11d658b8', N'e33d74bb-7e8f-4c72-ae71-04d61dbfcde0')
ALTER TABLE [dbo].[Binnacle]  WITH CHECK ADD  CONSTRAINT [FK_Binnacle_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Binnacle] CHECK CONSTRAINT [FK_Binnacle_User]
GO
ALTER TABLE [dbo].[Dictionary]  WITH CHECK ADD  CONSTRAINT [FK_Dictionary_Language] FOREIGN KEY([DictionaryID])
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
/****** Object:  StoredProcedure [dbo].[AddBinnacle]    Script Date: 22/9/2019 10:40:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddBinnacle]
	@UserID uniqueidentifier, @Description nvarchar
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
/****** Object:  StoredProcedure [dbo].[GetBinnacle]    Script Date: 22/9/2019 10:40:15 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[GetDVV]    Script Date: 22/9/2019 10:40:15 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[GetPermissions]    Script Date: 22/9/2019 10:40:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPermissions]
AS
BEGIN
	
	SELECT p.PermissionID, p.Name, p.IsGroup ,pg.ParentID, pparent.Name as 'ParentName' FROM Permission p
	left join PermissionGroup pg on pg.ChildID = p.PermissionID 
	left join permission pparent on pparent.PermissionID = pg.ParentID
END


GO
/****** Object:  StoredProcedure [dbo].[GetUserByUserName]    Script Date: 22/9/2019 10:40:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserByUserName]
	@UserName nvarchar(50)
AS
BEGIN
	
	SELECT u.UserID ,u.UserName, u.Name, u.LastName, u.ArtistName, u.Email, u.Password,
	u.Blocked, u.ImgKey, u.ContractID, u.Playbacks, l.LanguageID, l.Code, l.Name
	from [User] u left join Language l on l.LanguageID = u.LanguageID where u.UserName = @UserName
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserPermissions]    Script Date: 22/9/2019 10:40:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserPermissions]
	@UserID UniqueIdentifier
AS
BEGIN

SELECT p.PermissionID, p.Name, p.IsGroup,  pg.ParentID, pparent.Name as 'ParentName' FROM Permission p
	left join PermissionGroup pg on pg.ChildID = p.PermissionID 
	left join permission pparent on pparent.PermissionID = pg.ParentID
	right join UserPermission up on up.PermissionID = p.PermissionID
	where up.UserID = @UserID
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 22/9/2019 10:40:15 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[SetDVV]    Script Date: 22/9/2019 10:40:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SetDVV]
	 @TableName nvarchar, @DVVHash nvarchar
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
