using AutoMapper;

namespace Web.Miscellaneous;

public static class AutoMapperConfigure
{
    public static void AssertAutoMapperMappings(this IApplicationBuilder app)
    {
        var mapper = app.ApplicationServices.GetService<IMapper>();
        mapper?.ConfigurationProvider.AssertConfigurationIsValid();
    }
}