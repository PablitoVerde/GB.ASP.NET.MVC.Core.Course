using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace Core
{
    public class FileReaderAsync
    {
        ConcurrentDictionary<string, string> AllLines { get; set; }

        public FileReaderAsync()
        {
            AllLines = new ConcurrentDictionary<string, string>();
        }

        public async Task ReadAllLinesAsync(List<string> filenames)
        {
            foreach (string filename in filenames)
            {
                await ReadFileAsync(filename);
            }
        }

        private async Task ReadFileAsync(string filename)
        {
            Task t = Task.Run(() =>
            {
                StreamReader sr = new StreamReader(filename);

                int i = 0;

                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();

                    if (line != null)
                    {
                        AllLines.TryAdd(filename + $" {i}", line);
                    }
                    else
                    {
                        AllLines.TryAdd(filename + $" {i}", "Empty Line");
                    }
                }

                return Task.CompletedTask;
            });
        }
    }
}
