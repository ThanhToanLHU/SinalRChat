using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat
{
    public class Messages
    {

        public string UserName { get; set; }
        public string DisplayName { get; set; }

        public string Message { get; set; }

        public string Time { get; set; }

        public string UserImage { get; set; }

        public Messages(string UserName, string Message, string Time, string UserImage, string displayName)
        {
            this.UserName = UserName;
            this.Message = Message;
            this.Time = Time;
            this.UserImage = UserImage;
            DisplayName = displayName;
        }
    }
}