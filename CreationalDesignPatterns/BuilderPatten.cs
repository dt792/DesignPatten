using System;

namespace DesignPatten.CreationalDesignPatterns
{
    /// <summary>
    /// 有点复杂(￣、￣)
    /// 如果你需要创建的各种形式的产品， 它们的制造过程相似且仅有细节上的差异， 此时可使用生成器模式。
    /// 基本生成器接口中定义了所有可能的制造步骤， 具体生成器将实现这些步骤来制造特定形式的产品。 
    /// 同时， 主管类将负责管理制造步骤的顺序。
    /// 你可以分步创建对象， 暂缓创建步骤或递归运行创建步骤。
    /// 生成不同形式的产品时， 你可以复用相同的制造代码。
    /// 单一职责原则。 你可以将复杂构造代码从产品的业务逻辑中分离出来。
    /// </summary>
    public class BuilderPatten : IDesignPatten
    {
        public void Run()
        {
            Director director = new Director();

            ISmartPhoneBuilder huaWeiBuilder = new HuaWeiSmartPhoneBuilder();
            director.Builder = huaWeiBuilder;
            director.buildFullFeaturedPhone();
            ShowPhoneParams(huaWeiBuilder.GetPhone());
            director.buildTestPhone();
            ShowPhoneParams(huaWeiBuilder.GetPhone());

            ISmartPhoneBuilder xiaoMiBuilder = new XiaoMiSmartPhoneBuilder();
            director.Builder = xiaoMiBuilder;
            director.buildFullFeaturedPhone();
            ShowPhoneParams(xiaoMiBuilder.GetPhone());


        }
        public void ShowPhoneParams(SmartPhone phone)
        {
            Console.WriteLine($"has5G={phone.has5G}  CPU={phone.CPU} PhotoResolution={(phone.PhotoResolution is null ? 0 : phone.PhotoResolution())}");
        }
    }

    public abstract class SmartPhone
    {
        public string CPU;
        public bool has5G;
        public Func<int> PhotoResolution;
    }

    public class XiaoMiSmartPhone : SmartPhone
    {

    }

    public class HuaWeiSmartPhone : SmartPhone
    {

    }

    /// <summary>
    /// 不能在生成器接口中声明该方法， 因为不同生成器构造的产品可能没有公共接口， 因此你就不知道该方法返回的对象类型。 
    /// 但是， 如果所有产品都位于单一类层次中， 你就可以安全地在基本接口中添加获取生成对象的方法。
    /// 本次生成的所有对象都是智能手机
    /// </summary>
    public interface ISmartPhoneBuilder
    {
        void BuildCPU();
        void Build5G();
        void BuildCamera();

        SmartPhone GetPhone();
    }

    /// <summary>
    /// 构造对象由builder负责，由于以函数暴露了构造对象的过程
    /// 比工厂模式提供更多控制可能，同时也更累
    /// </summary>
    class XiaoMiSmartPhoneBuilder : ISmartPhoneBuilder
    {
        private SmartPhone product = new XiaoMiSmartPhone();
        public void Build5G()
        {
            product.has5G = false;
        }

        public void BuildCamera()
        {
            product.PhotoResolution = () => 5000;
        }

        public void BuildCPU()
        {
            product.CPU = "980";
        }
        /// <summary>
        /// 保护未构建完成的对象不被访问
        /// </summary>
        /// <returns></returns>
        private void Reset()
        {
            product = new XiaoMiSmartPhone();
        }
        /// <summary>
        /// 保护未构建完成的对象不被访问
        /// </summary>
        /// <returns></returns>
        public SmartPhone GetPhone()
        {
            SmartPhone result = this.product;

            this.Reset();

            return result;
        }
    }
    class HuaWeiSmartPhoneBuilder : ISmartPhoneBuilder
    {
        private SmartPhone product = new HuaWeiSmartPhone();
        public void Build5G()
        {
            product.has5G = true;
        }

        public void BuildCamera()
        {
            product.PhotoResolution = () => 6000;
        }

        public void BuildCPU()
        {
            product.CPU = "k980";
        }

        /// <summary>
        /// 保护未构建完成的对象不被访问
        /// </summary>
        /// <returns></returns>
        private void Reset()
        {
            product = new HuaWeiSmartPhone();
        }
        /// <summary>
        /// 保护未构建完成的对象不被访问
        /// </summary>
        /// <returns></returns>
        public SmartPhone GetPhone()
        {
            SmartPhone result = this.product;

            this.Reset();

            return result;
        }
    }

    /// <summary>
    /// 此类存储了一些常见调用模板，
    /// 使用该类能向客户端隐藏细节
    /// 不妨碍直接用builder创建对象
    /// </summary>
    public class Director
    {
        private ISmartPhoneBuilder _builder;

        public ISmartPhoneBuilder Builder
        {
            set { _builder = value; }
        }

        // The Director can construct several product variations using the same
        // building steps.
        public void buildTestPhone()
        {
            this._builder.BuildCPU();
        }

        public void buildFullFeaturedPhone()
        {
            this._builder.BuildCPU();
            this._builder.Build5G();
            this._builder.BuildCamera();
        }
    }

}
