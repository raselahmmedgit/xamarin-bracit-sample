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
using hBuddyApp.Features.Polls.Routes;
using hBuddyApp.Services.Navigation;
using MediatR;

namespace hBuddyApp.Features.Polls
{
    public class PollsActionHandler : AsyncRequestHandler<PollsAction>, IRequestHandler<PollsAction, Unit>
    {
        private readonly IPollsRoute _pollsRoute;
        private readonly INavigationServiceDelegate _navigationServiceDelegate;

        public PollsActionHandler(IPollsRoute pollsRoute,
            INavigationServiceDelegate navigationServiceDelegate)
        {
            _pollsRoute = pollsRoute;
            _navigationServiceDelegate = navigationServiceDelegate;
        }

        protected override async Task Handle(PollsAction request, CancellationToken cancellationToken)
        {
            await _pollsRoute.ExecuteAsync(_navigationServiceDelegate).ConfigureAwait(false);
        }
    }
}
