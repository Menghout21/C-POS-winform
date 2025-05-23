USE [master]
GO
/****** Object:  Database [ComputerShop_db]    Script Date: 4/27/2025 9:04:56 PM ******/
CREATE DATABASE [ComputerShop_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ComputerShop_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ComputerShop_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ComputerShop_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ComputerShop_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ComputerShop_db] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ComputerShop_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ComputerShop_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ComputerShop_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ComputerShop_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ComputerShop_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ComputerShop_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [ComputerShop_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ComputerShop_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ComputerShop_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ComputerShop_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ComputerShop_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ComputerShop_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ComputerShop_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ComputerShop_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ComputerShop_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ComputerShop_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ComputerShop_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ComputerShop_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ComputerShop_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ComputerShop_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ComputerShop_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ComputerShop_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ComputerShop_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ComputerShop_db] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ComputerShop_db] SET  MULTI_USER 
GO
ALTER DATABASE [ComputerShop_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ComputerShop_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ComputerShop_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ComputerShop_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ComputerShop_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ComputerShop_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ComputerShop_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [ComputerShop_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ComputerShop_db]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/27/2025 9:04:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Qty] [int] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreateAt] [datetime] NULL,
	[Image] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 4/27/2025 9:04:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[UserId] [int] NULL,
	[TotalAmount] [decimal](6, 2) NULL,
	[TotalQty] [int] NULL,
	[TotalPaid] [decimal](6, 2) NULL,
	[DueAmount] [decimal](6, 2) NULL,
	[InvoiceStatus] [varchar](50) NULL,
	[IsReturn] [bit] NULL,
	[CteateAt] [datetime] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 4/27/2025 9:04:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NULL,
	[ProductId] [int] NULL,
	[Qty] [int] NULL,
	[Price] [decimal](6, 2) NULL,
	[Discount] [decimal](6, 2) NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[OrderHistory]    Script Date: 4/27/2025 9:04:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[OrderHistory] AS
SELECT
    IDT.InvoiceId,
    P.Name AS ProductName,
    IDT.Qty,
    IDT.Price,
    (IDT.Qty * IDT.Price) AS TotalAmount
FROM
    InvoiceDetail IDT
INNER JOIN
    Product P ON IDT.ProductId = P.Id
INNER JOIN
    Invoice I ON IDT.InvoiceId = I.Id;
GO
/****** Object:  Table [dbo].[Category]    Script Date: 4/27/2025 9:04:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Status] [bit] NOT NULL,
	[CreateAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 4/27/2025 9:04:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](100) NOT NULL,
	[Lastname] [nvarchar](100) NOT NULL,
	[Tel] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[CreateAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Status]    Script Date: 4/27/2025 9:04:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Status] [bit] NOT NULL,
	[CreateAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment_Method]    Script Date: 4/27/2025 9:04:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment_Method](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Status] [bit] NOT NULL,
	[CreateAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 4/27/2025 9:04:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreateAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/27/2025 9:04:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_CteateAt]  DEFAULT (getdate()) FOR [CteateAt]
GO
ALTER TABLE [dbo].[Order_Status] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Order_Status] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Payment_Method] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Payment_Method] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [Qty]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD CHECK  (([Price]>=(0)))
GO
USE [master]
GO
ALTER DATABASE [ComputerShop_db] SET  READ_WRITE 
GO
