using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using PCLStorage;

namespace KeepShooting.Models
{
    public class IOData<T>
    {
        public IFolder RootFolder { get; } = FileSystem.Current.LocalStorage;
        public string Path { get; }
        public IOData(string path)
        {
            Path = path;
        }

        public async Task<T> ReadAsync() 
        {
            IFile file = await FileSystem.Current.GetFileFromPathAsync(Path);
            if (file == null) return default(T);
            
            string json = await file.ReadAllTextAsync();
            T data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        public async Task Write(string data)
        {
            IFile file = await FileSystem.Current.GetFileFromPathAsync(data);
            
        }
    }
}
