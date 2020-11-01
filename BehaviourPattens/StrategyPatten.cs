using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DesignPatten.BehaviourPattens
{
    /// <summary>
    /// 如果算法在上下文的逻辑中不是特别重要， 使用该模式能将类的业务逻辑与其算法实现细节隔离开来。
    /// 当你有许多仅在执行某些行为时略有不同的相似类时， 可使用策略模式。
    /// 客户端必须知晓策略间的不同——它需要选择合适的策略。
    /// 也可通过匿名函数实现
    /// </summary>
    class StrategyPatten : IDesignPatten
    {
        public void Run()
        {
            Calculater calculater = new Calculater();
            calculater.strategy = new SwingAddStrategy();
            List<int> intList = new List<int>() {2,3,4,5,7,8,9 };
            Console.WriteLine( calculater.DoMath(intList));
            calculater.strategy = new AddStrategy();
            Console.WriteLine(calculater.DoMath(intList));
        }
        public class Calculater
        {
            public ICalStrategy strategy;
            public object DoMath(object data)
            {
                return strategy.DoMath(data);
            }
        }
        public interface ICalStrategy
        {
            object DoMath(object data);
        }

        public class AddStrategy : ICalStrategy
        {
            public object DoMath(object data)
            {
                var intList=data as List<int>;
                if(intList != null)
                {
                    int sum=0;
                    foreach (var num in intList)
                    {
                        sum += num;
                    }
                    return sum;
                }
                return null;
                
            }
        }
        public class SwingAddStrategy : ICalStrategy
        {
            public object DoMath(object data)
            {
                var intList = data as List<int>;
                if (intList != null)
                {
                    int sum = 0;
                    for (int i = 0; i < intList.Count; i++)
                    {
                        if (i % 2 == 0)
                            sum += intList[i];
                        else
                            sum += intList[i] * 2;
                    }
                    return sum;
                }
                return null;

            }
        }
    }
}
