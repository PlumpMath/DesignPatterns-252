﻿using System;
using System.Collections;
using System.Threading;

namespace Patterns.Behavioral.Observer
{
    class Simulator : IEnumerable
    {
        string[] moves = { "5", "3", "1", "6", "7" };
        public IEnumerator GetEnumerator()
        {
            foreach (string element in moves)
                yield return element;
        }
    }

    class Subject
    {
        public delegate void Callback(string s);
        public event Callback Notify;
        Simulator simulator = new Simulator();
        const int speed = 200;
        public string SubjectState { get; set; }
        public void Go()
        {
            new Thread(new ThreadStart(Run)).Start();
        }
        void Run()
        {
            foreach (string s in simulator)
            {
                Console.WriteLine("Subject: " + s);
                SubjectState = s;
                Notify(s);
                Thread.Sleep(speed); // milliseconds
            }
        }
    }
    interface IObserver
    {
        void Update(string state);
    }

    class Observer : IObserver
    {
        string name;
        Subject subject;
        string state;
        string gap;
        public Observer(Subject subject, string name, string gap)
        {
            this.subject = subject;
            this.name = name;
            this.gap = gap;
            subject.Notify += Update;
        }
        public void Update(string subjectState)
        {
            state = subjectState;
            Console.WriteLine(gap + name + ": " + state);
        }
    }
}
