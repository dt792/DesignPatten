using System;

namespace DesignPatten.StructureDesignPattens
{
    /// <summary>
    /// 你无需创建新子类即可扩展对象的行为。
    ///你可以在运行时添加或删除对象的功能。
    ///你可以用多个装饰封装对象来组合几种行为。
    ///在封装器栈中删除特定封装器比较困难。
    ///实现行为不受装饰栈顺序影响的装饰比较困难。
    /// </summary>
    class Decorator : IDesignPatten
    {
        public void Run()
        {
            PriceComponent situation1 = new Discount5WhenSpending20(new HolidayDiscount(new TewntyYuanBasePrice()));

            Console.WriteLine($"先打折再满减 共计{situation1.Calculate()}");

            PriceComponent situation2 = new HolidayDiscount(new Discount5WhenSpending20(new TewntyYuanBasePrice()));

            Console.WriteLine($"先满减再打折 共计{situation2.Calculate()}");
        }
        abstract class PriceComponent
        {
            public abstract double Calculate();
        }

        class TewntyYuanBasePrice : PriceComponent
        {
            public override double Calculate()
            {
                return 20;
            }
        }
        abstract class PriceDecorator : PriceComponent
        {
            protected PriceComponent component;
            public PriceDecorator(PriceComponent component)
            {
                this.component = component;
            }
        }
        class HolidayDiscount : PriceDecorator
        {
            public HolidayDiscount(PriceComponent component) : base(component)
            {

            }
            public override double Calculate()
            {
                return component.Calculate() * 0.85;
            }
        }
        class Discount5WhenSpending20 : PriceDecorator
        {
            public Discount5WhenSpending20(PriceComponent component) : base(component)
            {

            }
            public override double Calculate()
            {
                if (component.Calculate() >= 20)
                    return component.Calculate() - 5;
                return component.Calculate();
            }
        }
    }
}
