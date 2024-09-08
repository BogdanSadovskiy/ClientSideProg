using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace ClientProgram
{
    public class StartClient
    {
        TcpClient client;
        NetworkStream stream;
        public StartClient()
        {
            SetConsoleSize(30, 20);
            ConnectionToServer();
            Messaging();
        }
        private void SetConsoleSize(int width, int height)
        {
            try
            {
                Console.SetWindowSize(width, height);
                Console.SetBufferSize(width, height); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting console size: " + ex.Message);
            }
        }
        private void ConnectionToServer()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 25565);
                stream = client.GetStream();
                Console.WriteLine("Connected to the server.");
                Console.WriteLine("Type your message below:");
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessages));
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex) {Console.WriteLine(ex.ToString());}
        }
        private void Messaging() 
        {
            while (true)
            {
                string message = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(message))
                {
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                 
                }
            }
        }
        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[256];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        DisplayMessageWithColor(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection closed.");
                Console.WriteLine(ex.Message);
                client.Close();
            }
        }
        private void DisplayMessageWithColor(string message)
        {
   
            ConsoleColor color = ConsoleColor.White; 
            int startIndex = message.IndexOf('\'');
            int endIndex = message.IndexOf('\'', startIndex + 1);

            if (startIndex >= 0 && endIndex > startIndex)
            {
                string colorName = message.Substring(startIndex + 1, endIndex - startIndex - 1).Trim();
                if (Enum.TryParse(colorName, true, out ConsoleColor parsedColor))
                {
                    color = parsedColor;
                }
            }

            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = previousColor;
        }
    }
}
