namespace BatchSystem.Application.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public object? Detail { get; }

        public BusinessRuleException(string message, object? detail = null)
            : base(message)
        {
            Detail = detail;
        }

    }
}
