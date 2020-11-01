


using System;

namespace DesignPatten.BehaviourPattens
{
    /// <summary>
    /// 允许你将请求沿着处理者链进行发送。 收到请求后， 每个处理者均可对请求进行处理， 或将其传递给链上的下个处理者。
    /// 当程序需要使用不同方式处理不同种类请求， 而且请求类型和顺序预先未知时， 可以使用责任链模式。
    /// 每个handler可传可不传，这里handler都能独立解决故解决后不再传递到下一个handler
    /// </summary>
    class ChainOfResponsibility : IDesignPatten
    {
        public void Run()
        {
            var errorHandler = new SimpleErrorCodeHandler();
            errorHandler.SetNext(new NormalErrorCodeHandler()).SetNext(new AdvancedErrorCodeHandler());
            Console.WriteLine("errorcode 200 "+  errorHandler.Handle("200"));
            Console.WriteLine("errorcode 500 " + errorHandler.Handle("500"));
        }
        /// <summary>
        /// 多种责任链都可以实现这个接口
        /// </summary>
        interface IHandler
        {
            IHandler SetNext(IHandler handler);

            object Handle(object Request);
        }

        abstract class ErrorCodeHandler : IHandler
        {
            /// <summary>
            /// 如果这里是个列表是不是能构建复杂的责任链树
            /// </summary>
            IHandler nextHandler;
            /// <summary>
            /// 子类需要重写，若没重写则会自动传递到下一handler
            /// </summary>
            /// <param name="request"></param>
            /// <returns></returns>
            public virtual object Handle(object request)
            {
                if (this.nextHandler != null)
                {
                    return nextHandler.Handle(request);
                }
                else
                {
                    return null;
                }
            }

            public IHandler SetNext(IHandler handler)
            {
                nextHandler = handler;
                return nextHandler;
            }

        }

        class SimpleErrorCodeHandler : ErrorCodeHandler
        {
            public override object Handle(object request)
            {
                if((request as string ).StartsWith("10"))
                {
                    return "Handled by simpleHandle";
                }
                else
                {
                    return base.Handle(request);
                }
                
            }
        }
        class NormalErrorCodeHandler : ErrorCodeHandler
        {
            public override object Handle(object request)
            {
                if ((request as string).StartsWith("20") || (request as string).StartsWith("30"))
                {
                    return "Handled by NormalErrorCodeHandler";
                }
                else
                {
                    return base.Handle(request);
                }

            }
        }

        class AdvancedErrorCodeHandler : ErrorCodeHandler
        {
            public override object Handle(object request)
            {
                if ((request as string).StartsWith("50"))
                {
                    return "Handled by AdvancedErrorCodeHandler";
                }
                else
                {
                    return base.Handle(request);
                }

            }
        }
    }
}
