using Bogus;
using Bogus.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekStore.Product.Tests.Fixtures
{
    public class ProductFixture : ICollectionFixture<ProductFixture>
    {
        public Domain.Products.Product GenerateValidProduct()
        {
            var productFaker = new Faker<Domain.Products.Product>()
                .CustomInstantiator(f =>
                {
                    var name = f.Commerce.ProductName().ClampLength(1, 150);
                    var category = f.Commerce.Categories(1).First().ClampLength(1, 50);
                    var description = f.Commerce.ProductDescription().ClampLength(1, 500);
                    var imageURL = f.Image.PlaceImgUrl().ClampLength(1, 300);
                    var price = decimal.Parse(f.Commerce.Price());

                    return new Domain.Products.Product(name, price, description, category, imageURL);
                });

            return productFaker.Generate();
        }
    }
}
