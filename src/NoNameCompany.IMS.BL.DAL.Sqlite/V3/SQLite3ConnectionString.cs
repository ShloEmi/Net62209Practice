using NoNameCompany.IMS.BL.DAL.Interfaces;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3;

public record SQLite3ConnectionString(string ConnectionString) : IDALConnectionString;