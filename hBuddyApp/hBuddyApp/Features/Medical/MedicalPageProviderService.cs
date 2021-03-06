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

using hBuddyApp.Configuration;
using hBuddyApp.Features.Account.Services.Authentication;
using hBuddyApp.Features.Main;
using hBuddyApp.Services.Feature;
using Xamarin.Forms;

namespace hBuddyApp.Features.Medical
{
    public class MedicalPageProviderService : IMainPageProviderService
    {
        private readonly IFeatureStateService _featureStateService;
        private readonly IAuthenticationInfoService _authenticationInfoService;
        private readonly IEnvironmentConfiguration _environmentConfiguration;

        public MedicalPageProviderService(
            IFeatureStateService featureStateService,
            IAuthenticationInfoService authenticationInfoService,
            IEnvironmentConfiguration environmentConfiguration)
        {
            _featureStateService = featureStateService;
            _authenticationInfoService = authenticationInfoService;
            _environmentConfiguration = environmentConfiguration;
        }

        public bool TryGetPage(out Page page, out bool isDefault)
        {
            if (_featureStateService.IsMedicalFeatureEnabled(_environmentConfiguration)
                && _authenticationInfoService.IsAuthenticated()
                && !_authenticationInfoService.IsAnonymous())
            {
                page = new MedicalPage();
            }
            else
            {
                page = null;
            }

            isDefault = false;
            return page != null;
        }
    }
}
