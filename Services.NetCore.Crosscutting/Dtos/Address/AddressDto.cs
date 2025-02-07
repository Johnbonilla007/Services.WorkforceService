using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.Workforce.Crosscutting.Core;

namespace Services.Workforce.Crosscutting.Dtos.Address
{
    public class AddressResponse : ResponseBase
    {
        public AddressDto Address { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
    public class AddressDto : BaseDto
    {
        public string HouseNumber { get; set; }
        public string Reference { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Coordinates { get; set; }
        public int UserId { get; set; }
        public bool IsDefault { get; set; }
        public string Alias { get; set; }
    }
}
