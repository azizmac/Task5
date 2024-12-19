using System.Net.Sockets;

const string SERVER_HOST = "127.0.0.1";
const int SERVER_PORT = 50000;


    while (true)
    {
        try
        {
            Console.Write("Введите первое число (0-255): ");
            byte x = byte.Parse(Console.ReadLine());
            Console.Write("Введите второе число (0-255): ");
            byte y = byte.Parse(Console.ReadLine());

            if (x < 0 || x > 255 || y < 0 || y > 255)
            {
                Console.WriteLine("Числа должны быть в диапазоне 0-255.");
                continue;
            }

            // Создаем сокет и подключаемся к серверу
            TcpClient client = new TcpClient(SERVER_HOST, SERVER_PORT);
            NetworkStream stream = client.GetStream();


            byte[] message = { x, y };
            stream.Write(message, 0, message.Length);
            Console.WriteLine($"Клиент отправил: x={x}, y={y}");


            byte[] buffer = new byte[2];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            if (bytesRead > 0)
            {

                ushort z = BitConverter.ToUInt16(buffer, 0);
                Console.WriteLine($"Клиент получил результат: z={z}");
            }

            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }
    }