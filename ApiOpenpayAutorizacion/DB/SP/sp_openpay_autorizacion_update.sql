USE [SoftCredito]
GO
/****** Object:  StoredProcedure [dbo].[sp_openpay_autorizacion_insert]    Script Date: 26/08/2021 09:12:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- CREATE PROC sp_openpay_autorizacion_update 
ALTER PROC [dbo].[sp_openpay_autorizacion_update]
	@FOLIO VARCHAR(35),
	@LOCAL_DATE VARCHAR(25),
	@AMOUNT DECIMAL(18,2),
	@TRX_NO VARCHAR(12),
	@NUM_AUT INT
AS
-- 960 segundos = 16 minutos
DECLARE @SEGUNDOS INT
SELECT @SEGUNDOS = DATEDIFF(SECOND, local_date2, GETDATE()) FROM openpay_autorizacion2 WHERE autorizacion_no=@NUM_AUT

IF ((@SEGUNDOS > 0) AND (@SEGUNDOS <= 960))
BEGIN
	UPDATE openpay_autorizacion2 SET estatus=0, fecha_cancelacion=GETDATE() WHERE autorizacion_no=@NUM_AUT
	SELECT @@ROWCOUNT AS resultado
END
ELSE
	SELECT 0 AS resultado

