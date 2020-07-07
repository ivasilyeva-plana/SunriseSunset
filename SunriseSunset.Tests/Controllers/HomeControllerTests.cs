using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SunriseSunset.Controllers;
using SunriseSunset.Models;
using SunriseSunset.Network;
using SunriseSunset.Repositories;

namespace SunriseSunset.Tests.Controllers
{

    public class HomeControllerTest
    {
        private static readonly IReadOnlyList<CityModel> Cities = new List<CityModel>
            {
                new CityModel
                {
                    Id = 1, Key = "Kiev", Name = "Киев", Latitude = 50.4546600, Longitude = 30.5238000
                },
                new CityModel
                {
                    Id = 2, Key = "Zaporozhye", Name = "Запорожье", Latitude = 47.8228900, Longitude = 35.1903100
                },
                new CityModel
                {
                    Id = 3, Key = "Odessa", Name = "Одесса", Latitude = 46.4774700, Longitude = 30.7326200
                },
                new CityModel
                {
                    Id = 4, Key = "Lyvov", Name = "Львов", Latitude = 49.8383,  Longitude = 24.0232
                }
            };

        private readonly Dictionary<int, SunriseSunsetModel> _sunriseSunsetModels = new Dictionary<int, SunriseSunsetModel>
        {
            {1, new SunriseSunsetModel { Sunrise = DateTime.Today.AddHours(1), Sunset = DateTime.Today.AddHours(-1) } },
            {2, new SunriseSunsetModel { Sunrise = DateTime.Today.AddHours(2), Sunset = DateTime.Today.AddHours(-2) } },
            {3, new SunriseSunsetModel { Sunrise = DateTime.Today.AddHours(3), Sunset = DateTime.Today.AddHours(-3) } },
            {4, new SunriseSunsetModel { Sunrise = DateTime.Today.AddHours(4), Sunset = DateTime.Today.AddHours(-4) } },
        };

        private Mock<ICityRepository> _cityRepository;
        private Mock<ISunriseSunsetApi> _sunriseSunsetApi;

        public HomeControllerTest()
        {
            InitMockObjects();
        }

        [Test]
        [TestCaseSource(nameof(Cities))]
        public async Task IndexReturnsExactCityInfoTest(CityModel city)
        {
            var controller = new HomeController(_cityRepository.Object, _sunriseSunsetApi.Object);

            var result = await controller.Index(city.Key, null);

            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;

            viewResult.Model.Should().NotBeNull();
            viewResult.Model.Should().BeOfType<CitiesListViewModel>();

            var model = viewResult.Model as CitiesListViewModel;
            model.Cities.Should().HaveCount(1);
            model.Cities.First().CityName.Should().Be(city.Name);
            model.Cities.First().Sunrise.Should().Be(_sunriseSunsetModels[city.Id].Sunrise);
            model.Cities.First().Sunset.Should().Be(_sunriseSunsetModels[city.Id].Sunset);

            model.CitySelectList.Select(x => x.Value).Should()
                .BeEquivalentTo(Cities.Select(x => x.Key));

            model.SelectionColumn.Select(x => x.Value).Should()
                .BeEquivalentTo("0", "1");

            model.SelectionColumnValue.Should().BeNull();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public async Task IndexReturnsAllCityInfosTest(string city)
        {
            var controller = new HomeController(_cityRepository.Object, _sunriseSunsetApi.Object);

            var result = await controller.Index(city, null);

            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;

            viewResult.Model.Should().NotBeNull();
            viewResult.Model.Should().BeOfType<CitiesListViewModel>();

            var model = viewResult.Model as CitiesListViewModel;
            model.Cities.Should().HaveCount(Cities.Count);

            foreach (var cityInfo in model.Cities)
            {
                var entity = Cities.First(x => x.Name == cityInfo.CityName);

                cityInfo.CityName.Should().Be(entity.Name);
                cityInfo.Sunrise.Should().Be(_sunriseSunsetModels[entity.Id].Sunrise);
                cityInfo.Sunset.Should().Be(_sunriseSunsetModels[entity.Id].Sunset);
            }

            model.CitySelectList.Select(x => x.Value).Should()
                .BeEquivalentTo(Cities.Select(x => x.Key));

            model.SelectionColumn.Select(x => x.Value).Should()
                .BeEquivalentTo("0", "1");

            model.SelectionColumnValue.Should().BeNull();
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public async Task IndexReturnsSelectedColumnTest(int column)
        {
            var controller = new HomeController(_cityRepository.Object, _sunriseSunsetApi.Object);

            var result = await controller.Index(null, column);

            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;

            viewResult.Model.Should().NotBeNull();
            viewResult.Model.Should().BeOfType<CitiesListViewModel>();

            var model = viewResult.Model as CitiesListViewModel;

            model.SelectionColumnValue.Should().Be(column);
        }

        private void InitMockObjects()
        {
            _sunriseSunsetApi = new Mock<ISunriseSunsetApi>();
            _cityRepository = new Mock<ICityRepository>();

            _sunriseSunsetApi.Setup(api =>
                    api.GetSunriseSunsetMessageAsync(It.IsAny<double>(), It.IsAny<double>()))
                .Returns<double, double>((x, y) =>
                {
                    var city = Cities.FirstOrDefault(c => c.Latitude == x && c.Longitude == y);
                    return Task.FromResult(_sunriseSunsetModels[city.Id]);
                });

            _cityRepository.Setup(repository => repository.ListAsync()).ReturnsAsync(Cities);
        }
    }
}