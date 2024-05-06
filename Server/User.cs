using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    class User
    {
        public Socket client { get; private set; }
        public string userName { get; set; }

        public NetworkStream netStream;

        public User(Socket client)
        {
            this.client = client;
            this.userName = "";
            try
            {
                netStream = new NetworkStream(client);
            }
            catch
            {
                MessageBox.Show("Failed to create network stream");
            }
        }

        public void Send(string mgs)
        {
            byte[] data = Encoding.UTF8.GetBytes(mgs);
            netStream.Write(data, 0, data.Length);
            netStream.Flush();
        }    
    }
}
