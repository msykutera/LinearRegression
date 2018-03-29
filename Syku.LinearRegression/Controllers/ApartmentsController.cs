using Microsoft.AspNetCore.Mvc;

namespace Syku.LinearRegression
{
    [Route("api/apartments")]
    public class ApartmentsController : Controller
    {
        private readonly IRepository<Apartment> _apartmentRepository;
        private readonly IApartmentEstimator _apartmentEstimator;

        public ApartmentsController(
            IRepository<Apartment> apartmentRepository,
            IApartmentEstimator apartmentEstimator)
        {
            _apartmentRepository = apartmentRepository;
            _apartmentEstimator = apartmentEstimator;
        }

        [HttpPost("add")]
        public void Add([FromBody] ApartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apartment = new Apartment
                {
                    Price = (uint) model.Price,
                    Surface = model.Surface,
                };
                _apartmentRepository.Save(apartment);
            }
        }

        [HttpGet("estimatePrice/{surface}")]
        public uint EstimatePrice(double surface)
        {
            var result = _apartmentEstimator.EstimatePrice(surface);
            return result;
        }

        [HttpGet("estimateSurface/{price}")]
        public double EstimateSurface(uint price)
        {
            var result = _apartmentEstimator.EstimateSurface(price);
            return result;
        }
    }
}
