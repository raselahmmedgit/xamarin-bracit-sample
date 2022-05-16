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

using Foundation;
using UIKit;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using hBuddyApp.iOS.CustomRenderers.HybridWebView;
using hBuddyApp.Logs;
using Microsoft.Extensions.Logging;

[assembly: ExportRenderer(typeof(hBuddyApp.Features.Controls.HybridWebView.HybridWebView), typeof(HybridWebViewRenderer))]

namespace hBuddyApp.iOS.CustomRenderers.HybridWebView
{
    public class HybridWebViewRenderer : WkWebViewRenderer, IWKScriptMessageHandler
    {
        const string JavaScriptFunction =
            @"window[""dispatchNative""] = function(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";

        private const string InvokeAction = "invokeAction";
        readonly WKUserContentController _userController;
        private ILogger _logger;

        public HybridWebViewRenderer()
            : this(new WKWebViewConfiguration())
        {
        }

        public HybridWebViewRenderer(WKWebViewConfiguration config)
            : base(config)
        {
            _userController = config.UserContentController;
            _logger = Logger.Factory.CreateLogger("iOS Web View");
            this.NavigationDelegate = new CustomWebViewDelegate(_logger);
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                CleanUp(e.OldElement);
            }

            if (e.NewElement != null)
            {
                SetUp();
            }

            Opaque = false;
            BackgroundColor = UIColor.Clear;
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            ((hBuddyApp.Features.Controls.HybridWebView.HybridWebView) Element).ReactAction(message.Body?.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CleanUp(Element);
            }

            base.Dispose(disposing);
        }

        private void CleanUp(VisualElement element)
        {
            _userController.RemoveAllUserScripts();
            _userController.RemoveScriptMessageHandler(InvokeAction);
            if (element is hBuddyApp.Features.Controls.HybridWebView.HybridWebView hybridWebView)
            {
                hybridWebView.Cleanup();
            }
        }

        private void SetUp()
        {
            var script = new WKUserScript(
                new NSString(JavaScriptFunction),
                WKUserScriptInjectionTime.AtDocumentStart,
                false);
            _userController.AddUserScript(script);
            _userController.AddScriptMessageHandler(this, InvokeAction);

            NSHttpCookieStorage.SharedStorage.RemoveCookiesSinceDate(NSDate.DistantPast);
            NSUrlCache.SharedCache.RemoveAllCachedResponses();
        }
    }

    public class CustomWebViewDelegate : WKNavigationDelegate
    {
        private readonly ILogger _logger;

        public CustomWebViewDelegate(ILogger logger)
        {
            _logger = logger;
        }

        public override void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            _logger.LogError($"Chatbot loading error: {webView.Url.AbsoluteString}, Reason: {error.Description}");
        }

        public override void DidFailProvisionalNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            _logger.LogError($"Chatbot loading error: {webView.Url.AbsoluteString}, Reason: {error.Description}");
        }
    }
}
