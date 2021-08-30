USE [SoftCredito]
GO
/****** Object:  StoredProcedure [dbo].[sp_openpay_autorizacion_insert]    Script Date: 26/08/2021 09:12:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- CREATE PROC sp_openpay_autorizacion_insert 
ALTER PROC [dbo].[sp_openpay_autorizacion_insert]
	@FOLIO VARCHAR(35),
	@LOCAL_DATE VARCHAR(25),
	@AMOUNT DECIMAL(18,2),
	@TRX_NO VARCHAR(12)
AS

INSERT INTO openpay_autorizacion2(folio, local_date2, amount, trx_no, estatus, fecha_alta) VALUES(@FOLIO, @LOCAL_DATE, @AMOUNT, @TRX_NO, 1, GETDATE())

SELECT @@IDENTITY AS autorizacion_no