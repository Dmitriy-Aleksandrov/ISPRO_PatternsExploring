using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Explore_Patterns.singleton;
using static Explore_Patterns.abstract_fabric;
using static Explore_Patterns.buildercs;
using static Explore_Patterns.fabric_methot;
using static Explore_Patterns.prototype;

namespace Explore_Patterns
{
    class Program
    {
        static void Awaiter()
        {
            var aaaaaaa = Console.ReadLine();
        }
        static void Main(string[] args)
        {
            int aa = 0;
            
            while(aa != 13)
            {
                Console.WriteLine("1 - одиночка;\n2 - прототип;\n3 - абстрактная фабрика;\n4 - строитель;\n5 - фабричный метод;\n6 - фасад;\n7 - наблюдатель");
                aa = int.Parse(Console.ReadLine());
                switch (aa)
                {
                    case 1:
                        var db1 = singleton.GetInstance();
                        var db2 = singleton.GetInstance();

                        Console.WriteLine(db1 == db2);

                        db1.Query("SELECT * FROM Users");

                        AppSettings.Instance.Theme = "Dark";
                        Console.WriteLine(AppSettings.Instance.Theme);
                        break;
                    case 2:
                        Circle prototypeCircle = new Circle(10, 0, 0, "Red");

                        // Клонируем и изменяем
                        Circle circle1 = (Circle)prototypeCircle.Clone();
                        circle1.X = 100;
                        circle1.Y = 50;
                        circle1.Color = "Blue";

                        Circle circle2 = (Circle)prototypeCircle.Clone();
                        circle2.Radius = 20;
                        circle2.X = 200;

                        prototypeCircle.Draw();
                        circle1.Draw();
                        circle2.Draw();

                        // Хранение прототипов в словаре
                        var prototypes = new Dictionary<string, Shape>();
                        prototypes["circle"] = new Circle(5, 0, 0, "Black");
                        prototypes["rectangle"] = new Rectangle(10, 20, 0, 0, "Green");

                        Shape clonedCircle = prototypes["circle"].Clone();
                        clonedCircle.Draw();
                        break;
                    case 3:
                        string os = "Windows"; // или "Mac"

                        IGUIFactory factory = os == "Windows"
                            ? (IGUIFactory)new WindowsFactory()
                            : (IGUIFactory)new MacFactory();

                        Application app = new Application(factory);
                        app.RenderUI();
                        app.HandleUserInput();
                        break;
                    case 4:
                        var gamingPC = new ComputerBuilder()
                        .SetCPU("Intel i9-13900K")
                        .SetRAM("32GB DDR5")
                        .SetStorage("1TB NVMe SSD")
                        .SetGPU("RTX 4090")
                        .SetMotherboard("Z790")
                        .AddPeripheral("Gaming Mouse")
                        .Build();

                        gamingPC.ShowSpecs();

                        // Использование директора
                        var director = new ComputerDirector();
                        var builder = new ComputerBuilder();

                        Computer officePC = director.BuildOfficeComputer(builder);
                        officePC.ShowSpecs();

                        // Пример с текстовым строителем
                        string document = new TextBuilder()
                            .AddHeader("Документация")
                            .AddParagraph("Это пример использования паттерна Строитель")
                            .AddList("Простота", "Гибкость", "Переиспользование")
                            .Build();

                        Console.WriteLine(document);
                        break;
                    case 5:
                        Console.WriteLine("=== Фабричный метод ===");
                        string deliveryType = "sea";
                        Logistics logistics;
                        switch (deliveryType)
                        {
                            case "road":
                                logistics = new RoadLogistics();
                                break;
                            case "sea":
                                logistics = new SeaLogistics();
                                break;
                            case "air":
                                logistics = new AirLogistics();
                                break;
                            default:
                                throw new ArgumentException("Неверный тип");
                        }

                        logistics.PlanDelivery();
                        DocumentCreator creator = new PDFCreator();
                        creator.ProcessDocument();
                        var newFactory = new TransportFactory();
                        var transport = newFactory.CreateTransport("truck");
                        transport.Deliver();
                        break;
                    case 6:
                        RestaurantFacade restaurant = new RestaurantFacade();
                        restaurant.OrderDelivery("Иван", "Пицца Маргарита","ул. Ленина 10", "+7-999-123-4567",
                                                 "ivan@mail.ru", 500);

                        restaurant.OrderPickup("Мария", "Салат Цезарь", 300);
                        break;
                    case 7:
                        Stock apple = new Stock("AAPL", 150.0);

                        Trader trader1 = new Trader("Иван", 145.0);
                        Trader trader2 = new Trader("Мария", 140.0);
                        AlertSystem alert = new AlertSystem(5.0);
                        Logger logger = new Logger();

                        apple.RegisterObserver(trader1);
                        apple.RegisterObserver(trader2);
                        apple.RegisterObserver(alert);
                        apple.RegisterObserver(logger);

                        apple.SetPrice(148.0);
                        apple.SetPrice(142.0);
                        apple.SetPrice(155.0);

                        apple.RemoveObserver(trader2);
                        apple.SetPrice(152.0);
                        break;

                }
                Awaiter();
                Console.Clear();
            }






            
        }
    }
}
