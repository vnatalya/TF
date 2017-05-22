using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF.Helpers
{
    public static class Writer
    {
        public static string WriteTriathlonTraining( ref TriathlonTraining item)
        {
            string json = "";
            json = string.Format(
                "Id: {0} Distance: {1} Type: {2} Time: {3} Date: {4} UserId: {5}",
                item.Id, item.Distance, (int)item.Type, item.Time, item.Date, 1);
            return json;
        }

        public static string WriteGroup(ref Group item)
        {
            string json = "";
            json = string.Format(
                "Id: {0}, Name: \"{1}\", userId: {2}",
                item.ID, item.Name, item.TeacherID);
            return json;
        }

        public static string WriteUser(ref User item)
        {
            string json = JsonConvert.SerializeObject(item, Formatting.Indented);
            return json;
        }
    }
}
