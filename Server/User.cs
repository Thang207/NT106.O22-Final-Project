using System;
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
            userName = "";
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
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(mgs);
                netStream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send message: {ex.Message}");
            }
        }
    }
}
