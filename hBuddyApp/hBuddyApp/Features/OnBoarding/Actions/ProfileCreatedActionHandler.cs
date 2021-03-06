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

using System;
using System.Threading;
using System.Threading.Tasks;
using hBuddyApp.Features.CreateProfile;
using hBuddyApp.Features.OnBoarding.Routes;
using hBuddyApp.Services.ApplicationMetadata;
using hBuddyApp.Services.Navigation;
using MediatR;

namespace hBuddyApp.Features.OnBoarding
{
    public class ProfileCreatedActionHandler : AsyncRequestHandler<ProfileCreatedAction>, IRequestHandler<ProfileCreatedAction, Unit>
    {
        private readonly IOnBoardingRoute _onBoardingRoute;
        private readonly INavigationServiceDelegate _navigationServiceDelegate;
        private readonly IMetadataService _metadataService;

        public ProfileCreatedActionHandler(
            IOnBoardingRoute onBoardingRoute,
            INavigationServiceDelegate navigationServiceDelegate,
            IMetadataService metadataService)
        {
            _metadataService = metadataService;
            _onBoardingRoute = onBoardingRoute;
            _navigationServiceDelegate = navigationServiceDelegate;
        }

        protected override async Task Handle(ProfileCreatedAction request, CancellationToken cancellationToken)
        {
            await _metadataService.FetchMetadataIfNeededAsync();
            await _onBoardingRoute.ExecuteAsync(_navigationServiceDelegate).ConfigureAwait(false);
        }
    }
}
