namespace StoreApi.Web.Messages
{
    public class CustomerCreatedMessage
    {
        public CustomerCreatedMessage(string message)
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }
}