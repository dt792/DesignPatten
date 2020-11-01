using System;
using System.Collections;
using System.Collections.Generic;

namespace DesignPatten.BehaviourPattens
{
    /// <summary>
    /// 让你能在不暴露集合底层表现形式 （列表、 栈和树等） 的情况下遍历集合中所有的元素。
    /// </summary>
    class IteratorPatten : IDesignPatten
    {
        public void Run()
        {
            SugerPackage sugerPackage = new SugerPackage();
            foreach (var suger in sugerPackage)
            {
                Console.WriteLine(suger);
            }
            Console.WriteLine("After shake");
            sugerPackage.Shake();
            foreach (var suger in sugerPackage)
            {
                Console.WriteLine(suger);
            }
            Console.WriteLine("After reorder");
            sugerPackage.ReOrder();
            foreach (var suger in sugerPackage)
            {
                Console.WriteLine(suger);
            }
        }

        abstract class Iterator : IEnumerator
        {
            object IEnumerator.Current => Current();

            // Returns the key of the current element
            public abstract int Key();

            // Returns the current element
            public abstract object Current();

            // Move forward to next element
            public abstract bool MoveNext();

            // Rewinds the Iterator to the first element
            public abstract void Reset();
        }
        class OrderIterator : Iterator
        {
            int position = -1;
            List<string> sugers;
            public OrderIterator(SugerPackage sugerPackage)
            {
                this.sugers = sugerPackage.getItems();
            }
            public override object Current()
            {
                return sugers[position];
            }

            public override int Key()
            {
                return position;
            }
            /// <summary>
            /// 关键在于如何实现这个函数
            /// </summary>
            /// <returns></returns>
            public override bool MoveNext()
            {
                if (position < sugers.Count - 1)
                {
                    position++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public override void Reset()
            {
                this.position = 0;
            }
        }
        class RandomIterator : Iterator
        {
            int position = -1;
            List<string> sugers;
            public RandomIterator(SugerPackage sugerPackage)
            {
                this.sugers = sugerPackage.getItems();
            }
            public override object Current()
            {
                Random rd = new Random();
                int randomPos = rd.Next(0, sugers.Count);
                return sugers[randomPos];
            }

            public override int Key()
            {
                return position;
            }

            public override bool MoveNext()
            {

                if (position < sugers.Count - 1)
                {
                    position++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public override void Reset()
            {
                position = -1;
            }
        }
        abstract class IteratorAggregate : IEnumerable
        {
            public abstract IEnumerator GetEnumerator();
        }
        class SugerPackage : IteratorAggregate
        {
            bool isOrdered = true;
            List<string> sugers = new List<string>()
            {
                "coffee",
                "coffee",

                "coffee","coffee",

                "apple","apple","apple","apple",

                "orange","orange","orange","orange",

            };
            public void Shake()
            {
                isOrdered = false;
            }
            public void ReOrder()
            {
                isOrdered = true;
            }
            public List<string> getItems()
            {
                return sugers;
            }

            public override IEnumerator GetEnumerator()
            {
                if (isOrdered)
                {
                    return new OrderIterator(this);
                }
                else
                {
                    return new RandomIterator(this);
                }
            }
        }
    }
}
