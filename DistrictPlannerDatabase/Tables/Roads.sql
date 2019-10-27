CREATE TABLE [dbo].[Roads]
(
	[RoadId] INT IDENTITY(1,1) PRIMARY KEY,
	[StartSettlementId] INT NOT NULL,
	[EndSettlementId] INT NOT NULL,
	[Distance] DECIMAL NOT NULL,
	FOREIGN KEY ([StartSettlementId]) REFERENCES [dbo].[Settlements]([SettlementId]),
	FOREIGN KEY ([EndSettlementId]) REFERENCES [dbo].[Settlements]([SettlementId])
)
