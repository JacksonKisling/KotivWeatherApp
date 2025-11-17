CREATE TABLE SearchHistory
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [SearchTerm] NVARCHAR(200) NOT NULL,
    [Latitude] DECIMAL(9,6) NOT NULL,
    [Longitude] DECIMAL(9,6) NOT NULL,
    [SearchTimestamp] DATETIME2 NOT NULL,
    [ResultSummary] NVARCHAR(400) NULL
);
GO
CREATE NONCLUSTERED INDEX IX_SearchTimestamp ON SearchHistory (SearchTimestamp DESC);
GO