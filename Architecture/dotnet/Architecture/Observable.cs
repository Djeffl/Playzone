using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public class Observable
    {
        public static void Execute()
        {
            //var subject = new Subject();

            //var observer1 = new Observer(subject);
            //var observer2 = new Observer(subject);
            //var observer3 = new Observer(subject);

            //subject.Subscribe(observer1);
            //subject.Subscribe(observer2);
            //subject.Subscribe(observer3);
            //subject.Notify();

            var parent = new Parent();

            var child1 = new Child(parent);
            var child2 = new Child(parent);

            parent.Subscribe(child1);
            parent.Subscribe(child2);

            Console.WriteLine("child1 time to eat prop: " + child1.TimeToEat);
            Console.WriteLine("child2 time to eat prop: " + child2.TimeToEat);

            parent.FoodIsServed = true;

            Console.WriteLine("child1 time to eat prop: " + child1.TimeToEat);
            Console.WriteLine("child2 time to eat prop: " + child2.TimeToEat);
        }
    }

    interface ISubject
    {
        List<Observer> Observers { get; }
        void Subscribe(Observer observer);
        void Unsubscribe(Observer observer);
        void Notify();
    }

    interface IObserver
    {
        void Update();
    }

    class Subject : ISubject
    {
        private List<Observer> _observers;
        public List<Observer> Observers
        {
            get
            {
                if (_observers == null)
                {
                    _observers = new List<Observer>();
                }
                return _observers;
            }
        }

        public void Notify()
        {
            Observers.ForEach(x => x.Update());
        }

        public void Notify<T>(T obj)
        {
            Observers.ForEach(x => x.Update(obj));
        }

        public void Subscribe(Observer observer)
        {
            if (!Observers.Contains(observer))
            {
                Observers.Add(observer);
            }
        }

        public void Unsubscribe(Observer observer)
        {
            if (Observers.Contains(observer))
            {
                Observers.Remove(observer);
            }
            else
            {
                throw new Exception("Not subscribed");
            }
        }
    }

    class Observer : IObserver
    {
        private readonly ISubject _subject;

        public Observer(ISubject subject)
        {
            _subject = subject;
        }
        public void Update()
        {
            Console.WriteLine("I UPDATE");
            //throw new NotImplementedException();
        }

        internal virtual void Update<T>(T obj)
        {
            Console.WriteLine($"Updated: {obj}");
        }
    }

    class Parent: Subject
    {
        private bool foodIsServed;
        public bool FoodIsServed
        {
            get { return foodIsServed; }
            set
            {
                foodIsServed = value;
                Notify(value);
            }
        }

    }

    class Child : Observer
    {
        public bool TimeToEat { get; set; }
        public Child(ISubject subject) : base(subject)
        {
        }
        internal override void Update<T>(T obj)
        {
            base.Update(obj);
            TimeToEat = obj is bool;
        }

    }
}
