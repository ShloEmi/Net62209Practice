using NoNameCompany.IMS.Data.ApplicationData;

namespace NoNameCompany.IMS.BL.DAL.Interfaces;

public interface IDAL
{
    bool CanAddItems();
    void AddItemsBulk(IEnumerable<ItemData> items);
}