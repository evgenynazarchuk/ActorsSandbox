﻿using Akka.Actor;
using System;

namespace ActorWithAsk
{
    class Program
    {
        static void Main()
        {
            using var system = ActorSystem.Create($"{nameof(Program)}");
            var actor = system.ActorOf<ActorWithAsk>($"{nameof(ActorWithAsk)}");

            var resultTask = actor.Ask<string>("Evgeny");
            var result = resultTask.GetAwaiter().GetResult();
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }


    class DoWork
    {
        public DoWork() { }
    }

    class ActorWithAsk : ReceiveActor
    {
        public ActorWithAsk()
        {
            Receive<string>(name =>
            {
                Sender.Tell($"Hello, {name}");
            });
        }
    }
}
