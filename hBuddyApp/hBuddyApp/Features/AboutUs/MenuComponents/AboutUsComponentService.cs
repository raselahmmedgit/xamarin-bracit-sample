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
using System.Collections.Generic;
using System.Reactive.Linq;
using hBuddyApp.Features.ComponentsManagement;
using Prism.Ioc;

namespace hBuddyApp.Features.AboutUs.MenuComponents
{
    public class AboutUsComponentService : StatelessComponentServiceBase
    {
        public AboutUsComponentService(IContainerProvider containerProvider)
            : base(containerProvider)
        {
        }

        public override string ComponentKey => nameof(AboutUsComponentService);

        protected override IObservable<IList<Type>> GetComponentTypes()
        {
            return Observable.Return(new List<Type>()
            {
                typeof(AboutUsMenuItemViewModel)
            });
        }
    }
}
