using System.CodeDom.Compiler;
using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using HotelConsole.Models;
using Newtonsoft.Json;

namespace HotelConsole.Controllers;

public class HotelApiController
{
    private readonly HttpClient _httpClient;
    private readonly Lazy<JsonSerializerSettings> _settings;

    public HotelApiController(string baseUrl, HttpClient httpClient)
    {
        BaseUrl = baseUrl;
        _httpClient = httpClient;
        _settings = new Lazy<JsonSerializerSettings>(CreateSerializerSettings);
    }

    public string BaseUrl { get; set; }

    public JsonSerializerSettings JsonSerializerSettings => _settings.Value;

    public bool ReadResponseAsString { get; set; }

    private JsonSerializerSettings CreateSerializerSettings()
    {
        var settings = new JsonSerializerSettings();
        return settings;
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<ICollection<Building>> BuildingsAllAsync()
    {
        return BuildingsAllAsync(CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<ICollection<Building>> BuildingsAllAsync(CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl.TrimEnd('/')).Append("/api/buildings");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    foreach (var item in response.Content.Headers)
                        headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<ICollection<Building>>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null!);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = await response.Content.ReadAsStringAsync(cancellationToken)
                            .ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null!);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Building> BuildingsPostAsync(Building body)
    {
        return BuildingsPostAsync(body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Building> BuildingsPostAsync(Building body, CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl.TrimEnd('/')).Append("/api/buildings");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    foreach (var item in response.Content.Headers)
                        headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Building>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Building> BuildingsGetAsync(int id)
    {
        return BuildingsGetAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Building> BuildingsGetAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/buildings/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Building>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task BuildingsPutAsync(int id, Building body)
    {
        return BuildingsPutAsync(id, body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task BuildingsPutAsync(int id, Building body, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/buildings/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("PUT");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task BuildingsDeleteAsync(int id)
    {
        return BuildingsDeleteAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task BuildingsDeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/buildings/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("DELETE");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<ICollection<Client>> ClientsAllAsync()
    {
        return ClientsAllAsync(CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<ICollection<Client>> ClientsAllAsync(CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/clients");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<ICollection<Client>>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Client> ClientsPostAsync(Client body)
    {
        return ClientsPostAsync(body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Client> ClientsPostAsync(Client body, CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/clients");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Client>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Client> ClientsGetAsync(int id)
    {
        return ClientsGetAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Client> ClientsGetAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/clients/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Client>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task ClientsPutAsync(int id, Client body)
    {
        return ClientsPutAsync(id, body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task ClientsPutAsync(int id, Client body, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/clients/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("PUT");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task ClientsDeleteAsync(int id)
    {
        return ClientsDeleteAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task ClientsDeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/clients/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("DELETE");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<ICollection<Parking>> ParkingsAllAsync()
    {
        return ParkingsAllAsync(CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<ICollection<Parking>> ParkingsAllAsync(CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/parkings");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<ICollection<Parking>>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Parking> ParkingsPostAsync(Parking body)
    {
        return ParkingsPostAsync(body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Parking> ParkingsPostAsync(Parking body, CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/parkings");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Parking>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Parking> ParkingsGetAsync(int id)
    {
        return ParkingsGetAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Parking> ParkingsGetAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/parkings/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Parking>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task ParkingsPutAsync(int id, Parking body)
    {
        return ParkingsPutAsync(id, body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task ParkingsPutAsync(int id, Parking body, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/parkings/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("PUT");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task ParkingsDeleteAsync(int id)
    {
        return ParkingsDeleteAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task ParkingsDeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/parkings/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("DELETE");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<ICollection<Reservation>> ReservationsAllAsync()
    {
        return ReservationsAllAsync(CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<ICollection<Reservation>> ReservationsAllAsync(CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/reservations");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<ICollection<Reservation>>(response, headers,
                                cancellationToken).ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Reservation> ReservationsPostAsync(Reservation body)
    {
        return ReservationsPostAsync(body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Reservation> ReservationsPostAsync(Reservation body, CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/reservations");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Reservation>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Reservation> ReservationsGetAsync(int id)
    {
        return ReservationsGetAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Reservation> ReservationsGetAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/reservations/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Reservation>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task ReservationsPutAsync(int id, Reservation body)
    {
        return ReservationsPutAsync(id, body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task ReservationsPutAsync(int id, Reservation body, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/reservations/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("PUT");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task ReservationsDeleteAsync(int id)
    {
        return ReservationsDeleteAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task ReservationsDeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/reservations/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("DELETE");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<ICollection<RoomKind>> RoomkindsAllAsync()
    {
        return RoomkindsAllAsync(CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<ICollection<RoomKind>> RoomkindsAllAsync(CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/roomkinds");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<ICollection<RoomKind>>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<RoomKind> RoomkindsPostAsync(RoomKind body)
    {
        return RoomkindsPostAsync(body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<RoomKind> RoomkindsPostAsync(RoomKind body, CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/roomkinds");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<RoomKind>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<RoomKind> RoomkindsGetAsync(int id)
    {
        return RoomkindsGetAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<RoomKind> RoomkindsGetAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/roomkinds/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<RoomKind>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task RoomkindsPutAsync(int id, RoomKind body)
    {
        return RoomkindsPutAsync(id, body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task RoomkindsPutAsync(int id, RoomKind body, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/roomkinds/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("PUT");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task RoomkindsDeleteAsync(int id)
    {
        return RoomkindsDeleteAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task RoomkindsDeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/roomkinds/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("DELETE");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<ICollection<Room>> RoomsAllAsync()
    {
        return RoomsAllAsync(CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<ICollection<Room>> RoomsAllAsync(CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/rooms");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<ICollection<Room>>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Room> RoomsPostAsync(Room body)
    {
        return RoomsPostAsync(body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Room> RoomsPostAsync(Room body, CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/rooms");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Room>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Room> RoomsGetAsync(int id)
    {
        return RoomsGetAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Room> RoomsGetAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/rooms/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Room>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task RoomsPutAsync(int id, Room body)
    {
        return RoomsPutAsync(id, body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task RoomsPutAsync(int id, Room body, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/rooms/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("PUT");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task RoomsDeleteAsync(int id)
    {
        return RoomsDeleteAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task RoomsDeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/rooms/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("DELETE");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<ICollection<Vehicle>> VehiclesAllAsync()
    {
        return VehiclesAllAsync(CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<ICollection<Vehicle>> VehiclesAllAsync(CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/vehicles");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<ICollection<Vehicle>>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Vehicle> VehiclesPostAsync(Vehicle body)
    {
        return VehiclesPostAsync(body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Vehicle> VehiclesPostAsync(Vehicle body, CancellationToken cancellationToken)
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/vehicles");

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Vehicle>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<Vehicle> VehiclesGetAsync(string id)
    {
        return VehiclesGetAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<Vehicle> VehiclesGetAsync(string id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/vehicles/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("text/plain"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse =
                            await ReadObjectResponseAsync<Vehicle>(response, headers, cancellationToken)
                                .ConfigureAwait(false);
                        if (objectResponse.Object == null)
                            throw new ApiException("Response was null which was not expected.", status,
                                objectResponse.Text, headers, null);
                        return objectResponse.Object;
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task VehiclesPutAsync(string id, Vehicle body)
    {
        return VehiclesPutAsync(id, body, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task VehiclesPutAsync(string id, Vehicle body, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/vehicles/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                var json = JsonConvert.SerializeObject(body, _settings.Value);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Content = content;
                request.Method = new HttpMethod("PUT");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task VehiclesDeleteAsync(string id)
    {
        return VehiclesDeleteAsync(id, CancellationToken.None);
    }

    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task VehiclesDeleteAsync(string id, CancellationToken cancellationToken)
    {
        if (id == null)
            throw new ArgumentNullException("id");

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/vehicles/{id}");
        urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

        var client = _httpClient;
        var disposeClient = false;
        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("DELETE");

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);


                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);
                var disposeResponse = true;
                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                        foreach (var item in response.Content.Headers)
                            headers[item.Key] = item.Value;

                    var status = (int) response.StatusCode;
                    if (status == 200)
                    {
                    }
                    else
                    {
                        var responseData = response.Content == null
                            ? null
                            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException(
                            "The HTTP status code of the response was not expected (" + status + ").", status,
                            responseData, headers, null);
                    }
                }
                finally
                {
                    if (disposeResponse)
                        response.Dispose();
                }
            }
        }
        finally
        {
            if (disposeClient)
                client.Dispose();
        }
    }

    private async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers, CancellationToken cancellationToken)
    {
        if (response == null || response.Content == null) return new ObjectResponseResult<T>(default, string.Empty);

        if (ReadResponseAsString)
        {
            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                var typedBody = JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                return new ObjectResponseResult<T>(typedBody, responseText);
            }
            catch (JsonException exception)
            {
                var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                throw new ApiException(message, (int) response.StatusCode, responseText, headers, exception);
            }
        }

        try
        {
            using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var streamReader = new StreamReader(responseStream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var serializer = JsonSerializer.Create(JsonSerializerSettings);
                var typedBody = serializer.Deserialize<T>(jsonTextReader);
                return new ObjectResponseResult<T>(typedBody, string.Empty);
            }
        }
        catch (JsonException exception)
        {
            var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
            throw new ApiException(message, (int) response.StatusCode, string.Empty, headers, exception);
        }
    }

    private string ConvertToString(object value, CultureInfo cultureInfo)
    {
        if (value == null) return "";

        if (value is Enum)
        {
            var name = Enum.GetName(value.GetType(), value);
            if (name != null)
            {
                var field = value.GetType().GetTypeInfo().GetDeclaredField(name);
                if (field != null)
                {
                    var attribute = field.GetCustomAttribute(typeof(EnumMemberAttribute))
                        as EnumMemberAttribute;
                    if (attribute != null) return attribute.Value != null ? attribute.Value : name;
                }

                var converted =
                    Convert.ToString(Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                return converted == null ? string.Empty : converted;
            }
        }
        else if (value is bool)
        {
            return Convert.ToString((bool) value, cultureInfo).ToLowerInvariant();
        }
        else if (value is byte[])
        {
            return Convert.ToBase64String((byte[]) value);
        }
        else if (value.GetType().IsArray)
        {
            var array = ((Array) value).OfType<object>();
            return string.Join(",", array.Select(o => ConvertToString(o, cultureInfo)));
        }

        var result = Convert.ToString(value, cultureInfo);
        return result == null ? "" : result;
    }

    private struct ObjectResponseResult<T>
    {
        public ObjectResponseResult(T responseObject, string responseText)
        {
            Object = responseObject;
            Text = responseText;
        }

        public T Object { get; }

        public string Text { get; }
    }
}

[GeneratedCode("NSwag", "13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v10.0.0.0))")]
public class ApiException : Exception
{
    public ApiException(string message, int statusCode, string response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception innerException)
        : base(
            message + "\n\nStatus: " + statusCode + "\nResponse: \n" + (response == null
                ? "(null)"
                : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
    {
        StatusCode = statusCode;
        Response = response;
        Headers = headers;
    }

    public int StatusCode { get; }

    public string Response { get; }

    public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; }

    public override string ToString()
    {
        return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
    }
}

[GeneratedCode("NSwag", "13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v10.0.0.0))")]
public class ApiException<TResult> : ApiException
{
    public ApiException(string message, int statusCode, string response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers, TResult result, Exception innerException)
        : base(message, statusCode, response, headers, innerException)
    {
        Result = result;
    }

    public TResult Result { get; }
}