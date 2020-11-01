using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatten.BehaviourPattens
{
    /// <summary>
    /// 好吧也不是很懂
    /// 如果为了能在不同情景下复用一些基本行为， 导致你需要被迫创建大量组件子类时， 可使用中介者模式。
    /// 由于所有组件间关系都被包含在中介者中， 因此你无需修改组件就能方便地新建中介者类以定义新的组件合作方式。
    /// 当组件因过于依赖其他组件而无法在不同应用中复用时， 可使用中介者模式。
    /// 应用中介者模式后， 每个组件不再知晓其他组件的情况。 尽管这些组件无法直接交流， 但它们仍可通过中介者对象进行间接交流。 如果你希望在不同应用中复用一个组件， 则需要为其提供一个新的中介者类。
    /// </summary>
    class MediatorPatten : IDesignPatten
    {
        public void Run()
        {
            Component1 component1 = new Component1();
            Component2 component2 = new Component2();
            new ConcreteMediator(component1, component2);

            Console.WriteLine("Client triggets operation A.");
            component1.DoA();

            Console.WriteLine();

            Console.WriteLine("Client triggers operation D.");
            component2.DoD();
        }
        public interface IMediator
        {
            void Notify(object sender, string ev);
        }

        // Concrete Mediators implement cooperative behavior by coordinating several
        // components.
        class ConcreteMediator : IMediator
        {
            private Component1 _component1;

            private Component2 _component2;

            public ConcreteMediator(Component1 component1, Component2 component2)
            {
                this._component1 = component1;
                this._component1.SetMediator(this);
                this._component2 = component2;
                this._component2.SetMediator(this);
            }

            public void Notify(object sender, string ev)
            {
                if (ev == "A")
                {
                    Console.WriteLine("Mediator reacts on A and triggers folowing operations:");
                    this._component2.DoC();
                }
                if (ev == "D")
                {
                    Console.WriteLine("Mediator reacts on D and triggers following operations:");
                    this._component1.DoB();
                    this._component2.DoC();
                }
            }
        }

        // The Base Component provides the basic functionality of storing a
        // mediator's instance inside component objects.
        class BaseComponent
        {
            protected IMediator _mediator;

            public BaseComponent(IMediator mediator = null)
            {
                this._mediator = mediator;
            }

            public void SetMediator(IMediator mediator)
            {
                this._mediator = mediator;
            }
        }

        // Concrete Components implement various functionality. They don't depend on
        // other components. They also don't depend on any concrete mediator
        // classes.
        class Component1 : BaseComponent
        {
            public void DoA()
            {
                Console.WriteLine("Component 1 does A.");

                this._mediator.Notify(this, "A");
            }

            public void DoB()
            {
                Console.WriteLine("Component 1 does B.");

                this._mediator.Notify(this, "B");
            }
        }

        class Component2 : BaseComponent
        {
            public void DoC()
            {
                Console.WriteLine("Component 2 does C.");

                this._mediator.Notify(this, "C");
            }

            public void DoD()
            {
                Console.WriteLine("Component 2 does D.");

                this._mediator.Notify(this, "D");
            }
        }
    }
}
