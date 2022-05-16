using Newtonsoft.Json;
using System;

namespace hBuddyApp.Features.RapidProFcmPushNotifications
{

    [Serializable]
    public class RapidProRegister
    {
        [JsonProperty(PropertyName = "contact_uuid")]
        public string ContactUuid { get; set; }
    }
}
