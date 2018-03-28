using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Syku.LinearRegression
{
    public class ApartmentControllerTests
    {
        private ApartmentsController GetController()
        {
            var repository = new ApartmentRepository();
            var estimator = new ApartmentEstimatorService(repository);
            return new ApartmentsController(repository, estimator);
        }

        [Fact]
        public void PriceIsEstimatedCorrectly()
        {
            var controller = GetController();
            var apartments = new List<ApartmentViewModel>
            {
                new ApartmentViewModel { Price = 100000, Surface = 50.0 },
                new ApartmentViewModel { Price = 200000, Surface = 70.0 },
                new ApartmentViewModel { Price = 300000, Surface = 100.0 },
                new ApartmentViewModel { Price = 80000, Surface = 33.0 },
                new ApartmentViewModel { Price = 1000000, Surface = 56.0 },
            };

            foreach (var apartment in apartments) controller.Add(apartment);

            var result = controller.EstimatePrice(80.0);

            result.Should().Be(363464);
        }

        [Fact]
        public void SurfaceIsEstimatedCorrectly()
        {
            var controller = GetController();
            var apartments = new List<ApartmentViewModel>
            {
                new ApartmentViewModel { Price = 100000, Surface = 50.0 },
                new ApartmentViewModel { Price = 200000, Surface = 70.0 },
                new ApartmentViewModel { Price = 300000, Surface = 100.0 },
                new ApartmentViewModel { Price = 80000, Surface = 33.0 },
                new ApartmentViewModel { Price = 1000000, Surface = 56.0 },
            };

            foreach (var apartment in apartments) controller.Add(apartment);

            var result = controller.EstimateSurface(560000);

            result.Should().BeApproximately(63.26, 0.1);
        }
    }
}
