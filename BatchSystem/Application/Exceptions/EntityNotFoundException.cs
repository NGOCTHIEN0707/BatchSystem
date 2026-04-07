namespace BatchSystem.Application.Exceptions
{
    public class EntityNotFoundException: Exception
    {
        public string EntityType { get; }
        public string Key { get; }
        public EntityNotFoundException(string entityType, string key)
            : base($"{entityType} '{key}' not found.")
        {
            EntityType = entityType;
            Key = key;
        }
    }
}
