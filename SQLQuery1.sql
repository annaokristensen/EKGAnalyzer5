CREATE TABLE EKGLokal (
 ekgid INT IDENTITY(1,1) NOT NULL,
 raa_data VARBINARY (MAX) NOT NULL,
 samplerate_hz FLOAT NOT NULL,
 interval_sec BIGINT NOT NULL,
 data_format NVARCHAR(MAX) NOT NULL,
 bin_eller_tekst CHAR(1) NOT NULL,
 maaleformat_type NVARCHAR(MAX) NOT NULL,
 start_tid DATETIME NOT NULL,
 cpr_borger NVARCHAR(MAX) NOT NULL,
 PRIMARY KEY (ekgid)
 )
