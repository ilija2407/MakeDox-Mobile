using Makedox2019.PageModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Makedox2019.Services
{
    public class JsonService
    {
        public string Movies { get { return movies; } }
        string movies = "Makedox2019.Resources.Movies.json";
       
        public JsonService()
        {

        }
        public string ReadFile(string file)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(JsonService)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(file);
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {

                text = reader.ReadToEnd();
            }
            return text;
        }

        public List<Event> DeserializeEvents(string path)
        {
            string json = ReadFile(path);
            List<Event> events = JsonConvert.DeserializeObject<List<Event>>(json, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm" });
            return events;
        }
    }
}
