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
using hBuddyApp.Features.NewsArticle.Actions;
using hBuddyApp.Services.Navigation;
using Prism.Navigation;

namespace hBuddyApp.Features.NewsArticle.Routes
{
    public class NewsArticleRoute : INewsArticleRoute
    {
        public async Task ExecuteAsync(INavigationService navigationService, NewsArticleParameters parameters)
        {
            NavigationParameters result = await parameters.ToNavigationParametersAsync();
            await ExecuteAsync(navigationService, result);
        }

        private async Task ExecuteAsync(INavigationService navigationService, INavigationParameters parameters)
        {
            var page = $"{nameof(NewsArticlePage)}";
            await navigationService.NavigateAsync(page, parameters);
        }
    }
}
