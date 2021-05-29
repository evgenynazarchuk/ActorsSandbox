﻿using Akka.Actor;
using System;
using System.Threading.Tasks;

namespace ActorWithWait
{
    class Program
    {
        static void Main()
        {
            var actorSystem = ActorSystem.Create($"{nameof(Program)}");
            var actor = actorSystem.ActorOf<ActorWithWait>($"{nameof(ActorWithWait)}");

            actor.Tell(new DoWork());
            actor.Tell(new DoWork());
            actor.Tell(new DoWork());
            actor.Tell(new DoWork());
            actor.Tell(new DoWork());

            Console.ReadKey();
        }
    }

    class DoWork
    {
        public DoWork()
        {
        }
    }

    class ActorWithWait : ReceiveActor
    {
        public ActorWithWait()
        {
            Receive<DoWork>(_ =>
            {
                Task.Run(() =>
                {
                    // и это никого не блокирует!
                    Task.Delay(5000).GetAwaiter().GetResult();
                    return "Hello world";
                }).PipeTo(Self);
            });

            Receive<string>(message =>
            {
                Console.WriteLine(message);
            });
        }
    }
}
