// =========================================================================
// Copyright 2020 EPAM Systems, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// =========================================================================

using hBuddyApp.Features.RapidProFcmPushNotifications;
using Plugin.FirebasePushNotification;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace hBuddyApp.Features.Rumours
{
    public partial class RumoursPage : ContentPage//, IInteractionChannelHost
    {
        //private readonly InteractionChannel _channel;

        private RumoursViewModel _rumoursViewModel;

        public RumoursPage()
        {
            InitializeComponent();

            //_channel = new InteractionChannel(this);

            //hybridWebView.React = s => _channel.HandleMessage(s);
            //hybridWebView.Navigating += OnWebViewNavigating;
            //hybridWebView.Navigated += OnWebViewNavigated;

            if (Device.RuntimePlatform == Device.Android)
            {
                CrossFirebasePushNotification.Current.OnNotificationAction += Current_OnNotificationAction;
                CrossFirebasePushNotification.Current.OnNotificationOpened += Current_OnNotificationOpened;
                CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                CrossFirebasePushNotification.Current.OnNotificationAction += Current_OnNotificationAction;
                CrossFirebasePushNotification.Current.OnNotificationOpened += Current_OnNotificationOpened;
                CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            //_channel.SetHandler(BindingContext as IInteractionChannelHandler);

            var rumoursViewModel = (BindingContext as RumoursViewModel);
            _rumoursViewModel = rumoursViewModel;
            rumoursViewModel?.FcmAndRapidProInit();
        }

        //private void OnWebViewNavigating(object sender, WebNavigatingEventArgs e)
        //{
        //    var ev = new UriOpeningConfig(e.Url);
        //    (BindingContext as RumoursViewModel)?.UriOpeningHandler.OnUriOpen(ev);
        //    e.Cancel = ev.Cancel;
        //}

        //private void OnWebViewNavigated(object sender, WebNavigatedEventArgs e)
        //{
        //    switch (e.Result)
        //    {
        //        case WebNavigationResult.Success:
        //            (BindingContext as IInteractionChannelHandler)?.ApiReadyMessage();
        //            break;
        //        case WebNavigationResult.Failure:
        //        case WebNavigationResult.Timeout:
        //            (BindingContext as RumoursViewModel)?.SetErrorState();
        //            break;
        //    }
        //}

        public async Task<bool> SendMessageAsync(string message)
        {
            //if (Xamarin.Essentials.MainThread.IsMainThread)
            //{
            //    return await hybridWebView.SendMessageAsync(message).ConfigureAwait(false);
            //}
            //else
            //{
            //    return await Xamarin.Essentials.MainThread.InvokeOnMainThreadAsync(async () => await hybridWebView.SendMessageAsync(message)).ConfigureAwait(false);
            //}

            return true;
        }

        #region RapidPro Fcm Push Notification

        public void ScrollTap(object sender, System.EventArgs e)
        {
            lock (new object())
            {
                if (BindingContext != null)
                {
                    var vm = BindingContext as RumoursViewModel;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        vm.ShowScrollTap = false;
                        //vm.LastMessageVisible = true;
                        RumoursList?.ScrollToFirst();
                    });

                }

            }
        }

        public void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            rumoursInput.Unfocus();
        }

        private void Current_OnNotificationAction(object source, FirebasePushNotificationResponseEventArgs e)
        {
            try
            {
                Console.WriteLine("RumoursPage - OnNotificationAction");

                if (e.Data != null)
                {
                    var rapidProFcmPushNotification = new RapidProFcmPushNotification();

                    foreach (var item in e.Data)
                    {
                        if (item.Key.Contains("title"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationAction: Data - title - {item.Value}");
                        }
                        else if (item.Key.Contains("body"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationAction: Data - body - {item.Value}");
                        }
                        else if (item.Key.Contains("type"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationAction: Data - type - {item.Value}");
                        }
                        else if (item.Key.Contains("message_id"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationAction: Data - message_id - {item.Value}");
                        }
                        else if (item.Key.Contains("message"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationAction: Data - message - {item.Value}");
                        }
                        else if (item.Key.Contains("quick_replies"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationAction: Data - quick_replies - {item.Value}");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"RumoursPage - OnNotificationAction: Exception - {ex.Message.ToString()}");
            }
        }

        private void Current_OnNotificationOpened(object source, FirebasePushNotificationResponseEventArgs e)
        {
            try
            {
                Console.WriteLine("RumoursPage - OnNotificationOpened");

                if (e.Data != null)
                {
                    var rapidProFcmPushNotification = new RapidProFcmPushNotification();

                    foreach (var item in e.Data)
                    {
                        if (item.Key.Contains("title"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationOpened: Data - title - {item.Value}");
                        }
                        else if (item.Key.Contains("body"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationOpened: Data - body - {item.Value}");
                        }
                        else if (item.Key.Contains("type"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationOpened: Data - type - {item.Value}");
                        }
                        else if (item.Key.Contains("message_id"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationOpened: Data - message_id - {item.Value}");
                        }
                        else if (item.Key.Contains("message"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationOpened: Data - message - {item.Value}");
                        }
                        else if (item.Key.Contains("quick_replies"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationOpened: Data - quick_replies - {item.Value}");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"RumoursPage - OnNotificationOpened: Exception - {ex.Message.ToString()}");
            }
        }

        private void Current_OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        {
            try
            {
                Console.WriteLine("RumoursPage - OnNotificationReceived");

                if (e.Data != null)
                {
                    var rapidProFcmPushNotification = new RapidProFcmPushNotification();

                    foreach (var item in e.Data)
                    {
                        if (item.Key.Contains("title"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationReceived: Data - title - {item.Value}");
                            rapidProFcmPushNotification.Title = item.Value.ToString();
                        }
                        else if (item.Key.Contains("body"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationReceived: Data - body - {item.Value}");
                            rapidProFcmPushNotification.Body = item.Value.ToString();
                        }
                        else if (item.Key.Contains("type"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationReceived: Data - type - {item.Value}");
                            rapidProFcmPushNotification.Type = item.Value.ToString();
                        }
                        else if (item.Key.Contains("message_id"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationReceived: Data - message_id - {item.Value}");
                            rapidProFcmPushNotification.MessageId = item.Value.ToString();
                        }
                        else if (item.Key.Contains("message"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationReceived: Data - message - {item.Value}");
                            rapidProFcmPushNotification.Message = item.Value.ToString();
                        }
                        else if (item.Key.Contains("quick_replies"))
                        {
                            Console.WriteLine($"RumoursPage - OnNotificationReceived: Data - quick_replies - {item.Value}");
                            rapidProFcmPushNotification.QuickReplies = item.Value.ToString();
                        }
                    }

                    if (rapidProFcmPushNotification != null)
                    {
                        _rumoursViewModel.InsertRapidProMessages(rapidProFcmPushNotification);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"RumoursPage - OnNotificationReceived: Exception - {ex.Message.ToString()}");
            }
        }

        #endregion
    }
}
