using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Composition;
using System.Reflection.PortableExecutable;


namespace KMGamesCore.Web.Areas.PayPal.Models
{
    public class PayPalClient
    {
        //----------PROPERTIES----------//

        public string BaseUrl = "https://api-m.sandbox.paypal.com";
        public string ClientId { get; private set; }
        public string Secret { get; private set; }
        public string Mode { get; private set; }

        //----------CONSTRUCTOR----------//
        public PayPalClient(string clientId, string secret, string mode)
        {
            ClientId = clientId;
            Secret = secret;
            Mode = mode;
        }

        //----------METHODS----------//


        #region GET ACCESS TOKEN

        private async Task<AuthResponse> Authenticate()
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{Secret}"));

            var content = new List<KeyValuePair<string, string>>
            {
                new ("grant_type","client_credentials")
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{BaseUrl}/v1/oauth2/token"),
                Method = HttpMethod.Post,
                Headers =
                {
                    {"Authorization", $"Basic {auth}" }
                },
                Content = new FormUrlEncodedContent(content)
            };

            var httpClient = new HttpClient();
            var httpResponse = await httpClient.SendAsync(request);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<AuthResponse>(jsonResponse);

            return response;
        }

        #endregion

        #region POST CREATE ORDER

        public async Task<CreateOrderResponse> CreateOrder(PurchaseUnit purchaseUnit, PaymentSource paymentSource)
        {
            var auth = await Authenticate();

            var request = new CreateOrderRequest
            {
                intent = "CAPTURE",
                purchase_units = new List<PurchaseUnit>
                {
                    purchaseUnit
                },
                payment_source = paymentSource

            };

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {auth.access_token}");

            // POST TO THE URI WITH DATA OBJECT. CONVERT TO JSON.

            var httpResponse = await httpClient.PostAsJsonAsync($"{BaseUrl}/v2/checkout/orders", request);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<CreateOrderResponse>(jsonResponse);

            return response;
        }

        #endregion

        #region POST CAPTURE ORDER

        public async Task<CaptureOrderResponse> CaptureOrder(string orderId)
        {
            var auth = await Authenticate();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {auth.access_token}");

            var httpContent = new StringContent("", Encoding.Default, "application/json");

            // SEND A REQUEST TO URI AND WAITING FOR RESPONSE IN JSON

            var httpResponse = await httpClient.PostAsync($"{BaseUrl}/v2/checkout/orders/{orderId}/capture", httpContent);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<CaptureOrderResponse>(jsonResponse);

            return response;
        }

        #endregion
    }
}