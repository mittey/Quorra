using System.Threading.Tasks;

namespace Quorra.Interfaces
{
    public interface ILuisService
    {
        Task<string> GetTopScoringIntentAsync(string query);
    }
}
