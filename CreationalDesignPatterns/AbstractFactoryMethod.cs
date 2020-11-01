using System;

namespace DesignPatten.CreationalDesignPatterns
{
    /// <summary>
    /// 当工作需要多个类完成，且类之间存在依赖关系
    /// 类的错误的搭配会导致问题发生
    /// 通过抽象工厂模式记录正确的搭配
    /// 如果创建对象步骤比较复杂也可由creator辅助
    /// Factory只用于保证哪些组合是有意义的
    /// </summary>
    public class AbstractFactoryMethod : IDesignPatten
    {
        public void Run()
        {
            ClientMethod(new Microscope3000());
            ClientMethod(new Microscope5000());
        }
        public void ClientMethod(IAbstractMicroscopeFactory factory)
        {
            var eyepiece = factory.CreateEyepiece();
            var objectGlass = factory.CreateObjectGlass();

            Console.WriteLine($"当前放大倍数 {objectGlass.MagnificationFactor(eyepiece)}");
        }
    }


    /// <summary>
    /// 目镜
    /// </summary>
    public abstract class Eyepiece
    {
        public abstract double MagnificationFactor();
    }
    /// <summary>
    /// 物镜
    /// </summary>
    public abstract class ObjectGlass
    {
        public abstract double MagnificationFactor(Eyepiece eyepiece);
    }

    class Eyepiece10 : Eyepiece
    {
        public override double MagnificationFactor()
        {
            return 10;
        }
    }
    class Eyepiece30 : Eyepiece
    {
        public override double MagnificationFactor()
        {
            return 30;
        }
    }

    class ObjectGlass100 : ObjectGlass
    {
        public override double MagnificationFactor(Eyepiece eyepiece)
        {
            return 100 * eyepiece.MagnificationFactor();
        }
    }
    class ObjectGlass500 : ObjectGlass
    {
        public override double MagnificationFactor(Eyepiece eyepiece)
        {
            return 500 * eyepiece.MagnificationFactor();
        }
    }

    /// <summary>
    /// 这里假设过高或过低的目物镜组合都无意义
    /// </summary>
    public abstract class IAbstractMicroscopeFactory
    {
        public abstract Eyepiece CreateEyepiece();

        public abstract ObjectGlass CreateObjectGlass();
    }

    /// <summary>
    /// 在类名中反映组合的特征
    /// </summary>
    class Microscope5000 : IAbstractMicroscopeFactory
    {
        public override Eyepiece CreateEyepiece()
        {
            return new Eyepiece10();
        }

        public override ObjectGlass CreateObjectGlass()
        {
            return new ObjectGlass500();
        }
    }

    class Microscope3000 : IAbstractMicroscopeFactory
    {
        public override Eyepiece CreateEyepiece()
        {
            return new Eyepiece30();
        }

        public override ObjectGlass CreateObjectGlass()
        {
            return new ObjectGlass100();
        }
    }
}
