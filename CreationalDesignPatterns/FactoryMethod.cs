using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DesignPatten.CreationalDesignPatterns
{
    /// <summary>
    /// 工厂方法
    /// </summary>
    public class FactoryMethod : IDesignPatten
    {
        public void Run()
        {
            ClientCode(new HuaWeiPhoneCreator());
            ClientCode(new XiaoMiPhoneCreator());
        }

        void ClientCode(PhoneCreator creator)
        {
            // ...
            Console.WriteLine("Client: I'm not aware of the creator's class," +
                "but it still works.\n" );
            Phone phone = creator.CreatePhone();
            phone.Call("apple");
            Console.WriteLine(phone.GetSellingInfo());
        }
    }
    /// <summary>
    /// 用抽象类或接口将一系列类的统一特点抽象出来
    /// </summary>
    abstract class Phone
    {
        public abstract string GetSellingInfo();
        public void Call(string target) 
        {
            Console.WriteLine($"calling {target}...");
        }
    }

    class XiaoMiPhone : Phone
    {
        public override string GetSellingInfo()
        {
            return "XiaoMiPhone sells 1000";
        }
    }
    class HuaWeiPhone : Phone
    {
        public override string GetSellingInfo()
        {
            return "HuaWeiPhone sells 2000";
        }
    }
    /// <summary>
    /// 当创建对象的方式较为复杂时
    /// 考虑建立Creator类负责创建对象
    /// 使用者不需要关心如何创建类
    /// 即分离了类的创建与使用
    /// </summary>
    abstract class PhoneCreator
    {
        public abstract Phone CreatePhone();
    }
    class XiaoMiPhoneCreator : PhoneCreator
    {
        public override Phone CreatePhone()
        {
            return new XiaoMiPhone();
        }
    }
    class HuaWeiPhoneCreator : PhoneCreator
    {
        public override Phone CreatePhone()
        {
            return new HuaWeiPhone();
        }
    }
}
