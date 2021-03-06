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

using hBuddyApp.Features.OnBoarding.Steps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hBuddyApp.Features.OnBoarding
{
    public class OnBoardingItemViewModel : ViewModelBase
    {
        private readonly IOnBoardingStep _onBoardingStep;

        public OnBoardingItemViewModel(IOnBoardingStep onBoardingStep)
        {
            _onBoardingStep = onBoardingStep;
            Instructions = onBoardingStep.Instructions.Select(s => new InstructionItemViewModel(s)).ToList();
        }

        public string Title => _onBoardingStep.Title;
        public string SubTitle => _onBoardingStep.SubTitle;
        public string IconCode => _onBoardingStep.IconCode;
        public string ErrorMessage => _onBoardingStep.ErrorMessage;
        public IReadOnlyList<InstructionItemViewModel> Instructions { get; }

        public async Task<bool> HandleStepAsync()
        {
            var result = await _onBoardingStep.HandleStepAsync();
            return result;
        }
    }
}
