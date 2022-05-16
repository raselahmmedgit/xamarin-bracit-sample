﻿// =========================================================================
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
using System.Collections.Generic;
using hBuddyApp.Features.AccountFeaturesInformation.Flows;

namespace hBuddyApp.Features.AccountFeaturesInformation.Services
{
    public class AccountInformationService : IAccountInformationService
    {
        private readonly Dictionary<string, Func<IAccountFeaturesInformationHandler>> _accountInfoItems;

        public AccountInformationService()
        {
            _accountInfoItems = new Dictionary<string, Func<IAccountFeaturesInformationHandler>>();
        }

        public IAccountFeaturesInformationHandler GetCurrentAccountInfoType(string currentAccountInfoType)
        {
            if (_accountInfoItems.TryGetValue(currentAccountInfoType, out var value))
            {
                return value.Invoke();
            }

            throw new ArgumentException($"{typeof(IAccountFeaturesInformationHandler)} with key '{currentAccountInfoType}' is not registered.");
        }

        public void RegisterCurrentAccountType(string type, Func<IAccountFeaturesInformationHandler> accountInfo)
        {
            _accountInfoItems.Add(type, accountInfo);
        }
    }
}
