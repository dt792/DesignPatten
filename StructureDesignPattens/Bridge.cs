using System.Globalization;

namespace DesignPatten.StructureDesignPattens
{
    public class Bridge : IDesignPatten
    {
        public void Run()
        {
            OnlineShopping onlineShop1 = new OnlineShopping(new TwoYuanShopCounter(),new Holiday88Discounter());
            System.Console.WriteLine(onlineShop1.ShowPriceAfterDiscount("cat"));
            OnlineShopping onlineShop2 = new OnlineShopping(new TwoYuanShopCounter(), new Double11Discounter());
            System.Console.WriteLine(onlineShop2.ShowPriceAfterDiscount("cat"));
            OnlineShopping onlineShop3 = new ShoppingWithHuaBei(new TwoYuanShopCounter(), new Double11Discounter());
            System.Console.WriteLine(onlineShop3.ShowPriceAfterDiscount("cat"));
        }

        class OnlineShopping
        {
            public OnlineShopping(IPriceCounter priceCounter,IDiscounter discounter)
            {
                this.counter = priceCounter;
                this.discounter = discounter;
            }
            protected IPriceCounter counter;
            protected IDiscounter discounter;
            public virtual double ShowPriceAfterDiscount(string product)
            {
                return counter.GetPrice(product) * discounter.GetDiscount(product);
            }
        }

        interface IPriceCounter
        {
            double GetPrice(string product);
        }
        interface IDiscounter
        {
            double GetDiscount(string product);
        }

        class TwoYuanShopCounter : IPriceCounter
        {
            public double GetPrice(string product)
            {
                return 2;
            }
        }

        class Holiday88Discounter : IDiscounter
        {
            public double GetDiscount(string product)
            {
                return 0.88;
            }
        }
        class Double11Discounter : IDiscounter
        {
            public double GetDiscount(string product)
            {
                return 1.25;
            }
        }

        /// <summary>
        /// 扩展新功能
        /// </summary>
        class ShoppingWithHuaBei : OnlineShopping
        {
            public ShoppingWithHuaBei(IPriceCounter priceCounter, IDiscounter discounter):base(priceCounter,discounter)
            {
                
            }

            public override double ShowPriceAfterDiscount(string product)
            {
                
                return base.ShowPriceAfterDiscount(product)*10;
            }
        }


    }
}
