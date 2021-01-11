using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShiteLauncher
{
    public static class Oracle
    {
        public static string OracleHostURL = "";

        // Try a login with the email and password, returns session id and account_id on success
        // Error message on login fail
        public static (LoginSession LoginData, Error Error) Login(string email, string password)
        {
            var url = $"{OracleHostURL}/WebAccounts/CreateLoginSession";
            var login = new
            {
                email,
                password
            };
            var loginStr = JsonConvert.SerializeObject(login);

            using (var wc = new WebClient())
            {
                try
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var resultStr = wc.UploadString(url, "POST", loginStr);
                    var loginData = JsonConvert.DeserializeObject<LoginSession>(resultStr);
                    return (loginData, null);
                }
                catch (WebException e)
                {
                    var rStream = e.Response?.GetResponseStream();
                    if (rStream != null)
                    {
                        using (var r = new StreamReader(rStream))
                        {
                            var responce = r.ReadToEnd();
                            var err = JsonConvert.DeserializeObject<Error>(responce);
                            return (null, err);
                        }
                    }

                    return (null, new Error() { code = "ERR_UNKNWON" });
                }
            }
        }

        public class LoginSession
        {
            public string session_id { get; set; }
            public ulong account_id { get; set; }
        }

        public class Error
        {
            public class ErrorData
            {
                public bool silent { get; set; } = false;
            }

            public string code { get; set; }
            public string message { get; set; }
            public ErrorData err_data { get; set; } = new ErrorData();
        }
    }
}
