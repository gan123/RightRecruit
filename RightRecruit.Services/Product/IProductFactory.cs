using RightRecruit.Domain.Plan;

namespace RightRecruit.Services.Product
{
    public interface IProductFactory
    {
        ProductType Get(string product);
    }
}