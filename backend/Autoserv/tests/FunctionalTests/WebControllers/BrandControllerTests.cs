using ApplicationCore.ModelsDTO.Brand;
using FunctionalTests.Extensions;
using System.Linq;
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

        private readonly int _count = 3;

        private readonly NewBrandDTO _newBrandDTO = new()
        {
            Name = "NewTestBrandDTO"
        };

        private BrandDTO? _brandDTO;

        public BrandControllerTests(WebAppFactoryTest<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_all_endpoint_return_items_given_valid()
        {
            //Arrange 
            var actualResponse = await _client.GetAsync("/Brand");
            actualResponse.EnsureSuccessStatusCode();

            //Act
            var actual = await JsonConverterExeptions.ReadListAsync<BrandDTO>(actualResponse);

            //Assert
            Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.Equal(_count, actual.Count);
        }

        [Fact]
        public async Task Create_endpoint_return_item_given_valid()
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
        public async Task Update_endpoint_returns_item_given_valid()
        {
            //Arrange     
            var brands = await _client.GetAsync("/Brand");
            brands.EnsureSuccessStatusCode();

            var allBrands = await JsonConverterExeptions.ReadListAsync<BrandDTO>(brands);
            _brandDTO = allBrands.OrderByDescending(x => x.Id).FirstOrDefault()!;

            _brandDTO.Name = "Update";
            var content = JsonContent.Create(_brandDTO);

            //Act 
            var expectedResponse = await _client.PutAsync("/Brand" + "/" + _brandDTO.Id, content);
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
        public async Task Delete_endpoint_returns_item_given_valid()
        {
            //Arrange
            var brands = await _client.GetAsync("/Brand");
            brands.EnsureSuccessStatusCode();

            var allBrands = await JsonConverterExeptions.ReadListAsync<BrandDTO>(brands);
            _brandDTO = allBrands.OrderByDescending(x => x.Id).FirstOrDefault()!;

            //Act 
            var actualResponse = await _client.DeleteAsync("/Brand" + "/" + _brandDTO.Id);
            actualResponse.EnsureSuccessStatusCode();

            var actual = await JsonConverterExeptions.ReadAsync<BrandDTO>(actualResponse);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, actualResponse.StatusCode);
            Assert.Null(actual);
        }
    }
}
