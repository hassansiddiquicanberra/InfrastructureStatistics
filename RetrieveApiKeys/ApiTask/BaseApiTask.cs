using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask
{
    public abstract class BaseApiTask
    {
        protected string Id;
        protected string Token;

        public delegate void SetOutputTextCallback(string text);
        public event SetOutputTextCallback RaiseSetOutputText;
        public abstract string Start();
        public virtual void TaskComplete()
        {
            SetOutputText($"{Environment.NewLine} Task completed!!");
            SetOutputText($"{Environment.NewLine}{Environment.NewLine}");
        }

        protected async Task<string> SendRequest(string uri, HttpMethod method, int attempt = 1, int maxAttempts = 5)
        {
            return await SendRequest(uri, Id, Token, method, string.Empty, attempt, maxAttempts);
        }

        protected async Task<string> SendRequest(string uri, string id, string token, HttpMethod method,
            string requestBody = "", int attempt = 1, int maxAttempts = 5)
        {
            if (attempt > maxAttempts)
            {
                return null;
            }

            var client = InitialiseHttpClient(id, token);
            var errorMessage = $"ERROR: System did not return a successful Http Status code after {maxAttempts} attempts.{Environment.NewLine}";

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = method,
            };

            if (!string.IsNullOrWhiteSpace(requestBody))
            {
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            }

            SetOutputText($"Attempting to communicate with {uri}...{Environment.NewLine}");

            using (var response = await client.SendAsync(request))
            {
                //var responseHeaderContainsLink = response.Headers.Contains("link");
                //TODO: if response header doesn't contain link means that is the last page.

                using (var content = response.Content)
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        SetOutputText($"SUCCESS: {BaseLogF1ErrorMessage(uri, attempt, maxAttempts)} with a '{response.StatusCode}' status code.{Environment.NewLine}");
                    }
                    catch (HttpRequestException ex)
                    {
                        SetOutputText($"ERROR: {BaseLogF1ErrorMessage(uri, attempt, maxAttempts)} with a '{response.StatusCode}' error code.{Environment.NewLine}");
                        if (attempt > maxAttempts)
                        {
                            SetOutputText(errorMessage);
                        }

                        return await SendRequest(uri, id, token, method, requestBody, attempt + 1);
                    }

                    var responseBody = await content.ReadAsStringAsync();

                    var isSuccessResponseButEmptyBody = response.IsSuccessStatusCode &&
                                                        (string.IsNullOrEmpty(responseBody) ||
                                                         string.IsNullOrWhiteSpace(responseBody));
                    if (!isSuccessResponseButEmptyBody)
                    {
                        return responseBody;
                    }

                    SetOutputText($"WARNING: {BaseLogF1ErrorMessage(uri, attempt, maxAttempts)} with a 200 Response code and an empty body.{Environment.NewLine}");

                    if (attempt > maxAttempts)
                    {
                        SetOutputText(errorMessage);
                    }

                    return await SendRequest(uri, id, token, method, requestBody, attempt + 1);
                }
            }
        }

        private HttpClient InitialiseHttpClient(string idValue, string tokenValue)
        {
            var httpClient = new HttpClient();
            var authenticationValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{idValue}:{tokenValue}"));
            var authenticationHeaderValue = new AuthenticationHeaderValue("Basic", authenticationValue);
            httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

            return httpClient;
        }

        private string BaseLogF1ErrorMessage(string uri, int attempt, int maxAttempts)
        {
            return $"URI: {uri} attempt {attempt} of {maxAttempts}";
        }

        protected void SetOutputText(string text)
        {
            RaiseSetOutputText?.Invoke(text);
        }

        protected async Task<string> GetAllTicketsAsync(string uri, HttpMethod method, int attempt = 1, int maxAttempts = 5)
        {
            return await GetAllTicketsAsync(uri, Id, Token, method, string.Empty, attempt, maxAttempts);
        }

        protected async Task<string> GetAllTicketsAsync(string uri, string id, string token, HttpMethod method, string requestBody = "", int attempt = 1, int maxAttempts = 5)
        {
            int pageNumber = 1;
            var client = InitialiseHttpClient(id, token);
            var errorMessage = $"ERROR: System did not return a successful Http Status code after {maxAttempts} attempts.{Environment.NewLine}";

            bool isResponseContainingLinkText = false;
            var responseBodyList = new List<string>();
            var stringBuilder = new StringBuilder();

            do
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(uri + "?page=" + pageNumber),
                    Method = method,
                };

                var response = await client.SendAsync(request);
                var content = response.Content;
                string responseBody = await content.ReadAsStringAsync();

                var isSuccessResponseButEmptyBody = response.IsSuccessStatusCode &&
                                                    (string.IsNullOrEmpty(responseBody) ||
                                                     string.IsNullOrWhiteSpace(responseBody));

                if (!isSuccessResponseButEmptyBody)
                {
                    //stringBuilder.AppendLine(responseBody);
                    responseBodyList.Add(responseBody);
                }

                if (response.Headers.Contains("link"))
                {
                    isResponseContainingLinkText = true;
                    pageNumber++;
                }

                //} while (isResponseContainingLinkText);
            } while (pageNumber < 11);


            foreach (var value in responseBodyList)
            {
                stringBuilder.Append(value);
            }


            var convertedValues = ConfigHelper.MergeJsonString(responseBodyList);

            return convertedValues;
        }
    }
}













