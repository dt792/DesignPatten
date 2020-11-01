using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatten.StructureDesignPattens
{
    /// <summary>
    /// 如果你需要实现树状对象结构， 可以使用组合模式。
    /// 确保应用的核心模型能够以树状结构表示。 尝试将其分解为简单元素和容器。 
    /// 记住， 容器必须能够同时包含简单元素和其他容器。
    /// </summary>
    class Composite :IDesignPatten
    {
        public void Run()
        {
            TextComposite composite = new TextComposite();
            composite.Add(new TextLeafA());
            composite.Add(new TextLeafC());
            composite.Add(new TextLeafB());
            TextComposite branch = new TextComposite();
            branch.Add(new TextLeafB());
            branch.Add(new TextLeafC());
            branch.Add(new TextLeafA());
            composite.Add(branch);
            composite.Write();
        }

        abstract class TextComponent
        {
            public abstract void Write();
            public virtual void Add(TextComponent component) { throw new NotImplementedException(); }
            public virtual void Remove(TextComponent component) { throw new NotImplementedException(); }
            public virtual bool IsComposite()=>true;
        }

        class TextLeafA : TextComponent
        {
            public override void Write()
            {
                Console.WriteLine("Life is good");
            }

            public override bool IsComposite()
            {
                return false;
            }
        }
        class TextLeafB : TextComponent
        {
            public override void Write()
            {
                Console.WriteLine("Wife is good");
            }

            public override bool IsComposite()
            {
                return false;
            }
        }

        class TextLeafC : TextComponent
        {
            public override void Write()
            {
                Console.WriteLine(" and ");
            }

            public override bool IsComposite()
            {
                return false;
            }
        }
        ///列表在此类定义而非基类
        ///利用IsComposite判断是否为组合节点(本例没有用上)
        class TextComposite : TextComponent
        {
            
            List<TextComponent> textComponents = new List<TextComponent>();

            public override void Add(TextComponent component)
            {
                textComponents.Add(component);
            }

            public override void Remove(TextComponent component)
            {
                textComponents.Remove(component);
            }
            public override void Write()
            {
                foreach (var component in textComponents)
                {
                    component.Write();
                }
            }
        }
    }
}
