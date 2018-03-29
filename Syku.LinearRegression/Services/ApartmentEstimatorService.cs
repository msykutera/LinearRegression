using Accord.Statistics.Models.Regression.Linear;
using System.Linq;

namespace Syku.LinearRegression
{
    public class ApartmentEstimatorService : IApartmentEstimator
    {
        private readonly IRepository<Apartment> _repository;

        public ApartmentEstimatorService(IRepository<Apartment> repository)
        {
            _repository = repository;
        }

        public uint EstimatePrice(double surface)
        {
            var apartments = _repository.GetAll();

            double[] inputs = apartments.Select(x => x.Surface).ToArray();
            double[] outputs = apartments.Select(x => (double) x.Price).ToArray();

            var ols = new OrdinaryLeastSquares();
            var regression = ols.Learn(inputs, outputs);
            double result = regression.Transform(surface);

            return (uint) result;
        }

        public double EstimateSurface(uint price)
        {
            var apartments = _repository.GetAll();

            double[] inputs = apartments.Select(x => (double) x.Price).ToArray();
            double[] outputs = apartments.Select(x => x.Surface).ToArray();

            var ols = new OrdinaryLeastSquares();
            var regression = ols.Learn(inputs, outputs);
            double result = regression.Transform(price);

            return result;
        }
    }
}
