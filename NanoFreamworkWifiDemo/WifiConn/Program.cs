using nanoFramework.Networking;
using System;
using System.Diagnostics;
using System.Threading;

namespace WifiConn
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
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message}");
            }

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
