﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace HUA.PCAAlephoo.Business.Helper
{
    public static class HttpClientEx
    {
        public const string MimeJson = "application/json";

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(client.BaseAddress + requestUri),
                Content = content,
            };

            return client.SendAsync(request);
        }

        public static Task<HttpResponseMessage> PostJsonAsync(this HttpClient client, string requestUri, Type type, object value)
        {
            return client.PostAsync(requestUri, new ObjectContent(type, value, new JsonMediaTypeFormatter(), MimeJson));
        }

        public static Task<HttpResponseMessage> PutJsonAsync(this HttpClient client, string requestUri, Type type, object value)
        {
            return client.PutAsync(requestUri, new ObjectContent(type, value, new JsonMediaTypeFormatter(), MimeJson));
        }

        public static Task<HttpResponseMessage> PatchJsonAsync(this HttpClient client, string requestUri, Type type, object value)
        {
            return client.PatchAsync(requestUri, new ObjectContent(type, value, new JsonMediaTypeFormatter(), MimeJson));
        }

    }
}
