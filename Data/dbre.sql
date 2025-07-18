USE [master]
GO
/****** Object:  Database [RepuestoAPI]    Script Date: 13/7/2025 10:00:29 ******/
CREATE DATABASE [RepuestoAPI]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RepuestoAPI', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\RepuestoAPI.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RepuestoAPI_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\RepuestoAPI_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [RepuestoAPI] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RepuestoAPI].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RepuestoAPI] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RepuestoAPI] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RepuestoAPI] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RepuestoAPI] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RepuestoAPI] SET ARITHABORT OFF 
GO
ALTER DATABASE [RepuestoAPI] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RepuestoAPI] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RepuestoAPI] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RepuestoAPI] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RepuestoAPI] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RepuestoAPI] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RepuestoAPI] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RepuestoAPI] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RepuestoAPI] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RepuestoAPI] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RepuestoAPI] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RepuestoAPI] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RepuestoAPI] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RepuestoAPI] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RepuestoAPI] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RepuestoAPI] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [RepuestoAPI] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RepuestoAPI] SET RECOVERY FULL 
GO
ALTER DATABASE [RepuestoAPI] SET  MULTI_USER 
GO
ALTER DATABASE [RepuestoAPI] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RepuestoAPI] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RepuestoAPI] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RepuestoAPI] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RepuestoAPI] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RepuestoAPI] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RepuestoAPI', N'ON'
GO
ALTER DATABASE [RepuestoAPI] SET QUERY_STORE = ON
GO
ALTER DATABASE [RepuestoAPI] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [RepuestoAPI]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 13/7/2025 10:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permisos]    Script Date: 13/7/2025 10:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permisos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombrePermiso] [nvarchar](max) NOT NULL,
	[dataKey] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Permisos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Repuestos]    Script Date: 13/7/2025 10:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Repuestos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL,
	[precio] [decimal](18, 2) NOT NULL,
	[marca] [nvarchar](max) NOT NULL,
	[modelo] [nvarchar](max) NOT NULL,
	[categoria] [nvarchar](max) NOT NULL,
	[stock] [int] NOT NULL,
 CONSTRAINT [PK_Repuestos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 13/7/2025 10:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolesPermisos]    Script Date: 13/7/2025 10:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolesPermisos](
	[idRol] [int] NOT NULL,
	[idPermiso] [int] NOT NULL,
 CONSTRAINT [PK_RolesPermisos] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC,
	[idPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 13/7/2025 10:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombreUsuario] [nvarchar](max) NOT NULL,
	[clave] [nvarchar](max) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[RefreshToken] [nvarchar](max) NOT NULL,
	[RefreshTokenExpiryTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuariosPermisos]    Script Date: 13/7/2025 10:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuariosPermisos](
	[idUsuario] [int] NOT NULL,
	[idPermiso] [int] NOT NULL,
	[Usuarioid] [int] NULL,
 CONSTRAINT [PK_UsuariosPermisos] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC,
	[idPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuariosRoles]    Script Date: 13/7/2025 10:00:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuariosRoles](
	[idUsuario] [int] NOT NULL,
	[idRol] [int] NOT NULL,
	[Usuarioid] [int] NULL,
 CONSTRAINT [PK_UsuariosRoles] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC,
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250712014448_inicial', N'9.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250712015206_inicial1', N'9.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250712024643_inicia2', N'9.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250712034045_inicia22', N'9.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250712213854_datanuea', N'9.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250712214031_datanuea1', N'9.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250712214109_datanuea12', N'9.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250713033020_rolpermiso', N'9.0.7')
GO
SET IDENTITY_INSERT [dbo].[Permisos] ON 

INSERT [dbo].[Permisos] ([id], [nombrePermiso], [dataKey]) VALUES (1, N'Crear repuesto', N'repuesto.crear')
INSERT [dbo].[Permisos] ([id], [nombrePermiso], [dataKey]) VALUES (2, N'Editar repuesto', N'repuesto.editar')
INSERT [dbo].[Permisos] ([id], [nombrePermiso], [dataKey]) VALUES (3, N'Listar repuesto', N'repuesto.listar')
INSERT [dbo].[Permisos] ([id], [nombrePermiso], [dataKey]) VALUES (4, N'Eliminar repuesto', N'repuesto.eliminar')
INSERT [dbo].[Permisos] ([id], [nombrePermiso], [dataKey]) VALUES (9, N'Modulo Usuario', N'usuario.modulo')
INSERT [dbo].[Permisos] ([id], [nombrePermiso], [dataKey]) VALUES (10, N'Modulo Roles', N'usuario.roles')
SET IDENTITY_INSERT [dbo].[Permisos] OFF
GO
SET IDENTITY_INSERT [dbo].[Repuestos] ON 

INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (6, N'Cadena', N'Cadena para moto de alta calidad', CAST(28940.11 AS Decimal(18, 2)), N'Renault', N'Fiesta', N'Moto', 51)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (7, N'Bateria', N'Bateria para auto de alta calidad', CAST(39364.88 AS Decimal(18, 2)), N'Kawasaki', N'CB250', N'Auto', 59)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (8, N'Cubierta', N'Cubierta para moto de alta calidad', CAST(17373.65 AS Decimal(18, 2)), N'Kawasaki', N'XR150', N'Moto', 69)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (9, N'Radiador', N'Radiador para auto de alta calidad', CAST(26294.09 AS Decimal(18, 2)), N'Ford', N'Ninja 400', N'Auto', 61)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (10, N'Cubierta', N'Cubierta para auto de alta calidad', CAST(34811.13 AS Decimal(18, 2)), N'Suzuki', N'Versa', N'Auto', 12)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (11, N'Pastilla de freno', N'Pastilla de freno para moto de alta calidad', CAST(33206.75 AS Decimal(18, 2)), N'Fiat', N'Uno', N'Moto', 15)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (12, N'Espejo', N'Espejo para moto de alta calidad', CAST(17483.15 AS Decimal(18, 2)), N'Honda', N'XR150', N'Moto', 62)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (13, N'Filtro de aire', N'Filtro de aire para auto de alta calidad', CAST(44279.72 AS Decimal(18, 2)), N'Fiat', N'Corsa', N'Auto', 18)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (14, N'Embrague', N'Embrague para auto de alta calidad', CAST(22823.32 AS Decimal(18, 2)), N'Ford', N'CB250', N'Auto', 36)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (15, N'Espejo', N'Espejo para moto de alta calidad', CAST(40287.17 AS Decimal(18, 2)), N'Honda', N'Ninja 400', N'Moto', 28)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (16, N'Paragolpe', N'Paragolpe para auto de alta calidad', CAST(15208.30 AS Decimal(18, 2)), N'Honda', N'Uno', N'Auto', 53)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (17, N'Filtro de aire', N'Filtro de aire para auto de alta calidad', CAST(12028.06 AS Decimal(18, 2)), N'Renault', N'Uno', N'Auto', 96)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (18, N'Radiador', N'Radiador para auto de alta calidad', CAST(27433.52 AS Decimal(18, 2)), N'Volkswagen', N'Fiesta', N'Auto', 50)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (19, N'Manubrio', N'Manubrio para moto de alta calidad', CAST(12441.81 AS Decimal(18, 2)), N'Chevrolet', N'Corsa', N'Moto', 72)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (20, N'Paragolpe', N'Paragolpe para auto de alta calidad', CAST(33560.23 AS Decimal(18, 2)), N'Kawasaki', N'Uno', N'Auto', 100)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (22, N'Radiador', N'Radiador para auto de alta calidad', CAST(28659.39 AS Decimal(18, 2)), N'Ford', N'Versa', N'Auto', 53)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (23, N'Paragolpe', N'Paragolpe para auto de alta calidad', CAST(43075.82 AS Decimal(18, 2)), N'Ford', N'Versa', N'Auto', 83)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (24, N'Faro', N'Faro para auto de alta calidad', CAST(14552.56 AS Decimal(18, 2)), N'Honda', N'Corsa', N'Auto', 55)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (25, N'Embrague', N'Embrague para moto de alta calidad', CAST(18601.93 AS Decimal(18, 2)), N'Fiat', N'Versa', N'Moto', 62)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (26, N'Paragolpe', N'Paragolpe para auto de alta calidad', CAST(38407.65 AS Decimal(18, 2)), N'Ford', N'Ninja 400', N'Auto', 75)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (27, N'Radiador', N'Radiador para auto de alta calidad', CAST(27396.30 AS Decimal(18, 2)), N'Kawasaki', N'Duster', N'Auto', 30)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (28, N'Sensor ABS', N'Sensor ABS para auto de alta calidad', CAST(16145.73 AS Decimal(18, 2)), N'Ford', N'Hilux', N'Auto', 88)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (29, N'Amortiguador', N'Amortiguador para moto de alta calidad', CAST(19231.19 AS Decimal(18, 2)), N'Fiat', N'Corsa', N'Moto', 69)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (30, N'Cubierta', N'Cubierta para moto de alta calidad', CAST(5352.07 AS Decimal(18, 2)), N'Chevrolet', N'Hilux', N'Moto', 27)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (31, N'Alternador', N'Alternador para auto de alta calidad', CAST(18282.35 AS Decimal(18, 2)), N'Chevrolet', N'Fiesta', N'Auto', 15)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (32, N'Pastilla de freno', N'Pastilla de freno para moto de alta calidad', CAST(22244.09 AS Decimal(18, 2)), N'Toyota', N'Fiesta', N'Moto', 88)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (33, N'Filtro de aceite', N'Filtro de aceite para moto de alta calidad', CAST(42127.72 AS Decimal(18, 2)), N'Yamaha', N'Hilux', N'Moto', 61)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (34, N'Corona', N'Corona para moto de alta calidad', CAST(33017.19 AS Decimal(18, 2)), N'Chevrolet', N'Uno', N'Moto', 85)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (35, N'Espejo', N'Espejo para moto de alta calidad', CAST(18868.74 AS Decimal(18, 2)), N'Suzuki', N'Gol', N'Moto', 39)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (36, N'Paragolpe', N'Paragolpe para auto de alta calidad', CAST(11721.57 AS Decimal(18, 2)), N'Kawasaki', N'Uno', N'Auto', 86)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (37, N'Faro', N'Faro para auto de alta calidad', CAST(16066.27 AS Decimal(18, 2)), N'Toyota', N'Corsa', N'Auto', 86)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (38, N'Espejo', N'Espejo para moto de alta calidad', CAST(28719.91 AS Decimal(18, 2)), N'Suzuki', N'Corsa', N'Moto', 40)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (40, N'Espejo', N'Espejo para moto de alta calidad', CAST(29978.59 AS Decimal(18, 2)), N'Ford', N'Corsa', N'Moto', 75)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (41, N'Cubierta', N'Cubierta para auto de alta calidad', CAST(46479.11 AS Decimal(18, 2)), N'Fiat', N'Corsa', N'Auto', 28)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (42, N'Espejo', N'Espejo para moto de alta calidad', CAST(11456.25 AS Decimal(18, 2)), N'Renault', N'CB250', N'Moto', 24)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (43, N'Embrague', N'Embrague para auto de alta calidad', CAST(17265.57 AS Decimal(18, 2)), N'Chevrolet', N'Ninja 400', N'Auto', 14)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (44, N'Radiador', N'Radiador para auto de alta calidad', CAST(46995.31 AS Decimal(18, 2)), N'Renault', N'Hilux', N'Auto', 8)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (45, N'Bateria', N'Bateria para auto de alta calidad', CAST(17803.36 AS Decimal(18, 2)), N'Volkswagen', N'Duster', N'Auto', 98)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (48, N'Pastilla de freno', N'Pastilla de freno para moto de alta calidad', CAST(36243.74 AS Decimal(18, 2)), N'Volkswagen', N'Fiesta', N'Moto', 15)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (49, N'Paragolpe', N'Paragolpe para auto de alta calidad', CAST(44995.42 AS Decimal(18, 2)), N'Kawasaki', N'Uno', N'Auto', 88)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (50, N'Cadena', N'Cadena para moto de alta calidad', CAST(47833.46 AS Decimal(18, 2)), N'Kawasaki', N'Corsa', N'Moto', 93)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (51, N'Bateria', N'Bateria para auto de alta calidad', CAST(45027.99 AS Decimal(18, 2)), N'Fiat', N'Ninja 400', N'Auto', 71)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (52, N'Sensor ABS', N'Sensor ABS para auto de alta calidad', CAST(26314.78 AS Decimal(18, 2)), N'Honda', N'XR150', N'Auto', 12)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (53, N'Cubierta', N'Cubierta para auto de alta calidad', CAST(43464.21 AS Decimal(18, 2)), N'Volkswagen', N'Gol', N'Auto', 83)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (54, N'Paragolpe', N'Paragolpe para auto de alta calidad', CAST(22228.87 AS Decimal(18, 2)), N'Ford', N'Fiesta', N'Auto', 77)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (55, N'Piñón', N'Piñón reforzado para moto de alta calidad', CAST(21509.85 AS Decimal(18, 2)), N'Yamaha', N'FZ 16', N'Moto', 84)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (56, N'Piñón', N'Piñón reforzado para moto de alta calidad', CAST(29197.21 AS Decimal(18, 2)), N'Honda', N'CBR 250', N'Moto', 74)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (57, N'Piñón', N'Piñón reforzado para moto de alta calidad', CAST(30320.73 AS Decimal(18, 2)), N'Suzuki', N'Ninja 400', N'Moto', 92)
INSERT [dbo].[Repuestos] ([id], [nombre], [descripcion], [precio], [marca], [modelo], [categoria], [stock]) VALUES (58, N'Piñón', N'Piñón reforzado para moto de alta calidad', CAST(15896.25 AS Decimal(18, 2)), N'Kawasaki', N'Z400', N'Moto', 32)
SET IDENTITY_INSERT [dbo].[Repuestos] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([id], [descripcion]) VALUES (1, N'Administrador')
INSERT [dbo].[Roles] ([id], [descripcion]) VALUES (2, N'Usuario')
INSERT [dbo].[Roles] ([id], [descripcion]) VALUES (7, N'Invitado')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[RolesPermisos] ([idRol], [idPermiso]) VALUES (2, 1)
INSERT [dbo].[RolesPermisos] ([idRol], [idPermiso]) VALUES (2, 2)
INSERT [dbo].[RolesPermisos] ([idRol], [idPermiso]) VALUES (2, 3)
INSERT [dbo].[RolesPermisos] ([idRol], [idPermiso]) VALUES (2, 4)
INSERT [dbo].[RolesPermisos] ([idRol], [idPermiso]) VALUES (7, 3)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([id], [nombreUsuario], [clave], [email], [RefreshToken], [RefreshTokenExpiryTime]) VALUES (2, N'mahada01', N'$2a$11$NtD/BTTcOFEI8LW.QHPlQOmrKoO6eCzU7LtoWv.iftGjw/IoipdE2', N'facumarti06@gmail.com', N'bJb69nwhsTfnAvxvoULsLsV6bYkh8Rmwzcg40T6YtcwOaQ4rM4xP+tXup9Hvfgr5uwA2ktcCEUSaj7DxNP6V2w==', CAST(N'2025-07-20T01:16:23.3260276' AS DateTime2))
INSERT [dbo].[Usuarios] ([id], [nombreUsuario], [clave], [email], [RefreshToken], [RefreshTokenExpiryTime]) VALUES (5, N'mahada02', N'$2a$11$LT67PGNyZYUH.XNMBh5dcec52mESE8n1ITd2p5Snc/SRKaZlRu2b.', N'roberto@gmail.com', N'B2UGy4o5fgpky+cbVL6VnSfm7ZPzQ0Eco8ZHD9xZ+uAviSmEvaRMJuZbkhDz1keC0FQMc0/iXCFrNWUk3rf67w==', CAST(N'2025-07-20T01:14:43.2738038' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
INSERT [dbo].[UsuariosRoles] ([idUsuario], [idRol], [Usuarioid]) VALUES (2, 1, NULL)
INSERT [dbo].[UsuariosRoles] ([idUsuario], [idRol], [Usuarioid]) VALUES (5, 7, 5)
GO
/****** Object:  Index [IX_UsuariosPermisos_Usuarioid]    Script Date: 13/7/2025 10:00:29 ******/
CREATE NONCLUSTERED INDEX [IX_UsuariosPermisos_Usuarioid] ON [dbo].[UsuariosPermisos]
(
	[Usuarioid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsuariosRoles_idRol]    Script Date: 13/7/2025 10:00:29 ******/
CREATE NONCLUSTERED INDEX [IX_UsuariosRoles_idRol] ON [dbo].[UsuariosRoles]
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsuariosRoles_Usuarioid]    Script Date: 13/7/2025 10:00:29 ******/
CREATE NONCLUSTERED INDEX [IX_UsuariosRoles_Usuarioid] ON [dbo].[UsuariosRoles]
(
	[Usuarioid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (N'') FOR [RefreshToken]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [RefreshTokenExpiryTime]
GO
ALTER TABLE [dbo].[UsuariosPermisos]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosPermisos_Usuarios_Usuarioid] FOREIGN KEY([Usuarioid])
REFERENCES [dbo].[Usuarios] ([id])
GO
ALTER TABLE [dbo].[UsuariosPermisos] CHECK CONSTRAINT [FK_UsuariosPermisos_Usuarios_Usuarioid]
GO
ALTER TABLE [dbo].[UsuariosRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosRoles_Roles_idRol] FOREIGN KEY([idRol])
REFERENCES [dbo].[Roles] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuariosRoles] CHECK CONSTRAINT [FK_UsuariosRoles_Roles_idRol]
GO
ALTER TABLE [dbo].[UsuariosRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosRoles_Usuarios_idUsuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuarios] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuariosRoles] CHECK CONSTRAINT [FK_UsuariosRoles_Usuarios_idUsuario]
GO
ALTER TABLE [dbo].[UsuariosRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosRoles_Usuarios_Usuarioid] FOREIGN KEY([Usuarioid])
REFERENCES [dbo].[Usuarios] ([id])
GO
ALTER TABLE [dbo].[UsuariosRoles] CHECK CONSTRAINT [FK_UsuariosRoles_Usuarios_Usuarioid]
GO
USE [master]
GO
ALTER DATABASE [RepuestoAPI] SET  READ_WRITE 
GO
