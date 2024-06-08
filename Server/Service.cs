using System.Windows.Forms;

namespace Server
{
    class Service
    {
        private ListBox listbox;
        private delegate void AddItemDelegate(string str);
        private AddItemDelegate addItemDelegate;
        public Service(ListBox listbox)
        {
            this.listbox = listbox;
            addItemDelegate = new AddItemDelegate(AddItem);
        }

        // Add information in listBox
        public void AddItem(string str)
        {
            if (listbox.InvokeRequired)
            {
                listbox.Invoke(addItemDelegate, str);
            }
            else
            {
                listbox.Items.Add(str);

                // scroll to bottom, easy to read status
                listbox.SelectedIndex = listbox.Items.Count - 1;
                listbox.ClearSelected();
            }
        }

        // Send message to client
        public void SendToOne(User user, string str)
        {
            try
            {
                user.Send(str);
                AddItem(string.Format("Send {1} to [{0}]", user.userName, str));
            }
            catch
            {
                AddItem(string.Format("Failed to send to [{0}]", user.userName));
            }
        }

        // Send a message to 2 client in table
        public void SendToBoth(GameTable gameTable, string str)
        {
            for (int i = 0; i < 2; i++)
            {
                if (gameTable.gamePlayer[i].someone == true)
                {
                    SendToOne(gameTable.gamePlayer[i].user, str);
                }
            }
        }

        // Send message to all clients
        public void SendToAll(System.Collections.Generic.List<User> userList, string str)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                SendToOne(userList[i], str);
            }
        }
    }
}
