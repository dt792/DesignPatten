using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatten.StructureDesignPattens
{
    /// <summary>
    ///  适配器模式通常在已有程序中使用， 让相互不兼容的类能很好地合作。
    ///  甚至还能加点新功能
    ///  如果您需要复用这样一些类， 他们处于同一个继承体系， 并且他们又有了额外的一些共同的方法，
    ///  但是这些共同的方法不是所有在这一继承体系中的子类所具有的共性。
    /// </summary>
    class Adapter : IDesignPatten
    {
        private interface ICalculateResult
        {
            string GetResult(string expr);
        }

        /// <summary>
        /// 加5的计算，甚至没有安全检查
        /// </summary>
        private class OldVersionCaltor
        {
            public string Plus5(string expr)
            {
                return (double.Parse(expr)+5).ToString();
            }
        }
        private class CalAdapter : ICalculateResult
        {
            public CalAdapter(OldVersionCaltor caltor)
            {
                OldVersionCaltor = caltor;
            }
            OldVersionCaltor OldVersionCaltor;
            public string GetResult(string expr)
            {
                try
                {
                    double.Parse(expr);
                }
                catch (Exception)
                {
                    return "error";
                }
                return OldVersionCaltor.Plus5(expr);
            }
        }
        public void Run()
        {
            OldVersionCaltor oldVersion = new OldVersionCaltor();
            ICalculateResult calculate = new CalAdapter(oldVersion);
            Console.WriteLine(calculate.GetResult("20"));
            Console.WriteLine(calculate.GetResult("(￣、￣)"));
        }
    }
}
