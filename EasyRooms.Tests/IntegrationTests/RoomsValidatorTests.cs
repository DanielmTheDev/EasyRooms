using EasyRooms.Model.Validation;
using Xunit;

namespace EasyRooms.Tests.IntegrationTests
{
    public class RoomsValidatorTests
    {
        private readonly RoomsValidator _validator;

        public RoomsValidatorTests() 
            => _validator = new RoomsValidator();

        [Fact]
        public void Validates_Partner_Massages()
        {
            
        }
    }
}