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
                //��ȡWIFI ������
                WifiAdapter wifi = WifiAdapter.FindAllAdapters()[0];

                // ��������ı��¼�
                wifi.AvailableNetworksChanged += Wifi_AvailableNetworksChanged;

                // ѭ��ɨ�� WIFI�б�
                while (true)
                {
                    Debug.WriteLine("��ʼɨ��WIFI");
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
        /// ɨ����ɱ���
        /// </summary>
        private static void Wifi_AvailableNetworksChanged(WifiAdapter sender, object e)
        {
            Debug.WriteLine("��ȡWIFI��Ч��Ϣ");

            WifiNetworkReport report = sender.NetworkReport;

            foreach (WifiAvailableNetwork net in report.AvailableNetworks)
            {
                Debug.WriteLine($"WIFI ����(SSID):{net.Ssid},  MAC��ַ��BSSID�� : {net.Bsid},  �ź�ǿ��(rssi) : {net.NetworkRssiInDecibelMilliwatts.ToString()},  �ź�ǿ��(signal) : {net.SignalBars.ToString()}");
            }
        }
    }
}
