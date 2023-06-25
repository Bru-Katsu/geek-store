namespace GeekStore.Product.Application.Products.Events
{
    public class ProductUpdatedEvent : ProductBaseEvent
    {
        public ProductUpdatedEvent(Domain.Products.Product product) : base(product)
        {
            AggregateId = product.Id;
        }
    }
}
