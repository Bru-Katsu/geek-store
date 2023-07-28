namespace GeekStore.WebApi.Core.Models
{
    public class Error
    {
        public Error(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public string Property { get; }
        public string Message { get; }
    }
}
