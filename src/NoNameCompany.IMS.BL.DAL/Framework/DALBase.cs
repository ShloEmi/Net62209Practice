using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.Data.ApplicationData;

namespace NoNameCompany.IMS.BL.DAL.Framework;

public abstract class DALBase : IDAL
{
    protected static readonly string DataLayerSectionName = "Data-layer";


    public abstract bool CanAddItems();
    public abstract bool AddItemsBulk(IEnumerable<ItemData>? items);

    public abstract IObservable<IEnumerable<ItemChanged>> ItemsChanged { get; }
}
