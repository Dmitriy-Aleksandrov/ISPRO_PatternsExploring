using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explore_Patterns
{
    class fabric_methot
    {
        public interface ITransport
        {
            void Deliver();
            double GetCost();
        }

        // Конкретные продукты
        public class Truck : ITransport
        {
            public void Deliver() => Console.WriteLine("Доставка грузовиком по суше");
            public double GetCost() => 1000.0;
        }

        public class Ship : ITransport
        {
            public void Deliver() => Console.WriteLine("Доставка кораблем по морю");
            public double GetCost() => 2000.0;
        }

        public class Airplane : ITransport
        {
            public void Deliver() => Console.WriteLine("Доставка самолетом по воздуху");
            public double GetCost() => 3000.0;
        }

        // Абстрактный создатель
        public abstract class Logistics
        {
            // Фабричный метод
            public abstract ITransport CreateTransport();

            // Бизнес-логика, использующая фабричный метод
            public void PlanDelivery()
            {
                var transport = CreateTransport();
                Console.WriteLine($"Планирование доставки:");
                transport.Deliver();
                Console.WriteLine($"Стоимость: {transport.GetCost()} руб.");
            }
        }

        // Конкретные создатели
        public class RoadLogistics : Logistics
        {
            public override ITransport CreateTransport() => new Truck();
        }

        public class SeaLogistics : Logistics
        {
            public override ITransport CreateTransport() => new Ship();
        }

        public class AirLogistics : Logistics
        {
            public override ITransport CreateTransport() => new Airplane();
        }

        // Альтернативная реализация с параметром
        public class TransportFactory
        {
            public ITransport CreateTransport(string type)
            {
                switch (type.ToLower())
                {
                    case "truck":
                        return new Truck();
                    case "ship":
                        return new Ship();
                    case "airplane":
                        return new Airplane();
                    default:
                        throw new ArgumentException($"Неизвестный тип транспорта: {type}");
                }
            }
        }

        // Пример с документами
        public interface IDocument
        {
            void Open();
            void Save();
        }

        public class PDFDocument : IDocument
        {
            public void Open() => Console.WriteLine("Открыт PDF документ");
            public void Save() => Console.WriteLine("Сохранен PDF документ");
        }

        public class WordDocument : IDocument
        {
            public void Open() => Console.WriteLine("Открыт Word документ");
            public void Save() => Console.WriteLine("Сохранен Word документ");
        }

        public class ExcelDocument : IDocument
        {
            public void Open() => Console.WriteLine("Открыт Excel документ");
            public void Save() => Console.WriteLine("Сохранен Excel документ");
        }

        public abstract class DocumentCreator
        {
            public abstract IDocument CreateDocument();

            public void ProcessDocument()
            {
                var doc = CreateDocument();
                doc.Open();
                // Обработка документа
                doc.Save();
            }
        }

        public class PDFCreator : DocumentCreator
        {
            public override IDocument CreateDocument() => new PDFDocument();
        }

        public class WordCreator : DocumentCreator
        {
            public override IDocument CreateDocument() => new WordDocument();
        }     
    }
}
