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
using hBuddyApp.Client.Services;
using hBuddyApp.Client.Services.Platform;
using hBuddyApp.Client.Services.Platform.Models;
using hBuddyApp.Logs;
using hBuddyApp.Services.Http.Connectivity;
using hBuddyApp.Services.Http.ExceptionsHandling;
using hBuddyApp.Services.Localization;

using Microsoft.Extensions.Logging;

namespace hBuddyApp.Features.AboutUs.Services
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IPlatformClient _platformClient;
        private readonly IErrorResponseHandler _serviceErrorHandler;
        private readonly IConnectivityService _connectivityService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger<AboutUsService> _logger;

        public AboutUsService(
            IPlatformClient platformClient,
            IErrorResponseHandler serviceErrorHandler,
            IConnectivityService connectivityService,
            ILocalizationService localizationService,
            ILoggerFactory loggerFactory)
        {
            _platformClient = platformClient;
            _serviceErrorHandler = serviceErrorHandler;
            _connectivityService = connectivityService;
            _localizationService = localizationService;
            _logger = loggerFactory.CreateLogger<AboutUsService>();
        }

        public async Task<UtilityArticle> GetAboutUsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var languageCode = _localizationService.CurrentCulture.TwoLetterISOLanguageName;

                _connectivityService.CheckConnection();
                cancellationToken.ThrowIfCancellationRequested();

                var utilityArticle = await _platformClient.Endpoints.GetAboutUsAsync(languageCode, cancellationToken).ConfigureAwait(false);
                return utilityArticle;
            }
            catch (Exception ex)
            {
                if (_serviceErrorHandler.TryHandle(ex, out var generatedException, cancellationToken))
                {
                    if (!(ex is OperationCanceledException))
                    {
                        _logger.LogErrorExceptCancellation(ex, $"Failed to load about us information.");
                    }
                    generatedException.Rethrow();
                }

                throw;
            }
        }
    }
}
