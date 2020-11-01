using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatten.BehaviourPattens
{
    /// <summary>
    /// 经典(￣、￣)
    /// </summary>
    class Observer : IDesignPatten
    {
        public void Run()
        {
            TextCenter textCenter = new TextCenter();
            TextAnalyser analyser = new TextAnalyser();
            TextMeasurer measurer = new TextMeasurer();
            textCenter.SetPublisher(new EasyTextPublisher());
            textCenter.Attach(analyser);
            textCenter.Attach(measurer);
            textCenter.WriteText("??");
            textCenter.Detach(measurer);
            textCenter.WriteText("206060");
        }
        interface ITextSubscriber
        {
            void Update(string obj);
        }
        interface ITextPublisher
        {
            void AddSubscriber(ITextSubscriber subscriber);
            void RemoveSubscriber(ITextSubscriber subscriber);
            void Notify(string text);
        }
        class EasyTextPublisher : ITextPublisher
        {
            List<ITextSubscriber> subscribers = new List<ITextSubscriber>();
            public void AddSubscriber(ITextSubscriber subscriber)
            {
                subscribers.Add(subscriber);
            }

            public void Notify(string text)
            {
                foreach (var subscriber in subscribers)
                {
                    subscriber.Update(text);
                }
            }

            public void RemoveSubscriber(ITextSubscriber subscriber)
            {
                subscribers.Remove(subscriber);
            }
        }
        class TextCenter
        {
            ITextPublisher publisher;
            public void SetPublisher(ITextPublisher publisher) => this.publisher = publisher;
            public void WriteText(string text)
            {
                Console.WriteLine("We get this=>"+text);
                publisher.Notify(text);
            }
            public void Attach(ITextSubscriber observer)
            {
                Console.WriteLine("Subject: Attached an observer.");
                this.publisher.AddSubscriber(observer);
            }

            public void Detach(ITextSubscriber observer)
            {
                this.publisher.RemoveSubscriber(observer);
                Console.WriteLine("Subject: Detached an observer.");
            }

        }

        class TextMeasurer : ITextSubscriber
        {
            public void Update(string text)
            {
                Console.WriteLine($"the length is {text.Length}");
            }
        }
        class TextAnalyser : ITextSubscriber
        {
            public void Update(string text)
            {
                Console.WriteLine($"Is text contents 60 = {text.Contains("60")}");
            }
        }


    }
}
