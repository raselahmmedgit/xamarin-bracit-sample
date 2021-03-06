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
using System.Windows.Input;
using hBuddyApp.Features.Chat.Handlers;
using hBuddyApp.Services.Http.UriHandlers;

using ReactiveUI;

namespace hBuddyApp.Features.Controls.HybridWebView
{
    public interface IInteractionChannelHandler
    {
        /// <summary>
        /// Command to receive channel messages.
        /// </summary>
        public ICommand HandleMessageCommand { get; }

        /// <summary>
        /// Interaction to send channel messages.
        /// </summary>
        Interaction<Message, bool> SendMessageInteraction { get; }

        /// <summary>
        /// Method which should be called when inertaction channel is initialized.
        /// </summary>
        void ApiReadyMessage();
    }
}
