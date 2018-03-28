namespace Syku.LinearRegression
{
    public interface IApartmentEstimator
    {
        double EstimateSurface(uint price);

        uint EstimatePrice(double surface);
    }
}