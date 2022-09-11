namespace Net62209Practice.Data.ApplicationData;

/// <example>     ItemData ItemData2 = ItemData1 with { Name = "John" }; </example>
public record ItemData(Guid Id, string Name, string Description, ItemCategorizationData ItemCategorization);