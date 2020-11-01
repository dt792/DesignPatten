using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DesignPatten.CreationalDesignPatterns
{
    /// <summary>
    /// 原型是一种创建型设计模式， 使你能够复制对象， 甚至是复杂对象， 而又无需使代码依赖它们所属的类。
    /// </summary>
    public class Prototype : IDesignPatten
    {
        

        public void Run()
        {
            SubClass sub = new SubClass() { index = 5 };
            SthNeedToClone org = new SthNeedToClone() { index = 1, subClass = sub };
            SthNeedToClone shallowClone = org.ShallowClone();
            SthNeedToClone deepClone = org.DeepClone();
            Console.WriteLine($"is org.subClass==shallowClone.subClass->{org.subClass == shallowClone.subClass}");
            Console.WriteLine($"is org.subClass==deepClone.subClass->{org.subClass == deepClone.subClass}");



        }
    }
    /// <summary>
    /// 一种实现方式
    /// </summary>
    [Serializable]
    class SthNeedToClone
    {
        public int index { get; set; }
        public SubClass subClass { get; set; }
        /// <summary>
        /// 利用object的浅引用复制
        /// 如果字段是值类型的，则对该字段执行逐位复制。
        /// 如果字段是引用类型，则复制引用但不复制引用的对象。
        /// 因此，原始对象及其复本引用同一对象。
        /// </summary>
        /// <returns></returns>
        public SthNeedToClone ShallowClone()
        {
            return this.MemberwiseClone() as SthNeedToClone;
        }
        /// <summary>
        /// 由于深克隆需要复制引用对象。
        /// 而对于复杂类可能存在循环引用情况需要慎重处理
        /// 可以利用.Net的[Serializable]自动处理循环引用问题
        /// 但需要对所有相关的类添加[Serializable]特性
        /// </summary>
        /// <returns></returns>
        public SthNeedToClone DeepClone()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as SthNeedToClone;
        }

    }
    [Serializable]
    class SubClass
    {
        public int index;
    }

    /// <summary>
    /// 使用ICloneable表示可复制的。
    /// 但似乎不分深或浅克隆。
    /// 而且只能返回object类型
    /// </summary>
    class AnotherNeedToClone : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone() ;
        }
    }
}
