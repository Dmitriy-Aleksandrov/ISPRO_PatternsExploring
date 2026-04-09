using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explore_Patterns
{
    public class StockEventArgs : EventArgs
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public double Change { get; set; }

        public StockEventArgs(string symbol, double price, double change)
        {
            Symbol = symbol;
            Price = price;
            Change = change;
        }
    }

    // Интерфейс наблюдателя
    public interface IStockObserver
    {
        void Update(StockEventArgs args);
        string Name { get; }
    }

    // Субъект (акция)
    public class Stock
    {
        private List<IStockObserver> _observers = new List<IStockObserver>();
        private string _symbol;
        private double _price;

        public Stock(string symbol, double initialPrice)
        {
            _symbol = symbol;
            _price = initialPrice;
        }

        public void RegisterObserver(IStockObserver observer)
        {
            _observers.Add(observer);
            Console.WriteLine($"{observer.Name} подписался на {_symbol}");
        }

        public void RemoveObserver(IStockObserver observer)
        {
            _observers.Remove(observer);
            Console.WriteLine($"{observer.Name} отписался от {_symbol}");
        }

        private void NotifyObservers()
        {
            StockEventArgs args = new StockEventArgs(_symbol, _price, 0);

            // Вычисляем изменение для всех наблюдателей
            foreach (var observer in _observers)
            {
                observer.Update(args);
            }
        }

        public void SetPrice(double newPrice)
        {
            double oldPrice = _price;
            _price = newPrice;
            double change = ((newPrice - oldPrice) / oldPrice) * 100;

            Console.WriteLine($"\n=== {_symbol}: {oldPrice} -> {newPrice} ({change:F2}%) ===");

            NotifyObservers();
        }
    }

    // Конкретные наблюдатели
    public class Trader : IStockObserver
    {
        public string Name { get; private set; }
        private double _buyLimit;

        public Trader(string name, double buyLimit)
        {
            Name = name;
            _buyLimit = buyLimit;
        }

        public void Update(StockEventArgs args)
        {
            Console.WriteLine($"  [Трейдер {Name}] {args.Symbol} = {args.Price}");

            if (args.Price <= _buyLimit)
            {
                Console.WriteLine($"    *** Трейдер {Name} покупает {args.Symbol} по {args.Price}!");
            }
        }
    }

    public class AlertSystem : IStockObserver
    {
        public string Name => "Система оповещений";
        private double _alertThreshold;

        public AlertSystem(double threshold)
        {
            _alertThreshold = threshold;
        }

        public void Update(StockEventArgs args)
        {
            if (Math.Abs(args.Change) > _alertThreshold)
            {
                Console.WriteLine($"  [ALERT] Резкое изменение {args.Symbol}: {args.Change:F2}%!");
            }
        }
    }

    public class Logger : IStockObserver
    {
        public string Name => "Логгер";

        public void Update(StockEventArgs args)
        {
            Console.WriteLine($"  [Лог] {DateTime.Now}: {args.Symbol} = {args.Price}");
        }
    }
}
