using RightRecruit.Domain.Plan;

namespace RightRecruit.Services.Product
{
    public class ProductFactory : IProductFactory
    {
        public ProductType Get(string product)
        {
            switch (product)
            {
                case "Basic":
                    return ProductType.Basic;
                case "Intermediate":
                    return ProductType.Intermediate;
                case "Pro":
                    return ProductType.Pro;
                default: 
                    return ProductType.Basic;
            }
        }
    }
}