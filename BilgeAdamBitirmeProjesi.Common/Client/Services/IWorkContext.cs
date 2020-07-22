using BilgeAdamBitirmeProjesi.Common.DTOs.User;

namespace BilgeAdamBitirmeProjesi.Common.Client.Services
{
    public interface IWorkContext
    {
        //UI Taraftada kullanması için bu katmana tanımladım.

        UserResponse CurrentUser { get; set; }
    }
}
