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
using System.Reactive.Disposables;

namespace hBuddyApp.Features
{
    public static class ActivatableViewModelExtensions
    {
        public static void WhenActivated(this IActivatableViewModel viewModel, Func<IEnumerable<IDisposable>> block)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            viewModel.HostContext.ActivatorManager.AddActivationBlock(block);
        }

        public static void WhenActivated(this IActivatableViewModel viewModel, Action<CompositeDisposable> block)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            viewModel.HostContext.ActivatorManager.AddActivationBlock(() =>
            {
                var d = new CompositeDisposable();
                block(d);
                return new[] { d };
            });
        }
    }
}
