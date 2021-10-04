USE [SoftCredito]
GO

/****** Object:  Table [dbo].[openpay_autorizacion2]    Script Date: 27/09/2021 01:34:49 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[openpay_autorizacion2](
	[autorizacion_no] [int] IDENTITY(100000,1) NOT NULL,
	[folio] [varchar](35) NULL,
	[local_date] [varchar](25) NULL,
	[amount] [decimal](18, 2) NULL,
	[trx_no] [varchar](12) NULL,
	[estatus] [int] NULL,
	[local_date2] [datetime] NULL,
	[fecha_alta] [datetime] NULL,
	[fecha_cancelacion] [datetime] NULL
) ON [PRIMARY]
GO