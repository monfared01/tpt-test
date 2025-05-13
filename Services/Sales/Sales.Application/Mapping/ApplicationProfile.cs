using MainTest.Framework.Mapping;
using System.Reflection;


namespace Sales.Application.Mapping
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            AutoMapperProfile.LoadMaps(this, Assembly.GetExecutingAssembly());
        }
    }
}
