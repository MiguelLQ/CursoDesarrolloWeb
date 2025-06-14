﻿namespace Ecomerce.frontend.Services
{
    public interface IRepository
    {
        Task<T> GetAsync<T>(string url);

        Task<T> GetByIdAsync<T>(string url, int id);

        Task<HttpResponseMessage> PostAsync<T>(string url, T model);

        Task<object> DeleteAsync(string url);

        Task<HttpResponseMessage> PutAsync<T>(string url, T model);
    }
}
