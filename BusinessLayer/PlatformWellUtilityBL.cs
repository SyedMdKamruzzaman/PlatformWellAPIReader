using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CommonLayer;
using CommonLayer.DTO;
using CommonLayer.DTO.ViewModel;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BusinessLayer
{
    public class PlatformWellUtilityBL
    {
        private DALUtility DALUtility;
        public PlatformWellUtilityBL()
        {
            DALUtility = new DALUtility();
        }
        public async Task<string> GetBearerToken()
        {
            Authentication auth = new Authentication();
            auth.username = ConfigurationManager.AppSetting["LoginAPI:UserName"]; //"user@aemenersol.com";
            auth.password = ConfigurationManager.AppSetting["LoginAPI:Password"];

            string Url = ConfigurationManager.AppSetting["LoginAPI:URL"];

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(auth), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(Url, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return apiResponse.Replace('"', ' ');
                }
            }
        }

        public PlatformWellViewModel GetPlatformWellList()
        {
            return DALUtility.GetPlatformWellList();
        }

        public async void GetPlatformActualAndSaveorUpdate(Bearer bearer)
        {
            List<Platform> platformList = new List<Platform>();

            string PlatformWellActualURL = ConfigurationManager.AppSetting["PlatformWellActualAPI:URL"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer.token);

                using (var response = await httpClient.GetAsync(PlatformWellActualURL))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    try
                    {
                        platformList = JsonConvert.DeserializeObject<List<Platform>>(apiResponse);
                        DALUtility.InsertOrUpdatePlatformWellData(platformList);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
        }


        public async Task<string> GetPlatformActualAndSaveorUpdate(PlatformWellActual platformWellActual,Bearer bearer)
        {
            string message = string.Empty;
            List <Platform> platformList = new List<Platform>();

            string PlatformWellActualURL = platformWellActual.Url; //ConfigurationManager.AppSetting["PlatformWellActualAPI:URL"];

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer.token);

                using (var response = await httpClient.GetAsync(PlatformWellActualURL))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    try
                    {
                        platformList = JsonConvert.DeserializeObject<List<Platform>>(apiResponse);
                        DALUtility.InsertOrUpdatePlatformWellData(platformList);
                        message = "Data saved and loaded successfully.";
                        return message;
                    }
                    catch (Exception ex)
                    {
                        message = "API return different set of data for example some key are missing or new key is added. " + ex.Message.ToString().Replace("'","");
                        return message;
                    }
                }
            }
        }



    }
}
