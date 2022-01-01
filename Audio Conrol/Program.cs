using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Media;

namespace Audio_Conrol
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                HttpListener listener = new HttpListener();
                // установка адресов прослушки
                listener.Prefixes.Add("http://192.168.1.101:50000/");
                listener.Start();
                Console.WriteLine("Ожидание подключений...");

                // метод GetContext блокирует текущий поток, ожидая получение запроса 
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                HttpListenerResponse response = context.Response;
                // создаем ответ в виде кода html
                string responseStr = "<html><head><meta charset='utf8'></head><body>"
                + "<form action='http://192.168.1.101:50000/' method='get'>"
                    + "<input type='range' min='0' max='100' name='volume'>"
                    + "<input type='submit'/>"
                + "</form>"
                + "</body></html>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);
                // получаем поток ответа и пишем в него ответ
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // закрываем поток
                output.Close();

                context = listener.GetContext();
                request = context.Request;

                // получаем объект ответа
                string url = request.Url.ToString();

                // останавливаем прослушивание подключений
                listener.Stop();
                Console.WriteLine("Обработка подключений завершена");

                if(url.IndexOf("volume=") > 1)
                {
                    // извлекаем подстроку с искомым числом
                    int volume = Convert.ToInt32(url.Substring(url.IndexOf("volume=") + 7));
                    Console.WriteLine("Volume: " + volume.ToString() + "\n");
                    // устанавливаем громкость на компе
                    AudioControl.SetMasterVolume(Convert.ToSingle(volume / 100.0));
                }
                else
                {
                    Console.WriteLine("Получена другая информация. Продолжаем слушать...\n");
                }
            }
        }
    }
}
