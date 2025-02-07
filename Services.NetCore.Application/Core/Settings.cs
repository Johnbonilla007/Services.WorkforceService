namespace Services.NetCore.Application.Core
{
    public static class Settings
    {
        public const string AddUser = nameof(AddUser);
        public const string UpdateUser = nameof(UpdateUser);
        public const string serviceDoesntExist = nameof(serviceDoesntExist);
        public const string providerDoesntExist = nameof(providerDoesntExist);
        public const string addressUserDoesntExist = nameof(addressUserDoesntExist);
        public const string userAlreadyExist = nameof(userAlreadyExist);
        public const string servicesByProviderDoesntExist = nameof(servicesByProviderDoesntExist);
    }
    public static class SchemaTypes
    {
        public const string ExceptionHandler = nameof(ExceptionHandler);
    }
}
