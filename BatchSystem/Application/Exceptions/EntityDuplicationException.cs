namespace BatchSystem.Application.Exceptions
{
    public class EntityDuplicationException : Exception
    {
        public string EntityType { get; }
        public string Key { get; }

        public EntityDuplicationException(string entityType, string key)
            : base($"The entity '{entityType}' with value '{key}' already exists.")
        {
            EntityType = entityType;
            Key = key;
        }
    }
}
