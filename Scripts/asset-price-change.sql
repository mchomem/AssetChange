
-- SQL version of the asset price change analytical report.
declare @assetId int = 2

select
	a.Id
	, a.AssetId
	, a.EventDate
	, a.OpeningValue
	, ((a.OpeningValue - lag(a.OpeningValue) over (order by a.Id)) / a.OpeningValue) * 100 as PercentageD1 -- 'lag' function to get the previous record of the column in relation to the current record.
	, ((a.OpeningValue - (select top 1 aux.OpeningValue from AssetTradingDate aux where aux.AssetId = @assetId )) / a. OpeningValue) * 100 as PercentageFirstDay
from
	AssetTradingDate a
where
	a.AssetId = @assetId
