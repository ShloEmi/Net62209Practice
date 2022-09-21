using AutoMapper;
using NoNameCompany.IMS.BL.DAL.SQLite.V3.DTOs;
using NoNameCompany.IMS.Data.ApplicationData;

namespace NoNameCompany.IMS.BL.DAL.SQLite.V3.AutoMapper;

// ReSharper disable once UnusedMember.Global
public class SQLite3ModuleProfile : Profile
{
    public SQLite3ModuleProfile()
    {
        CreateMap<ItemData, ItemDataSqlite3DTO>();
    }
}