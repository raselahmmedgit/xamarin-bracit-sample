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

namespace hBuddyApp.Features.Polls
{
    public partial class PollsPage : ContentPage//, IInteractionChannelHost
    {
        //private readonly InteractionChannel _channel;

        private PollsViewModel _pollsViewModel;

        public PollsPage()
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

            var pollsViewModel = (BindingContext as PollsViewModel);
            _pollsViewModel = pollsViewModel;
            pollsViewModel?.FcmAndRapidProInit();
        }

        //private void OnWebViewNavigating(object sender, WebNavigatingEventArgs e)
        //{
        //    var ev = new UriOpeningConfig(e.Url);
        //    (BindingContext as PollsViewModel)?.UriOpeningHandler.OnUriOpen(ev);
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
        //            (BindingContext as PollsViewModel)?.SetErrorState();
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
                    var vm = BindingContext as PollsViewModel;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        vm.ShowScrollTap = false;
                        //vm.LastMessageVisible = true;
                        PollsList?.ScrollToFirst();
                    });

                }

            }
        }

        public void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            pollsInput.Unfocus();
        }

        private void Current_OnNotificationAction(object source, FirebasePushNotificationResponseEventArgs e)
        {
            try
            {
                Console.WriteLine("PollsPage - OnNotificationAction");

                if (e.Data != null)
                {
                    var rapidProFcmPushNotification = new RapidProFcmPushNotification();

                    foreach (var item in e.Data)
                    {
                        if (item.Key.Contains("title"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationAction: Data - title - {item.Value}");
                        }
                        else if (item.Key.Contains("body"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationAction: Data - body - {item.Value}");
                        }
                        else if (item.Key.Contains("type"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationAction: Data - type - {item.Value}");
                        }
                        else if (item.Key.Contains("message_id"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationAction: Data - message_id - {item.Value}");
                        }
                        else if (item.Key.Contains("message"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationAction: Data - message - {item.Value}");
                        }
                        else if (item.Key.Contains("quick_replies"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationAction: Data - quick_replies - {item.Value}");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"PollsPage - OnNotificationAction: Exception - {ex.Message.ToString()}");
            }
        }

        private void Current_OnNotificationOpened(object source, FirebasePushNotificationResponseEventArgs e)
        {
            try
            {
                Console.WriteLine("PollsPage - OnNotificationOpened");

                if (e.Data != null)
                {
                    var rapidProFcmPushNotification = new RapidProFcmPushNotification();

                    foreach (var item in e.Data)
                    {
                        if (item.Key.Contains("title"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationOpened: Data - title - {item.Value}");
                        }
                        else if (item.Key.Contains("body"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationOpened: Data - body - {item.Value}");
                        }
                        else if (item.Key.Contains("type"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationOpened: Data - type - {item.Value}");
                        }
                        else if (item.Key.Contains("message_id"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationOpened: Data - message_id - {item.Value}");
                        }
                        else if (item.Key.Contains("message"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationOpened: Data - message - {item.Value}");
                        }
                        else if (item.Key.Contains("quick_replies"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationOpened: Data - quick_replies - {item.Value}");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"PollsPage - OnNotificationOpened: Exception - {ex.Message.ToString()}");
            }
        }

        private void Current_OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        {
            try
            {
                Console.WriteLine("PollsPage - OnNotificationReceived");

                if (e.Data != null)
                {
                    var rapidProFcmPushNotification = new RapidProFcmPushNotification();

                    foreach (var item in e.Data)
                    {
                        if (item.Key.Contains("title"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationReceived: Data - title - {item.Value}");
                            rapidProFcmPushNotification.Title = item.Value.ToString();
                        }
                        else if (item.Key.Contains("body"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationReceived: Data - body - {item.Value}");
                            rapidProFcmPushNotification.Body = item.Value.ToString();
                        }
                        else if (item.Key.Contains("type"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationReceived: Data - type - {item.Value}");
                            rapidProFcmPushNotification.Type = item.Value.ToString();
                        }
                        else if (item.Key.Contains("message_id"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationReceived: Data - message_id - {item.Value}");
                            rapidProFcmPushNotification.MessageId = item.Value.ToString();
                        }
                        else if (item.Key.Contains("message"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationReceived: Data - message - {item.Value}");
                            rapidProFcmPushNotification.Message = item.Value.ToString();
                        }
                        else if (item.Key.Contains("quick_replies"))
                        {
                            Console.WriteLine($"PollsPage - OnNotificationReceived: Data - quick_replies - {item.Value}");
                            rapidProFcmPushNotification.QuickReplies = item.Value.ToString();
                        }
                    }

                    if (rapidProFcmPushNotification != null)
                    {
                        _pollsViewModel.InsertRapidProMessages(rapidProFcmPushNotification);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"PollsPage - OnNotificationReceived: Exception - {ex.Message.ToString()}");
            }
        }

        #endregion
    }
}
