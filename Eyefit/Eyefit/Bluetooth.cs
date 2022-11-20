using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eyefit
{
    public class Bluetooth
    {
        private string ble_data;
        private int time = 0;
        private int id = 0;
        public IBluetoothLE ble;
        public IAdapter adapter;
        public IDevice device;
        private ICharacteristic characteristic;
        public bool IsProcedStop { get; set; }
        public bool IsProcStart { get; set; }
        public bool IsDisconnect { get; set; } = false;
        public void FirstConnect(IBluetoothLE blecon, IAdapter adaptercon, IDevice devicecon)
        {
            ble = blecon;
            adapter = adaptercon;
            device = devicecon;

            StartUpdate();
        }
        public async void StartUpdate()
        {
            await Task.Delay(200);
            Device.BeginInvokeOnMainThread(async () =>
            {
                var service = await device.GetServiceAsync(Guid.Parse(""));
                characteristic = await service.GetCharacteristicAsync(Guid.Parse(""));

                characteristic.ValueUpdated += ValueUpdated_Change;

                await characteristic.StartUpdatesAsync();
            });
        }


        private void ValueUpdated_Change(object sender, CharacteristicUpdatedEventArgs e)
        {
            try
            {

                ble_data = Encoding.UTF8.GetString(e.Characteristic.Value);
                Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send(ble_data, "hi");
                });

                if (ble_data.Contains("started"))
                {
                    Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send(ble_data, "Start");
                    });
                }
                if (ble_data.Contains("finished"))
                {
                    Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send(ble_data, "Finesh");
                    });
                }
                if (ble_data.Contains("removed"))
                {
                    Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send(ble_data, "Remove");
                    });
                }
                if (ble_data.Contains("continue"))
                {
                    Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send(ble_data, "Continue");
                    });
                }
                if (ble_data.Contains("Program"))
                {
                    Device.BeginInvokeOnMainThread(() => {
                        MessagingCenter.Send(ble_data, "Program");
                    });
                }
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() => {

                    var properties = new Dictionary<string, string> {
                                            { "Page", "Меню" },
                                            { "State", device.State.ToString() }
                                            };
                    //Crashes.TrackError(ex, properties);
                });

            }
        }

        public async Task<bool> CheckStart(string proc)
        {
            ble_data = null;
            WriteBLE(proc);
            await Task.Delay(700);

            if (ble_data != null)
            {
                ble_data = null;
                return true;
            }
            else
            {
                return false;
            }

        }

        public string GetValue()
        {
           
            Device.BeginInvokeOnMainThread(async () => {
                try
                {
                    var service = await device.GetServiceAsync(Guid.Parse(""));
                    var characteristic = await service.GetCharacteristicAsync(Guid.Parse(""));
                    if(characteristic.CanRead)
                    {
                        var bytes = await characteristic.ReadAsync();
                            ble_data = Encoding.UTF8.GetString(bytes);
                    }
                    else
                    {
                       
                    }
                    Application.Current.MainPage.DisplayAlert(" ", characteristic.CanRead.ToString(), "ok");
                }
                catch(Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert(" ", ex.ToString(), "ok");
                }
           
            });
          return ble_data;
        }

        public async Task<bool> MenuOrNext()
        {
            await Task.Delay(700);
            WriteBLE("s909 0 0 2p");
            await Task.Delay(1000);
            try
            {
                if (ble_data != null)
                {
                    string[] stroki = ble_data.Split('\n');
                    string[] laststr = stroki[stroki.Length - 2].Split(',');
                    string[] timestr = stroki[stroki.Length - 3].Split(',');
                    time = Convert.ToInt32(timestr[0]);
                    id = Convert.ToInt32(laststr[0]);
                }
            }
            catch
            {
                return false;
            }

            if (time > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> GetTok()
        {
            ble_data = null;
            WriteBLE("s900 1 1p");
            await Task.Delay(1500);
            try
            {
                string[] stroki = ble_data.Split('\n');
                string laststr = stroki[stroki.Length - 2];
                return Convert.ToInt32(laststr);
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() => {
                    var properties = new Dictionary<string, string> {
                                            { "Page", "подключение девайса" },
                                            { "State", device.State.ToString() }
                                            };
                    //Crashes.TrackError(ex, properties);
                });
                return 1;
            }
        }

        public int GetTime()
        {
            return time;
        }
        public int GetId()
        {
            return id;
        }
        public DeviceState GetState()
        {
            return device.State;
        }
        public void Disconnect()
        {
            adapter.DisconnectDeviceAsync(device);
            characteristic.ValueUpdated += ValueUpdated_Change;
            characteristic.StopUpdatesAsync();
        }

        public async Task<string> GetGraph()
        {
            ble_data = null;
            WriteBLE("s910 0 1 0p");
            await Task.Delay(1000);
            return ble_data;
        }

        public async void StopUpdate()
        {
            await characteristic.StopUpdatesAsync();
        }
        public void StartChracteristicUpdate()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await characteristic.StopUpdatesAsync();

                await Task.Delay(150);
                var service = await device.GetServiceAsync(Guid.Parse(""));
                characteristic = await service.GetCharacteristicAsync(Guid.Parse(""));
                await characteristic.StartUpdatesAsync();
            });
        }

        public async Task<bool> CheckProced()
        {

            int time = 0;
            try
            {
                await Task.Delay(2500);
                ble_data = null;
           
                await Task.Delay(1500);
                if (ble_data != null)
                {
                    string[] stroki = ble_data.Split('\n');
                    string[] laststr = stroki[stroki.Length - 2].Split(',');
                    string[] timestr = stroki[stroki.Length - 3].Split(',');
                    time = Convert.ToInt32(timestr[0]);

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert(time.ToString(), ble_data, "ok");
                    });
                    if (time <= 0)
                    {
                        IsProcedStop = true;
                        return true;
                    }
                    else
                    {
                        IsProcedStop = false;
                        return false;
                    }
                }
                else
                {
               
                    IsProcedStop = true;
                    return true;
                }

            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert(" ", ex.ToString(), "ok");
                });
                IsProcedStop = true;
                return true;
            }

         

        }

        public void LostConnect()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await adapter.DisconnectDeviceAsync(device);
                await Task.Delay(400);
                await adapter.ConnectToDeviceAsync(device);
                if (device.State != DeviceState.Connected)
                {
                    await characteristic.StopUpdatesAsync();
                    characteristic.ValueUpdated -= ValueUpdated_Change;
                }
            });

        }

        public async void WriteBLE(string write)
        {

            try
            {
                var service = await device.GetServiceAsync(Guid.Parse(""));
                var characteristic = await service.GetCharacteristicAsync(Guid.Parse(""));
                try
                {
                    if (characteristic.CanWrite == true)
                    {
                        byte[] byt = Encoding.UTF8.GetBytes(write);
                        await characteristic.WriteAsync(byt);
                    }
                }
                catch (Exception ex)
                {
                    var properties = new Dictionary<string, string> {
                                            { "Page", "Меню" },
                                            { "State", device.State.ToString() }
                                            };
                    //Crashes.TrackError(ex, properties);
                    await Application.Current.MainPage.DisplayAlert(" ", ex.ToString(), "ok");
                }
            }
            catch (Exception ee)
            {
                await Application.Current.MainPage.DisplayAlert(" ", ee.ToString(), "ok");
            }
        }
    }
}
