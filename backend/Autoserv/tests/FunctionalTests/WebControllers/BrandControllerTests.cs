using ApplicationCore.ModelsDTO.Brand;
using FunctionalTests.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.WebControllers
{
    public class BrandControllerTests : IClassFixture<WebAppFactoryTest<Program>>
    {

        private readonly HttpClient _client;

        private readonly int _shift = 1;

        private readonly NewBrandDTO _newBrandDTO = new()
        {
            Name = "NewTestBrandDTO"
        };

        private readonly BrandDTO _updatingBrand = new()
        {
            Id = 4,
            Name = "Update"
        };

        public BrandControllerTests(WebAppFactoryTest<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_Brand_Than_Checed_NewBrand_in_DB_Result_is_Positive()
        {
            //Arrange            
            var content = JsonContent.Create(_newBrandDTO);

            //Act
            var expectedResponse = await _client.PostAsync("/Brand", content);
            expectedResponse.EnsureSuccessStatusCode();

            var expected = await JsonConverterExeptions.ReadAsync<BrandDTO>(expectedResponse);

            var actualResponse = await _client.GetAsync("/Brand" + "/" + expected.Id);
            actualResponse.EnsureSuccessStatusCode();

            var actual = await JsonConverterExeptions.ReadAsync<BrandDTO>(actualResponse);

            //Assert
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Creating_a_check_on_update_Brand_is_Positive()
        {
            //Arrange     
            var content = JsonContent.Create(_updatingBrand);

            //Act 
            var expectedResponse = await _client.PutAsync("/Brand" + "/" + _updatingBrand.Id, content);
            expectedResponse.EnsureSuccessStatusCode();

            var expected = await JsonConverterExeptions.ReadAsync<BrandDTO>(expectedResponse);

            var actualResponse = await _client.GetAsync("/Brand" + "/" + expected.Id);
            actualResponse.EnsureSuccessStatusCode();

            var actual = await JsonConverterExeptions.ReadAsync<BrandDTO>(actualResponse);

            //Assert
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Delete_Brand_Than_Checed_Count_in_DB_Result_is_Positive()
        {
            //Arrange
            var expectedCount = await _client.GetAsync("/Brand");
            expectedCount.EnsureSuccessStatusCode();

            var brandsCountBefor = await JsonConverterExeptions.ReadListAsync<BrandDTO>(expectedCount);

            //Act 
            var expectedResponse = await _client.DeleteAsync("/Brand" + "/" + _updatingBrand.Id);
            expectedResponse.EnsureSuccessStatusCode();

            var expected = await JsonConverterExeptions.ReadAsync<BrandDTO>(expectedResponse);

            var actualCount = await _client.GetAsync("/Brand");
            actualCount.EnsureSuccessStatusCode();

            var brandsCountAfter = await JsonConverterExeptions.ReadListAsync<BrandDTO>(actualCount);

            //Assert
            Assert.Null(expected);
            Assert.Equal(brandsCountBefor.Count - _shift, brandsCountAfter.Count);
        }
    }
}
