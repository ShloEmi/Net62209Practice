namespace Net62209Practice.Data.ApplicationData;

public record ItemContainerData(Guid Id, string Name, string Description,
        Dictionary<Guid, ItemData> ContainedItems) 
    : ItemData(Id, Name, Description);