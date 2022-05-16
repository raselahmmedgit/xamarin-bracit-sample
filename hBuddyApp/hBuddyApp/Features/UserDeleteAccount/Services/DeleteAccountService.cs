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

using hBuddyApp.Client.Services;
using hBuddyApp.Features.Account.Services.Authentication;
using hBuddyApp.Features.Account.Services.SignOut;
using hBuddyApp.Features.RapidProFcmPushNotifications;
using hBuddyApp.Logs;
using hBuddyApp.Services.Http.Connectivity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace hBuddyApp.Features.UserDeleteAccount.Services
{
    public class DeleteAccountService : IDeleteAccountService
    {
        private readonly IPlatformClient _platformClient;
        private readonly IConnectivityService _connectivityService;
        private readonly IAuthenticationServiceErrorHandler _serviceErrorHandler;
        private readonly ISignOutService _signOutService;
        private readonly RapidProDatabase _rapidProDatabase;

        private readonly ILogger _logger;

        public DeleteAccountService(
            IPlatformClient platformClient,
            IConnectivityService connectivityService,
            IAuthenticationServiceErrorHandler serviceErrorHandler,
            ISignOutService signOutService,
            ILoggerFactory loggerFactory)
        {
            _platformClient = platformClient;
            _connectivityService = connectivityService;
            _serviceErrorHandler = serviceErrorHandler;
            _signOutService = signOutService;
            _rapidProDatabase = new RapidProDatabase();

            _logger = loggerFactory.CreateLogger<DeleteAccountService>();
        }

        public async Task DeleteUserAsync()
        {
            await DeleteUserDataAsync().ConfigureAwait(false);
            await _rapidProDatabase.DeleteAllRapidProMessageAsync();
            await _signOutService.SignOutAsync().ConfigureAwait(false);
        }

        private async Task DeleteUserDataAsync()
        {
            try
            {
                _connectivityService.CheckConnection();
                await _platformClient.Endpoints.DeleteUserWithHttpMessagesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogErrorExceptCancellation(ex, "Failed to delete user account.");
                if (_serviceErrorHandler.TryHandle(ex, out var generatedException))
                {
                    generatedException.Rethrow();
                }

                throw;
            }
        }
    }
}
