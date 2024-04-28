using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    class Service
    {
        ListBox listbox;
        StreamWriter sw;
        public Service(ListBox listbox, StreamWriter sw)
        {
            this.listbox = listbox;
            this.sw = sw;
        }
        // send data to server
        public void SendToServer(string str)
        {
            try
            {
                sw.WriteLine(str);
                sw.Flush();
            }
            catch
            {
                AddItemToListBox("Failed to send data");
            }
        }
        delegate void ListBoxDelegate(string str);
        //Write information to listBox
        public void AddItemToListBox(string str)
        {
            if (listbox.InvokeRequired)
            {
                ListBoxDelegate d = AddItemToListBox;
                listbox.Invoke(d, str);
            }
            else
            {
                listbox.Items.Add(str);
                listbox.SelectedIndex = listbox.Items.Count - 1;
                listbox.ClearSelected();
            }
        }
    }
}
