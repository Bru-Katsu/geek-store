namespace GeekStore.Product.Application.Products.Events
{
    public class ProductRemovedEvent : ProductBaseEvent
    {
        public ProductRemovedEvent(Domain.Products.Product product) : base(product)
        {
            AggregateId = product.Id;
        }
    }
}
