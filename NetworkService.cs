using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multiplayer_Game_Sample
{
    class NetworkService
    {
        Thread serverThread, clientThread;

        public void StartServer()
        {
            serverThread = new Thread(() => {


                Byte[] bytes = new Byte[Constants.BUFFER_SIZE];
                string data = null;

                var localAddr = IPAddress.Parse(Constants.ADDRESS);

                try
                {
                    TcpListener serverSocket = new TcpListener(localAddr, Constants.PORT);
                    serverSocket.Start();

                    Console.WriteLine("Server started");
                    //listening loop
                    while (true)
                    {
                        TcpClient client = serverSocket.AcceptTcpClient();
                        data = null;

                        NetworkStream stream = client.GetStream();
                        int i;
                        do
                        {
                            //read from stream
                            i = stream.Read(bytes, 0, bytes.Length);

                            //convert to ASCII
                            data = Encoding.ASCII.GetString(bytes, 0, i);
                            data = data.ToUpper();
                            Console.WriteLine("Server got: " + data);

                            byte[] msg = System.Text.Encoding.ASCII.GetBytes("return");
                            stream.Write(msg, 0, msg.Length);
                        }

                        while (i != 0);

                        Console.Write(data);
                        client.Close();
                        stream.Close();
                    }
                    
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("ArgumentNullException: {0}", e);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            serverThread.Start();
        }

        public void ClientSend()
        {
            clientThread = new Thread(() =>
            {

                TcpClient client;
                NetworkStream stream;
                try
                {
                    client = new TcpClient(Constants.ADDRESS, Constants.PORT);

                    Byte[] data = System.Text.Encoding.ASCII.GetBytes("Hello World");

                    stream = client.GetStream();
                    stream.Write(data, 0, data.Length);


                    //response
                    data = new Byte[Constants.BUFFER_SIZE];
                    stream.Read(data, 0, data.Length);

                    Console.WriteLine("Client got: " + Encoding.ASCII.GetString(data));

                    stream.Close();
                    client.Close();
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("ArgumentNullException: {0}", e);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            clientThread.Start();
            
        }



    }
}
