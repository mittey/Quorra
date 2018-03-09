using System.Threading.Tasks;
using Quorra.Models;

namespace Quorra.Interfaces
{
    public interface IJokeService
    {
        Task<Joke> GetJokeAsync();
    }
}
