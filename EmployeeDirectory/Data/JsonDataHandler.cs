using EmployeeDirectory.Models;
using Newtonsoft.Json;

namespace EmployeeDirectory.DATA
{
    public class JsonDataHandler
    {
        public static List<T> GetDataFromJson<T>(string type)
        {
            string data;
            List<T>? dataList = [];
            try
            {
                if (type.Equals("employee"))
                {
                    data = File.ReadAllText(Directory.GetCurrentDirectory() + "\\DATA\\employees.json");
                    dataList = System.Text.Json.JsonSerializer.Deserialize<List<T>>(data);
                }
                else
                {
                    data = File.ReadAllText(Directory.GetCurrentDirectory() + "\\DATA\\roles.json");
                    dataList = System.Text.Json.JsonSerializer.Deserialize<List<T>>(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataList;
        }

        public static void UpdateEmployeesDataToJson<T>(List<T> listData,string type){

            string updatedList = JsonConvert.SerializeObject(listData);
            string path;
            if (type .Equals("employee"))
            {
                path = Directory.GetCurrentDirectory() + "\\DATA\\employees.json";
            }
            else
            {
                path = Directory.GetCurrentDirectory() + "\\DATA\\roles.json";
            }
            File.WriteAllText(path, updatedList);
        }
    }
}
