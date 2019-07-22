using System.Threading.Tasks;
using TimCoDesktopUI.Models;

namespace TimCoDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}