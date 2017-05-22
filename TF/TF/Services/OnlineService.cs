using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TF.Helpers;

namespace TF.Services
{
    public static class OnlineService
    {
        private static object obj = new object();
        private static HttpClient client;
        public static HttpClient Client
        {
            get
            {
                lock(obj)
                {
                    if (client == null)
                    {
                        client = new System.Net.Http.HttpClient();
                        client.BaseAddress = new Uri("http://johnz4747-001-site1.btempurl.com/");
                    }
                    return client;
                }
            }
        }

        #region trainings
        public static async Task<Result> SaveTrainingAsync(TriathlonTraining item)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return new Result(false, StringService.Instance.Error, StringService.Instance.NoInternet);
            
            string jsonData = Writer.WriteTriathlonTraining(ref item);

            var content = new StringContent(jsonData, Encoding.UTF8);
            HttpResponseMessage response = item.Id == -1 ? await Client.PostAsync("api/Trainings", content) : await client.PutAsync("api/Trainings", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
            return new Result(true);
        }

        public static async Task<Result> GetTrainingsAsync(int userId)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return new Result(false, StringService.Instance.Error, StringService.Instance.NoInternet);

            HttpResponseMessage response = await Client.GetAsync(string.Format("api/Trainings/?userId={0}", userId));
            
            var result = await response.Content.ReadAsStringAsync();
            return new Result(true);
        }

        public static async Task<Result> DeleteTrainingAsync(int id)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return new Result(false, StringService.Instance.Error, StringService.Instance.NoInternet);

            HttpResponseMessage response = await client.DeleteAsync(string.Format("api/Trainings/", id));

            var result = await response.Content.ReadAsStringAsync();
            return new Result(true);
        }
        #endregion

        #region groups
        public static async Task<Result> GetGroupsAsync(int userId)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return new Result(false, StringService.Instance.Error, StringService.Instance.NoInternet);

            HttpResponseMessage response = await client.GetAsync(string.Format("api/Groups/", userId));

            var result = await response.Content.ReadAsStringAsync();
            return new Result(true);
        }

        public static async Task<Result> DeleteGroupAsync(int id)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return new Result(false, StringService.Instance.Error, StringService.Instance.NoInternet);

            HttpResponseMessage response = await client.DeleteAsync(string.Format("api/Groups/", id));

            var result = await response.Content.ReadAsStringAsync();
            return new Result(true);
        }
        #endregion

        #region user

        public static async Task<Result> CreateUserAsync(User item)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return new Result(false, StringService.Instance.Error, StringService.Instance.NoInternet);

            string jsonData = Writer.WriteUser(ref item);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
           
            HttpResponseMessage response = await Client.PostAsync("api/Users/", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
            return new Result(true);
        }
        #endregion
    }
}
