using SQLite;
using System;

namespace hBuddyApp.Features.RapidProFcmPushNotifications
{
    public class RapidProMessage
    {
        [PrimaryKey]
        public string PId { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string User { get; set; }
        public string UserName { get; set; }
        public bool MessageAction { get; set; }
        public string ChannelId { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
