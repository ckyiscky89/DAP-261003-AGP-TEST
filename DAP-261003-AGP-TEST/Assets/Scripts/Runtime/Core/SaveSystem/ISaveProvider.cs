
namespace DAP.Runtime.Core
{
    public interface ISaveProvider
    {
        void SaveStars(int levelIndex, int stars);
        int GetStars(int levelIndex);
        void Load();
    }
}