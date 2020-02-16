select (select UniqueName from platforms platform where platform.id=well.PlatformID) PlatformName
,well.ID
,well.PlatformID
,well.UniqueName
,well.Latitude
,well.Longitude
,well.CreatedAt
,well.UpdatedAt
from Wells well
inner join (
    select PlatformID, max(UpdatedAt) as MaxDate
    from Wells
    group by PlatformID
) UpdatedWell on well.PlatformID = UpdatedWell.PlatformID and well.UpdatedAt = UpdatedWell.MaxDate
order by well.PlatformID
