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
using hBuddyApp.Client.Services.Platform.Models;
using hBuddyApp.Features.UserProfile.Services;

namespace hBuddyApp.Features.UserData.Services
{
    public class UserDataInfo
    {
        public static UserDataInfo CreateFromResponse(UserProfileResponse userProfileResponse)
        {
            return new UserDataInfo()
            {
                UserAccountInfo = userProfileResponse?.UserAccount != null ?
                    new UserAccountInfo(userProfileResponse.UserAccount)
                    : null,
                Metadata = userProfileResponse.Metadata,
                UserStatus = userProfileResponse.UserStatus
            };
        }

        public UserAccountInfo UserAccountInfo { get; set; }

        public UserStatus UserStatus { get; set; }

        public Metadata Metadata { get; set; }
    }
}
