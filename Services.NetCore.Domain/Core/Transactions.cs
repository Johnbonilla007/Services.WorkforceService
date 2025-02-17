﻿namespace Services.NetCore.Domain.Core
{
    public static class Transactions
    {
        public const string Insert = nameof(Insert);
        public const string Update = nameof(Update);
        public const string CreateLogException = nameof(CreateLogException);
        public const string Added = nameof(Added);
        public const string CreateService = nameof(CreateService);
        public const string DeleteService = nameof(DeleteService);
        public const string UpdateService = nameof(UpdateService);
        public const string CreateProvider = nameof(CreateProvider);
        public const string UpdateProvider = nameof(UpdateProvider);
        public const string DeleteProvider = nameof(DeleteProvider);
        public const string CreateAddress = nameof(CreateAddress);
        public const string UpdateAddress = nameof(UpdateAddress);
        public const string DeleteAddress = nameof(DeleteAddress);
        public const string SetAsDefaultAddress = nameof(SetAsDefaultAddress);
        public const string UpdateServicesByProvider = nameof(UpdateServicesByProvider);
    }
}
