using Facts.Web.Infrastructure.Mappers.Base;
using Xunit;

namespace Facts.Web.Tests
{
    public class AutomapperTests
    {
        [Fact]
        [Trait("Automapper", "Mapper Configuration")]
        public void ItShouldConfigurationMapper()
        {
            var config = MapperRegistration.GetMapperConfiguration();

            config.AssertConfigurationIsValid();
        }
    }
}
