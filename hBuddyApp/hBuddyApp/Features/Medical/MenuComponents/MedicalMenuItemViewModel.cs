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
using hBuddyApp.Features.Menu;
using hBuddyApp.Utils.ReactiveCommandHelpers;
using MediatR;

namespace hBuddyApp.Features.Medical.MenuComponents
{
    public class MedicalMenuItemViewModel : MenuItemViewModel
    {
        private readonly IMediator _mediator;

        public MedicalMenuItemViewModel(IMediator mediator, string title, string iconName)
        {
            _mediator = mediator;
            Title = title;
            IconSource = iconName;
            NavigateCommand = ReactiveCommandFactory.CreateLockedCommand(GoToMedicalAsync, nameof(MenuItemViewModel));
        }

        protected async Task GoToMedicalAsync()
        {
            await _mediator.Send(new StartMedicalFlowAction());
        }
    }
}
