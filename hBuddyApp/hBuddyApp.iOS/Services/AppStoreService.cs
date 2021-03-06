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
using System.Threading.Tasks;
using hBuddyApp.Configuration;
using hBuddyApp.Services;
using Xamarin.Essentials;

namespace hBuddyApp.iOS.Services
{
    public class AppStoreService : IAppStoreService
    {
        private const string AppStoreUrlKey = "AppStoreUrl";
        private readonly IEnvironmentConfiguration _environmentConfiguration;


        public AppStoreService(IEnvironmentConfiguration environmentConfiguration)
        {
            _environmentConfiguration = environmentConfiguration;
        }

        public async Task OpenAppPageInStoreAsync()
        {
            var uriString = _environmentConfiguration.GetValue(AppStoreUrlKey);
            await Launcher.OpenAsync(new Uri(uriString));
        }
    }
}
