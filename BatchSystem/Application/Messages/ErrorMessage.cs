using BatchSystem.Application.Exceptions;

namespace BatchSystem.Application.Messages
{
    public class ErrorMessage
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public object Detail { get; set; }

        public ErrorMessage(string errorCode, string message, string detail)
        {
            ErrorCode = errorCode;
            Message = message;
            Detail = detail;
        }

        public ErrorMessage(EntityNotFoundException ex)
        {
            ErrorCode = $"EntityNotFound.{ex.EntityType}";
            Message = ex.Message;
            Detail = new
            {
                ex.EntityType,
                ex.Key,
            };
        }

        public ErrorMessage(EntityDuplicationException ex)
        {
            ErrorCode = $"EntityDuplication.{ex.EntityType}";
            Message = ex.Message;
            Detail = new
            {
                ex.EntityType,
                ex.Key,
            };
        }

        public ErrorMessage(BusinessRuleException ex)
        {
            ErrorCode = "BusinessRule";
            Message = ex.Message;
            Detail = string.Empty;
        }
    }
}
