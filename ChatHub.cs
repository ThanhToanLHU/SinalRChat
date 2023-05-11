using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;


namespace SignalRChat
{
    public class ChatHub :Hub
    {
        static List<Users> ConnectedUsers = new List<Users>();
        static List<Messages> CurrentMessage = new List<Messages>();
        ConnClass ConnC = new ConnClass();

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            OnDisconnected(true);
            foreach (var user in ConnectedUsers.ToList())
            {
                if (user.UserName == userName)
                {
                    ConnectedUsers.Remove(user);
                }
            }
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                string UserImg = GetUserImage(userName);
                string logintime = DateTime.Now.ToString();
                string displayName = ConnC.GetUserColData(userName, "Displayname");
                ConnectedUsers.Add(new Users { ConnectionId = id, UserName = userName, Displayname = displayName, UserImage = UserImg, LoginTime = logintime });

                CurrentMessage = ConnC.ReadMessage();

                // send to caller
                Clients.Caller.onConnected(id, userName, displayName, ConnectedUsers, CurrentMessage);

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName, displayName, UserImg, logintime);
            }
        }

        public void RemoveUser(string userName)
        {
            foreach (var user in ConnectedUsers.ToList())
            {
                if (user.UserName == userName)
                {
                    ConnectedUsers.Remove(user);
                }
            }
        }

        public void SendMessageToAll(string userName, string message, string time)
        {
           string UserImg = GetUserImage(userName);
            // store last 100 messages in cache
            AddMessageinCache(userName, message, time, UserImg);

            // Broad cast message
            Clients.All.messageReceived(userName, ConnC.GetUserColData(userName, "Displayname"), message, time, UserImg);

        }

        private void AddMessageinCache(string userName, string message, string time, string UserImg)
        {
            CurrentMessage.Add(new Messages (userName, message, time, UserImg, ConnC.GetUserColData(userName, "Displayname")));
            DateTime today = DateTime.Now;
            string query = $"insert into Messages (MessageID,UID,MessageText,SentDateTime) values({ConnC.GenerateID()},{ConnC.GetUserColData(userName, "UID")}, '{message}', '{today.ToString()}')";
            ConnC.ExecuteQuery(query);

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);

            // Refresh();
        }

        public string GetUserImage(string username)
        {
            string RetimgName = "images/dummy.png";
            try
            {
                string query = "select Avatar from UserData where UserName='" + username + "'";
                string ImageName = ConnC.GetColumnVal(query, "Avatar");

                if (ImageName != "")
                    RetimgName = "images/DP/" + ImageName;
            }
            catch (Exception ex)
            { }
            return RetimgName;
        }


        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }
            return base.OnDisconnected(stopCalled);
        }
    }
}