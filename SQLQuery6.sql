﻿--CREATE TABLE EKGMAELING (
--ekgmaaleid BIGINT IDENTITY(1,1) NOT NULL,
--dato DATETIME NOT NULL,
--antalmaalinger INT NOT NULL,
--sfp_maaltagerfornavn NVARCHAR(MAX) NULL,
--sfp_maltagerefternavn NVARCHAR(MAX) NULL,
--sfp_maaltagermedarbjnr NVARCHAR(MAX) NULL,
--sfp_mt_org NVARCHAR(MAX) NULL,
--sfp_mt_kommentar NTEXT NULL,
--sfp_ansvfornavn NVARCHAR(MAX) NULL,
--sfp_ansvefternavn NVARCHAR(MAX) NULL,
--sfp_ansvrmedarbjnr NVARCHAR(50) NOT NULL,
--sfp_ans_org NVARCHAR(MAX) NOT NULL,
--sfp_anskommentar NVARCHAR(MAX) NULL,
--borger_fornavn NVARCHAR(MAX) NULL,
--borger_efternavn NVARCHAR(MAX) NULL,
--borger_beskrivelse NVARCHAR(MAX) NULL,
--borger_cprnr NVARCHAR(MAX) NULL,
--CONSTRAINT pk_EKGMAELING PRIMARY KEY CLUSTERED (ekgmaaleid))


--CREATE TABLE EKGDATA (
--ekgdataid INT IDENTITY(1,1) NOT NULL, 
--raa_data VARBINARY (MAX) NOT NULL,
--samplerate_hz FLOAT NOT NULL,
--interval_sec BIGINT NOT NULL,
--interval_min FLOAT NULL,
--data_format NVARCHAR(MAX) NOT NULL,
--bin_eller_tekst CHAR(1) NOT NULL,
--maaleformat_type NVARCHAR(MAX) NOT NULL,
--start_tid DATETIME NOT NULL,
--kommentar TEXT NULL,
--ekgmaaleid BIGINT NULL,
--maalenehed_identifikation NVARCHAR(MAX) NULL,
--CONSTRAINT pk_EKGDATA PRIMARY KEY CLUSTERED (ekgdataid),
--CONSTRAINT fk_EKGDATA FOREIGN KEY (ekgmaaleid)
--REFERENCES EKGMAELING (ekgmaaleid)
--ON UPDATE CASCADE)