using System.Threading.Tasks;
using Quorra.Models;

namespace Quorra.Interfaces
{
    public interface ILuisService
    {
        Task<string> GetTopScoringIntentAsync(string query);
        Task<RootObjectLuis> GetAllDataAsync(string query);
    }
}
