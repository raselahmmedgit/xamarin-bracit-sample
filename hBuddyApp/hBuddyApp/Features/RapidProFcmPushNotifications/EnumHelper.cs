using System.ComponentModel;

namespace hBuddyApp.Features.RapidProFcmPushNotifications
{
    public enum MessageTypeEnum
    {
        [Description("Chat")]
        Chat = 1,
        [Description("Polls")]
        Polls = 2,
        [Description("Rumours")]
        Rumours = 3
    }

    public enum MessageUserEnum
    {
        [Description("UserHealthBuddy")]
        UserHealthBuddy = 1,
        [Description("UserLogin")]
        UserLogin = 2,
        [Description("UserAnonymous")]
        UserAnonymous = 3
    }
}
