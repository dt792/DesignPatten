using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatten.BehaviourPattens
{
    /// <summary>
    /// 允许在不暴露对象实现细节的情况下保存和恢复对象之前的状态。
    /// 创建一个快照类，能保存工作类里的隐藏数据，具有工作类的引用
    /// 工作类通过构造函数将隐藏参数传入快照类
    /// 通过工作类的getsnapshot获取快照
    /// 需要恢复时调用快照类的restore函数
    /// </summary>
    class Memento : IDesignPatten
    {
        public void Run()
        {
            Editor editor = new Editor();
            editor.SetCurX(20);
            editor.SetContent("23");
            editor.SetCurY(90);
            var snapShot=editor.CreateSnapShot();
            editor.SetCurY(0);
            editor.ShowDetail();
            snapShot.Restore();
            editor.ShowDetail();
        }
        class Editor
        {
            string content;
            int curX, curY;
            public void SetCurX(int curX)
            {
                this.curX = curX;
            }
            public void SetCurY(int curY)
            {
                this.curY = curY;
            }
            public void SetContent(string content)
            {
                this.content = content;
            }
            public void ShowDetail()
            {
                Console.WriteLine($"curX={curX},curY={curY},content={content}");
            }
            public Snapshot CreateSnapShot()
            {
                return new Snapshot(this,curX,curY,content);
            }
        }
        class Snapshot
        {
            Editor editor;
            string content;
            int curX, curY;
            public Snapshot(Editor editor,int X,int Y,string content)
            {
                this.editor = editor;
                this.content = content;
                curX = X;
                curY = Y;
            }
            public void Restore()
            {
                editor.SetContent(content);
                editor.SetCurX(curX);
                editor.SetCurY(curY);
            }
        }
    }
}
