using NoNameCompany.IMS.Data.ApplicationData;

namespace NoNameCompany.IMS.BL.DAL.Interfaces;

public interface IDAL
{
    bool CanAddItems();
    bool AddItemsBulk(IEnumerable<ItemData>? items);

    IObservable<IEnumerable<ItemChanged>> ItemsChanged { get; }
}


public enum ChangeDescriptions { added, removed, updated }

public record ItemChanged(ItemData ChangedItem, ChangeDescriptions ChangeDescription);
