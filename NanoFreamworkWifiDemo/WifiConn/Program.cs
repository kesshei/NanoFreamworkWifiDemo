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
                    Debug.WriteLine($"����״̬ {success}");
                    Debug.WriteLine($"���ǻ�ȡ��������ʱ��: {DateTime.UtcNow.AddHours(8)}");
                }
                else
                {
                    Debug.WriteLine($"�������쳣");
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
