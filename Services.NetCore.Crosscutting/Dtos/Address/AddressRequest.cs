using Services.NetCore.Crosscutting.Core;

namespace Services.Workforce.Crosscutting.Dtos.Address
{
    public class CreateOrUpdateAddressRequest : RequestBase
    {
        public AddressDto Address { get; set; }
    }

    public class DeleteAddressRequest : RequestBase
    {
        public int Id { get; set; }
    }

    public class AddressRequest
    {
    }
}
