using System;
using System.Net;
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
                listener.Prefixes.Add("http://localhost:8888/connection/");
                listener.Start();
                Console.WriteLine("Ожидание подключений...");

                // метод GetContext блокирует текущий поток, ожидая получение запроса 
                HttpListenerRequest request = listener.GetContext().Request;
                
                // получаем объект ответа
                string url = request.Url.ToString();
                // извлекаем подстроку с искомым числом
                int volume = Convert.ToInt32(url.Substring(url.IndexOf("value=") + 6));
                Console.WriteLine("Volume: " + volume.ToString());
                // устанавливаем громкость на компе
                AudioControl.SetMasterVolume(Convert.ToSingle(volume / 100.0));
                
                // останавливаем прослушивание подключений
                listener.Stop();
                Console.WriteLine("Обработка подключений завершена\n");
            }
        }
    }
}
