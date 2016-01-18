using EliteReporter.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace EliteReporter.Utils
{
    public class EDAPI
    {
        private string agent = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_1 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12B411";
        private string baseUrl = "https://companion.orerve.net/";
        private string cookieFile = Directory.GetCurrentDirectory() + "\\edapi.cookies";
        private Regex cookieExpiresPattern = new Regex(@"expires=(.+?);");
        private CookieContainer cookieContainer;

        public EDAPI()
        {
            cookieContainer = new CookieContainer();
            if (File.Exists(cookieFile))
            {
                try
                {
                    using (Stream stream = File.Open(cookieFile, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        cookieContainer = (CookieContainer)formatter.Deserialize(stream);
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceInformation("Problem reading cookies from disk: " + e.GetType());
                    File.Delete(cookieFile);
                }
            }
        }

        private ResponseData makeRequest(string uri, Dictionary<string, string> values = null)
        {
            if (uri.Equals("/"))
                uri = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + uri);
            request.AllowAutoRedirect = false;
            request.UserAgent = agent;
            var cc = new CookieContainer();
            cc.Add(cookieContainer.GetCookies(new Uri(baseUrl)));
            request.CookieContainer = cookieContainer;
            if (values == null)
                request.Method = "GET";
            else
            {
                request.Method = "POST";
                string postData = "";

                foreach (string key in values.Keys)
                {
                    postData += HttpUtility.UrlEncode(key) + "="
                          + HttpUtility.UrlEncode(values[key]) + "&";
                }
                postData = postData.TrimEnd('&');
                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
            }
            
            using (var resp = (HttpWebResponse)request.GetResponse())
            {
                var responseText = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                if (resp.Headers["Set-Cookie"] != null)
                {
                    var cookieString = resp.Headers["Set-Cookie"];
                    var match = cookieExpiresPattern.Match(cookieString);
                    List<DateTime> expirationDates = new List<DateTime>();
                    do
                    {
                        if (match.Success)
                        {
                            var dateTime = DateTime.ParseExact(match.Groups[1].Value,
                                                               "ddd, dd-MMM-yyyy HH:mm:ss UTC", System.Globalization.CultureInfo.InvariantCulture);
                            cookieString = cookieString.Replace(match.Value, "");
                            expirationDates.Insert(0, dateTime);
                        }
                        match = match.NextMatch();
                    } while (match.Success);
                    cookieContainer.SetCookies(new Uri(baseUrl), cookieString);
                    for(int i = 0; i< expirationDates.Count; i++)
                        cookieContainer.GetCookies(new Uri(baseUrl))[cookieContainer.Count - i - 1].Expires = expirationDates[i];
                }
                if (!cookieContainer.GetCookies(new Uri(baseUrl)).Cast<Cookie>().SequenceEqual(request.CookieContainer.GetCookies(new Uri(baseUrl)).Cast<Cookie>()))
                {
                    //save cookies
                    using (Stream stream = File.Create(cookieFile))
                    {
                        try
                        {
                            Trace.TraceInformation("Writing cookies to disk... ");
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(stream, cookieContainer);
                            Trace.TraceInformation("Done.");
                        }
                        catch (Exception e)
                        {
                            Trace.TraceInformation("Problem writing cookies to disk: " + e.GetType());
                        }
                    }
                }
                if (resp.Headers["Location"] != null)
                    return makeRequest(resp.Headers["Location"], values);
                return new ResponseData()
                {
                    Text = responseText,
                    RedirectUrl = resp.ResponseUri.ToString()
                };
            }
        }

        public string getCookieFileName()
        {
            return cookieFile;
        }

        public bool isLoginRequired()
        {
            if (cookieContainer.Count == 0)
                return true;

            var response = makeRequest("");

            // redirect given? login required
            if (!string.IsNullOrEmpty(response.RedirectUrl) && response.RedirectUrl.EndsWith("user/login"))
                return true;

            return false;
        }

        public bool doLogin(string username, string password)
        {
            cookieContainer = new CookieContainer();
            var response = makeRequest("");

            var values = new Dictionary<string, string>();
            values["email"] = username;
            values["password"] = password;
            response = makeRequest("user/login", values);

            //# If we end up being redirected back to login, the login failed.
            if ( response.Text.Contains("Password") )
                throw new AuthenticationException("Login failed, invalid password?");

            // Check to see if we need to do the auth token dance.
            if (response.RedirectUrl.EndsWith("user/confirm"))
                throw new ConfirmationRequired("Confirmation code sent to the email provided.");

            return false;
        }

        public void confirm(string code)
        {
            var values = new Dictionary<string, string>();
            values["code"] = code;
            var resp = makeRequest("user/confirm", values);
            Thread.Sleep(2000);
        }

        public EDProfile getProfile()
        {
            var response = makeRequest("profile");
            try
            {  
                var jsonResp = JObject.Parse(response.Text);
                return new EDProfile()
                {
                    SystemName = jsonResp["lastSystem"]["name"].Value<string>(),
                    PortName = jsonResp["lastStarport"]["name"].Value<string>(),
                    CommanderName = jsonResp["commander"]["name"].Value<string>(),
                };
            } catch (Exception ex)
            {
                return null;
            }
        }

        private class ResponseData
        {
            public string Text { get; set; }
            public string RedirectUrl { get; set; }
        }

        public class AuthenticationException : Exception
        {
            public AuthenticationException(string message) : base(message)
            {
            }
        }

        public class ConfirmationRequired : Exception
        {
            public ConfirmationRequired(string message) : base(message)
            {
            }
        }

    }
}
