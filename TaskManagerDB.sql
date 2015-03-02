USE [master]
GO

/****** Object:  Database [TaskManager]    Script Date: 3/2/2015 7:25:14 PM ******/
CREATE DATABASE [TaskManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaskManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TaskManager.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TaskManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TaskManager_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [TaskManager] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaskManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TaskManager] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [TaskManager] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [TaskManager] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [TaskManager] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [TaskManager] SET ARITHABORT OFF 
GO

ALTER DATABASE [TaskManager] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [TaskManager] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [TaskManager] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [TaskManager] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [TaskManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [TaskManager] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [TaskManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [TaskManager] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [TaskManager] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [TaskManager] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [TaskManager] SET  DISABLE_BROKER 
GO

ALTER DATABASE [TaskManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [TaskManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [TaskManager] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [TaskManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [TaskManager] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [TaskManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [TaskManager] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [TaskManager] SET RECOVERY FULL 
GO

ALTER DATABASE [TaskManager] SET  MULTI_USER 
GO

ALTER DATABASE [TaskManager] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [TaskManager] SET DB_CHAINING OFF 
GO

ALTER DATABASE [TaskManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [TaskManager] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [TaskManager] SET  READ_WRITE 
GO




USE [TaskManager]
GO

/****** Object:  Table [dbo].[TaskList]    Script Date: 3/2/2015 7:25:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TaskList](
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_TaskList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [TaskManager]
GO

/****** Object:  Table [dbo].[TaskItem]    Script Date: 3/2/2015 7:26:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TaskItem](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsDone] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[TaskListId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TaskItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TaskItem]  WITH CHECK ADD  CONSTRAINT [FK_TaskItem_TaskList] FOREIGN KEY([TaskListId])
REFERENCES [dbo].[TaskList] ([Id])
GO

ALTER TABLE [dbo].[TaskItem] CHECK CONSTRAINT [FK_TaskItem_TaskList]
GO


