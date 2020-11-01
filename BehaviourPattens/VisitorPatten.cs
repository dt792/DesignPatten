using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatten.BehaviourPattens
{
    /// <summary>
    /// 允许你在不修改已有代码的情况下向已有类层次结构中增加新的行为。
    /// 通过实现一个最小接口只包含accept（）函数 满足新的功能
    /// 新的功能由visitor实现
    /// 由于visitor需要对不同子类分别操作。。好像也可以利用反射获取子类的信息
    /// </summary>
    class VisitorPatten : IDesignPatten
    {
        public void Run()
        {
            List<IComponent> components = new List<IComponent>() { new TextNode(), new VideoNode() };
            IInfoVisitor visitor = new SimpleVisitor();
            foreach (var node in components)
            {
                node.Accept(visitor);
            }
        }
        public interface IComponent
        {
            void Accept(IInfoVisitor visitor);
        }

        public class TextNode : IComponent
        {
            public void Accept(IInfoVisitor visitor)
            {
                visitor.VisitTextNode(this);
            }
        }

        public class VideoNode : IComponent
        {
            public void Accept(IInfoVisitor visitor)
            {
                visitor.VisitVideoNode(this);
            }
        }

        public interface IInfoVisitor
        {
            void VisitTextNode(TextNode element);

            void VisitVideoNode(VideoNode element);
        }

        class SimpleVisitor : IInfoVisitor
        {
            public void VisitTextNode(TextNode element)
            {
                Console.WriteLine("visiting textnode"+ element);
            }

            public void VisitVideoNode(VideoNode element)
            {
                Console.WriteLine("visiting videonode" + element);
            }
        }
    }
}
