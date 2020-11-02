using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask
{
    public abstract class BaseApiTask
    {
        protected string Id;
        protected string Token;
        private int initialPageNumber = 1;

        public delegate void SetOutputTextCallback(string text);
        public event SetOutputTextCallback RaiseSetOutputText;
        public abstract string Start(string ticketId = null, string url = null);
        public virtual void TaskComplete()
        {
            SetOutputText($"{Environment.NewLine} Task completed!!");
            SetOutputText($"{Environment.NewLine}{Environment.NewLine}");
        }

        protected async Task<string> SendRequest(string url, string uri, HttpMethod method, int attempt = 1, int maxAttempts = 5)
        {
            return await SendRequest(url, uri, Id, Token, method, string.Empty, attempt, maxAttempts);
        }

        protected async Task<string> SendRequest(string url, string uri, string id, string token, HttpMethod method,
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
                RequestUri = string.IsNullOrEmpty(url) ? new Uri(uri) : new Uri(url),
                Method = method,
            };

            if (!string.IsNullOrWhiteSpace(requestBody))
            {
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            }

            SetOutputText($"Attempting to communicate with {uri}...{Environment.NewLine}");

            using (var response = await client.SendAsync(request))
            {
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

                        return await SendRequest(url, uri, id, token, method, requestBody, attempt + 1);
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

                    return await SendRequest(url, uri, id, token, method, requestBody, attempt + 1);
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
            int pageNumber = initialPageNumber;
            var httpClient = InitialiseHttpClient(id, token);

            bool isResponseContainingLinkText = false;
            var responseBodyList = new List<string>();

            do
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(uri + "?page=" + pageNumber),
                    Method = method,
                };

                var response = await httpClient.SendAsync(request);
                var content = response.Content;
                string responseBody = await content.ReadAsStringAsync();

                var isSuccessResponseButEmptyBody = response.IsSuccessStatusCode &&
                                                    (string.IsNullOrEmpty(responseBody) ||
                                                     string.IsNullOrWhiteSpace(responseBody));

                if (!isSuccessResponseButEmptyBody)
                {
                    responseBodyList.Add(responseBody);
                }

                if (response.Headers.Contains(Constants.LinkInResponseHeader))
                {
                    isResponseContainingLinkText = true;
                    pageNumber++;
                }
                else if (!response.Headers.Contains(Constants.LinkInResponseHeader))
                {
                    isResponseContainingLinkText = false;
                }

            } while (isResponseContainingLinkText);

            return JsonHelper.MergeJsonStringValues(responseBodyList);
        }

        protected async Task<string> GetAllTimeEntriesAsync(string ticketId, string uri, HttpMethod method, int attempt = 1, int maxAttempts = 5)
        {
            return await GetAllTimeEntriesAsync(ticketId, uri, Id, Token, method, string.Empty, attempt, maxAttempts);
        }

        protected async Task<string> GetAllTimeEntriesAsync(string ticketId, string uri, string id, string token, HttpMethod method, string requestBody = "", int attempt = 1, int maxAttempts = 5)
        {
            var client = InitialiseHttpClient(id, token);
            var url = ConfigHelper.FreshServiceForTicketsUri + "/" + ticketId + "/time_entries";

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
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
                return responseBody;
            }

            return await GetAllTimeEntriesAsync(ticketId, uri, id, token, method, requestBody, attempt + 1);
        }
    }
}













