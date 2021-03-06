USE [master]
GO
/****** Object:  Database [dbNotes]    Script Date: 12/10/2021 7:11:49 PM ******/
CREATE DATABASE [dbNotes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbNotes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLSERVEREXPRESS\MSSQL\DATA\dbNotes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbNotes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLSERVEREXPRESS\MSSQL\DATA\dbNotes_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [dbNotes] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbNotes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbNotes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbNotes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbNotes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbNotes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbNotes] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbNotes] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbNotes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbNotes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbNotes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbNotes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbNotes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbNotes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbNotes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbNotes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbNotes] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbNotes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbNotes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbNotes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbNotes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbNotes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbNotes] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbNotes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbNotes] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbNotes] SET  MULTI_USER 
GO
ALTER DATABASE [dbNotes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbNotes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbNotes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbNotes] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbNotes] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbNotes] SET QUERY_STORE = OFF
GO
USE [dbNotes]
GO
/****** Object:  Table [dbo].[tabLogin]    Script Date: 12/10/2021 7:11:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tabLogin](
	[id] [int] NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tabLogin] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_tabLogin] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tabNotes]    Script Date: 12/10/2021 7:11:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tabNotes](
	[number] [int] NOT NULL,
	[title] [varchar](50) NULL,
	[contents] [varchar](max) NULL,
	[userid] [int] NOT NULL,
 CONSTRAINT [PK_tabNotes] PRIMARY KEY CLUSTERED 
(
	[number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[tabNotes]  WITH CHECK ADD  CONSTRAINT [FK_tabNotes_tabLogin] FOREIGN KEY([userid])
REFERENCES [dbo].[tabLogin] ([id])
GO
ALTER TABLE [dbo].[tabNotes] CHECK CONSTRAINT [FK_tabNotes_tabLogin]
GO
USE [master]
GO
ALTER DATABASE [dbNotes] SET  READ_WRITE 
GO
