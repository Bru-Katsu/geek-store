using GeekStore.Core.Messages;

namespace GeekStore.Product.Application.Products.Events
{
    public class ProductBaseEvent : Event
    {
        public ProductBaseEvent(Domain.Products.Product entity)
        {
            Name = entity.Name;
            Price = entity.Price;
            Description = entity.Description;
            Category = entity.Category;
            ImageURL = entity.ImageURL;
        }

        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public string Description { get; protected set; }
        public string Category { get; protected set; }
        public string ImageURL { get; protected set; }
    }
}
