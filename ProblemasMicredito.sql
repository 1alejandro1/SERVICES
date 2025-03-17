
--------------------------------------------------------------LEVANTAMIENTO DE RESTRICCIONES---------------------------------------------------------------------------
SELECT dcNumIDC, dcCIC,fcTipoIDC,dcNombres, dcApellidoPaterno, dcApellidoMaterno FROM micredito.SOLICITUDPERSONA where dcNumIDC = '03828840SC' AND  fcTipoIDC = 'Q'
SELECT dcCIC,fcTipoIDC,pnCodSolicitud FROM micredito.SOLICITUDPERSONA where dcNumIDC = '03828840SC' AND  fcTipoIDC = 'Z'

update micredito.SOLICITUDPERSONA set fcTipoIDC = 'Z' where dcNumIDC = '03828840SC' AND fcTipoIDC = 'Q'
update micredito.SOLICITUDPERSONA set fcTipoIDC = 'Q' where dcNumIDC = '03828840SC' AND fcTipoIDC = 'Z'

--------------------------------------------------------------LEVANTAMIENTO DE RESTRICCIONES---------------------------------------------------------------------------
------------------------------------------------------------------------PROBLEMA DATOS ADICIONALES-----------------------------------------------------------------------
------------AIC_SS_DatAdic_CabPropPie
	
	select * from micredito.SOLICITUDINFOADICIONAL where CodSolicitud = 432626 AND IndicadorTipoRegistro = '23'
	select * from micredito.SOLICITUDINFOADICIONAL where CodSolicitud = 461520
	---[micredito].[AIC_SS_DatAdic_CabPropPie]432626 --falata IndicadorTipoRegistro = '16'
	
	use BD_MICREDITO

INSERT INTO micredito.SOLICITUDINFOADICIONAL ([CodSolicitud],[IndicadorTipoRegistro],[CampoStr1],[CampoStr2],[CampoStr3],[CampoStr4],[CampoStr5],[CampoStr6],[CampoStr7],[CampoStr8],[CampoStr9],[CampoDec1],[CampoDec2],[CampoInt1],[CampoMoney1],[CampoDate1],[FechaIngreso],[FechaModificacion],[IndicadorRegistroActivo])values (432626,   '16',   null,   null,   null,   null,   null,   null,   null,   null,   null,   0,   0,   null,   0,    null,    '2019-11-25',    '2019-11-25',    'S')

UPDATE micredito.SOLICITUDINFOADICIONAL SET CampoStr9 = 'ACFI'  WHERE CodSolicitud = 432626 AND [IndicadorTipoRegistro] = '16'
UPDATE micredito.SOLICITUDINFOADICIONAL SET CampoStr9 = 'ACFI'  WHERE CodSolicitud = 432626 AND [IndicadorTipoRegistro] = '16'
-------------------------------------------------------------------------------REPROCESAR OPERACION PARA GENERACION ALS------------------------------------------------------------
SELECT dcNumSolicitud,pnCodSolicitud FROM micredito.SOLICITUD WHERE pnCodSolicitud in  (669783,670159)
-- para el reproceso y se genere el als modificar los datos en las siguientes tablas
---CREDITOCUNSUMO (fecha, cuota)
---DESEMBOLSO (fecha)

--NOSE.....
--delete micredito.SOLICITUDINFOADICIONAL WHERE CodSolicitud = 432626 AND [IndicadorTipoRegistro] = '23'
INSERT INTO micredito.SOLICITUDINFOADICIONAL ([CodSolicitud],[IndicadorTipoRegistro],[CampoStr1],[CampoStr2],[CampoStr3],[CampoStr4],[CampoStr5],[CampoStr6],[CampoStr7],[CampoStr8],[CampoStr9],[CampoDec1],[CampoDec2],[CampoInt1],[CampoMoney1],[CampoDate1],[FechaIngreso],[FechaModificacion],[IndicadorRegistroActivo])											values (432626,   '20',   null,   null,   null,   null,   null,   null,   null,   null,   0,   0,   0,   null,   0,    null,    '2019-11-25',    '2019-11-25',    'S')
INSERT INTO micredito.SOLICITUDINFOADICIONAL ([CodSolicitud],[IndicadorTipoRegistro],[CampoStr1],[CampoStr2],[CampoStr3],[CampoStr4],[CampoStr5],[CampoStr6],[CampoStr7],[CampoStr8],[CampoStr9],[CampoDec1],[CampoDec2],[CampoInt1],[CampoMoney1],[CampoDate1],[FechaIngreso],[FechaModificacion],[IndicadorRegistroActivo])values (432626,   '23',   null,  null,  null,   null,   null,   null,   null,   null,   null,   0,   0,   null,   0,    null,    '2019-06-17',    '2019-06-17',    'S')
SELECT pcCodCampo, dcDescripcion FROM micredito.EFECNEGOCIO_TABLAS WHERE CodTabla =12 AND IndicadorActivo='S'


--MODIFICARFIN CALIFICACION
select ddFechaFinCalificacion from micredito.SOLICITUD WHERE pnCodSolicitud in  (741294)--"[{\"ddFechaFinCalificacion\":\"2022-12-31T08:05:58.47\"}]"
update micredito.SOLICITUD set ddFechaFinCalificacion = GETDATE() where pnCodSolicitud = 719316


--FECHA CALCULO 
select ddFechaCalculo from micredito.CREDITOBANCANEGOCIO WHERE CodSolicitud  in  (739645)
update micredito.CREDITOBANCANEGOCIO set ddFechaCalculo = '2019-11-25 19:37:29.76' where CodSolicitud = 432626
select * from micredito.CREDITOCONSUMO WHERE pnCodSolicitud  in  (739645)

-------------------------------------------------------------ERROR NO CREA GARANTÍA - NO SE TRANSMITIO Y EL ESTADO SE QUEDO EN "N"------------------------------------
  --VALIDA Q SE NECESITA GENERAR GARANTIA, PARA LO CUAL EL SCORE DEBE SER >326
  
  -- SI ES MAYOR A 326 indicar que tienen que registrar manualmente la garantía

SELECT diScoreValor FROM micredito.SCORE_CONSUMO sc  INNER JOIN micredito.SCORE_SOLICITUD ss  ON sc.codSolicitud_ID_IN = ss.pnCodSolicitud  where sc.codSolicitud_ID_IN = 765006

--Si no tiene garantía registrar genérica
-- Si tiene garantía pedir el numero

insert into micredito.BP_GARANTIAS (pnCodSolicitud, dcNroGarantia, dvUsrCreacion, ddFechaCreacion, dvUsrModificacion, ddFechaModificacion, dcObservacion, dnMontoAfectacion, dnMontoGarantia)  values (761305, 'G000-00000000', 'T24005', GETDATE(), 'T24005', GETDATE(), null, null, null)

insert into micredito.BN_GARANTIAS (pnCodSolicitud, dcNroGarantia, dcTipoGarantia, dnMontoAfectacion, dvUsrCreacion, ddFechaCreacion, dvUsrModificacion,ddFechaModificacion, dbLlaveModificacion) values (765006, 'G000-00000000', 'FOG', '0.00', 'B01725', GETDATE(), 'B01725', GETDATE(), null)
  ------------------------------------------------------------------------------------ERROR NO EXISTE REGISTRO EN CREDITOCONSUMO------------------------------------
  
  INSERT INTO [micredito].[CREDITOCONSUMO] ([pnCodSolicitud],[dcCargoAutomaticoProducto],[dcCargoAutomaticoNumCuenta],[dcEstadoDiferenciaTasaCuota],[dcTelefonoEstablecimiento],[fcCodEstablecimiento],[dcIndicadorCorrespondencia],[dcIndiceTasa],[dcNomVendedorEstablecimiento],[dcDocumVendedorEstablecimiento],[dcIndicadorSelloPremium],[dcNumCtaDesembolsoAutomatico],[dcProductoDesembolsoAutomatico],[dcUsuarioResolvioDifeTasaCuota],[dcIndicadorRecursosHumanos],[ddFechaResolucionDifeTasaCuota],[dnPlazoEstablecimiento],[dnNumeroCuotasAnuales],[dnPeriodoGraciaAprobado],[dnNumeroCuotasAprobadas],[fnCodVehiculo],[dnNumeroCuotasJulio],[dnNumeroCuotasDiciembre],[dnPrecioContadoEstablecimiento],[dnMontoSolicitadoFinanciar],[dnMontoSeguroEstablecimiento],[dnGastoNotarialEstablecimiento],[dnImporteTotalFinanciar],[dnCuotaMensualIngresada],[dnTasaInteres],[dnTasaInteresCalculada],[dmMontoAprobado],[dmCuotaMensualAprobada],[dmMontoSeguroAprobado],[dmGastoNotarialAprobado],[dmImporteTotalFinanciarAprobad],[dmMontoAdicional],[dnTasaCuotaBallon],[dcUsuarioFiscalizador],[dcIndicadorGps],[dcDescripcionMercaderia],[dcModelo],[dcAnio],[dnNumeroCuotasJulioAprobadas],[dnNumeroCuotasDiciembreAprobad],[dnPlazoAprobado],[dnNumeroCuotasAnualesAprobadas],[dnTasaCuotaBallonIngresada],[dnMontoAdicionalIngresado],[dmCuotaMensualCalculada],[dcCredimasTitular],[dnDiaPago],[dcConDocsImpresos],[dcIndSeguroInmueble],[dbLlaveModificacion],[AnalistaTipoTasaInteres],[ddFechaPrimerPago],[dmPrimeraCuota],[ddFechaSegPago],[ddFechaCalculo],[fcCodFinalidad],[pnCodTipoContrato]) values (739645, 06, '20151006109304', null, null, '30300000200020556', 'D', 'BAN', null, null, 'N', '20151006109304', '06', NULL, 'N', null, '78', '12', NULL, '12', 0,0,0,'0.0000', '84000.0000', '0.0000', '0.0000', '84000.0000', '1852.5000',16.50, 16.50, '84000.0000', 1852.50, '0.0000', '0.0000', 84000.00, null, null, 'T10126', null, null, null, '____', '0.0000', '0.0000', '78.0000', '12.0000', null, null, 1852.50, null, 7, 'S', 1,null, 2,'2023-07-07 00:00:00.000', 2105.90, '2023-08-07 00:00:00.000', GETDATE(), '00', 1)
  
  ------------------------------------------------------------------------------------FIN ERROR NO EXISTE REGISTRO EN CREDITOCONSUMO------------------------------------

------------------------------------------------------------------------------------NO SE TRANSMITIO Y EL ESTADO SE QUEDO EN "N"------------------------------------
--VALIDA Q SE NECESITA GENERAR GARANTIA, PARA LO CUAL EL SCORE DEBE SER >326
SELECT diScoreValor FROM micredito.SCORE_CONSUMO sc  INNER JOIN micredito.SCORE_SOLICITUD ss  ON sc.codSolicitud_ID_IN = ss.pnCodSolicitud  where sc.codSolicitud_ID_IN = 765006
--VALIDAR EL ERROR QUE ESCRIBIO EN LA TABLA
select * from micredito.CONTROLGARANTIA_BP WHERE pnCodSolicitud = 754300
--[{"pcCodControlGarantia":41513,"pnCodSolicitud":721399,"dcNroGarantia":"G301-00049684  ","dcIndicadorCreacion":"S","dcIndicadorActivacion":"X","ddFechaCreacion":"2023-01-17T19:00:53.137","ddFechaModificacion":"2023-01-17T19:00:53.137","Respuesta":"ERROR.No se escribio el comando AGA. en pantalla DATOS  GENERALESACTIVO GARANTIA?                                                      "}]</a:Data>
--PARA REPROCESAR COPIAR LA GARANTIA PARA ELIMINARLO POR COMPLETO DE HOST (OSO) Y SE DEBE ELIMINAR DE LA TABLA Y ACTUALIZAR FECHA DESEMBOLSO
delete micredito.CONTROLGARANTIA_BP WHERE pnCodSolicitud = 743105
update micredito.DESEMBOLSO_MICREDITO set FechaDesembolso = GETDATE() where pnCodSolicitud in  (721399)
select * from micredito.CONTROLGARANTIA_BP WHERE pnCodSolicitud = 734900
SELECT * FROM micredito.BP_GARANTIAS where pnCodSolicitud = 734900
--solicitar el recalculo a fecha de hoy y los datos
--Para poder reprocesar la operación con fecha de hoy, favor su apoyo con el recalculo a fecha de hoy y proporcionando los siguientes datos:
--Cliente MAMANI MAYTA RUBEN LUIS - 4775439 LP
--Operación: CCZ90927222
--Fecha Desembolso: 18/01/2023
--Fecha Primer Pago: 2022-01-03
--Nueva Primera Cuota: 8,491.86
--Cuotas Siguientes: 5,697.55
SELECT [ddFechaPrimerPago], [dnDiaPago],dmCuotaMensualAprobada,[dmPrimeraCuota] ,[ddFechaCalculo]FROM micredito.CREDITOCONSUMO where [pnCodSolicitud] in (741294)
--UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2021-12-02', [dnDiaPago]= '02',[dmPrimeraCuota] ='2,271.79',dmCuotaMensualAprobada = '1,647.79',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 721399
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2023-02-18', [dnDiaPago]= '18',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 721399
UPDATE micredito.CREDITO SET [ddFechaPrimerPago] = '2023-02-18', [dnDiaPago]= '18',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 721399
update micredito.SOLICITUD set ddFechaFinCalificacion = GETDATE() where pnCodSolicitud = 721399
 
 ------------------------------------------------------------------------------------NO SE TRANSMITIO Y EL ESTADO SE QUEDO EN "N"------------------------------------
 --Revisar a primera hora reporte de operaciones transmitidas y al final del día. En caso de error revisar los Logs y estado de operación.
 -- Si la operacion se cambió el estado en otra fecha posterior no se transmitirá por lo que hay que reprocesar cambiando fechas.

-------------------------------------------------------------------------------------------------------------------
--tienen que tener la primera cuota y cuota mensual, para lo que se pide el recalculo a la fecha de dsembolso modificada
--cuando piden la modificacion del dia de pago
--1. actualizar la fecha de calculo a la fecha actual
UPDATE micredito.CREDITOCONSUMO SET [ddFechaCalculo]= GETDATE () where [pnCodSolicitud] in (677623)
--En el reporte los dias de diferencia deben ser mayor a 30

--MODIFICAR ESTADO
select dcIndicadorTransmitido from micredito.SOLICITUD where pnCodSolicitud in (742242)
--[{"FechaDesembolso":"2021-09-29T20:52:27.947"},{"FechaDesembolso":"2021-09-29T20:57:02.373"}]
update micredito.SOLICITUD set dcIndicadorTransmitido = 'P' WHERE pnCodSolicitud in  (739694)


SELECT [ddFechaPrimerPago], [dnDiaPago],dmCuotaMensualAprobada,[dmPrimeraCuota] ,[ddFechaCalculo]FROM micredito.CREDITOCONSUMO where [pnCodSolicitud] in (742242)

--**************************************************---------------------------*****************

select AnalistaFechaPrimerPago,AnalistaDiaPago,AnalistaPrimeraCuota,AnalistaCuotaAprobada,[ddFechaCalculo], AnalistaTasaInteres, AnalistaNroCuotas from micredito.CREDITOBANCANEGOCIO where CodSolicitud = 762371
--**************************************************---------------------------*****************

--[{"AnalistaFechaPrimerPago":"2022-03-05T00:00:00","AnalistaDiaPago":5,"AnalistaPrimeraCuota":1265.3700,"AnalistaCuotaAprobada":1265.3700,"ddFechaCalculo":"2022-02-05T13:46:25.603"}]
UPDATE micredito.CREDITOBANCANEGOCIO SET AnalistaFechaPrimerPago = '2023-09-03', AnalistaDiaPago = '03', AnalistaPrimeraCuota='958.73', AnalistaCuotaAprobada='827.59', ddFechaCalculo= GETDATE() WHERE CodSolicitud = 766606

--[{"ddFechaPrimerPago":"2021-11-02T00:00:00","dnDiaPago":2,"dmCuotaMensualAprobada":1648.2000,"dmPrimeraCuota":1752.2000,"ddFechaCalculo":"2021-09-29T20:52:24.333"}]
--Cliente VELASCO PANIAGUA FRAHAN REINALDO - 5836039 SC
--Fecha Desembolso: 29/09/2021
--Fecha Pago: 02/12/2021
--Monto Desembolso: 80,000 Bs
--Operación: CCZ90091521
--Tasa: 14.5%
--Plazo: 78 Meses
--Seg. Desgravamen: 1.1%
--Antigua Primera Cuota: 1,752.20
--Nueva Primera Cuota: 2,271.79
--Cuotas Siguientes: 1,647.79
UPDATE micredito.CREDITOCONSUMO SET [dmPrimeraCuota] ='298.80',dmCuotaMensualAprobada = '180.41',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 760614

--[{"ddFechaPrimerPago":"2021-11-10T00:00:00","dnDiaPago":10,"dmCuotaMensualAprobada":5698.8200,"dmPrimeraCuota":6881.0300,"ddFechaCalculo":"2021-09-29T20:56:58.133"}]
--Cliente MAMANI MAYTA RUBEN LUIS - 4775439 LP
--Fecha Desembolso: 29/09/2021
--Fecha Pago: 10/12/2021
--Monto Desembolso: 274,400 Bs
--Operación: CCZ90113021
--Tasa: 13%
--Plazo: 72 Meses
--Seg. Desgravamen: 1.1%
--Antigua Primera Cuota: 6,691.73
--Nueva Primera Cuota: 8,491.86
--Cuotas Siguientes: 5,697.55
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2023-08-14', [dnDiaPago]= '14',[dmPrimeraCuota] ='795.59',dmCuotaMensualAprobada = '778.87',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 742242


UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-03', [dnDiaPago]= '03',[dmPrimeraCuota] ='563.11',dmCuotaMensualAprobada  = '530.29',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 676975
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-10', [dnDiaPago]= '10',[dmPrimeraCuota] ='1239.54',dmCuotaMensualAprobada = '1042.33',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677789
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-15', [dnDiaPago]= '15',[dmPrimeraCuota] ='1957.92',dmCuotaMensualAprobada = '1475.3',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677607
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-20', [dnDiaPago]= '20',[dmPrimeraCuota] ='1078.55',dmCuotaMensualAprobada = '839.61',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677492
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-10', [dnDiaPago]= '10',[dmPrimeraCuota] ='5073.16',dmCuotaMensualAprobada = '4217.95',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677976
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-15', [dnDiaPago]= '15',[dmPrimeraCuota] ='1990.86',dmCuotaMensualAprobada = '1537.45',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677406
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-23', [dnDiaPago]= '23',[dmPrimeraCuota] ='2202.53',dmCuotaMensualAprobada = '1496.03',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 676824
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-05', [dnDiaPago]= '05',[dmPrimeraCuota] ='4830.97',dmCuotaMensualAprobada = '4330.19',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677922
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-18', [dnDiaPago]= '18',[dmPrimeraCuota] ='1067.48',dmCuotaMensualAprobada = '773.81',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677003
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2021-12-27', [dnDiaPago]= '27',[dmPrimeraCuota] ='585.43',dmCuotaMensualAprobada  = '574.89',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 674576
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-20', [dnDiaPago]= '20',[dmPrimeraCuota] ='5034.3',dmCuotaMensualAprobada  = '3532.81',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677927
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-15', [dnDiaPago]= '15',[dmPrimeraCuota] ='3133.86',dmCuotaMensualAprobada = '2414.53',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677845
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-25', [dnDiaPago]= '25',[dmPrimeraCuota] ='3571.41',dmCuotaMensualAprobada = '2414.36',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677244
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-20', [dnDiaPago]= '10',[dmPrimeraCuota] ='217.04',dmCuotaMensualAprobada  = '152.52',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 676396
UPDATE micredito.CREDITOCONSUMO SET [ddFechaPrimerPago] = '2022-01-16', [dnDiaPago]= '16',[dmPrimeraCuota] ='5542.82',dmCuotaMensualAprobada = '4123.95',[ddFechaCalculo] = GETDATE() where [pnCodSolicitud] = 677623

-------------------------------------CASO GREMIO - EMPLOYED--- caso muy excepcional 
--1. HACER COREGIR CON DANIELA
--2. MODIFICAR LOS DATOS PARA TOMARLOS NUEVAMENETE -> (LOS DIAS DE DIFERENCIA NO DEBE SER <30 ni mayor a ¿60 o 90?)
--303-301-08417348
--303-301-08417348

--MODIFICAR FECHA DESEMBOLSO
select * from micredito.DESEMBOLSO_MICREDITO where pnCodSolicitud in  (742242)
update micredito.DESEMBOLSO_MICREDITO set FechaDesembolso = GETDATE() where pnCodSolicitud in  (766606)
select  dcIndicadorTransmitido,ddFechaTransmision from micredito.SOLICITUD WHERE pnCodSolicitud IN (742242)
update micredito.SOLICITUD set ddFechaFinCalificacion = GETDATE() where pnCodSolicitud = 766606
update micredito.SOLICITUD set dcIndicadorTransmitido = 'N' WHERE pnCodSolicitud IN (765006)


--pyme, HIPOTECARIO y consumo = P
--MICREOCREDITO = N
--PARA Q TOME AL MOMENTO = R (casos excepciones)
------------------------------------------------------------------------FIN-----------------------------------------------------------------------

------------------------------------------------------------------------PROBLEMA DATOS CONYUGUE (NO APARECEN DATOS DEL TITULAR)---------------------------------------------------

micredito.spMicDigGetDatosPersonaEN 436300,'1'

UPDATE micredito.SOLICITUDPERSONA SET ddFechaIngresoEmpresa= '1900-01-01 00:00:00',	ddFechaInicioEnCargo= '1900-01-01 00:00:00',ddViviendaResideDesde= '1900-01-01 00:00:00',dnNumDependientes=0,dnOtroIngresoMensual=0 WHERE pnCodSolicitud = 436300 AND pcIndicadorTipoPersona = '1'


UPDATE micredito.SOLICITUDPERSONA SET ddFechaIngresoEmpresa = '1900-01-01 00:00:00',ddFechaInicioEnCargo = '1900-01-01 00:00:00',dnOtroIngresoMensual = 0 WHERE pnCodSolicitud = 431973 AND pcIndicadorTipoPersona = '1'
------------------------------------------------------------------------FIN-----------------------------------------------------------------------
------------------------------------------------------------------------Tasa de pantalla APROBACION-----------------------------------------------------------------------

SELECT ddTasa FROM [micredito].[TARIFARIOTASAS_PYME] WHERE dcCodTama�oEmpresa= '1' AND 5<=ddTasa AND @Tasa<=ddTasa

select * from micredito.SOLICITUD WHERE dcNumSolicitud = 'CHA07012007'

select * from micredito.SOLICITUDINFOADICIONAL WHERE CodSolicitud = 465044
--Agregar en la tabla �micredito.SOLICITUDINFOADICIONAL�, los IndicadorTipoRegistro 16, 19, 20 y 23 (23 doble) en caso de no existir
------------------------------------------------------------------------FIN-----------------------------------------------------------------------
------------------------------------------------------------------------EN DIRECTO------------------------------------------------------------------------

SELECT DISTINCT S.pnCodSolicitud,S.dcNumSolicitud,S.fcCodEstadoSolicitud,SP.pcIndicadorTipoPersona FROM micredito.SOLICITUDPERSONA SP INNER JOIN micredito.SOLICITUD S on (S.pnCodSolicitud = SP.pnCodSolicitud) WHERE dcNumIDC = '01852729TJ'

SELECT pcIndicadorTipoPersona,dcNumIDC FROM micredito.SOLICITUDPERSONA WHERE pnCodSolicitud = 435304


---@vchSQLIDC-----------funcion para ver las solicitudes del titutlar
SELECT f.pnCodSolicitud from micredito.fnSsIDCTitular ('Q', '01852729TJ', null ) f 
-- contenido de la funcion
		select  so.pnCodSolicitud from  micredito.SOLICITUDPERSONA sp (NOLOCK) inner join micredito.SOLICITUD so (NOLOCK) on (so.pnCodSolicitud = sp.pnCodSolicitud and sp.pcIndicadorTipoPersona in ('T','M'))	where(fcTipoIDC = 'Q') and (dcNumIDC = '01852729TJ')

--@vchSQLCal select * from micredito.fnMicDigResultDetalleCal ('Q', '01852729TJ', null, null, null, null,null, NULL, NULL, null, null, null, null, null, null , null)
--@vchSQLCorr select * from micredito.fnMicDigResultDetalleCorr ('Q', '01852729TJ', null, null, null, null, null, NULL, NULL, null, null, null, null, null, null , null)

------------------------------------------------------------------------FIN-----------------------------------------------------------------------


select DISTINCT pcIndicadorTipoPersona from micredito.SOLICITUDPERSONA WHERE dcCIC = '1000010476553'


select * from micredito.TIPOSOLICITUD WHERE pnCodTipoSolicitud = '04'
select * from micredito.PRODUCTO WHERE fcCodTipoSolicitud = '04'
select * from [micredito].[TIPOPERSONA]
--select * from [micredito].[TMPTXCLIENTESALS]
select dcNumSolicitud,dcIndicadorTransmitido,* from micredito.SOLICITUD 



SELECT * FROM OPENQUERY ([NSAP], 'SELECT TOP 5 pcIndicadorTipoPersona FROM SOLICITUDPERSONA where pnCodSolicitud = 235515')
--{"pnCodSolicitud":235515}

--[{"pnCodSolicitud":126855},{"pnCodSolicitud":94732},{"pnCodSolicitud":37879},{"pnCodSolicitud":138442},{"pnCodSolicitud":138931}]


SELECT pcIndicadorTipoPersona,pnCodSolicitud FROM micredito.SOLICITUDPERSONA SP WHERE pcIndicadorTipoPersona <> 'T' AND 5 > (SELECT COUNT (pnCodSolicitud) FROM micredito.SOLICITUDPERSONA WHERE pnCodSolicitud = SP.pnCodSolicitud AND pcIndicadorTipoPersona <> 'T')


------SP para micgrar personas de calificacion a soliciud
[micredito].[spMicDigProcCreaSolPersonaBP_aux] 


----------------------------------------------ACTUALIZACION CIIU
select fcCodCIIU from micredito.SOLICITUDPERSONA where pnCodSolicitud = 460883 -- 431973
select fcCodCIIU from micredito.SOLICITUDEMPRESA where pnCodSolicitud = 460883

UPDATE micredito.SOLICITUDPERSONA SET fcCodCIIU =50300   where pnCodSolicitud = 460883
UPDATE micredito.SOLICITUDEMPRESA SET fcCodCIIU =50300   where pnCodSolicitud = 460883
----------------------------------------------

---------------------------------------------------TARJETA-------NO MUESTRA PERFIL CONSULTA---------------------------------------------
--SIEMPRE DEBE SER NDI Y NO "NO" O "TEC"

  SELECT distinct  SP.fcCodInstruccion  FROM micredito.PERSONA P  INNER JOIN micredito.SOLICITUDPERSONA SP ON (P.pnCodPersona = SP.pnCodPersona) inner join micredito.SOLICITUD S ON (S.pnCodSolicitud = SP.pnCodSolicitud) where S.dcNumSolicitud IN ('BNO12220128') AND SP.pcIndicadorTipoPersona = 'T'
  
  UPDATE SP  SET fcCodInstruccion = 'NDI' FROM micredito.SOLICITUDPERSONA SP  INNER JOIN micredito.PERSONA P ON (P.pnCodPersona = SP.pnCodPersona) inner join micredito.SOLICITUD S ON (S.pnCodSolicitud = SP.pnCodSolicitud) where S.dcNumSolicitud IN ('TCA23092030'  ) AND SP.pcIndicadorTipoPersona = 'C'



----------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------TARJETA  NO REALIZA EL DESEMBOLSO---------------------------------------------------------
--Validar que exista la campa�a (CodRelacion):
select * from micredito.RELACIONOBJINVES where CodObjInves = 33

select * from micredito.RELACIONOBJINVES where CodRelacion = 822

insert into micredito.RELACIONOBJINVES values (822, 'CA', 33, null, 'A', GETDATE(), GETDATE())
----------------------------------------------TARJETA  NO REALIZA EL DESEMBOLSO---------------------------------------------------------
------------------------------------------------------------------------------------------------------------------MIGRACION MIC
SELECT dcIndicadorTransmitido FROM micredito.SOLICITUD where pnCodSolicitud = 690571
UPDATE micredito.SOLICITUD SET dcIndicadorTransmitido = 'N' where pnCodSolicitud = 461707
SELECT * FROM micredito.SEGUROS_HIP_VEHI_CONS WHERE pnCodSolicitud_IN = 689587
SELECT * FROM micredito.CREDITOHIPOTECARIO WHERE pnCodSolicitud = 689587

SELECT * FROM OPENQUERY ([NSAP], 'SELECT * FROM SOLICITUD where dcNumSolicitud = ''CHA24010087'' ')
SELECT * FROM OPENQUERY ([NSAP], 'SELECT * FROM CREDITOHIPOTECARIO where pnCodSolicitud = ''519116'' ')
SELECT * FROM OPENQUERY ([NSAP], 'SELECT * FROM SOLICITUDPERSONA where pnCodSolicitud = ''519116'' ')
SELECT * FROM OPENQUERY ([NSAP], 'SELECT * FROM SOLICITUDEMPRESA where pnCodSolicitud = ''519116'' ')

DELETE OPENQUERY ([NSAP], 'SELECT * FROM SOLICITUD where dcNumSolicitud = ''CHA24010087'' ')
--UPDATE OPENQUERY ([NSAP], 'SELECT dcIndicadorTransmitido FROM SOLICITUD where dcNumSolicitud = ''CHA24010087'' ')dcIndicadorTransmitido

UPDATE OPENQUERY([NSAP],'SELECT dcIndicadorTransmitido from dbo.SOLICITUD where pnCodSolicitud=519116') SET dcIndicadorTransmitido = 'P'


------------------------------------------------------------------------------------------------------------------MIGRACION MIC

SELECT * FROM micredito.SOLICITUD WHERE dcNumSolicitud = 'BNN18030001'

SELECT * FROM micredito.SOLICITUD WHERE dcNumSolicitud = 'BNN18030001'


------------------------------------------------------------------------------------------------------------------AUTONOMIAS HIPOTECARIO,TARJETA----------------------------------------------------------------------------------------
--las autonomias de una matricula especifica para una solicitud especifica

exec micredito.spRD_GetSolicitudDecision1 718043,'B03410'
--garantia @IndicadorGarantia
select (CASE WHEN (SELECT COUNT(pnCodSolicitud) FROM [micredito].[BP_GARANTIAS] WHERE pnCodSolicitud=719630)>0 THEN 'S' ELSE 'N' END)  


--TARJETA
SELECT fcCodMoneda, pnCodSolicitud, fcCodTipoSolicitud from micredito.SOLICITUD where  dcNumSolicitud= 'CCA24082205'


select *
 FROM micredito.SOLICITUD s 
 --inner join micredito.CREDITOHIPOTECARIO cc on s.pnCodSolicitud = cc.pnCodSolicitud
 inner join micredito.TARJETACREDITO tc on s.pnCodSolicitud = tc.pnCodSolicitud
 --inner join  micredito.CREDITOCONSUMO cc on s.pnCodSolicitud = cc.pnCodSolicitud
 --inner join micredito.CREDITOBANCANEGOCIO bc on s.pnCodSolicitud = bc.CodSolicitud
 inner join micredito.SOLICITUDPERSONA sp  ON s.pnCodSolicitud = sp.pnCodSolicitud and  sp.pcIndicadorTipoPersona in ('T', 'M', 'P')
 inner join micredito.TIPOSOLICITUD ts on ts.pcCodTipoSolicitud = s.fcCodTipoSolicitud
 inner join micredito.OBJINVES o  on o.Nombre = 'AUTSBP_AutonomiasBancaPersonal'
 inner join micredito.RELACIONOBJINVES ro on o.CodObjInves = ro.CodObjInves and (( ro.CodTipoRelacion = 'BA' ) OR ( ro.CodTipoRelacion = 'TS' AND ro.CodRelacion = s.fcCodTipoSolicitud ) OR ( ro.CodTipoRelacion = 'PD' AND ro.CodRelacion = s.fcCodProducto ) OR ( ro.CodTipoRelacion = 'CA' AND ro.CodRelacion = s.fcCodCampana ))
 inner join micredito.RELACIONOBJINVESAUTONOMIA roa  on roa.CodRelObjInves = ro.CodRelObjInves and roa.Activo = 'A' and roa.NombreUsuario = 'S94427' 

 --tarjeta
 AND roa.RangoAniosPlazoMenor <= 0 and roa.RangoAniosPlazoMayor >= 0 AND
 (roa.MontoLimiteAprobacion) >= (tc.dmLineaCreditoAprobado * (CASE @CodMoneda WHEN '01' THEN 1 ELSE @TipoCambio END)) 
 --consumo
 --and roa.RangoAniosPlazoMenor <= isnull(cc.dnPlazoAprobado, 0) / 12.00 
	--		and roa.RangoAniosPlazoMayor >= isnull(cc.dnPlazoAprobado, 0) / 12.00 
	-- 		and (roa.MontoLimiteAprobacion) >= (cc.dmMontoAprobado * (CASE @CodMoneda WHEN '01' THEN 1 ELSE @TipoCambio END))
--TARJETA
--select roa.RangoAniosPlazoMenor,roa.RangoAniosPlazoMayor,roa.MontoLimiteAprobacion,tc.dmLineaCreditoAprobado * (1) FROM micredito.SOLICITUD s  inner join micredito.TARJETACREDITO tc on s.pnCodSolicitud = tc.pnCodSolicitud inner join micredito.SOLICITUDPERSONA sp  ON s.pnCodSolicitud = sp.pnCodSolicitud and  sp.pcIndicadorTipoPersona in ('T', 'M', 'P') inner join micredito.TIPOSOLICITUD ts on ts.pcCodTipoSolicitud = s.fcCodTipoSolicitud inner join micredito.OBJINVES o  on o.Nombre = 'AUTSBP_AutonomiasBancaPersonal' inner join micredito.RELACIONOBJINVES ro on o.CodObjInves = ro.CodObjInves and (( ro.CodTipoRelacion = 'BA' ) OR ( ro.CodTipoRelacion = 'TS' AND ro.CodRelacion = s.fcCodTipoSolicitud ) OR ( ro.CodTipoRelacion = 'PD' AND ro.CodRelacion = s.fcCodProducto ) OR ( ro.CodTipoRelacion = 'CA' AND ro.CodRelacion = s.fcCodCampana )) inner join micredito.RELACIONOBJINVESAUTONOMIA roa  on roa.CodRelObjInves = ro.CodRelObjInves and roa.Activo = 'A' and roa.NombreUsuario = 'B03053' AND roa.IndicadorGarantia ='N' and roa.dcCodProducto = s.fcCodProducto WHERE  s.pnCodSolicitud =  694549
 WHERE  s.pnCodSolicitud =  693835

 select ro.CodRelObjInves  FROM micredito.SOLICITUD s inner join  micredito.CREDITOHIPOTECARIO cc on s.pnCodSolicitud = cc.pnCodSolicitud inner join micredito.SOLICITUDPERSONA sp  ON s.pnCodSolicitud = sp.pnCodSolicitud and  sp.pcIndicadorTipoPersona in ('T', 'M', 'P') inner join micredito.TIPOSOLICITUD ts on ts.pcCodTipoSolicitud = s.fcCodTipoSolicitud inner join micredito.OBJINVES o  on o.Nombre = 'AUTSBP_AutonomiasBancaPersonal' inner join micredito.RELACIONOBJINVES ro on o.CodObjInves = ro.CodObjInves and (( ro.CodTipoRelacion = 'BA' ) OR ( ro.CodTipoRelacion = 'TS' AND ro.CodRelacion = s.fcCodTipoSolicitud ) OR ( ro.CodTipoRelacion = 'PD' AND ro.CodRelacion = s.fcCodProducto ) OR ( ro.CodTipoRelacion = 'CA' AND ro.CodRelacion = s.fcCodCampana ))  WHERE  s.pnCodSolicitud =  661111
  --hipotecario
 --and roa.RangoAniosPlazoMenor <= isnull(cc.dnPlazoPagoTotal, 0)  and roa.RangoAniosPlazoMayor >= isnull(cc.dnPlazoPagoTotal, 0)  and roa.IndicadorGarantia = 'N' AND roa.dcTipoSolicitud = s.fcCodTipoSolicitud AND roa.dcCodProducto = s.fcCodProducto

 --hipotecario
 select roa.RangoAniosPlazoMenor, roa.RangoAniosPlazoMayor,cc.dnPlazoPagoTotal ,roa.MontoLimiteAprobacion, (cc.dmMontoAprobado * 1) FROM micredito.SOLICITUD s inner join micredito.CREDITOHIPOTECARIO cc on s.pnCodSolicitud = cc.pnCodSolicitud inner join micredito.SOLICITUDPERSONA sp  ON s.pnCodSolicitud = sp.pnCodSolicitud and  sp.pcIndicadorTipoPersona in ('T', 'M', 'P') inner join micredito.TIPOSOLICITUD ts on ts.pcCodTipoSolicitud = s.fcCodTipoSolicitud inner join micredito.OBJINVES o  on o.Nombre = 'AUTSBP_AutonomiasBancaPersonal' inner join micredito.RELACIONOBJINVES ro on o.CodObjInves = ro.CodObjInves and (( ro.CodTipoRelacion = 'BA' ) OR ( ro.CodTipoRelacion = 'TS' AND ro.CodRelacion = s.fcCodTipoSolicitud ) OR ( ro.CodTipoRelacion = 'PD' AND ro.CodRelacion = s.fcCodProducto ) OR ( ro.CodTipoRelacion = 'CA' AND ro.CodRelacion = s.fcCodCampana )) inner join micredito.RELACIONOBJINVESAUTONOMIA roa  on roa.CodRelObjInves = ro.CodRelObjInves and roa.Activo = 'A' and roa.NombreUsuario = 'BC2295'    and roa.IndicadorGarantia = 'N' AND roa.dcTipoSolicitud = s.fcCodTipoSolicitud AND roa.dcCodProducto = s.fcCodProducto WHERE  s.pnCodSolicitud =  100792 and roa.RangoAniosPlazoMenor <= isnull(cc.dnPlazoPagoTotal, 0)  and roa.RangoAniosPlazoMayor >= isnull(cc.dnPlazoPagoTotal, 0)
 --consumo
 select roa.RangoAniosPlazoMenor, roa.RangoAniosPlazoMayor,cc.dnPlazoAprobado/12 ,roa.MontoLimiteAprobacion, (cc.dmMontoAprobado * 1),fcCodMoneda FROM micredito.SOLICITUD s inner join micredito.CREDITOCONSUMO cc on s.pnCodSolicitud = cc.pnCodSolicitud inner join micredito.SOLICITUDPERSONA sp  ON s.pnCodSolicitud = sp.pnCodSolicitud and  sp.pcIndicadorTipoPersona in ('T', 'M', 'P') inner join micredito.TIPOSOLICITUD ts on ts.pcCodTipoSolicitud = s.fcCodTipoSolicitud inner join micredito.OBJINVES o  on o.Nombre = 'AUTSBP_AutonomiasBancaPersonal' inner join micredito.RELACIONOBJINVES ro on o.CodObjInves = ro.CodObjInves and (( ro.CodTipoRelacion = 'BA' ) OR ( ro.CodTipoRelacion = 'TS' AND ro.CodRelacion = s.fcCodTipoSolicitud ) OR ( ro.CodTipoRelacion = 'PD' AND ro.CodRelacion = s.fcCodProducto ) OR ( ro.CodTipoRelacion = 'CA' AND ro.CodRelacion = s.fcCodCampana )) inner join micredito.RELACIONOBJINVESAUTONOMIA roa  on roa.CodRelObjInves = ro.CodRelObjInves and roa.Activo = 'A' and roa.NombreUsuario = 'B03053'    and roa.IndicadorGarantia = 'S' AND roa.dcTipoSolicitud = s.fcCodTipoSolicitud AND roa.dcCodProducto = s.fcCodProducto WHERE  s.pnCodSolicitud =  706962
 
 select o.CodObjInves  FROM micredito.SOLICITUD s inner join  micredito.CREDITOCONSUMO cc on s.pnCodSolicitud = cc.pnCodSolicitud inner join micredito.SOLICITUDPERSONA sp  ON s.pnCodSolicitud = sp.pnCodSolicitud and  sp.pcIndicadorTipoPersona in ('T', 'M', 'P') inner join micredito.TIPOSOLICITUD ts on ts.pcCodTipoSolicitud = s.fcCodTipoSolicitud inner join micredito.OBJINVES o  on o.Nombre = 'AUTSBP_AutonomiasBancaPersonal' inner join micredito.RELACIONOBJINVES ro on o.CodObjInves = ro.CodObjInves and (( ro.CodTipoRelacion = 'BA' ) OR ( ro.CodTipoRelacion = 'TS' AND ro.CodRelacion = s.fcCodTipoSolicitud ) OR ( ro.CodTipoRelacion = 'PD' AND ro.CodRelacion = s.fcCodProducto ) OR ( ro.CodTipoRelacion = 'CA' AND ro.CodRelacion = s.fcCodCampana ))  WHERE  s.pnCodSolicitud =  713829

 SELECT dcCodProducto FROM micredito.RELACIONOBJINVESAUTONOMIA roa WHERE NombreUsuario = 'BC2295' AND Activo = 'A' AND CodRelObjInves = 59 and roa.IndicadorGarantia = 'S' AND roa.dcTipoSolicitud = '02' AND roa.dcCodProducto = 141

 --pyme
 SELECT roa.RangoAniosPlazoMenor , roa.RangoAniosPlazoMayor, isnull(bc.AnalistaNroCuotas, 0) / 12.00 , roa.IndicadorGarantia ,   roa.dcTipoSolicitud , s.fcCodTipoSolicitud , roa.dcCodProducto , s.fcCodProducto  FROM  micredito.SOLICITUD s inner join micredito.CREDITOBANCANEGOCIO bc on s.pnCodSolicitud = bc.CodSolicitud inner join micredito.TIPOSOLICITUD ts on ts.pcCodTipoSolicitud = s.fcCodTipoSolicitud inner join micredito.OBJINVES o  on o.Nombre = 'AUTSBP_AutonomiasBancaPersonal' inner join micredito.RELACIONOBJINVES ro on o.CodObjInves = ro.CodObjInves and  (( ro.CodTipoRelacion = 'BA' ) OR  ( ro.CodTipoRelacion = 'TS' AND ro.CodRelacion = s.fcCodTipoSolicitud ) OR  ( ro.CodTipoRelacion = 'PD' AND ro.CodRelacion = s.fcCodProducto ) OR  ( ro.CodTipoRelacion = 'CA' AND ro.CodRelacion = s.fcCodCampana ))inner join micredito.RELACIONOBJINVESAUTONOMIA roa on roa.CodRelObjInves = ro.CodRelObjInves and roa.Activo = 'A' and roa.NombreUsuario = 'S75843' AND roa.RangoAniosPlazoMenor <= isnull(bc.AnalistaNroCuotas, 0) / 12.00 and roa.RangoAniosPlazoMayor >= isnull(bc.AnalistaNroCuotas, 0) / 12.00 AND roa.IndicadorGarantia = 'N'  AND roa.dcTipoSolicitud = s.fcCodTipoSolicitud   WHERE s.pnCodSolicitud = 100783
 ---------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------VER HISTORIAL ESTADO ----------------------------------------------------------------------------------------
SELECT pnCodSolicitud,ddFechaSolicitud FROM micredito.SOLICITUD WHERE dcNumSolicitud = 'TCA16832432'
 http://btbnmw00/srv_mic_coordinador/api/HistorialEstados?CodSolicitud=694696
 http://btbnmw00/srv_mic_coordinador/api/Documentos?codSolicitud=68471&usuario=SISTEMA
 http://btbnmw00/srv_mic_coordinador/api/DatosInfoCliente?idc=03091566OR&tipoIDC=Q&complemento=00
 http://btbnmw00/srv_mic_coordinador/api/CompraRefinanciamiento?CodSolicitud=704346

 http://btbnmw00/srv_mic_coordinador/api/DataEntry?codigoSolicitud=886002&matricula=SISTEMAS&xVentasAnuales=610560&xPatrimonio=220684&xNroPersonal=2&xActividad=SERVICIO&descCIIU=SERVICIO PREPARACION Y VENTA DE COMIDAS
 http://btbnmw00/srv_mic_coordinador/api/DataEntry?codigoSolicitud=698759&matricula=SISTEMAS&xVentasAnuales=610560&xPatrimonio=220684&xNroPersonal=2&xActividad=SERVICIO&descCIIU=SERVICIOS DE EXPENDIO DE COMIDAS EN ESTA

 http://cernmw00/srv_mic_coordinador/api/RegistrarFiador?pnCodSolicitud=85643

 http://devnmw00/srv_mic_coordinador/api/Decision?CodSolicitud=485892&diScore=554&dnDetalle=&dcEstado=ACPT&dvUsrCreacion=SISTEMAS

 http://cernmw00/srv_mic_coordinador/api/Filtro?IndicadorTipoPersona=T&mtUsuario=S75843&codSolicitud=499012


 SELECT fcCodEstadoSolicitud,fcCodJustificacion, ddFechaSolicitud, dcNumSolicitud FROM micredito.SOLICITUD WHERE pnCodSolicitud =  713829
 UPDATE micredito.SOLICITUD SET fcCodEstadoSolicitud = 'FACA', fcCodJustificacion = 'COAU' WHERE pnCodSolicitud =  713829
 UPDATE micredito.SOLICITUD SET fcCodEstadoSolicitud = 'DACT' WHERE pnCodSolicitud =  718928

 --HF05","fcCodJustificacion":"PCGA",
 --2021-05-20T00:00:00
 --699817
 --706511

 select * from micredito.JUSTIFICACION WHERE dcDescripcion LIKE '%REVIS%'

 SELECT * FROM micredito.ESTADOSOLICITUD
 --select 
 ---------------------------------------------------------------------------------------------------


 SELECT fcCodCampana,fcCodProducto, fcCodTipoSolicitud FROM micredito.SOLICITUD WHERE  pnCodSolicitud = 664527
update micredito.SOLICITUD set fcCodCampana = 672 WHERE  pnCodSolicitud = 664527


 select pcCodCampana, dcDescripcion from micredito.CAMPANA WHERE dcDescripcion like '%NORMAL DEBIDAMENTE GARANTIZADO%' and fcCodProducto = 117

 -------------------------------NO SE GENERAN FILTROS

 micredito.SELINFOFILTRO3 666632
 micredito.PERSONASCREDITO_GetByCODSOLICITUD 666624

 micredito.SP_ObtnerDocumentosMicredito 666624,'FILTRO'

 micredito.ExtraerCALIF 
 ------------------------------------------------------------------------------levantar retencion SIREJ-------------------------------------------------------------------
 --paso corto al ejecutar se elimina el filtro y registra nuevamente
 --btbnmw00/srv_mic_coordinador/api/Sirej?codSolicitud=679993&Matricula=SISTEMA

 --CONSULTA FILTROS GUARDADOS con el codigo de solicitud
 exec [micredito].[RESULTADO_SERVICIOSelect]760454

 --para levantar restricccion
SELECT  pnCodCalificacion FROM [BD_MICREDITO].[micredito].[CALIFICACIONSOLICITUD]  WHERE pnCodSolicitud in (817711)
SELECT * FROM [BD_MICREDITO].[micredito].[RESULTADO_SERVICIO] WHERE pnCodCalificacion in (1010901) and [dcCodServivio] = 'SIREJ' and [dcCodTipoCalificacion] = 'FILTI CONY'--FILTI CONY

SELECT TOP 10 dcCodEstado,dcDescError,ddFechaCreacion,[dcCodServivio],[dcCodTipoCalificacion] FROM [BD_MICREDITO].[micredito].[RESULTADO_SERVICIO] WHERE pnCodCalificacion = 921303 and [dcCodTipoCalificacion] = 'FILTI CONY'  and [dcCodServivio] = 'ENAMECHEKE'
--'ENAMECHEKE' No se encontro al cliente en Ename Checker guarda con dcCodEstado = 0
UPDATE [BD_MICREDITO].[micredito].[RESULTADO_SERVICIO] SET dcCodEstado = 1,dcDescError = 'N',ddFechaCreacion = GETDATE () WHERE pnCodCalificacion = 1010991  and [dcCodServivio] IN ('SIREJ')

---rollback
UPDATE [BD_MICREDITO].[micredito].[RESULTADO_SERVICIO] SET dcCodEstado = 0,dcDescError = 'Se encontro al cliente en Retension Sirej',ddFechaCreacion = '2023-07-06 09:47:37.203' WHERE pnCodCalificacion = 951352  and [dcCodTipoCalificacion] = 'FILTI CONY'	and [dcCodServivio] = 'SIREJ'
-----------------------------------------------------------------PERFIL CONSULTA------------------------------------------------------------
--ERROR: no muestra los datos en las siguientes pantallas 
--SOLUCCION: uno de los datos persona esta mal
SELECT fcCodInstruccion FROM micredito.SOLICITUDPERSONA WHERE pnCodSolicitud = 705655

UPDATE micredito.SOLICITUDPERSONA SET fcCodInstruccion= 'TIT' where pnCodSolicitud = 705655 AND fcCodInstruccion = '   '
SELECT pcCodInstruccion, dcDescripcion FROM  micredito.INSTRUCCION WHERE pcCodInstruccion = 'TIT'
------------------------------------------------------------NO MUESTRA PERFIL CONSULTA----------------------

-----------------------------------------------PERFIL CONSULTA------------------------------------------------------------

-----------------------------------------------NO QUIERE GENERAR SCORE------------------------------------------------------------
select dcNumIDC from micredito.SOLICITUDPERSONA WHERE pnCodSolicitud = 722540
[micredito].[usp_GetConsultaInfocred]10033756,'VICTOR HUGO MAMI ARIAS',1001 --(5928923 IDC SIN EXTENSION)
select COUNT(*)	from micredito.CONSULTAINFOCRED	where dnNumDocumento = 08329480	and dcNombreCompleto like 'MARY GEMIO HUANCA'	and dnTipoDocumento = 1001
SELECT * FROM micredito.CONSULTAINFOCRED	where dnNumDocumento = 04797828 	and  dcResultadoServicio like '%Se produjo un error mientras%' 
SELECT COUNT(*) FROM micredito.CONSULTAINFOCRED	where dcResultadoServicio like '%Se produjo un error mientras%' AND CONVERT(varchar,dtFechaCreacion,112) between CONVERT(varchar, DATEADD(day, -4, GETDATE()) ,112) AND CONVERT(varchar,GETDATE(),112)
delete micredito.CONSULTAINFOCRED	where dcResultadoServicio like '%Se produjo un error mientras%' AND CONVERT(varchar,dtFechaCreacion,112) between CONVERT(varchar, DATEADD(day, -4, GETDATE()) ,112) AND CONVERT(varchar,GETDATE(),112)

--el error se debe a que hubo problemas con INFOCRED --> MONITOREO

SELECT COUNT(*) FROM micredito.FILE_INFOCRED WHERE Xml like '%Se agotó el tiempo de espera %' AND CONVERT(varchar,FechaCreacion,112) between CONVERT(varchar, DATEADD(day, -1, GETDATE()) ,112) AND CONVERT(varchar,GETDATE(),112)
DELETE micredito.FILE_INFOCRED WHERE Xml like '%Se agotó el tiempo de espera %' AND CONVERT(varchar,FechaCreacion,112) between CONVERT(varchar, DATEADD(day, -1, GETDATE()) ,112) AND CONVERT(varchar,GETDATE(),112)
select * from  micredito.FILE_INFOCRED WHERE Xml like '%Se produjo un error mientras%' AND Documento = 04797828
SELECT COUNT(*) FROM micredito.FILE_INFOCRED WHERE Xml like '%Se produjo un error mientras%' AND CONVERT(varchar,FechaCreacion,112) between CONVERT(varchar, DATEADD(day, -6, GETDATE()) ,112) AND CONVERT(varchar,GETDATE(),112)
delete micredito.FILE_INFOCRED WHERE Xml like '%Se produjo un error mientras%' AND CONVERT(varchar,FechaCreacion,112) between CONVERT(varchar, DATEADD(day, -4, GETDATE()) ,112) AND CONVERT(varchar,GETDATE(),112)

--se debe eliminar de la tabla todos los q se guardaron un xml con mesnaje de error
-----------------------------------------------NO QUIERE GENERAR SCORE------------------------------------------------------------

-------------------------------------------------registrar INDICE TASA TRE----
--------------------------------------------
  SELECT dcIndicadorDesactivada,pcIndice,* FROM [micredito].[INDICETRE] WHERE pcIndice in ('PRU')--12:02
  --[{"fcCodTipoSolicitud":"03","dcNumSolicitud":"CHA30082170         
  insert into [micredito].[INDICETRE] values ('BQZ','N',getdate(),7.49,3.02)
  UPDATE [micredito].[INDICETRE] SET dcIndicadorDesactivada = 'S'  WHERE pcIndice = 'PRU'
  insert into [micredito].[INDICETRE] values ('PRU','N',getdate(),5.49,3.08)

  ------------------------------------------SOLICITUD ENVIADO EN EXCEL CON SOPORTE ----------
  insert into [micredito].[INDICETRE] values ('BQY','N',getdate(),7.99,3.01)

 SELECT COUNT(*) FROM [micredito].[INDICETRE] WHERE pcIndice NOT  IN ('BDA','BDB',	'BDE',	'BDF',	'BDG',	'BDH',	'BDI',	'BDJ',	'BDK',	'BN5',	'BN6',	'BN7',	'BN8',	'BN9',	'BOA',	'BOB',	'BOC',	'BOD',	'BOE',	'BOF',	'BOG',	'BOH',	'BOI',	'BOJ',	'BOK',	'BOL',	'BOM',	'BON',	'BOO',	'BOP',	'BOW',	'BPB',	'BOX',	'BPC',	'BOU',	'BOQ',	'BOR',	'BOY',	'BOS',	'BOT',	'BOV',	'BOZ',	'BO1',	'BO2',	'BO3',	'BO4',	'BO5',	'BO6',	'BO7',	'BO8',	'BO9',	'BPA',	'BPD',	'BPE',	'BPF',	'BPG',	'BPH',	'BPI',	'BPJ',	'BPK',	'BPL',	'BPM',	'BPN',	'BPO',	'BPP',	'BPQ',	'BPS',	'BPT',	'BPU',	'BPV',	'BPW',	'BPX',	'BPY',	'BPZ',	'BQA',	'BQB',	'BQC',	'BQD',	'BQE',	'BQF',	'BQG',	'BQH',	'BQK',	'BQL',	'BQM',	'BQN',	'BQO',	'BQP',	'BQQ',	'BQR',	'BQS',	'BQT',	'BQU'	)
 --93
 --BORRA 79
 SELECT * FROM [micredito].[INDICETRE] WHERE pcIndice IN ('BQV')
	
-------------------------------------------------registrar INDICE TASA TRE------------------------------------------------
-------------------------------------------------NO SE ESTA TRANSMITIENTDO-------------------------------------------------------------------------
--VALIDAR LOS ESTADOS EN
--http://btbnmw00/srv_mic_coordinador/api/HistorialEstados?CodSolicitud=676586
select fcCodEstadoSolicitud,* from micredito.SOLICITUD WHERE pnCodSolicitud = 711736
select * from micredito.SOLICITUDPERSONA WHERE pnCodSolicitud = 674116

--CAMBIAR AL PRIMER ACPD Y JUSTIFICACION
update micredito.SOLICITUD set fcCodEstadoSolicitud = 'DACT' where pnCodSolicitud = 714749
-------------------------------------------------NO SE ESTA TRANSMITIENTDO-------------------------------------------------------------------------
--delete micredito.REFINANCIAMIENTO where NRO_SOLICITUD = 'BNP26101062'
--delete micredito.ITEM_ASFI where ID_REF = 35592
--delete micredito.DETALLE_ASFI where ID_ITEM = 41462
------------------------------------------------BANCA EMPRESA YA SE PROCESO PERO NO DESAPARECE DE LA BANDEJA-------------------------------------------------------------------------
--PARA INGRESAR A BANCA EMPRESA
select * from micredito.BE_PARAMETRO WHERE tipo = 'ACCESS' 
UPDATE micredito.BE_PARAMETRO SET nemonico = 'GGLPZ' WHERE tipo = 'ACCESS' AND id = 54 --como estaba originalmente
UPDATE micredito.BE_PARAMETRO SET nemonico = 'DESARROLLO' WHERE tipo = 'ACCESS' AND id = 54--para desarrollo



--EN LA TABLA DE BE_OPERACION.. ESTA DUPLICADO mismo cliente monto y dia
SELECT *  FROM [micredito].[BE_OPERACION] where  id = 53391
SELECT *  FROM [micredito].[BE_OPERACION] where  solicitud = 'T3107420230721112747'
--delete [micredito].[BE_OPERACION] where  id = 53391
------------------------------------------------YA SE PROCESO PERO NO DESAPARECE DE LA BANDEJA-------------------------------------------------------------------------
------------------------------------------------ADJUNTO QUE NO SE MUESTRA BE-------------------------------------------------------------------------
SELECT *  FROM [micredito].[BE_OPERACION] where monto like '16,940.56%'
SELECT *  FROM [micredito].[BE_OPERACION] where solicitud like 'T0640520230215111103%'
SELECT * FROM micredito.BE_DOCUMENTACIONCONTRATO WHERE id =49244
SELECT * FROM micredito.BE_ADJUNTO WHERE id = 37533
--37533
--38511

--3088
------------------------------------------------ADJUNTO QUE NO SE MUESTRA BE-------------------------------------------------------------------------
------------------------------------------------DUPLICADO EN BANCA EMPRESA------------------------------------------------------------------
	--SELECT op.id, op.solicitud, rdc.NroCuenta as nroCuentaActivacion, op.nombrecliente as nombreCliente, op.nroIdentidad, es.estadoSolicitud, es.estadoDocsContrato, es.estadoDocsDesembolso, es.estadoContrato, es.estadoDigitacion, es.fechaInicio, es.fechaObsDocsContrato, es.fechaRegDocsContrato, es.fechaObsDocsContrato2, es.fechaRegDocsContrato2, es.fechaObsDocsContrato3, es.fechaRegDocsContrato3, es.fechaObsDocsContrato4, es.fechaRegDocsContrato4, es.fechaRevDocsContrato, es.fechaIngDocsDesembolso, es.fechaObsDocsDesembolso, es.fechaRegDocsDesembolso, es.fechaObsDocsDesembolso2, es.fechaRegDocsDesembolso2, es.fechaObsDocsDesembolso3, es.fechaRegDocsDesembolso3, es.fechaObsDocsDesembolso4, es.fechaRegDocsDesembolso4, es.fechaRevDocsDesembolso, es.fechaElaboracionContrato, es.fechaObsContrato, es.fechaCorrecionContrato, es.fechaRevContrato, es.fechaDigitacion, es.fechaObsActivacion, es.fechaRegObsActivacion, es.fechaActivacion, op.sucursal, op.tipoOperacion, op.codigoLinea, op.monto, op.moneda, op.plazoTotal, op.plazo, op.aPartirDe, op.formaPago, op.detalleFormaPago, op.tasaInteres, op.beneficiario, op.nroCuenta, op.garantia, op.garantiaOtros, op.nroGarantia, op.ciiu, op.banca, op.nombreFFNN, op.tipoSolicitud, op.testimonios, es.usuarioAuxiliar, es.usuarioFiscal, es.usuarioSupervisor, es.desestimadoPor, es.autorizadoPor, op.usuarioCreador, op.usuarioModificador
	--FROM 
	--INNER JOIN micredito.BE_ESTADO es ON 
	--inner join micredito.BE_DOCUMENTACIONCONTRATO dc on dc.id_operacion = op.id
	--left join micredito.BE_DOCUMENTACIONDESEMBOLSO dd on dd.id_operacion = op.id
	--left join micredito.BE_ACTIVACIONDESEMBOLSO ad on ad.id_operacion = op.id
	--left join micredito.BE_ENVIOCONTRATO ec on ec.id_operacion = op.id
	--left join micredito.BE_REGISTRODATOSCONSIST rdc on rdc.id_operacion = op.id
	--left join micredito.BE_REVISIONCONTRATO rc on rc.id_operacion = op.id
	--where es.id_operacion = 49243


	SELECT rdc.NroCuenta as nroCuentaActivacion, es.estadoSolicitud, es.estadoDocsContrato, es.estadoDocsDesembolso, es.estadoContrato, es.estadoDigitacion, es.fechaInicio, es.fechaObsDocsContrato, es.fechaRegDocsContrato, es.fechaObsDocsContrato2, es.fechaRegDocsContrato2, es.fechaObsDocsContrato3, es.fechaRegDocsContrato3, es.fechaObsDocsContrato4, es.fechaRegDocsContrato4, es.fechaRevDocsContrato, es.fechaIngDocsDesembolso, es.fechaObsDocsDesembolso, es.fechaRegDocsDesembolso, es.fechaObsDocsDesembolso2, es.fechaRegDocsDesembolso2, es.fechaObsDocsDesembolso3, es.fechaRegDocsDesembolso3, es.fechaObsDocsDesembolso4, es.fechaRegDocsDesembolso4, es.fechaRevDocsDesembolso, es.fechaElaboracionContrato, es.fechaObsContrato, es.fechaCorrecionContrato, es.fechaRevContrato, es.fechaDigitacion, es.fechaObsActivacion, es.fechaRegObsActivacion, es.fechaActivacion, es.usuarioAuxiliar, es.usuarioFiscal, es.usuarioSupervisor, es.desestimadoPor, es.autorizadoPor	FROM micredito.BE_ESTADO es 	inner join micredito.BE_DOCUMENTACIONCONTRATO dc on dc.id_operacion = 49244	left join micredito.BE_DOCUMENTACIONDESEMBOLSO dd on dd.id_operacion = 49244	left join micredito.BE_ACTIVACIONDESEMBOLSO ad on ad.id_operacion = 49244	left join micredito.BE_ENVIOCONTRATO ec on ec.id_operacion = 49244	left join micredito.BE_REGISTRODATOSCONSIST rdc on rdc.id_operacion = 49244	left join micredito.BE_REVISIONCONTRATO rc on rc.id_operacion = 49244	where es.id_operacion = 49244
	SELECT COUNT(*)	FROM micredito.BE_ESTADO es 	inner join micredito.BE_DOCUMENTACIONCONTRATO dc on dc.id_operacion = 49244	left join micredito.BE_DOCUMENTACIONDESEMBOLSO dd on dd.id_operacion = 49244	left join micredito.BE_ACTIVACIONDESEMBOLSO ad on ad.id_operacion = 49244	left join micredito.BE_ENVIOCONTRATO ec on ec.id_operacion = 49244	left join micredito.BE_REGISTRODATOSCONSIST rdc on rdc.id_operacion = 49244	left join micredito.BE_REVISIONCONTRATO rc on rc.id_operacion = 49244	where es.id_operacion = 49244

	select * from micredito.BE_ESTADO es where es.id_operacion = 54333
	DELETE micredito.BE_ESTADO where id = 51307

	select count(*) from micredito.BE_DOCUMENTACIONCONTRATO dc where dc.id_operacion = 49244
	select count(*) from micredito.BE_DOCUMENTACIONDESEMBOLSO dd where dd.id_operacion = 49244
	DELETE  micredito.BE_DOCUMENTACIONDESEMBOLSO  where id=48345

	select count(*) from micredito.BE_ACTIVACIONDESEMBOLSO ad where ad.id_operacion = 49244
	select count(*) from micredito.BE_ENVIOCONTRATO ec where ec.id_operacion = 49244
	select count(*) from micredito.BE_REGISTRODATOSCONSIST rdc where rdc.id_operacion = 49244
	select count(*) from micredito.BE_REVISIONCONTRATO rc where rc.id_operacion = 49244
------------------------------------------------DUPLICADO EN BANCA EMPRESA------------------------------------------------------------------

------------------------------------------------ERROR DE CODIGO DE CODIGO DE REPARTO QUE PROVOCA DESCUADRE-------------------------------------------------------------------------

--validador 0.0
SELECT S.pnCodSolicitud,S.fcCodOficina,S.dcCodReparto,S.dcUsuarioFiscalizador, D.FechaDesembolso FROM micredito.SOLICITUD S INNER JOIN micredito.DESEMBOLSO_MICREDITO D ON D.pnCodSolicitud = S.pnCodSolicitud WHERE D.FechaDesembolso > (GETDATE ()) and S.dcCodReparto not in ('101','201','301','401','501','601','701','801','901')
--S.fcCodVendedor

--validar si existe solicitudes activas con un mas codRepart -> descuadre
select S.dcNumSolicitud,S.dcCodReparto,S.pnCodSolicitud,S.ddFechaUltimoEstado,E.pcCodEstadoSolicitud,E.dcDescripcion  from micredito.SOLICITUD S INNER JOIN micredito.ESTADOSOLICITUD E ON E.pcCodEstadoSolicitud = S.fcCodEstadoSolicitud WHERE dcNumSolicitud like '%CH%' AND S.dcCodReparto = '0.0' AND E.pcCodEstadoSolicitud NOT IN  ('ACPD','DACT','DCLD') order by S.ddFechaUltimoEstado

SELECT S.dcNumSolicitud,S.pnCodSolicitud,S.fcCodOficina,S.dcCodReparto,S.dcUsuarioFiscalizador, D.FechaDesembolso FROM micredito.SOLICITUD S INNER JOIN micredito.DESEMBOLSO_MICREDITO D ON D.pnCodSolicitud in (680957,680962 )
--SELECT S.dcNumSolicitud,S.pnCodSolicitud,S.fcCodOficina,S.dcCodReparto,S.dcUsuarioFiscalizador, D.FechaDesembolso FROM micredito.SOLICITUD S INNER JOIN micredito.DESEMBOLSO_MICREDITO D ON D.pnCodSolicitud = S.pnCodSolicitud WHERE S.pnCodSolicitud = '41858'

------------------------------------------------ERROR DE CODIGO DE CODIGO DE REPARTO QUE PROVOCA DESCUADRE-------------------------------------------------------------------------

SELECT  D.* FROM micredito.SOLICITUD S INNER JOIN micredito.DESEMBOLSO_MICREDITO D ON D.pnCodSolicitud = S.pnCodSolicitud WHERE S.pnCodSolicitud = '679730'

select * from micredito.CREDITOCONSUMO --WHERE

---------------------------------------------------LAS GARANTIAS NO SE ACTIVARON---------------------------------
--LA CREACION DEBE SER CORRECTA 'S'
SELECT COUNT(*)  FROM [BD_MICREDITO].[micredito].[CONTROLGARANTIA_BP] WHERE CONVERT(VARCHAR,ddFechaCreacion,112) = CONVERT(VARCHAR,GETDATE(),112) AND dcIndicadorActivacion = 'N'
--CUANDO EXISTE UN PROBLEMA CON LOS CREDENCIALES DE EXTRA B91056
--1. DETENER EL SERVICIO
--2. ESPERAR 1 HORA
--3. ACTUALIZAR LA FECHA DE LOS Q NO SE ACTIVARON (si es de otro dia el problema)
update [BD_MICREDITO].[micredito].[CONTROLGARANTIA_BP] SET ddFechaCreacion = GETDATE() where CONVERT(VARCHAR,ddFechaCreacion,112) = CONVERT(VARCHAR,GETDATE()-1,112) AND dcIndicadorActivacion = 'N'
--4. VER EL LOOG SI ALGUNA DICE "GARANTIA YA ACTIVADA" (ubicarlo por el nro de garantia [micredito].[CONTROLGARANTIA_BP])
--5. CAMBIAR EL ESTADO MANUALMENTE DE ESTA GARANTIA dcIndicadorActivacion = 'S'
--6. INICIAR EL ROBOT

--EL CAMBIO SE VERA AL DIA SIGUEINTE (dia habil) EN PROCESO BATCH EN REPEXT
---------------------------------------------------LAS GARANTIAS NO SE ACTIVARON---------------------------------
-----------------------------------------------------CASO DESMBOLSO REUTILIZACION NORMAL CON TRANSMISION Y AL DIA SIGUIENTE EXTORNO REUTILIZACION-----------------------------------
--validar prestamo
select * from micredito.TRANSACCION WHERE CODIGO_SOLICITUD_IN = 690712
--validar reutilizacion
select * from [micredito].[COMPRA_DEUDA_REUTILIZACION] where fcCod_Solicitud = 704346
--MANDAR CAPTURA DEL EXTRA DEL USUARIO QUE REALIZO EL EXTORNO
--Y Jesus Remigio, CTPV y Contabilidad
--rebisar el el JOB de peru que hace el doble registro para la confirmacion (puede ser que este le haya dato el extorno)
-----------------------------------------------------CASO DESMBOLSO REUTILIZACION NORMAL CON TRANSMISION Y AL DIA SIGUIENTE EXTORNO REUTILIZACION-----------------------------------

--------------------------------------------------------PLAZO DE MICROCREDITO-----------------------------------------
SELECT P.pnCodProducto, P.dcDescripcionProducto, P.dnCPOP, T.ddTasa , P.dnPlazoMin , P.dnPlazoMax  
FROM micredito.CAMPANA as C 
INNER JOIN micredito.PRODUCTOMICROCREDITO P on P.pnCodProducto=C.pcCodCampana 
INNER JOIN micredito.TARIFARIOMICROCREDITO T ON T.dcTipoCredito = 'No Productivo'  AND '185000' BETWEEN T.dmMontoMin AND T.dmMontoMax
WHERE P.pnCodProducto = 1 AND '185000' BETWEEN P.dmMontoMinFinanciar AND P.dmMontoMaxFinanciar 
			AND @plazo BETWEEN P.dnPlazoMin AND P.dnPlazoMax 	
			AND P.dnExcepcion=1	

			SELECT * FROM  micredito.PRODUCTOMICROCREDITO T
			select AnalistaCuotaAprobada, AnalistaTotalMontoDesembolsar,dcProductoMicrocredito from micredito.CREDITOBANCANEGOCIO WHERE CodSolicitud = 694432

SELECT * FROM micredito.PRODUCTOMICROCREDITO WHERE pnCodProducto = 701 and dnCPOP = 0 and dnExcepcion = 1
UPDATE micredito.PRODUCTOMICROCREDITO SET dnPlazoMax = 48 WHERE pnCodProducto = 700 and dnCPOP = 0 and dnExcepcion = 1
UPDATE micredito.PRODUCTOMICROCREDITO SET dnPlazoMax = 48 WHERE pnCodProducto = 701 and dnCPOP = 0 and dnExcepcion = 1
--------------------------------------------------------PLAZO DE MICROCREDITO-----------------------------------------

---------------------------------------------------------PROBLEMA DE APROBACION PYME-------------------------------------------------


select pnCodCalificacion from micredito.CALIFICACIONSOLICITUD where pnCodSolicitud = 690959

SELECT * from micredito.SOLICITUD S 
 INNER JOIN micredito.SOLICITUDPERSONA SP on SP.pnCodSolicitud = S.pnCodSolicitud
 INNER JOIN micredito.CALIFICACIONSOLICITUD CS ON S.pnCodSolicitud = CS.pnCodSolicitud --añadir codCalificacion
 INNER JOIN micredito.CALIFICACIONPERSONA CP ON (RIGHT(CP.dcCIC, 8) = RIGHT(SP.dcCIC, 8) AND CP.pnCodCalificacion = CS.pnCodCalificacion)
 INNER JOIN micredito.TIPOSOLICITUD T on
 S.fcCodTipoSolicitud = T.pcCodTipoSolicitud
 INNER JOIN micredito.PRODUCTO P on
 S.fcCodProducto = P.pcCodProducto
 INNER JOIN micredito.ESTADOCIVIL E on
 SP.fcCodEstadoCivil = E.pcCodEstadoCivil
 INNER JOIN micredito.MONEDA M on
 S.fcCodMoneda = M.pcCodMoneda
 WHERE  S.pnCodSolicitud = 685936 and  SP.pcIndicadorTipoPersona in ('T','M')


 SELECT * from micredito.SOLICITUD S  INNER JOIN micredito.SOLICITUDPERSONA SP on SP.pnCodSolicitud = S.pnCodSolicitud  INNER JOIN micredito.CALIFICACIONSOLICITUD CS <span lang=ES style='font-size:10.0pt;font-family:
"Arial",sans-serif;mso-fareast-font-family:"Times New Roman";mso-ansi-language:
ES;mso-fareast-language:EN-US;mso-bidi-language:AR-SA'><!--[if gte vml 1]><v:shapetype
 id="_x0000_t75" coordsize="21600,21600" o:spt="75" o:preferrelative="t"
 path="m@4@5l@4@11@9@11@9@5xe" filled="f" stroked="f">
 <v:stroke joinstyle="miter"/>
 <v:formulas>
  <v:f eqn="if lineDrawn pixelLineWidth 0"/>
  <v:f eqn="sum @0 1 0"/>
  <v:f eqn="sum 0 0 @1"/>
  <v:f eqn="prod @2 1 2"/>
  <v:f eqn="prod @3 21600 pixelWidth"/>
  <v:f eqn="prod @3 21600 pixelHeight"/>
  <v:f eqn="sum @0 0 1"/>
  <v:f eqn="prod @6 1 2"/>
  <v:f eqn="prod @7 21600 pixelWidth"/>
  <v:f eqn="sum @8 21600 0"/>
  <v:f eqn="prod @7 21600 pixelHeight"/>
  <v:f eqn="sum @10 21600 0"/>
 </v:formulas>
 <v:path o:extrusionok="f" gradientshapeok="t" o:connecttype="rect"/>
 <o:lock v:ext="edit" aspectratio="t"/>
</v:shapetype><v:shape id="_x0000_i1025" type="#_x0000_t75" style='width:217.5pt;
 height:40.5pt' o:ole="">
 <v:imagedata src="file:///C:/Users/S75843/AppData/Local/Temp/msohtmlclip1/01/clip_image001.emz"
  o:title=""/>
</v:shape><![endif]--><![if !vml]><img width=290 height=54
src="file:///C:/Users/S75843/AppData/Local/Temp/msohtmlclip1/01/clip_image002.gif"
v:shapes="_x0000_i1025"><![endif]><!--[if gte mso 9]><xml>
 <o:OLEObject Type="Embed" ProgID="Package" ShapeID="_x0000_i1025"
  DrawAspect="Content" ObjectID="_1736691666">
 </o:OLEObject>
</xml><![endif]--></span>ON S.pnCodSolicitud = CS.pnCodSolicitud  INNER JOIN micredito.CALIFICACIONPERSONA CP ON (RIGHT(CP.dcCIC, 8) = RIGHT(SP.dcCIC, 8) AND CP.pnCodCalificacion = CS.pnCodCalificacion) INNER JOIN micredito.TIPOSOLICITUD T on S.fcCodTipoSolicitud = T.pcCodTipoSolicitud INNER JOIN micredito.PRODUCTO P on S.fcCodProducto = P.pcCodProducto INNER JOIN micredito.ESTADOCIVIL E on SP.fcCodEstadoCivil = E.pcCodEstadoCivil INNER JOIN micredito.MONEDA M on S.fcCodMoneda = M.pcCodMoneda WHERE  S.pnCodSolicitud = 690959 and  SP.pcIndicadorTipoPersona in ('T','M')


SELECT RIGHT(CP.dcCIC, 8) , RIGHT(SP.dcCIC, 8), CP.dcNumIDC, SP.dcNumIDC from micredito.SOLICITUD S  INNER JOIN micredito.SOLICITUDPERSONA SP on SP.pnCodSolicitud = S.pnCodSolicitud  INNER JOIN micredito.CALIFICACIONSOLICITUD CS ON S.pnCodSolicitud = CS.pnCodSolicitud  INNER JOIN micredito.CALIFICACIONPERSONA CP ON ( CP.pnCodCalificacion = CS.pnCodCalificacion)  WHERE  S.pnCodSolicitud = 690959 and  SP.pcIndicadorTipoPersona in ('T','M')



--SELECT pcIndicadorTipoPersona,dcCIC FROM micredito.SOLICITUDPERSONA WHERE pnCodSolicitud = 690959
-- [{"pcIndicadorTipoPersona":"1","dcCIC":"100000814264"},{"pcIndicadorTipoPersona":"M","dcCIC":"100000812487"}]
SELECT pcIndicadorTipoPersona,dcCIC FROM micredito.CALIFICACIONPERSONA WHERE pnCodCalificacion = 877192

UPDATE  micredito.CALIFICACIONPERSONA SET dcCIC = 100000814264 WHERE pnCodCalificacion = 877192 AND pcIndicadorTipoPersona = '1'
UPDATE  micredito.CALIFICACIONPERSONA SET dcCIC = 100000812487 WHERE pnCodCalificacion = 877192 AND pcIndicadorTipoPersona = 'M'

--intentar identificar de manera masiva

SELECT RIGHT(CP.dcCIC, 8) , RIGHT(SP.dcCIC, 8), CP.dcNumIDC, SP.dcNumIDC from micredito.SOLICITUD S  INNER JOIN micredito.SOLICITUDPERSONA SP on SP.pnCodSolicitud = S.pnCodSolicitud  INNER JOIN micredito.CALIFICACIONSOLICITUD CS ON S.pnCodSolicitud = CS.pnCodSolicitud  INNER JOIN micredito.CALIFICACIONPERSONA CP ON ( CP.pnCodCalificacion = CS.pnCodCalificacion)  WHERE   SP.pcIndicadorTipoPersona in ('T','M') and  S.pnCodSolicitud = 485899 

SELECT count(*) from micredito.SOLICITUD S  INNER JOIN micredito.SOLICITUDPERSONA SP on SP.pnCodSolicitud = S.pnCodSolicitud  INNER JOIN micredito.CALIFICACIONSOLICITUD CS ON S.pnCodSolicitud = CS.pnCodSolicitud  INNER JOIN micredito.CALIFICACIONPERSONA CP ON ( CP.pnCodCalificacion = CS.pnCodCalificacion)  WHERE   SP.pcIndicadorTipoPersona in ('T','M') AND S.ddFechaInicioDigitacion > '04-04-2022' and RIGHT(CP.dcCIC, 8) <> RIGHT(SP.dcCIC, 8) and CP.dcNumIDC = SP.dcNumIDC
---------------------------------------------------------PROBLEMA DE APROBACION PYME-------------------------------------------------
---------------------------------------------------------GENERACION DE TARJETAS-------------------------------------------------

SELECT *
from micredito.SOLICITUD SL
inner join micredito.TARJETACREDITO T on SL.pnCodSolicitud =  T.pnCodSolicitud
inner join micredito.SOLICITUDPERSONA SP on SL.pnCodSolicitud = SP.pnCodSolicitud and SP.pcIndicadorTipoPersona in ('T')
--LEFT JOIN micredito.OFICINA OFI on SL.fcCodOficina = OFI.pcCodOficina and OFI.dcActivo = 'A'
--LEFT JOIN micredito.PARAMTRANSMISION on SL.fcCodCampana = micredito.PARAMTRANSMISION.pcCodCampana and dcRequiereTransmision = 'S'
--LEFT JOIN micredito.LOCALIDAD L on SP.fcCodLocalidad = L.pcCodLocalidad
--LEFT JOIN micredito.SOLICITUDEMPRESA SOLICITUDEMPRESATI on SL.pnCodSolicitud = SOLICITUDEMPRESATI.pnCodSolicitud and SOLICITUDEMPRESATI.pcIndicadorTipoEmpresa = 'ET'
--LEFT JOIN micredito.LOCALIDAD LOCALIDADEMPTIT on SOLICITUDEMPRESATI.fcCodLocalidad = LOCALIDADEMPTIT.pcCodLocalidad
INNER JOIN micredito.CREDITOVPCONDPORCAMPANA CCC ON SL.fcCodCampana = CCC.fcCodCampana and T.dcSegmento = CCC.fcCodVPSegmento and CCC.dcHabilitado = 'S'
INNER JOIN micredito.VPCONDICION V on  SL.fcCodProductoHost = V.CodTipoCredito AND T.dcSegmento = V.CodVPSegmento and CCC.dcAfiliacion = V.Afiliacion
LEFT join micredito.TARJETAS_UPGRADE TU on SL.pnCodSolicitud = TU.pnCodSolicitud
WHERE (SL.fcCodEstadoSolicitud = 'ACPD') 
AND SL.fcCodJustificacion <> 'AMPL'
AND SL.fcCodTipoSolicitud = '01'
AND SL.dcIndicadorTransmitido <> 'S'
AND  V.PCT_ID<>''
AND CONVERT(VARCHAR(8),SL.ddFechaFinCalificacion,112)=CONVERT(VARCHAR(8),GETDATE(),112)
and isnull(TU.Flag, 0) = 0

SELECT *
from micredito.SOLICITUD SL
inner join micredito.TARJETACREDITO T on SL.pnCodSolicitud =  T.pnCodSolicitud
inner join micredito.SOLICITUDPERSONA SP on SL.pnCodSolicitud = SP.pnCodSolicitud and SP.pcIndicadorTipoPersona in ('T')
INNER JOIN micredito.CREDITOVPCONDPORCAMPANA CCC ON SL.fcCodCampana = CCC.fcCodCampana and T.dcSegmento = CCC.fcCodVPSegmento and CCC.dcHabilitado = 'S'
INNER JOIN micredito.VPCONDICION V on  SL.fcCodProductoHost = V.CodTipoCredito AND T.dcSegmento = V.CodVPSegmento and CCC.dcAfiliacion = V.Afiliacion
WHERE (SL.fcCodEstadoSolicitud = 'ACPD') 
AND SL.fcCodJustificacion <> 'AMPL'
AND SL.fcCodTipoSolicitud = '01'
AND SL.dcIndicadorTransmitido <> 'S'
AND  V.PCT_ID<>''
AND SL.pnCodSolicitud = 699239
--AND CONVERT(VARCHAR(8),SL.ddFechaFinCalificacion,112)=CONVERT(VARCHAR(8),GETDATE(),112)


SELECT SL.fcCodJustificacion,SL.dcIndicadorTransmitido,V.PCT_ID   from micredito.SOLICITUD SL  inner join micredito.TARJETACREDITO T on SL.pnCodSolicitud =  T.pnCodSolicitud   inner join micredito.SOLICITUDPERSONA SP on SL.pnCodSolicitud = SP.pnCodSolicitud and SP.pcIndicadorTipoPersona in ('T')  INNER JOIN micredito.CREDITOVPCONDPORCAMPANA CCC ON SL.fcCodCampana = CCC.fcCodCampana and '02' = CCC.fcCodVPSegmento and CCC.dcHabilitado = 'S'  INNER JOIN micredito.VPCONDICION V on  SL.fcCodProductoHost = V.CodTipoCredito AND '02' = V.CodVPSegmento and CCC.dcAfiliacion = V.Afiliacion  WHERE (SL.fcCodEstadoSolicitud = 'ACPD')  AND SL.pnCodSolicitud = 714595 AND SL.fcCodTipoSolicitud = '01' AND SL.dcIndicadorTransmitido <> 'S' AND SL.fcCodJustificacion <> 'AMPL' AND  V.PCT_ID<>''
 
 select SL.pnCodSolicitud, SL.fcCodProducto, SL.fcCodMoneda, SL.fcCodEstadoSolicitud, SL.fcCodJustificacion, SL.fcCodTipoSolicitud, SL.dcIndicadorTransmitido, SL.ddFechaFinCalificacion, SL.fcCodProductoHost, SL.fcCodCampana, SL.fcCodJustificacion, T.pnCodSolicitud, T.dcCardHolder, T.dcNombreTarjeta, T.dcIndicadorCorrespondencia, T.dmLineaCreditoAprobado, T.dcCargoAutomaticoProducto, T.dcCargoAutomaticoNumCuenta, T.dcIndicadorPagoTarjeta, T.dcSegmento, T.dcNombreTarjetaAdicional1, T.dcNombreTarjetaAdicional2, SP.pnCodSolicitud, SP.dcCIC, SP.dcNombres, SP.dcApellidoPaterno, SP.dcApellidoMaterno, SP.fcTipoIDC, SP.dcNumIDC, SP.ddFechaNacimiento, SP.fcCodEstadoCivil, SP.dcSexo, SP.dcCodTipoCalle, SP.dcNombreCalle, SP.dcDireccion, SP.dcTelefono, SP.dcCelular, SP.fcCodLocalidad, SP.dcEmail, SP.dnNumDependientes from micredito.SOLICITUD SL inner join micredito.TARJETACREDITO T on SL.pnCodSolicitud =  T.pnCodSolicitud inner join micredito.SOLICITUDPERSONA SP on SL.pnCodSolicitud = SP.pnCodSolicitud and SP.pcIndicadorTipoPersona in ('T') LEFT JOIN micredito.CREDITOVPCONDPORCAMPANA CV ON SL.fcCodCampana = CV.fcCodCampana and T.dcSegmento = CV.fcCodVPSegmento and dcHabilitado = 'S' LEFT JOIN micredito.VPCONDICION V on  SL.fcCodProductoHost = CodTipoCredito AND T.dcSegmento = V.CodVPSegmento and CV.dcAfiliacion = V.Afiliacion LEFT join micredito.TARJETAS_UPGRADE TU on SL.pnCodSolicitud = TU.pnCodSolicitud WHERE (SL.fcCodEstadoSolicitud = 'ACPD')  AND SL.fcCodTipoSolicitud = '01' AND SL.pnCodSolicitud in ('714595')
 select SL.pnCodSolicitud, SL.fcCodProducto, SL.fcCodMoneda, SL.fcCodEstadoSolicitud, SL.fcCodJustificacion, SL.fcCodTipoSolicitud, SL.dcIndicadorTransmitido, SL.ddFechaFinCalificacion, SL.fcCodProductoHost, SL.fcCodCampana, SL.fcCodJustificacion, T.pnCodSolicitud, T.dcCardHolder, T.dcNombreTarjeta, T.dcIndicadorCorrespondencia, T.dmLineaCreditoAprobado, T.dcCargoAutomaticoProducto, T.dcCargoAutomaticoNumCuenta, T.dcIndicadorPagoTarjeta, T.dcSegmento, T.dcNombreTarjetaAdicional1, T.dcNombreTarjetaAdicional2, SP.pnCodSolicitud, SP.dcCIC, SP.dcNombres, SP.dcApellidoPaterno, SP.dcApellidoMaterno, SP.fcTipoIDC, SP.dcNumIDC, SP.ddFechaNacimiento, SP.fcCodEstadoCivil, SP.dcSexo, SP.dcCodTipoCalle, SP.dcNombreCalle, SP.dcDireccion, SP.dcTelefono, SP.dcCelular, SP.fcCodLocalidad, SP.dcEmail, SP.dnNumDependientes from micredito.SOLICITUD SL inner join micredito.TARJETACREDITO T on SL.pnCodSolicitud =  T.pnCodSolicitud inner join micredito.SOLICITUDPERSONA SP on SL.pnCodSolicitud = SP.pnCodSolicitud and SP.pcIndicadorTipoPersona in ('T') LEFT JOIN micredito.CREDITOVPCONDPORCAMPANA CV ON SL.fcCodCampana = CV.fcCodCampana and T.dcSegmento = CV.fcCodVPSegmento and dcHabilitado = 'S' LEFT JOIN micredito.VPCONDICION V on  SL.fcCodProductoHost = CodTipoCredito AND T.dcSegmento = V.CodVPSegmento and CV.dcAfiliacion = V.Afiliacion LEFT join micredito.TARJETAS_UPGRADE TU on SL.pnCodSolicitud = TU.pnCodSolicitud WHERE (SL.fcCodEstadoSolicitud = 'ACPD')  AND SL.fcCodTipoSolicitud = '01' AND SL.pnCodSolicitud in ('709946')

 select * from micredito.CREDITOVPCONDPORCAMPANA where fcCodVPSegmento = '02' and fcCodCampana = 819

select Afiliacion from micredito.VPCONDICION where CodVPSegmento = '02' and CodTipoCredito = '010210' and Afiliacion = convert (char,CHAR(92)+'u0015')

select S.fcCodProducto,C.dcDescripcion,SP.fcCodIndicadorBancaExclusiva,T.dcIndicadorRecursosHumanos FROM micredito.SOLICITUDPERSONA SP (NOLOCK) INNER JOIN micredito.TARJETACREDITO T (NOLOCK)		ON SP.pnCodSolicitud = T.pnCodSolicitud INNER JOIN micredito.SOLICITUD S (nolock)		on SP.pnCodSolicitud = S.pnCodSolicitud	INNER JOIN micredito.CAMPANA C (nolock) on (C.pcCodCampana =S.fcCodCampana and C.fcCodProducto=S.fcCodProducto )			WHERE SP.pnCodSolicitud=710150	AND		SP.pcIndicadorTipoPersona  ='T'		

--insert into micredito.CREDITOVPCONDPORCAMPANA select fcCodCampana,'02',dcHabilitado,dcAfiliacion from micredito.CREDITOVPCONDPORCAMPANA where fcCodVPSegmento = '01' and fcCodCampana = 387

insert into micredito.VPCONDICION (CodTipoCredito,CodVPSegmento,Afiliacion,PCT_ID,Pricing_Ctrl,dmLineaCredito) VALUES ('010210','02',convert (char,'\u0015'),'626','629',6.00 )

UPDATE micredito.TARJETACREDITO SET dcSegmento = '01' WHERE pnCodSolicitud = 710150

SELECT dcSegmento FROM  micredito.TARJETACREDITO WHERE pnCodSolicitud = 714595

---------------------------------------------------------GENERACION DE TARJETAS-------------------------------------------------
---------------------------------------------------------EXTORNO-------------------------------------------------
--ACTUALMENTE EL EXTORNO SOLO TIENE Q VER CON LA CUENTA DEL CLIENTE

--1. EN CASO DE Q LA OPERACION SI SE VUELVA A HACER EL MISMO DIA NO HABRIA PROBLEMA 
  -- SE EXTORNE 
  -- SE MODIFIQUE DESPUES (corrigioendo la operacion)
  -- SE PONGA EN DESEMBOLSADO MANUALMENTE
  -- AL DIA SIGUIENTE SE MODIFICA MANUALMENTE POR CTV el usuario

--2. EN CASO DE UN EXTORNO TOTAL
  -- SE EXTORNA
  -- SE CANCELA EL ALS CON LA PLANTA DE ITALS
  

--3. EN CASO DE REDESEMBOLSAR LA OPERACION, ESTANDO YA EXTORNADA (COSA Q NO SE DEBERIA HACER!!!)
--ESTO NO ES POSIBLE, SE DEBE DIGITAR NUEVAMENTE LA OPERACION Y SIMULAR EL DESEMBOLSO PARA Q SE TRANSMITA NUEVAMENTE
--( FLUJO COMPLICADO
  --  ACTUALIZAR EL ESTADO DE TRANSMISION (PARA Q EL ROBOT LO VUELVA A TOMAR)
  --  ACTUALIZAR EL ALS:
   UPDATE micredito.ALS_ROBOT_MICREDITO SET dcNroProcesoCredito='303-201-0000-00000008457863' WHERE  pnCodSolicitud = '754307'
   --CREAR OTRA OPERACION Y SIMULAR DESEMBOLSO
   --PARA ESTO SE TUVO Q DETENER EL ROBOT DE CREACION ALS, PARA SOLO OBTENER EL COD MIC
   -- RELACIONAR EN EL AMZ1 el nueVo ALS CON EL NUEVO COD MIC
   --)

--4. PASA SIMULAR UN DESEMBOLSO
--INSERTAR EN LA TABLA DESEMBOLSO
--'false' es para que lo tome contabilidad y 1 para que no lo tome contabilidad
insert into micredito.DESEMBOLSO_MICREDITO values (752100,'10303301000100000050990458','768320.00','BOL','false',GETDATE(),'S88658')
-- INSERTAR EN LA TABLA ALS (EN CASO DE QUE YA ESTE CREADO POR ALGUN MOTIVO)
         INSERT INTO micredito.ALS_ROBOT_MICREDITO VALUES (752100,'303-201-0000-00000008457618',getdate(),'10300.00')
-- RELACIONAR EN EL AMZ1 el ALS CON EL NUEVO COD MIC (SI NO ESTAN RELACIONADOS)

select * from micredito.DESEMBOLSO_MICREDITO where pnCodSolicitud in  (704643)
update micredito.DESEMBOLSO_MICREDITO set FechaDesembolso = GETDATE()-7 where pnCodSolicitud in  (704643)
select  dcIndicadorTransmitido,ddFechaTransmision from micredito.SOLICITUD WHERE pnCodSolicitud IN (740461)
update micredito.SOLICITUD set dcIndicadorTransmitido = 'S', ddFechaTransmision = getdate() WHERE pnCodSolicitud IN (707593)
--pyme, HIPOTECARIO y consumo = P
--MICREOCREDITO = N
--PARA Q TOME AL MOMENTO = R (casos excepciones)
---------------------------------------------------------EXTORNO-------------------------------------------------

--------------------------------------------------------------TRANSMISION OPERACION SIN ABONO EN CUENTA----------------------------------------------
--CONSUMO, HIPOTECARIO Y PYME

--obtener datos de la operacion
select pnCodSolicitud from micredito.SOLICITUD WHERE dcNumSolicitud = 'CCO11043216'
SELECT * from micredito.SOLICITUD where pnCodSolicitud  in (737064)
SELECT * FROM micredito.SOLICITUDPERSONA
--validar q no existen
select top 3 * from micredito.DESEMBOLSO_MICREDITO where pnCodSolicitud  in (740159)
--dependiendo del tipo de operacion buscar los datos MONTO APROBADO y CUENTA ABONO
select CargoAutomaticoNumCuenta,AnalistaTotalMontoDesembolsar,* from micredito.CREDITOBANCANEGOCIO WHERE CodSolicitud = 737064
SELECT pnCodSolicitud,dcCargoAutomaticoNumCuenta,dmMontoAprobado FROM micredito.CREDITOHIPOTECARIO WHERE pnCodSolicitud  in (737064)
SELECT dcCargoAutomaticoNumCuenta,dmMontoAprobado FROM micredito.CREDITOCONSUMO WHERE pnCodSolicitud  in (740159)
-->[{"pnCodSolicitud":713167,"NumeroCuenta":"10303501000100000051272557","MontoDesembolso":65561.00,"MonedaDesembolso":"BOL","Contabilizado":true,"FechaDesembolso":"2022-11-17T14:31:24.51","UsuarioDesembolso":"T24005"}]</a:Data>
   
   --insert into micredito.DESEMBOLSO_MICREDITO VALUES (716106,'10303501000100000051272557',65561.00,'BOL',1,GETDATE(),'T24005')
--AHO -> 14
--CTE -> 13
--LLENAR LOS DATOS CONVIRTIENDO LA CUENTA DE COMERCIAL A REPEXT (QUERY "FLUJO CUENTAS AHO CTE")
DECLARE @ret nvarchar(100);
EXEC @ret = [dbo].[FN_COMERCIAL_A_REPEXT] @chrTIPO='AHO', @chrNUMEROCUENTA = '20151089195328';
SELECT @ret;


insert into micredito.DESEMBOLSO_MICREDITO VALUES (740159,'10303201000100000051089195',43511.00,'BOL',1,GETDATE(),'B03053')--20150289540396    

insert into micredito.DESEMBOLSO_MICREDITO VALUES (735765,'10303201000100000099999999',421390.79,'BOL',1,GETDATE(),'B03053')-- 30150748237327 

insert into micredito.DESEMBOLSO_MICREDITO VALUES (735757,'10303201000100000099999999',54882.41,'BOL',1,GETDATE(),'B03053') --   70151568195371  

insert into micredito.DESEMBOLSO_MICREDITO VALUES (726202,'10303701000100000050826044',427560.82,'BOL',1,GETDATE(),'B03053') --   70150826044324   
insert into micredito.DESEMBOLSO_MICREDITO VALUES (726167,'10303101000100000050474994',294942.65,'BOL',1,GETDATE(),'B03053') --10150474994340         
insert into micredito.DESEMBOLSO_MICREDITO VALUES (726171,'10303801000100000051012977',307970.39,'BOL',1,GETDATE(),'B03053') --80151012977347         
insert into micredito.DESEMBOLSO_MICREDITO VALUES (726187,'10303201000100000051335400',468315.84,'BOL',1,GETDATE(),'B03053') --20151335400321         
insert into micredito.DESEMBOLSO_MICREDITO VALUES (726194,'10303301000100000050817352',300590.97,'BOL',1,GETDATE(),'B03053') --30150817352340         
insert into micredito.DESEMBOLSO_MICREDITO VALUES (722545,'10303301000100000051562284',212773.82,'BOL',1,GETDATE(),'B03053') --30151562284397         

--CTE
--10303301000100000051532935
--301-5153293552
--AHO
--10101
--101-50915214307

UPDATE micredito.DESEMBOLSO_MICREDITO SET NumeroCuenta = '10303201000100000051271487', MontoDesembolso = 153482.44 WHERE pnCodSolicitud = 714218
UPDATE micredito.DESEMBOLSO_MICREDITO SET NumeroCuenta = '10303501000100000050689587', MontoDesembolso = 532969.07 WHERE pnCodSolicitud = 714231
UPDATE micredito.DESEMBOLSO_MICREDITO SET NumeroCuenta = '10303201000100000050055729', MontoDesembolso = 215281.23 WHERE pnCodSolicitud = 714234

select * from micredito.ALS_ROBOT_MICREDITO where pnCodSolicitud = 722496
--VALIDAR LA TRANSMISION EN EL REPORTE
--si no puede llegar a transmitir borrar la op
DELETE micredito.DESEMBOLSO_MICREDITO WHERE pnCodSolicitud = 722496

update micredito.SOLICITUD set dcIndicadorTransmitido = 'X' where pnCodSolicitud = 722496
--------------------------------------------------------------TRANSMISION OPERACION----------------------------------------------
-------------------------------------------------------------NO GENERA ALS CARTERA MICROCREDITO SABADO-----------------------
--FINES DE SEMANA NO SE CREA GARANTIA
--CAMBIAR FECHA DE DESMBOLSO PARA Q EL GENERADOR DE GARANTIA LO TOME
SELECT FechaDesembolso FROM micredito.[DESEMBOLSO_MICREDITO] WHERE pnCodSolicitud = 765006
UPDATE micredito.[DESEMBOLSO_MICREDITO] SET FechaDesembolso = GETDATE() WHERE pnCodSolicitud = 727145
SELECT * FROM micredito.CONTROLGARANTIA_BP WHERE pnCodSolicitud = 765006
--Y ELIMINAR ERROR PARA QUE EL GENERADOR DE GARANTIA LO TOME
DELETE micredito.CONTROLGARANTIA_BP WHERE pnCodSolicitud = 765006
[micredito].[SP_GET_CREACION_GARANTIA_FOGAGRE_GetListByTipoSolicitud]'04'
SELECT * FROM micredito.BP_GARANTIAS where pnCodSolicitud = 765006 AND dcTipoGarantia = 'FOG'
-------------------------------------------------------------NO GENERA ALS CARTERA MICROCREDITO SABADO-----------------------

