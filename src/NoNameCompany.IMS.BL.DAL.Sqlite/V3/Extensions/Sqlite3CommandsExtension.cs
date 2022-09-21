using NoNameCompany.InfraStructure.Patterns.Code;
using System.Text;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.Extensions;

public static class Sqlite3CommandsExtension
{
    public static IDisposable BeginTransaction(this StringBuilder sb)
    {
        sb.AppendLine("BEGIN TRANSACTION;");

        return new DisposableCommand(() => sb.AppendLine("COMMIT;"));
    }
}
