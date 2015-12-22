using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
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


                BinaryFormatter formatter = new BinaryFormatter();
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
                        NetworkStream stream = client.GetStream();

                        //message:
                        //1 byte - Messages
                        //the rest - object

                        //Read message type
                        byte[] buffer = new byte[1];
                        stream.Read(buffer, 0, 1);
                        Messages messageType = (Messages)buffer[0];

                        switch(messageType)
                        {
                            case Messages.POSITION:
                                Point pos = (Point)formatter.Deserialize(stream);
                                Console.WriteLine("Server got: " + pos.X);
                                
                                break;
                           
                        }

                        //write response
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("1");
                        stream.Write(msg, 0, msg.Length);

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

        public void ClientSend(Messages mType,object obj)
        {
            clientThread = new Thread(() =>
            {

                TcpClient client;
                NetworkStream stream;
                try
                {
                    client = new TcpClient(Constants.ADDRESS, Constants.PORT);
                    BinaryFormatter formatter = new BinaryFormatter();
                    stream = client.GetStream();

                    //send message type - 1 byte
                    byte[] buffer = { (byte)mType };
                    stream.Write(buffer, 0, buffer.Length);

                    //send serialized object
                    formatter.Serialize(stream, obj);//send to stream as binary data
                    

                    //response
                    Byte[] data;
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
