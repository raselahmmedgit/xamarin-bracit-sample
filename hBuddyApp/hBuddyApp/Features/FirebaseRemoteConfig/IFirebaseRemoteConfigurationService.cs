using System.Collections.Generic;
using System.Threading.Tasks;

namespace hBuddyApp.Features.FirebaseRemoteConfig
{
    public interface IFirebaseRemoteConfigurationService
    {
        Task FetchAndActivateAsync();
        Task<T> GetAsync<T>(string key);
        Task<IEnumerable<T>> GetAllAsync<T>(string key);
    }
}
