using nanoFramework.Networking;
using System;
using System.Device.Wifi;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;

namespace WifiScan
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                //获取WIFI 适配器
                WifiAdapter wifi = WifiAdapter.FindAllAdapters()[0];

                // 设置网络改变事件
                wifi.AvailableNetworksChanged += Wifi_AvailableNetworksChanged;

                // 循环扫描 WIFI列表
                while (true)
                {
                    Debug.WriteLine("开始扫描WIFI");
                    wifi.ScanAsync();

                    Thread.Sleep(30000);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("message:" + ex.Message);
                Debug.WriteLine("stack:" + ex.StackTrace);
            }

            Thread.Sleep(Timeout.Infinite);
        }

        /// <summary>
        /// 扫描完成遍历
        /// </summary>
        private static void Wifi_AvailableNetworksChanged(WifiAdapter sender, object e)
        {
            Debug.WriteLine("获取WIFI有效信息");

            WifiNetworkReport report = sender.NetworkReport;

            foreach (WifiAvailableNetwork net in report.AvailableNetworks)
            {
                Debug.WriteLine($"WIFI 名称(SSID):{net.Ssid},  MAC地址（BSSID） : {net.Bsid},  信号强度(rssi) : {net.NetworkRssiInDecibelMilliwatts.ToString()},  信号强度(signal) : {net.SignalBars.ToString()}");
            }
        }
    }
}
