using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Syku.LinearRegression
{
    public class ApartmentControllerTests : IDisposable
    {
        private readonly ApartmentsController _controller;

        public ApartmentControllerTests()
        {
            var repository = new ApartmentRepository();
            var estimator = new ApartmentEstimatorService(repository);
            _controller = new ApartmentsController(repository, estimator);
        }

        [Fact]
        public void PriceIsEstimatedCorrectly()
        {
            var apartments = new List<ApartmentViewModel>
            {
                new ApartmentViewModel { Price = 100000, Surface = 50.0 },
                new ApartmentViewModel { Price = 200000, Surface = 70.0 },
                new ApartmentViewModel { Price = 300000, Surface = 100.0 },
                new ApartmentViewModel { Price = 80000, Surface = 33.0 },
                new ApartmentViewModel { Price = 1000000, Surface = 56.0 },
            };

            foreach (var apartment in apartments) _controller.Add(apartment);

            var result = _controller.EstimatePrice(80.0);

            result.Should().Be(363464);
        }

        [Fact]
        public void SurfaceIsEstimatedCorrectly()
        {
            var apartments = new List<ApartmentViewModel>
            {
                new ApartmentViewModel { Price = 100000, Surface = 50.0 },
                new ApartmentViewModel { Price = 200000, Surface = 70.0 },
                new ApartmentViewModel { Price = 300000, Surface = 100.0 },
                new ApartmentViewModel { Price = 80000, Surface = 33.0 },
                new ApartmentViewModel { Price = 1000000, Surface = 56.0 },
            };

            foreach (var apartment in apartments) _controller.Add(apartment);

            var result = _controller.EstimateSurface(560000);

            result.Should().BeApproximately(63.26, 0.1);
        }

        public void Dispose()
        {
            _controller.Dispose();
        }
    }
}
