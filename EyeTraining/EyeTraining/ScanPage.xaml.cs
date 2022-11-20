using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eyefit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        public List<Tests> test = new List<Tests>();
        public IBluetoothLE ble;
        public IAdapter adapter;
        public ObservableCollection<IDevice> devices;
        public IDevice device;
        public Bluetooth bluetooth = new Bluetooth();
        public ScanPage()
        {
            InitializeComponent();
            //test = new List<Tests>
            //{
            //    new Tests {Title="Eyesfit 1140", Image="eyeglasses.png", Loud=false },
            //    new Tests {Title="Eyesfit 1141", Image="eyeglasses.png", Loud=false },
            //    new Tests {Title="Eyesfit 1142", Image="eyeglasses.png", Loud=false },
            //    new Tests {Title="Eyesfit 1143", Image="eyeglasses.png", Loud=false }
            //};
            //List.ItemsSource = test;

            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            devices = new ObservableCollection<IDevice>();
            //devices.Clear();
            //test.Clear();
            Scan();
            List.RefreshCommand = new Command(() =>
            {
                devices.Clear();
                test.Clear();
                Scan();
            });
        }

        public async void Scan()
        {
            if (ble.State == BluetoothState.Off)
            {
                await DisplayAlert("", "Включите Bluetooth", "OK");
                
            }
            else
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        var systemDevices = adapter.GetSystemConnectedOrPairedDevices(new[] { Guid.Parse("00001800-0000-1000-8000-00805F9B34FB"), Guid.Parse("00001801-0000-1000-8000-00805F9B34FB") });
                        //foreach (var device in systemDevices)
                        //{
                        //    if (device.Name != null)
                        //    {
                        //        if (device.Name.Contains("Eyesfit"))
                        //        {
                        //            devices.Add(device);
                        //        }
                        //    }
                        //}


                        //if (devices != null)
                        //{
                        //    List.ItemsSource = devices;
                        //}
                        adapter.ScanTimeout = 10000;
                        adapter.ScanMode = ScanMode.Balanced;
                        adapter.DeviceDiscovered += (obj, a) =>
                        {
                            if (!devices.Contains(a.Device))
                            {
                                if ((a.Device.Name != null) && (a.Device.Name.Contains("Eyesfit")))
                                {
                                    if (!devices.Contains(a.Device))
                                    {
                                        devices.Add(a.Device);
                                        test.Add(new Tests { Title = a.Device.Name, Image = "eyeglasses.png", Loud = false });
                                        List.ItemsSource = test;
                                    }
                                }

                            }

                        };
                        await adapter.StartScanningForDevicesAsync();
                        List.ItemsSource = devices;
                        break;

                    case Device.Android:
                        adapter.ScanTimeout = 10000;
                        adapter.ScanMode = ScanMode.LowPower;
                        adapter.DeviceDiscovered += (obj, a) =>
                        {
                            if (!devices.Contains(a.Device))
                            {
                                if (a.Device.Name != null)
                                {
                                    if (a.Device.Name.Contains("Eyesfit"))
                                    {
                                        if (!devices.Contains(a.Device))
                                        {
                                            devices.Add(a.Device);
                                            test.Add(new Tests { Title = a.Device.Name , Image = "eyeglasses.png", Loud = false });
                                            List.ItemsSource = test;
                                        }

                                    }
                                }

                            }

                        };
                        await adapter.StartScanningForDevicesAsync();
                        List.ItemsSource = devices;
                        break;
                    default: break;
                }

            }
            await Task.Delay(10000);
            await adapter.StopScanningForDevicesAsync();
            List.EndRefresh();
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            List.EndRefresh();
            await adapter.StopScanningForDevicesAsync();
            foreach (Tests s in test)
            {
                s.Loud = false;
            }
            var index = test.IndexOf((List.SelectedItem as Tests));
            List.ItemsSource = null;
            test[index].Loud = true;
            List.ItemsSource = test;

            IDevice dev = devices.FirstOrDefault(x => x.Name == test[index].Title);
            await adapter.ConnectToDeviceAsync(dev);
            await Task.Delay(1500);
            if (dev.State == DeviceState.Connected)
            {   
                await Task.Delay(500);
                bluetooth = new Bluetooth();
                bluetooth.FirstConnect(ble, adapter, dev);
                bluetooth.IsDisconnect = false;
                await Navigation.PushAsync(new ProfilePage(bluetooth));
                devices.Clear();
                test.Clear();
                List.ItemsSource = null;
            }


            //List.ItemsSource = test;
            //await Task.Delay(3000);
            //foreach (Tests s in test)
            //{
            //    s.Loud = false;
            //}
            //List.ItemsSource = null;
            //List.ItemsSource = test;
            //await Navigation.PushAsync(new ProfilePage());
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if(bluetooth.adapter!=null)
            {
                bluetooth.Disconnect();
                bluetooth.IsDisconnect = true;
            }
        }
    }
}