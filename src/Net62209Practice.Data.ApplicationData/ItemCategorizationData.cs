namespace NoNameCompany.IMS.Data.ApplicationData;

public record ItemCategorizationData
{
    public ItemCategorizationData() { }

    public ItemCategorizationData(Guid Id, string Name, string Description)
        : this()
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }

    public void Deconstruct(out Guid Id, out string Name, out string Description)
    {
        Id = this.Id;
        Name = this.Name;
        Description = this.Description;
    }
}
