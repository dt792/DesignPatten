using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatten.BehaviourPattens
{
    /// <summary>
    /// 状态模式建议为对象的所有可能状态新建一个类， 然后将所有状态的对应行为抽取到这些类中。
    /// 在状态模式中， 特定状态知道其他所有状态的存在， 且能触发从一个状态到另一个状态的转换；
    /// 状态模式可通过受外部控制且能根据对象状态改变行为的方法来识别。
    /// 挺好玩的
    /// </summary>
    class StatePatten : IDesignPatten
    {
        public void Run()
        {
            Computer computer = new Computer();
            computer.HitKeyboard();
            computer.PressSwitch();
            computer.HitKeyboard();
            computer.HitKeyboard();
            computer.PressSwitch();
        }
        class Computer
        {
            public Computer()
            {
                var offState = new OffState();
                offState.SetComputer(this);
                computerState = offState;

            }
            ComputerState computerState;
            public void TransitionTo(ComputerState computerState)
            {
                this.computerState = computerState;
                computerState.SetComputer(this);
            }
            public void HitKeyboard()
            {
                computerState.HitKeyboard();
            }

            public void PressSwitch()
            {
                computerState.PressSwitch();
            }

        }
        abstract class ComputerState
        {
            protected Computer computer;
            
            public void SetComputer(Computer computer)
            {
                this.computer = computer;
            }
            public abstract void PressSwitch();
            public abstract void HitKeyboard();
        }

        class OffState : ComputerState
        {
            public override void HitKeyboard()
            {
                Console.WriteLine("Is not even on,What are you doing?");
            }

            public override void PressSwitch()
            {
                Console.WriteLine("ziziziz...");
                computer.TransitionTo(new LockState());
            }
        }
        class LockState : ComputerState
        {
            public override void HitKeyboard()
            {
                Console.WriteLine("Ok,Is unlocked!");
                computer.TransitionTo(new UnLockState());
            }

            public override void PressSwitch()
            {
                Console.WriteLine("Is off");
                computer.TransitionTo(new OffState());
            }
        }
        class UnLockState : ComputerState
        {
            
            public override void HitKeyboard()
            {
                Console.WriteLine("you are hitting keyboard！");
            }

            public override void PressSwitch()
            {
                Console.WriteLine("ziziziz... is off now");
                computer.TransitionTo(new OffState());
            }
        }
    }
}
