﻿INSERT INTO dbo.EKGLokal(ekgid,raa_data,samplerate_hz,interval_sec,data_format,bin_eller_tekst,maaleformat_type,start_tid,cpr_borger) OUTPUT Inserted.PrimaryKey
VALUES(1,2,3,4,'get','g','g',4/15/96,"1234567890")