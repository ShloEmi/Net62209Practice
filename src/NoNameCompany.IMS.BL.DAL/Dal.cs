using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.Data.ApplicationData;

namespace NoNameCompany.IMS.BL.DAL;

internal class Dal : IDAL
{
    public bool CanAddItems() => true;
    public void AddItemsBulk(IEnumerable<ItemData> items) => 
        throw new NotImplementedException();
}