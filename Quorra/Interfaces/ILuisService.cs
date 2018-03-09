using System.Threading.Tasks;
using Quorra.Models;

namespace Quorra.Interfaces
{
    public interface ILuisService
    {
        Task<RootObjectLuis> GetLuisDataAsync(string query);
    }
}
