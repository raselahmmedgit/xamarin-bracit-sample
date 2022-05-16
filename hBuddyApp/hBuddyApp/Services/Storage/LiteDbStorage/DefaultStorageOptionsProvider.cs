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

using System.Threading.Tasks;
using hBuddyApp.Services.Security.SecretsProvider;

namespace hBuddyApp.Services.Storage.LiteDbStorage
{
    public class DefaultStorageOptionsProvider : IStorageOptionsProvider
    {
        private readonly ISecretsProvider _secretsProvider;
        private StorageOptions _options;

        public DefaultStorageOptionsProvider(ISecretsProvider secretsProvider)
        {
            _secretsProvider = secretsProvider;
        }

        public async Task<StorageOptions> GetAsync()
        {
            if (_options == null)
            {
                var encryptionKey = await _secretsProvider.GetEncryptionKeyAsync();
                _options = new StorageOptions()
                {
                    EncryptionKey = encryptionKey,
                };
            }

            return _options;
        }

        public void ResetCache()
        {
            _options = null;
        }
    }
}
