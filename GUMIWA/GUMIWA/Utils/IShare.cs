using System.Threading.Tasks;

namespace PassXYZ.Utils
{
	public interface IShare
	{
        string BaseUrl { get; }
        Task Show(string title, string message, string filePath);
        string GetPlatformPath(string fileName);
    }
}
