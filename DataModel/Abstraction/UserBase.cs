using DataModel.Model;
using System.Text.Json;

namespace DataModel.Abstraction
{
    public abstract class UserBase<T>
    {
        private readonly string FilePath;

        protected UserBase(string fileName)
        {
            FilePath = Path.Combine(@"D:\CWdotnet", fileName);
            EnsureDirectoryExists();
        }

        private void EnsureDirectoryExists()
        {
            var directory = Path.GetDirectoryName(FilePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        protected List<T> LoadData()
        {
            if (!File.Exists(FilePath)) return new List<T>();

            var json = File.ReadAllText(FilePath);

            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        protected void SaveData(List<T> item)
        {
            var json = JsonSerializer.Serialize(item);
            File.WriteAllText(FilePath, json);
        }

        protected bool UpdateItem(Func<T, bool> predicate, Action<T> updateAction)
        {
            var items = LoadData();
            var item = items.FirstOrDefault(predicate);

            if (item == null)
            {
                return false;
            }
            updateAction(item);
            SaveData(items);
            return true;
        }
    }
}
