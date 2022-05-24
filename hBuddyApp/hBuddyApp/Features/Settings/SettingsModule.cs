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

using hBuddyApp.Features.ChangeLanguage.SettingsComponents;
using hBuddyApp.Features.ComponentsManagement;
using hBuddyApp.Features.Menu;
using hBuddyApp.Features.Menu.Components;
//using hBuddyApp.Features.PushNotifications.SettingsComponents;
using hBuddyApp.Features.Regions;
using hBuddyApp.Features.Settings.Components;
using hBuddyApp.Features.Settings.MenuComponents;
using hBuddyApp.Features.Settings.Routes;
using Prism.Ioc;
using Prism.Modularity;

namespace hBuddyApp.Features.Settings
{
    public class SettingsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegionManager.RegisterFunctionalityToTheRegion<SettingsComponentService>(
                MenuViewModel.MenuRegionIdentifier,
                DataTemplateProviderFactory.CreatePlainFor<MenuItem>());

            containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>();
            containerRegistry.Register<ISettingsRoute, SettingsRoute>();

            //RegionManager.RegisterFunctionalityToTheRegion<NotificationsComponentService>(
            //    SettingsViewModel.SettingsRegionIdentifier,
            //    DataTemplateProviderFactory.CreatePlainFor<NotificationsItemView>());
            //containerRegistry.Register<NotificationsItemViewModel>();

            //RegionManager.RegisterFunctionalityToTheRegion<LanguageSettingsComponentService>(
            //    SettingsViewModel.SettingsRegionIdentifier,
            //    DataTemplateProviderFactory.CreatePlainFor<LanguageSettingsItemView>());

            containerRegistry.Register<LanguageSettingsItemViewModel>();

        }
    }
}
