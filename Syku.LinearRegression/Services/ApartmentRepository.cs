using System.Collections.Generic;

namespace Syku.LinearRegression
{
    public class ApartmentRepository : IRepository<Apartment>
    {
        private List<Apartment> _apartments = new List<Apartment>();

        public IEnumerable<Apartment> GetAll() => _apartments;

        public void Save(Apartment apartment) => _apartments.Add(apartment);
    }
}