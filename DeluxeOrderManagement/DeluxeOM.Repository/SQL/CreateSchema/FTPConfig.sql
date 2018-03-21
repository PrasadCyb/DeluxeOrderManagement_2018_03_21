INSERT INTO FTPConfig
(
	UserName,
	Password,
	Host,
	FtpDirectory,
	FileName,
	DownloadTo,
	KeepOriginal,
	OverWriteExisting,
	Port,
	EnableSSL,
	Environment,
	FtpArchivalDirectory
)
VALUES
(
	'Andy.Nair',
	'Cyb_Sys@123',
	'ftp://cybftp.cybage.com',
	'WB-Apple-Grid/Pipeline Orders',
	'PipelineOrder',
	'C:/DeluxeOrderManagement/FTPDownloadPath/',
	NULL,
	NULL,
	21,
	1,
	'CYB',
	'WB-Apple-Grid/Archive/Pipeline Orders'
),
(
	'Andy.Nair',
	'Cyb_Sys@123',
	'ftp://cybftp.cybage.com',
	'WB-Apple-Grid/Apple QC Report',
	'QCReport',
	'C:/DeluxeOrderManagement/FTPDownloadPath/',
	NULL,
	NULL,
	21,
	1,
	'CYB',
	'WB-Apple-Grid/Archive/Apple QC Report'
),
(
	'Andy.Nair',
	'Cyb_Sys@123',
	'ftp://cybftp.cybage.com',
	'WB-Apple-Grid/Apple Price Report',
	'PriceReport',
	'C:/DeluxeOrderManagement/FTPDownloadPath/',
	NULL,
	NULL,
	21,
	1,
	'CYB',
	'WB-Apple-Grid/Archive/Apple Price Report'
),
(
	'Andy.Nair',
	'Cyb_Sys@123',
	'ftp://cybftp.cybage.com',
	'WB-Apple-Grid/Announcements',
	'Announcement',
	'C:/DeluxeOrderManagement/FTPDownloadPath/',
	NULL,
	NULL,
	21,
	1,
	'CYB',
	'WB-Apple-Grid/Archive/Announcements'
)




/*====================================================  QA ==================================================================*/


INSERT INTO FTPConfig
(
	UserName,
	Password,
	Host,
	FtpDirectory,
	FileName,
	DownloadTo,
	KeepOriginal,
	OverWriteExisting,
	Port,
	EnableSSL,
	Environment,
	FtpArchivalDirectory
)
VALUES
(
	'CentaurTest',
	'MyCentaur123',
	'ftp://35.165.251.3',
	'FTP/WB-Apple-Grid/Pipeline Orders',
	'PipelineOrder',
	'\\192.172.0.22\ftp\DeluxeOrderManagement\FTPDownloadPath\',
	NULL,
	NULL,
	21,
	0,
	'QA',
	'FTP/WB-Apple-Grid/Archive/Pipeline Orders'
),
(
	'CentaurTest',
	'MyCentaur123',
	'ftp://35.165.251.3',
	'FTP/WB-Apple-Grid/Apple QC Report',
	'QCReport',
	'\\192.172.0.22\ftp\DeluxeOrderManagement\FTPDownloadPath\',
	NULL,
	NULL,
	21,
	0,
	'QA',
	'FTP/WB-Apple-Grid/Archive/Apple QC Report'
),
(
	'CentaurTest',
	'MyCentaur123',
	'ftp://35.165.251.3',
	'FTP/WB-Apple-Grid/Apple Price Report',
	'PriceReport',
	'\\192.172.0.22\ftp\DeluxeOrderManagement\FTPDownloadPath\',
	NULL,
	NULL,
	21,
	0,
	'QA',
	'FTP/WB-Apple-Grid/Archive/Apple Price Report'
),
(
	'CentaurTest',
	'MyCentaur123',
	'ftp://35.165.251.3',
	'FTP/WB-Apple-Grid/Announcements',
	'Announcement',
	'\\192.172.0.22\ftp\DeluxeOrderManagement\FTPDownloadPath\',
	NULL,
	NULL,
	21,
	0,
	'QA',
	'FTP/WB-Apple-Grid/Archive/Announcements'
)





/*====================================================  PRODUCTION ==================================================================*/


INSERT INTO FTPConfig
(
	UserName,
	Password,
	Host,
	FtpDirectory,
	FileName,
	DownloadTo,
	KeepOriginal,
	OverWriteExisting,
	Port,
	EnableSSL,
	Environment,
	FtpArchivalDirectory
)
VALUES
(
	'CentaurTest',
	'MyCentaur123',
	'ftp://34.215.32.55',
	'FTP/WB-Apple-Grid/Pipeline Orders',
	'PipelineOrder',
	'\\10.0.0.79\ftp\DeluxeOrderManagement\FTPDownloadPath\',
	NULL,
	NULL,
	21,
	0,
	'PROD',
	'FTP/WB-Apple-Grid/Archive/Pipeline Orders'
),
(
	'CentaurTest',
	'MyCentaur123',
	'ftp://34.215.32.55',
	'FTP/WB-Apple-Grid/Apple QC Report',
	'QCReport',
	'\\10.0.0.79\ftp\DeluxeOrderManagement\FTPDownloadPath\',
	NULL,
	NULL,
	21,
	0,
	'PROD',
	'FTP/WB-Apple-Grid/Archive/Apple QC Report'
),
(
	'CentaurTest',
	'MyCentaur123',
	'ftp://34.215.32.55',
	'FTP/WB-Apple-Grid/Apple Price Report',
	'PriceReport',
	'\\10.0.0.79\ftp\DeluxeOrderManagement\FTPDownloadPath\',
	NULL,
	NULL,
	21,
	0,
	'PROD',
	'FTP/WB-Apple-Grid/Archive/Apple Price Report'
),
(
	'CentaurTest',
	'MyCentaur123',
	'ftp://34.215.32.55',
	'FTP/WB-Apple-Grid/Announcements',
	'Announcement',
	'\\10.0.0.79\ftp\DeluxeOrderManagement\FTPDownloadPath\',
	NULL,
	NULL,
	21,
	0,
	'PROD',
	'FTP/WB-Apple-Grid/Archive/Announcements'
)