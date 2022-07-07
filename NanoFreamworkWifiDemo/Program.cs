using nanoFramework.Networking;
using System;
using System.Device.Wifi;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace WifiRequest
{
    public class Program
    {
        const string MYSSID = "Myhome";
        const string MYPASSWORD = "1213141516";
        public static void Main()
        {
            try
            {
                var success = WifiNetworkHelper.ConnectDhcp(MYSSID, MYPASSWORD, requiresDateTime: true, token: new CancellationTokenSource(60000).Token);
                if (success)
                {
                    Debug.WriteLine($"连接状态 {success}");
                    Debug.WriteLine($"我们获取到的最新时间: {DateTime.UtcNow.AddHours(8)}");
                }
                else
                {
                    Debug.WriteLine($"发生了异常");
                }
                X509Certificate rootCACert = new X509Certificate(Resource.GetBytes(Resource.BinaryResources.digicertglobalrootca));
                //请求地址，并获取信息
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://kesshei.github.io/esp32.html");
                httpWebRequest.Method = "GET";
                httpWebRequest.SslProtocols = System.Net.Security.SslProtocols.Tls12;
                httpWebRequest.HttpsAuthentCert = rootCACert;
                using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream());
                    var data = sr.ReadToEnd();

                    Debug.WriteLine(">>>>>>>>>>>>>");
                    Debug.WriteLine("获取请求完毕");
                    Debug.WriteLine($"获取到： {data}  数据长度:{data.Length}");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message}");
            }

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
