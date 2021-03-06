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

using System.Threading.Tasks;
using hBuddyApp.Client.Services;
using hBuddyApp.Client.Services.Platform;
using hBuddyApp.Client.Services.Platform.Models;
using hBuddyApp.Services.Http.SessionContainer;

namespace hBuddyApp.Services.Http.AuthorizationHandling
{
    public class TokenRefreshInvoker : ITokenRefreshInvoker
    {
        private readonly IPlatformClient _platformClient;

        public TokenRefreshInvoker(IPlatformClient platformClient)
        {
            _platformClient = platformClient;
        }

        public async Task<SessionInfo> InvokeRefreshTokenAsync(string refreshToken)
        {
            var request = new RefreshTokenRequest(refreshToken: refreshToken);
            var tokenResponse = await _platformClient.Endpoints.RefreshTokenAsync(request);

            var tokenRefreshModel = SessionInfo.CreateFromToken(tokenResponse?.Token);
            return tokenRefreshModel;
        }
    }
}
