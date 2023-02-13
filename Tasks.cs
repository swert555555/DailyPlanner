using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppPract2
{
    public class Tasks
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public object Date { get; set; }


        public Tasks(string name, string description, object date)
        {
            this.Name = name;
            this.Description = description;
            this.Date = date;
        }
    }

    public class FileIOService 
    {
        private readonly string PATH;

        public FileIOService(string path) 
        { 
            PATH = path;
        }

        public T Deserialize<T>()
        {
            string text = File.ReadAllText("C:\\Users\\Kotam\\OneDrive\\Рабочий стол\\DP.json");
            T result = JsonConvert.DeserializeObject<T>(text);
            return result;
        }
        public void Serialize<T>(T tasks)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            { 
                string json = JsonConvert.SerializeObject(tasks);
                writer.WriteLine(json);
            }    
        }
    }
}