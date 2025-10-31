using System;

namespace DecoratorLab
{
    public interface IBeverage
    {
        double GetCost();
        string GetDescription();
    }

    public class Coffee : IBeverage
    {
        public double GetCost()
        {
            return 50.0;
        }

        public string GetDescription()
        {
            return "Кофе";
        }
    }
    public class Tea : IBeverage
    {
        public double GetCost() => 40.0;
        public string GetDescription() => "Чай";
    }

    public class Latte : IBeverage
    {
        public double GetCost() => 60.0;
        public string GetDescription() => "Латте";
    }

    public abstract class BeverageDecorator : IBeverage
    {
        protected IBeverage _beverage;

        public BeverageDecorator(IBeverage beverage)
        {
            _beverage = beverage;
        }

        public virtual double GetCost() => _beverage.GetCost();
        public virtual string GetDescription() => _beverage.GetDescription();
    }

    public class MilkDecorator : BeverageDecorator
    {
        public MilkDecorator(IBeverage beverage) : base(beverage) { }

        public override double GetCost() => base.GetCost() + 10.0;
        public override string GetDescription() => base.GetDescription() + " с молоком";
    }

    public class SugarDecorator : BeverageDecorator
    {
        public SugarDecorator(IBeverage beverage) : base(beverage) { }

        public override double GetCost() => base.GetCost() + 5.0;
        public override string GetDescription() => base.GetDescription() + " с сахаром";
    }

    public class ChocolateDecorator : BeverageDecorator
    {
        public ChocolateDecorator(IBeverage beverage) : base(beverage) { }

        public override double GetCost() => base.GetCost() + 15.0;
        public override string GetDescription() => base.GetDescription() + " с шоколадом";
    }

    public class VanillaDecorator : BeverageDecorator
    {
        public VanillaDecorator(IBeverage beverage) : base(beverage) { }

        public override double GetCost() => base.GetCost() + 12.0;
        public override string GetDescription() => base.GetDescription() + " с ванилью";
    }

    public class CinnamonDecorator : BeverageDecorator
    {
        public CinnamonDecorator(IBeverage beverage) : base(beverage) { }

        public override double GetCost() => base.GetCost() + 8.0;
        public override string GetDescription() => base.GetDescription() + " с корицей";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Кафе");
            Console.WriteLine("Выберите напиток:");
            Console.WriteLine("1 - Кофе");
            Console.WriteLine("2 - Чай");
            Console.WriteLine("3 - Латте");
            Console.Write("Ваш выбор: ");
            int choice = int.Parse(Console.ReadLine() ?? "1");

            IBeverage beverage = choice switch
            {
                1 => new Coffee(),
                2 => new Tea(),
                3 => new Latte(),
                _ => new Coffee()
            };

            bool adding = true;
            while (adding)
            {
                Console.WriteLine("\nДобавить что-то?");
                Console.WriteLine("1 - Молоко");
                Console.WriteLine("2 - Сахар");
                Console.WriteLine("3 - Шоколад");
                Console.WriteLine("4 - Ваниль");
                Console.WriteLine("5 - Корица");
                Console.WriteLine("0 - Готово");
                Console.Write("Ваш выбор: ");
                int addChoice = int.Parse(Console.ReadLine() ?? "0");

                switch (addChoice)
                {
                    case 1: beverage = new MilkDecorator(beverage); break;
                    case 2: beverage = new SugarDecorator(beverage); break;
                    case 3: beverage = new ChocolateDecorator(beverage); break;
                    case 4: beverage = new VanillaDecorator(beverage); break;
                    case 5: beverage = new CinnamonDecorator(beverage); break;
                    case 0: adding = false; break;
                }
            }

            Console.WriteLine($"\nВаш заказ: {beverage.GetDescription()}");
            Console.WriteLine($"Итоговая стоимость: {beverage.GetCost()} тг");
        }
    }
}
