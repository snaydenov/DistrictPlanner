﻿CREATE TABLE [dbo].[Settlements]
(
	[SettlementId] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(256) NOT NULL,
	[IsMain] BIT NOT NULL DEFAULT(0)
)

