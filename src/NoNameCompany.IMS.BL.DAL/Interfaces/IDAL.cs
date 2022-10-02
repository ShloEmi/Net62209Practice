using NoNameCompany.IMS.Data.ApplicationData;

namespace NoNameCompany.IMS.BL.DAL.Interfaces;

public interface IDAL
{
    bool CanAddItems();
    bool AddItemsBulk(IEnumerable<ItemData>? items);

    IObservable<IEnumerable<ItemData>> ItemsChanged { get; }
}
