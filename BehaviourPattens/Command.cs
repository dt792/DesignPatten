using System;

namespace DesignPatten.BehaviourPattens
{
    /// <summary>
    /// 也是不太懂实用
    /// 如果你想要将操作放入队列中、 操作的执行或者远程执行操作， 可使用命令模式
    /// 如果你想要实现操作回滚功能， 可使用命令模式。
    /// </summary>
    class Command : IDesignPatten
    {
        public void Run()
        {
            ICommand showCommand = new ShowCommand("20小锤");
            ICommand saveCommand1 = new SaveCommand(new DBSaver(), "20小锤");
            ICommand saveCommand2 = new SaveCommand(new LocalSaver(), "20小锤");
            ShowAndSaveInvoker invoker = new ShowAndSaveInvoker();
            invoker.SetSaveCommand(saveCommand1);
            invoker.SetShowCommand(showCommand);
            invoker.ShowAndSave();
            Console.WriteLine("test another saver");
            invoker.SetSaveCommand(saveCommand2);
            invoker.ShowAndSave();
        }
        interface IContentSaver {
            bool SaveContent(string content);
        }

        class DBSaver : IContentSaver
        {
            public bool SaveContent(string content)
            {
                Console.WriteLine("DB has save this "+content);
                return true;
            }
        }

        class LocalSaver : IContentSaver
        {
            public bool SaveContent(string content)
            {
                Console.WriteLine("Localtext has save this " + content);
                return true;
            }
        }

        interface ICommand
        {
            void Excute();
        }
        /// <summary>
        /// 以构造函数传入参数确保Command能执行
        /// </summary>
        class SaveCommand : ICommand
        {
            string content;
            IContentSaver contentSaver;
            public SaveCommand(IContentSaver contentSaver,string content)
            {
                this.contentSaver = contentSaver;
                this.content = content;
            }
            public void Excute()
            {
                contentSaver.SaveContent(content);
            }
        }
        class ShowCommand : ICommand
        {
            string content;
            public ShowCommand(string content)
            {
                this.content = content;
            }
            public void Excute()
            {
                Console.WriteLine("the content is "+content);
            }
        }
        /// <summary>
        /// 多命令组合
        /// </summary>
        class ShowAndSaveInvoker {
            ICommand showCommand;
            ICommand saveCommand;
            public ShowAndSaveInvoker()
            {
                
            }
            public void SetShowCommand(ICommand command) => showCommand = command;
            public void SetSaveCommand(ICommand command) => saveCommand = command;
            public void ShowAndSave()
            {
                Console.WriteLine("Invoker trying to show and save content");
                showCommand.Excute();
                saveCommand.Excute();
            }
        }




    }
}
