using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explore_Patterns
{
    public sealed class singleton
    {
        private static singleton _instance = null;
        private static readonly object _lock = new object();

        // Приватный конструктор - предотвращает создание экземпляров извне
        private singleton()
        {
            Console.WriteLine("Создано подключение к базе данных");
            ConnectionString = "Server=localhost;Database=MyDB;";
        }
        
        public string ConnectionString { get; private set; }

        public static singleton GetInstance()
        {
            if (_instance == null) // Первая проверка (без блокировки)
            {
                lock (_lock) // Блокировка для потокобезопасности
                {
                    if (_instance == null) // Вторая проверка
                    {
                        _instance = new singleton();
                    }
                }
            }
            return _instance;
        }

        public void Query(string sql)
        {
            Console.WriteLine($"Выполнение запроса: {sql}");
        }
    }

    // Простой вариант (не потокобезопасный, но для простых приложений)
    public sealed class AppSettings
    {
        private static AppSettings _instance;

        private AppSettings() { }

        public static AppSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AppSettings();
                return _instance;
            }
        }

        public string Theme { get; set; } = "Light";
        public string Language { get; set; } = "ru-RU";
    }
    
}
