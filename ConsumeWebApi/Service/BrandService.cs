using Crud_with_webApi.Model;
using Newtonsoft.Json;
using System.Text;


namespace ConsumeWebApi.Service
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAll();
        Task<HttpResponseMessage> Create(Brand model);
        Task<Brand> GetByID(int id);
        Task<HttpResponseMessage> Edit(Brand model);
        Task<HttpResponseMessage> Delete(int id);
    }
    public class BrandService : IBrandService
    {
        #region variable
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        string AppsettingsValue;
        public BrandService(HttpClient httpClient, IConfiguration iConfig)
        {
            _httpClient = httpClient;
            _configuration = iConfig;
            AppsettingsValue = _configuration["Appsettings:WebApiBaseUrl"];
            Uri baseAddress = new Uri(AppsettingsValue);
            _httpClient.BaseAddress = baseAddress;
        }
        #endregion
        #region Method
        public async Task<IEnumerable<Brand>> GetAll()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Brand/GetAll/GetAll");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Brand>>(content)!;
        }
        public async Task<HttpResponseMessage> Create(Brand model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(_httpClient.BaseAddress + "Brand/Addnew/Addnew", content);
        }
        public async Task<Brand> GetByID(int id)
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Brand/Edit/" + id);
            response.EnsureSuccessStatusCode();
            string data = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Brand>(data)!;
        }
        public async Task<HttpResponseMessage> Edit(Brand model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync(_httpClient.BaseAddress + "Brand/Update", content);
        }
        public async Task<HttpResponseMessage> Delete(int id)
        {
            return await _httpClient.DeleteAsync(_httpClient.BaseAddress + "Brand/Delete/" +id);
        }
        #endregion
        
    }
}