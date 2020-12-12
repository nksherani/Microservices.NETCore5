using System.Threading.Tasks;

namespace Common
{
    public interface IAppLogger
    {
        Task Debug(string message);
    }
}
