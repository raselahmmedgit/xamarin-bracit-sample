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

using hBuddyApp.Features.Analytics;
using hBuddyApp.Features.ChangeCountryProgram.Routes;
using hBuddyApp.Features.Components.InfoView;
using hBuddyApp.Features.RapidProFcmPushNotifications;
using hBuddyApp.Features.RapidProFcmPushNotifications.Services;
//using hBuddyApp.Features.UserProfile.Services;
using hBuddyApp.Services.Navigation;
using hBuddyApp.Services.Notification;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Plugin.FirebasePushNotification;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace hBuddyApp.Features.Polls
{
    public class PollsViewModel : ViewModelBase//, IInteractionChannelHandler
    {
        //private const string LogoImageSource = "resource://hBuddyApp.Features.Polls.Resources.Images.poll.svg";
        private const string ErrorImgSource = "connection_problem.svg";
        private const string InitPhrase = "polls";

        private readonly ILogger _logger;
        //private readonly IMessagesProcessor _messagesProcessor;
        //private readonly IMessageInteractor _messageInteractor;
        //private readonly IUserAccountContainer _userAccountContainer;
        //private readonly IAccountInformationContainer _accountContainer;
        //private readonly IMediator _mediator;
        private readonly INotificationManager _notificationManager;
        private readonly INavigationServiceDelegate _navigationServiceDelegate;
        private readonly IChangeCountryProgramRoute _changeCountryProgramRoute;

        //public bool ShowRestricted { get; private set; }
        //public bool ShowPolls { get; private set; }

        //public string PollsLogoImageSource => LogoImageSource;

        //public IReadOnlyList<string> PollsInformationItemsList { get; set; }
        //public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> CreateAccountCommand { get; private set; }
        //public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> LogInCommand { get; private set; }

        //public string Uri { get; }
        //public string InputText { get; set; }
        public bool IsInputVisible { get; private set; } = true;
        //public Interaction<Message, bool> SendMessageInteraction { get; }
        //public ICommand SendCommand { get; }
        //public ICommand HandleMessageCommand { get; }
        public IInfoViewModel InfoViewModel { get; private set; }
        //public IUriOpeningHandler UriOpeningHandler { get; }

        //private bool IsOpenedAtLeastOnce { get; set; }
        //private bool IsApiReady { get; set; }
        //private bool IsInitMessageSent { get; set; }


        private readonly RapidProContainer _rapidProContainer;
        private readonly RapidProService _rapidProService;
        private readonly FirebaseContainer _firebaseContainer;

        private readonly RapidProDatabase _rapidProDatabase;

        public string RapidProInitPhrase = "riseup";

        public bool ShowScrollTap { get; set; } = false;
        //public bool LastMessageVisible { get; set; } = true;


        public ObservableCollection<RapidProMessage> RapidProMessages { get; set; } = new ObservableCollection<RapidProMessage>();
        public string InputText { get; set; }
        public string RapidProMessageId { get; set; }
        public string ActionInputText { get; set; }
        //public UserAccountInfo _userAccountInfo { get; set; }


        public ICommand OnSendCommand { get; set; }
        public ICommand OnActionSendCommand { get; set; }
        public ICommand RapidProMessageAppearingCommand { get; set; }
        public ICommand RapidProMessageDisappearingCommand { get; set; }

        //public PollsViewModel(
        //    ISerializer serializer,
        //    ILoggerFactory loggerFactory,
        //    IMediator mediator,
        //    IMessagesProcessor messagesProcessor,
        //    IAccountInformationContainer accountContainer,
        //    IUserAccountContainer userAccountContainer)
        //{
        //    _mediator = mediator;
        //    _messagesProcessor = messagesProcessor;
        //    _logger = loggerFactory.CreateLogger<PollsViewModel>();
        //    _accountContainer = accountContainer;

        //    Uri = Configuration.Constants.HybridConstants.HybridEndpointUrl;
        //    UriOpeningHandler = new HybridWebViewUriOpeningHandler(Uri);
        //    SendMessageInteraction = new Interaction<Message, bool>();
        //    _messageInteractor = new MessageInteractor(loggerFactory, serializer, userAccountContainer, SendMessageInteraction);

        //    _messagesProcessor.AddHandler(new InputMessageHandler(b => IsInputVisible = b));
        //    _messagesProcessor.AddHandler(new WebSessionConnectionHandler(loggerFactory, isConnected => IsBusy = !isConnected));

        //    HandleMessageCommand = ReactiveCommand.CreateFromTask<Message>(HandleMessageAsync);
        //    SendCommand = ReactiveCommand.CreateFromTask(SendAsync);

        //    IsBusy = true;
        //}

        public PollsViewModel(
            ILoggerFactory loggerFactory,
            //IUserAccountContainer userAccountContainer,
            INotificationManager notificationManager,
            IChangeCountryProgramRoute changeCountryProgramRoute,
            INavigationServiceDelegate navigationServiceDelegate)
        {
            _logger = loggerFactory.CreateLogger<PollsViewModel>();
            _navigationServiceDelegate = navigationServiceDelegate;
            _notificationManager = notificationManager;
            _changeCountryProgramRoute = changeCountryProgramRoute;

            //Uri = Configuration.Constants.HybridConstants.HybridEndpointUrl;
            //UriOpeningHandler = new HybridWebViewUriOpeningHandler(Uri);
            //SendMessageInteraction = new Interaction<Message, bool>();
            //_messageInteractor = new MessageInteractor(loggerFactory, serializer, userAccountContainer, SendMessageInteraction);

            //_messagesProcessor.AddHandler(new InputMessageHandler(b => IsInputVisible = b));
            //_messagesProcessor.AddHandler(new WebSessionConnectionHandler(loggerFactory, isConnected => IsBusy = !isConnected));

            //HandleMessageCommand = ReactiveCommand.CreateFromTask<Message>(HandleMessageAsync);
            //SendCommand = ReactiveCommand.CreateFromTask(SendAsync);

            //_userAccountContainer = userAccountContainer;

            IsBusy = true;


            RapidProMessageAppearingCommand = new Command<RapidProMessage>(OnRapidProMessageAppearing);
            RapidProMessageDisappearingCommand = new Command<RapidProMessage>(OnRapidProMessageDisappearing);

            OnSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(InputText))
                {
                    SendMessageAsync(InputText);
                    InputText = string.Empty;
                }

            });

            OnActionSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(ActionInputText))
                {
                    SendMessageAsync(ActionInputText);
                    ActionInputText = string.Empty;
                }

                if (!string.IsNullOrEmpty(RapidProMessageId))
                {
                    RemovePollsRapidProMessageQuickReplie(RapidProMessageId);
                }
            });

            NavigateBackCommand = ReactiveCommand.CreateFromTask(NavigateBackAsync);

            _rapidProContainer = new RapidProContainer();
            _rapidProService = new RapidProService();

            _firebaseContainer = new FirebaseContainer();

            _rapidProDatabase = new RapidProDatabase();
        }

        public ICommand NavigateBackCommand { get; }

        public async void FcmAndRapidProInit()
        {
            try
            {
                if (string.IsNullOrEmpty(_firebaseContainer.FirebaseChannelHost) && string.IsNullOrEmpty(_firebaseContainer.FirebaseChannelId))
                {
                    var title = Resources.Localization.Polls_Dialog_TitleText;
                    var description = Resources.Localization.Polls_Dialog_DescriptionText;

                    var result = await _notificationManager.ShowNotificationAsync(
                            title,
                            description,
                            Resources.Localization.Polls_Dialog_AcceptBtn);

                    if (result)
                    {
                        IsBusy = false;
                        await _changeCountryProgramRoute.ExecuteAsync(_navigationServiceDelegate).ConfigureAwait(false);
                    }
                }
                else
                {
                    InitializeFcmAndRapidPro();
                    //OnInitialized();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PollsViewModel - FcmAndRapidProInit: Exception - {ex.Message.ToString()}");
            }
        }

        public async void ApiReadyMessage()
        {
            //IsApiReady = true;
            //await InitializeIfNeededAsync();
        }

        public override void OnActivated(CompositeDisposable lifecycleDisposable)
        {
            base.OnActivated(lifecycleDisposable);

            AnalyticsProvider.Instance.LogViewModel(nameof(PollsViewModel));

            //_accountContainer.Changes.Subscribe(HandleAccountChanged).DisposeWith(lifecycleDisposable);
            //IsOpenedAtLeastOnce = true;

            //await InitializeIfNeededAsync();
        }

        //private async Task InitializeIfNeededAsync()
        //{
        //    if (!IsOpenedAtLeastOnce)
        //    {
        //        return;
        //    }

        //    if (IsApiReady && !IsInitMessageSent)
        //    {
        //        await _messageInteractor.SendInit(InitPhrase);
        //        OnInitialized();
        //        IsInitMessageSent = true;
        //    }
        //}

        //private void HandleAccountChanged(AccountInformation obj)
        //{
        //    if (obj != null && obj.Roles.Count > 0)
        //    {
        //        // Polls page
        //        ShowRestricted = false;
        //        ShowPolls = true;
        //    }
        //    else
        //    {
        //        // Polls restricted
        //        InitRestricted();

        //        ShowRestricted = true;
        //        ShowPolls = false;

        //        IsBusy = false;
        //    }
        //}

        //private void InitRestricted()
        //{
        //    PollsInformationItemsList = new List<string>()
        //    {
        //        Resources.Localization.Polls_InfoItem1_Text,
        //        Resources.Localization.Polls_InfoItem2_Text
        //    };

        //    CreateAccountCommand = ReactiveCommand.CreateFromTask(HandleCreateAccountAsync);
        //    LogInCommand = ReactiveCommand.CreateFromTask(HandleLogInAsync);
        //}

        //private async Task HandleCreateAccountAsync()
        //{
        //    try
        //    {
        //        IsBusy = true;
        //        await _mediator.Send(new CreateProfileAction());
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        //private async Task HandleLogInAsync()
        //{
        //    try
        //    {
        //        IsBusy = true;
        //        await _mediator.Send(new NavigateToLogInAction());
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        public void SetErrorState()
        {
            InfoViewModel = InfoViewModelFactory.CreateViewModel(ErrorImgSource, hBuddyApp.Resources.Localization.Exception_NoInternetConnection);
            IsBusy = false;
        }

        private void OnInitialized()
        {
            IsBusy = false;
        }

        public void HideBusy()
        {
            IsBusy = false;
        }

        public void ShowBusy()
        {
            IsBusy = true;
        }

        //private async Task SendAsync()
        //{
        //    await _messageInteractor.SendText(InputText);
        //    InputText = string.Empty;
        //}

        private async Task NavigateBackAsync()
        {
            await _navigationServiceDelegate.GoBackAsync();
        }

        //private async Task HandleMessageAsync(Message message, CancellationToken ct = default)
        //{
        //    await _messagesProcessor.HandleAsync(message);
        //}

        void OnRapidProMessageAppearing(RapidProMessage rapidProMessage)
        {
            //var idx = RapidProMessages.IndexOf(rapidProMessage);
            //if (idx <= 8)
            //{
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        //while (DelayedMessages.Count > 0)
            //        //{
            //        //    Messages.Insert(0, DelayedMessages.Dequeue());
            //        //}
            //        ShowScrollTap = false;
            //        //LastMessageVisible = true;
            //        //PendingMessageCount = 0;
            //    });
            //}
        }

        void OnRapidProMessageDisappearing(RapidProMessage rapidProMessage)
        {
            //var idx = RapidProMessages.IndexOf(rapidProMessage);
            //if (idx >= 8)
            //{
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            //        ShowScrollTap = true;
            //        //LastMessageVisible = false;
            //    });
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InsertRapidProMessages(RapidProFcmPushNotification rapidProFcmPushNotification)
        {
            var rapidProMessage = new RapidProMessage() { PId = Guid.NewGuid().ToString(), Text = rapidProFcmPushNotification.Body, Value = rapidProFcmPushNotification.Body, User = MessageUserEnum.UserHealthBuddy.ToDescriptionAttr(), MessageAction = false, ChannelId = _firebaseContainer.FirebaseChannelId, Type = MessageTypeEnum.Polls.ToDescriptionAttr(), CreatedDateTime = DateTime.UtcNow };
            InsertPollsRapidProMessage(rapidProMessage);

            if (rapidProFcmPushNotification.QuickReplies != null)
            {
                var quickReplies = JsonConvert.DeserializeObject<List<string>>(rapidProFcmPushNotification.QuickReplies);
                if (quickReplies != null)
                {
                    string rapidProMessageId = Guid.NewGuid().ToString();
                    foreach (var quickReplie in quickReplies)
                    {
                        var rapidProMessageQuickReplie = new RapidProMessage() { PId = Guid.NewGuid().ToString(), Id = rapidProMessageId, Text = quickReplie, Value = quickReplie, User = MessageUserEnum.UserHealthBuddy.ToDescriptionAttr(), MessageAction = true, ChannelId = _firebaseContainer.FirebaseChannelId, Type = MessageTypeEnum.Polls.ToDescriptionAttr(), CreatedDateTime = DateTime.UtcNow };
                        InsertPollsRapidProMessage(rapidProMessageQuickReplie);
                    }
                }
            }

            this.HideBusy();
        }

        private async void GetPollsRapidProMessages()
        {
            try
            {
                var rapidProMessages = await _rapidProDatabase.GetRapidProMessagesByChannelIdAsync(_firebaseContainer.FirebaseChannelId, MessageTypeEnum.Polls.ToDescriptionAttr());
                if (rapidProMessages.Any())
                {
                    foreach (var rapidProMessage in rapidProMessages)
                    {
                        RapidProMessages.Insert(0, rapidProMessage);
                    }
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PollsViewModel - GetPollsRapidProMessages: Exception - {ex.Message.ToString()}");
            }
        }

        private async void InsertPollsRapidProMessage(RapidProMessage rapidProMessage)
        {
            try
            {
                if (rapidProMessage != null)
                {
                    RapidProMessages.Insert(0, rapidProMessage);
                    await _rapidProDatabase.InsertRapidProMessageAsync(rapidProMessage);
                    if (_rapidProContainer.RapidProIsDatabase == false)
                    {
                        _rapidProContainer.RapidProIsDatabase = true;
                        Console.WriteLine($"PollsViewModel - InsertPollsRapidProMessage: RapidProIsDatabase");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PollsViewModel - InsertPollsMessage: Exception - {ex.Message.ToString()}");
            }
        }

        private async void RemovePollsRapidProMessageQuickReplie(string rapidProMessageId)
        {
            try
            {
                var rapidProMessageQuickReplies = RapidProMessages.Where(x => x.Id == rapidProMessageId).ToList();
                foreach (var rapidProMessageQuickReplie in rapidProMessageQuickReplies)
                {
                    RapidProMessages.Remove(rapidProMessageQuickReplie);
                    await _rapidProDatabase.DeleteRapidProMessageQuickReplieAsync(rapidProMessageQuickReplie);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PollsViewModel - RemovePollsRapidProMessage: Exception - {ex.Message.ToString()}");
            }
        }


        #region RapidPro Fcm Push Notification

        private async void InitializeFcmAndRapidPro()
        {
            this.ShowBusy();

            try
            {
                //_userAccountInfo = await _userAccountContainer.GetAsync().ConfigureAwait(false);

                if (_rapidProContainer.RapidProIsDatabase == true && _rapidProContainer.RapidProIsInitMsg == true)
                {
                    GetPollsRapidProMessages();

                    Console.WriteLine($"PollsViewModel - InitializeFcmAndRapidPro: RapidProDatabase");
                }
                else
                {
                    if (string.IsNullOrEmpty(_rapidProContainer.RapidProFcmToken))
                    {
                        string fcmPushNotificationToken = CrossFirebasePushNotification.Current?.Token;
                        _rapidProContainer.RapidProFcmToken = fcmPushNotificationToken;

                        Console.WriteLine($"PollsViewModel - InitializeFcmAndRapidPro: Token - {fcmPushNotificationToken}");
                    }

                    if (string.IsNullOrEmpty(_rapidProContainer.RapidProUrn))
                    {
                        string rapidProUrn = RapidProHelper.GetUrnFromGuid();
                        _rapidProContainer.RapidProUrn = rapidProUrn;

                        Console.WriteLine($"PollsViewModel - InitializeFcmAndRapidPro: Urn - {rapidProUrn}");
                    }

                    await RapidProRegisterAndReceiveInit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PollsViewModel - InitializeFcmAndRapidPro: Exception - {ex.Message.ToString()}");
            }
        }

        private async Task RapidProRegisterAndReceiveInit()
        {
            try
            {
                string rapidProUrn = _rapidProContainer.RapidProUrn;
                string rapidProFcmToken = _rapidProContainer.RapidProFcmToken;

                if (_rapidProContainer.RapidProIsInit == false)
                {
                    if (!string.IsNullOrEmpty(rapidProUrn) && !string.IsNullOrEmpty(rapidProFcmToken))
                    {
                        var rapidProRegister = await _rapidProService.RapidProRegister(rapidProUrn, rapidProFcmToken);
                        if (!string.IsNullOrEmpty(rapidProRegister.ContactUuid))
                        {
                            var rapidProReceive = _rapidProService.RapidProReceive(rapidProUrn, rapidProFcmToken, RapidProInitPhrase);
                            _rapidProContainer.RapidProIsInit = true;
                            _rapidProContainer.RapidProIsInitMsg = true;
                        }
                    }
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PollsViewModel - RapidProRegisterAndReceiveInit: Exception - {ex.Message.ToString()}");
            }
        }

        private void SendMessageAsync(string inputText)
        {
            try
            {
                this.ShowBusy();

                string rapidProUrn = _rapidProContainer.RapidProUrn;
                string rapidProFcmToken = _rapidProContainer.RapidProFcmToken;

                if (!string.IsNullOrEmpty(rapidProUrn) && !string.IsNullOrEmpty(rapidProFcmToken))
                {
                    //if (_rapidProContainer.RapidProIsInit == true && _rapidProContainer.RapidProIsInitMsg == true)
                    if (_rapidProContainer.RapidProIsInit == true)
                    {
                        //string user = _userAccountInfo.IsAnonymous ? MessageUserEnum.UserAnonymous.ToDescriptionAttr() : MessageUserEnum.UserLogin.ToDescriptionAttr();
                        //string userName = _userAccountInfo.UserAccount != null ? _userAccountInfo.UserAccount.Username : string.Empty;
                        string user = MessageUserEnum.UserAnonymous.ToDescriptionAttr();
                        string userName = string.Empty;
                        var rapidProMessage = new RapidProMessage() { PId = Guid.NewGuid().ToString(), Text = inputText, Value = inputText, User = user, UserName = userName, ChannelId = _firebaseContainer.FirebaseChannelId, Type = MessageTypeEnum.Polls.ToDescriptionAttr(), CreatedDateTime = DateTime.UtcNow };
                        InsertPollsRapidProMessage(rapidProMessage);

                        var rapidProReceive = _rapidProService.RapidProReceive(rapidProUrn, rapidProFcmToken, inputText);
                        _rapidProContainer.RapidProIsInitSend = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PollsViewModel - SendMessageAsync: Exception - {ex.Message.ToString()}");
            }
        }

        #endregion
    }
}
