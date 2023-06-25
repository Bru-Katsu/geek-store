namespace GeekStore.Product.Application.Products.Events
{
    public class ProductAddedEvent : ProductBaseEvent
    {
        public ProductAddedEvent(Domain.Products.Product product) : base(product)
        {
            AggregateId = product.Id;
        }
    }
}
