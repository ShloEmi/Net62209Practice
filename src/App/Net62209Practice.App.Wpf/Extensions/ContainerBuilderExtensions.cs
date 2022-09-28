using Autofac;
using Bogus;
using NoNameCompany.IMS.Data.ApplicationData;
using System;

namespace NoNameCompany.IMS.App.Wpf.Extensions;

public static class ContainerBuilderExtensions
{
    public static void RegisterItemDataProvider(this ContainerBuilder builder)
    {
        builder.Register((context, parameters) =>
        {
            /* TODO: Shlomi, replace me with RL data provider */
            return new Faker<ItemData>()
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode
                .StrictMode(true)
                //OrderId is deterministic
                .RuleFor(o => o.Id, f => 0ul)
                .RuleFor(o => o.Name, f => f.Name.JobTitle())
                .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                .RuleFor(o => o.ItemCategorization, f =>
                    {
                        ItemCategorizationData itemCategorizationData = new Faker<ItemCategorizationData>()
                            .RuleFor(o => o.Id, f1 => 0ul)
                            .RuleFor(o => o.Name, f2 => f2.Name.JobTitle())
                            .RuleFor(o => o.Description, f3 => f3.Lorem.Sentence()).Generate();
                        return itemCategorizationData;
                    }
                );
        });
    }
}
