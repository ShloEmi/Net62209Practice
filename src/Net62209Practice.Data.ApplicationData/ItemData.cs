﻿namespace NoNameCompany.IMS.Data.ApplicationData;

/// <example>     ItemData ItemData2 = ItemData1 with { Name = "John" }; </example>
public record ItemData
{
    public ItemData() { }

    /// <example>     ItemData ItemData2 = ItemData1 with { Name = "John" }; </example>
    public ItemData(ulong Id, string Name, string Description, ItemCategorizationData ItemCategorization)
        : this()
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.ItemCategorization = ItemCategorization;
    }

    public ulong Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public ItemCategorizationData ItemCategorization { get; init; }

    public void Deconstruct(out ulong Id, out string Name, out string Description, out ItemCategorizationData ItemCategorization)
    {
        //byte[] byteArray = Id.ToByteArray();
        //Guid guid = Guid.Parse(byteArray.Cast<char>().ToArray());

        Id = this.Id;
        Name = this.Name;
        Description = this.Description;
        ItemCategorization = this.ItemCategorization;
    }
}