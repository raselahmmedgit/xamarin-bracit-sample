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
using hBuddyApp.Services.Security.SecureVault.Internal;

namespace hBuddyApp.Services.Security.SecureVault
{
    public static class SecureVaultProvider
    {
        private static readonly Lazy<ISecureVault> DefaultInstance =
            new Lazy<ISecureVault>(() => new DefaultSecureVault());

        private static readonly Func<ISecureVault> DefaultInstanceProvider = () => DefaultInstance.Value;

        private static Func<ISecureVault> _instanceProvider;

        public static ISecureVault Instance => (_instanceProvider ?? DefaultInstanceProvider).Invoke();

        public static void Setup(Func<ISecureVault> provider)
        {
            _instanceProvider = provider;
        }
    }
}