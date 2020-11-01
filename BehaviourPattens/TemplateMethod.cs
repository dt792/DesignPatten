using System;

namespace DesignPatten.BehaviourPattens
{
    class TemplateMethod:IDesignPatten
    {
        public void Run()
        {
            AITemplate catAI = new CatAI();
            AITemplate dogAI = new DogAI();
            catAI.RunTempletePlane();
            dogAI.RunTempletePlane();
        }

        abstract class AITemplate 
        {
            public void RunTempletePlane()
            {
                ShowInfo();
                Build();
                Attack();
            }
            /// <summary>
            /// 必须操作且无法更改
            /// </summary>
            protected void ShowInfo()
            {
                System.Console.WriteLine($"now={DateTime.Now}");
            }
            /// <summary>
            /// 有默认实现，如有需要也可重写
            /// </summary>
            protected virtual void Build() { }
            //没有默认实现子类AI必须完善才能使用
            protected abstract void Attack();
        }
        class CatAI : AITemplate
        {
            protected override void Attack()
            {
                Console.WriteLine("爪巴");
            }
        }
        class DogAI : AITemplate
        {
            protected override void Build()
            {
                Console.WriteLine("Dog build houses");
            }
            protected override void Attack()
            {
                Console.WriteLine("wroof,wroof");
            }
        }

    }
}
