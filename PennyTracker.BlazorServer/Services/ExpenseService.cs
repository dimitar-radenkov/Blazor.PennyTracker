using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using PennyTracker.Shared.Models;

namespace PennyTracker.BlazorServer.Services
{
    public class ExpenseService : IExpenseService
    {
        private const string URL_BASE = "api/expenses";
        private readonly HttpClient httpClient;

        public ExpenseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Expense> AddAsync(Expense expense)
        {
            var expenseJson =
                 new StringContent(JsonSerializer.Serialize(expense), Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync(URL_BASE, expenseJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Expense>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            await this.httpClient.DeleteAsync($"{URL_BASE}/{id}");
        }

        public async Task<IList<Expense>> GetAll()
        {
            var response = await this.httpClient.GetStreamAsync($"{URL_BASE}");
            var res = await JsonSerializer.DeserializeAsync<IList<Expense>>(
                response, 
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return res;
        }

        public async Task<Expense> GetAsync(int id)
        {
            var response = await this.httpClient.GetAsync($"{URL_BASE}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
  
            var res = await JsonSerializer.DeserializeAsync<Expense>(
                await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return res;
        }

        public async Task UpdateAsync(int id, Expense expense)
        {
            var expenseJson = new StringContent(JsonSerializer.Serialize(expense), Encoding.UTF8, "application/json");

            await this.httpClient.PutAsync("{URL_BASE}", expenseJson);
        }
    }
}
