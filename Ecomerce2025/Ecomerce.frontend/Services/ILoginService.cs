namespace Ecomerce.frontend.Services
{
    public interface ILoginService
    {
        Task Login(string token);

        Task Logout();
    }
}
