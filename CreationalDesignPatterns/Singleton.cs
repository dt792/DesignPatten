using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatten.CreationalDesignPatterns
{
    /// <summary>
    /// 如果程序中的某个类对于所有客户端只有一个可用的实例， 可以使用单例模式。
    /// 如果你需要更加严格地控制全局变量， 可以使用单例模式。
    /// 相关资料
    /// https://www.cnblogs.com/pangblog/p/3357966.html
    /// https://www.cnblogs.com/zh7791/p/7930342.html
    /// </summary>
    class Singleton : IDesignPatten
    {
        public void Run()
        {
            var manager1 = MemoryResourceManager.GetMemoryResourceManager();
            var manager2 = MemoryResourceManager.GetMemoryResourceManager();
            Console.WriteLine($"对象1 2是否为同一对象{manager1==manager2}");

            Console.WriteLine("---------多线程测试----------");

            //多线程状态下
            TaskFactory taskFactory = new TaskFactory();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(taskFactory.StartNew(()=> 
                {
                    Thread.CurrentThread.IsBackground = true;
                    var propManager = MemoryResourceManagerProp.MemoryResourceManager;
                    var lockManager = MemoryResourceManagerLock.GetMemoryResourceManager();
                    var lazyManager = MemoryResourceManagerLazy.GetMemoryResourceManager();
                    Console.WriteLine($"懒版本剩余内存{lazyManager.GetLeftMemory()}");
                    Console.WriteLine($"属性版本剩余内存{propManager.GetLeftMemory()}");
                }));
            }
            var propManager = MemoryResourceManagerProp.MemoryResourceManager;
            Console.WriteLine($"属性版本剩余内存{propManager.GetLeftMemory()}");
            var lazyManager = MemoryResourceManagerLazy.GetMemoryResourceManager();
            Console.WriteLine($"懒版本剩余内存{lazyManager.GetLeftMemory()}");

            Console.WriteLine("分析： 由于懒版本没有实现线程安全 另外我还没搞懂多线程（完）");
        }
    }
    /// <summary>
    /// 假设有个资源管理者负责内存资源
    /// </summary>
    class MemoryResourceManager
    {
        static MemoryResourceManager manager;
        /// <summary>
        /// 隐藏构造函数
        /// </summary>
        private MemoryResourceManager()
        {
            //do sth.
        }
        public static MemoryResourceManager GetMemoryResourceManager()
        {
            if (manager is null)
            {
                manager = new MemoryResourceManager();
            }
            return manager;
        }
    }

    /// <summary>
    /// 上锁版本
    /// </summary>
    class MemoryResourceManagerLock
    {
        static MemoryResourceManagerLock manager;
        private static object singletonLock = new object();
        /// <summary>
        /// 隐藏构造函数
        /// </summary>
        private MemoryResourceManagerLock()
        {
            //do sth.
        }
        public static MemoryResourceManagerLock GetMemoryResourceManager()
        {
            if (manager is null)
            {
                lock (singletonLock)
                {
                    Console.WriteLine("创建对象前-加锁版本");
                    if (manager is null)
                    {
                        Console.WriteLine("创建对象-加锁版本");
                        manager = new MemoryResourceManagerLock();
                    }
                        
                }

            }
            return manager;
        }
    }

    /// <summary>
    /// 利用属性实现版本 无锁且线程安全
    /// </summary>
    public class MemoryResourceManagerProp
    {
        private static readonly MemoryResourceManagerProp manager =
            new MemoryResourceManagerProp();
        /// <summary>
        /// 隐藏构造函数
        /// </summary>
        private MemoryResourceManagerProp()
        {
            Console.WriteLine("利用属性版本");
            //do sth.
        }
        public static MemoryResourceManagerProp MemoryResourceManager
        {
            get { return manager; }
        }
        int leftMemory = 20;
        public int GetLeftMemory() => leftMemory--;
    }

    /// <summary>
    /// 懒加载版本
    /// </summary>
    sealed class MemoryResourceManagerLazy
    {
        private static readonly Lazy<MemoryResourceManagerLazy> instanceLock =
                      new Lazy<MemoryResourceManagerLazy>(() => new MemoryResourceManagerLazy());
        /// <summary>
        /// 隐藏构造函数
        /// </summary>
        private MemoryResourceManagerLazy()
        {
            Console.WriteLine("创建懒加载对象中");
            //do sth.
        }
        public static MemoryResourceManagerLazy GetMemoryResourceManager()
        {
            return instanceLock.Value;
        }

        int leftMemory = 20;
        public int GetLeftMemory() => leftMemory--;
    }
}
