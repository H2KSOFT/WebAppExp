-- Script: Creación de Base de Datos
-- Descripción: Crea la base de datos y configuración inicial

CREATE DATABASE [Prueba_Exp]
GO
USE [Prueba_Exp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleOrdenes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdOrden] [bigint] NOT NULL,
	[Producto] [varchar](500) NULL,
	[Cantidad] [decimal](18, 2) NULL,
	[PrecioUnitario] [decimal](18, 2) NULL,
	[SubTotal] [decimal](18, 2) NULL,
 CONSTRAINT [PK_DetalleOrden] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ordenes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[Cliente] [varchar](500) NULL,
	[Total] [decimal](18, 2) NULL,
	[Fecha_Registro] [datetime] NULL,
 CONSTRAINT [PK_Orden] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NULL,
	[Clave] [varchar](100) NULL,
	[IdRol] [bigint] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ordenes] ADD  CONSTRAINT [DF_Orden_Fecha_Registro]  DEFAULT (getdate()) FOR [Fecha_Registro]
GO
ALTER TABLE [dbo].[DetalleOrdenes]  WITH CHECK ADD  CONSTRAINT [FK_DetalleOrden_Orden] FOREIGN KEY([IdOrden])
REFERENCES [dbo].[Ordenes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleOrdenes] CHECK CONSTRAINT [FK_DetalleOrden_Orden]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
USE [master]
GO
ALTER DATABASE [Prueba_Exp] SET  READ_WRITE 
GO

/*
Insert de roles y usuarios de prueba.
*/

USE [Prueba_Exp]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([Id], [Nombre]) VALUES (2, N'Vendedor')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([Id], [Nombre], [Clave], [IdRol]) VALUES (1, N'Administrador', N'123', 1)
GO
INSERT [dbo].[Usuarios] ([Id], [Nombre], [Clave], [IdRol]) VALUES (2, N'Vendedor', N'123', 2)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO

