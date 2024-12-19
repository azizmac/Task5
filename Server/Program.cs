using System.Net;
using System.Net.Sockets;

const string SERVER_HOST = "127.0.0.1";
const int SERVER_PORT = 50000;



    TcpListener listener = new TcpListener(IPAddress.Parse(SERVER_HOST), SERVER_PORT);
    listener.Start();

    Console.WriteLine($"Сервер запущен на {SERVER_HOST}:{SERVER_PORT}");

    while (true)
    {
        TcpClient client = listener.AcceptTcpClient();
        Console.WriteLine("Клиент подключился.");
        
        NetworkStream stream = client.GetStream();
        
        byte[] buffer = new byte[2];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);

        if (bytesRead == 0)
        {
            client.Close();
            Console.WriteLine("Нет данных от клиента.");
            continue;
        }


        byte x = buffer[0];
        byte y = buffer[1];
        Console.WriteLine($"Сервер получил: x={x}, y={y}");


        int z = x * y;


        byte[] response = BitConverter.GetBytes((ushort)z);


        stream.Write(response, 0, response.Length);
        Console.WriteLine($"Сервер отправил результат: z={z}");

        client.Close();
    }