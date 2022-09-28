namespace NoNameCompany.IMS.Data.ApplicationData;

public record ItemContainerData(ulong Id, string Name, string Description, ItemCategorizationData ItemCategorization,
        Dictionary<ulong, ItemData> ContainedItems) 
    : ItemData(Id, Name, Description, ItemCategorization);
