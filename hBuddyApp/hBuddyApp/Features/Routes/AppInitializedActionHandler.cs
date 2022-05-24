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

using System.Threading;
using System.Threading.Tasks;
//using hBuddyApp.Features.Account.Services.Authentication;
using hBuddyApp.Features.AppSettings;
using hBuddyApp.Features.ChangeLanguage.Routes;
using hBuddyApp.Features.Main.Routes;
//using hBuddyApp.Features.OnBoarding;
//using hBuddyApp.Features.OnBoarding.Routes;
using hBuddyApp.Features.Welcome.Routes;
using hBuddyApp.Services.Actions;
using hBuddyApp.Services.ApplicationMetadata;
using hBuddyApp.Services.Localization;
using hBuddyApp.Services.Navigation;
using MediatR;

namespace hBuddyApp.Features.Routes
{
    public class AppInitializedActionHandler : AsyncRequestHandler<AppInitializedAction>, IRequestHandler<AppInitializedAction, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IMainStartupRoute _mainStartupRoute;
        private readonly IChangeLanguageRoute _changeLanguageRoute;
        //private readonly IOnBoardingRoute _onBoardingRoute;
        private readonly IWelcomeRoute _welcomeRoute;
        //private readonly IAuthenticationInfoService _authenticationInfoService;
        private readonly IAppPreferencesService _appPreferencesService;
        private readonly INavigationServiceDelegate _navigationServiceDelegate;
        private readonly ILocalizationService _localizationService;

        private static readonly string IsFirstRunFieldName = Configuration.Constants.SecretsProviderConstants.IsFirstRunFieldName;

        public AppInitializedActionHandler(
            IMediator mediator,
            IMainStartupRoute mainStartupRoute,
            //IOnBoardingRoute onBoardingRoute,
            IWelcomeRoute welcomeRoute,
            //IAuthenticationInfoService authenticationInfoService,
            IAppPreferencesService appPreferencesService,
            INavigationServiceDelegate navigationServiceDelegate,
            IChangeLanguageRoute changeLanguageRoute,
            ILocalizationService localizationService)
        {
            _mediator = mediator;
            _mainStartupRoute = mainStartupRoute;
            //_onBoardingRoute = onBoardingRoute;
            _welcomeRoute = welcomeRoute;
            //_authenticationInfoService = authenticationInfoService;
            _appPreferencesService = appPreferencesService;
            _navigationServiceDelegate = navigationServiceDelegate;
            _changeLanguageRoute = changeLanguageRoute;
            _localizationService = localizationService;
        }

        protected override async Task Handle(AppInitializedAction request, CancellationToken cancellationToken)
        {
            //if (_authenticationInfoService.IsAuthenticated())
            //{
            //    _localizationService.InitializeLocale();
            //    _mediator.Send(new FetchMetadataAction()).FireAndForget();
            //    if (_appPreferencesService.GetOnBoardingState() || _authenticationInfoService.IsAnonymous())
            //    {
            //        await _mainStartupRoute.ExecuteAsync(_navigationServiceDelegate);
            //    }
            //    else
            //    {
            //        await _onBoardingRoute.ExecuteAsync(_navigationServiceDelegate);
            //    }
            //}
            //else
            //{
            //    var isFirstLaunch = Xamarin.Essentials.Preferences.Get(IsFirstRunFieldName, true);
            //    if (isFirstLaunch)
            //    {
            //        await _changeLanguageRoute.ExecuteAsync(_navigationServiceDelegate);
            //    }
            //    else
            //    {
            //        _localizationService.InitializeLocale();
            //        await _welcomeRoute.ExecuteAsync(_navigationServiceDelegate);
            //    }
            //}

            _localizationService.InitializeLocale();
            await _welcomeRoute.ExecuteAsync(_navigationServiceDelegate);
        }
    }
}
