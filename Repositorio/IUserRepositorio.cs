using ClientesAPI.Models;
using System.Threading.Tasks;

namespace ClientesAPI.Repositorio {
    public interface IUserRepositorio {
        Task<int> Register(User user, string password);
        Task<string> Login(string userName, string password);
        Task<bool> UserExist(string userName);
    }
}
