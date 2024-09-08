using AutoMapper;
using CashFlow.Application.Services.AutoMapper;

namespace Commom.Test.Utilities.Mapper;
public class MapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapping());
        });

        return mapper.CreateMapper();
    }
}
