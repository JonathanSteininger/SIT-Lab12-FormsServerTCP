using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using PlayingCardLib;
using System.Runtime.Serialization.Formatters.Binary;

namespace FormsServerTCP
{
    internal class TCPProgram
    {
        private string ip;
        private int port;
        public TCPProgram(string ip, int port) {
            this.ip = ip;
            this.port = port;
        }

        public PlayingCard requestCard()
        {
            TcpClient tcpClient = new TcpClient();
            PlayingCard card;
            try
            {
                tcpClient.Connect(ip, port);
                BinaryFormatter formatter = new BinaryFormatter();
                using(NetworkStream stream = tcpClient.GetStream())
                {
                    card = formatter.Deserialize(stream) as PlayingCard;
                }
                tcpClient.Close();

            }catch (Exception ex)
            {
                tcpClient.Close();
                throw ex;
            }
            return card;
        }


    }
}
