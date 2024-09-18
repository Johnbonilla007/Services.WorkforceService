namespace Services.NetCore.Crosscutting.Core
{
    public class UserInfoDto
    {
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDescription { get; set; }
    }
}