using System;
using System.Collections.Generic;

namespace PetRegistry
{
    interface ITrainable
    {
        void TrainCommand(string command);
    }

    public abstract class Animal : ITrainable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Commands { get; }

        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
            Commands = new List<string>();
        }
        public virtual void TrainCommand(string command)
        {
            // Пока пусто :)
        }

        public abstract void DisplayInfo();
    }

    class Dog : Animal, ITrainable
    {
        public Dog(string name, int age) : base(name, age) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Information about the dog: {Name}, age: {Age}");
            Console.WriteLine("Commands that the dog can perform:");
            foreach (var command in Commands)
            {
                Console.WriteLine(command);
            }
        }

        public void TrainCommand(string command)
        {
            Commands.Add(command);
            Console.WriteLine($"The dog '{Name}' has been trained to perform the command: {command}");
        }
    }

    class Cat : Animal, ITrainable
    {
        public Cat(string name, int age) : base(name, age) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Information about the cat: {Name}, age: {Age}");
            Console.WriteLine("Commands that the cat can perform:");
            foreach (var command in Commands)
            {
                Console.WriteLine(command);
            }
        }

        public void TrainCommand(string command)
        {
            Commands.Add(command);
            Console.WriteLine($"The cat '{Name}' has been trained to perform the command: {command}");
        }
    }

    struct CommandTracker
    {
        public List<Animal> Animals;
        public string Command;

        public CommandTracker(List<Animal> animals, string command)
        {
            Animals = animals;
            Command = command;
        }
    }

    class Counter : IDisposable
    {
        private int _value;
        private bool _disposed;

        public int Value
        {
            get => _value;
            private set => _value = value;
        }

        public Counter()
        {
            Value = 0;
        }

        public void Add()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Counter");
            }
            Value++;
        }

        public void Reset()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Counter");
            }
            Value = 0;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                Console.WriteLine($"Counter disposed. Current value: {Value}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Get a new pet");
                Console.WriteLine("2. View list of commands");
                Console.WriteLine("3. Teach an animal new commands");
                Console.WriteLine("4. Exit the program");

                using (var counter = new Counter())
                {
                    Console.Write("Select menu item: ");
                    var choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter the animal's name: ");
                            var name = Console.ReadLine();
                            Console.Write("Enter the age of the animal: ");
                            var age = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Is this a dog (Y/N)? ");
                            var isDog = Console.ReadLine().ToLower() == "y";

                            if (isDog)
                            {
                                var dog = new Dog(name, age);
                                animals.Add(dog);
                                dog.DisplayInfo();
                                counter.Add();
                            }
                            else
                            {
                                var cat = new Cat(name, age);
                                animals.Add(cat);
                                cat.DisplayInfo();
                                counter.Add();
                            }

                            Console.ReadKey();
                            break;
                        case 2:
                            if (animals.Count > 0)
                            {
                                Console.WriteLine("Currently registered commands:");
                                foreach (var animal in animals)
                                {
                                    Console.WriteLine($"\nInformation about the {animal.GetType().Name}: {animal.Name}, age: {animal.Age}");
                                    Console.WriteLine("Commands that the animal can perform:");
                                    foreach (var command in animal.Commands)
                                    {
                                        Console.WriteLine(command);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No animals have been registered yet.");
                            }

                            Console.ReadKey();
                            break;
                        case 3:
                            Console.Write("Write a command you want to teach your pets: ");
                            var commandInput = Console.ReadLine();

                            CommandTracker tracker = new CommandTracker(animals, commandInput);
                            TeachAnimalsCommand(tracker);

                            Console.ReadKey();
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Invalid selection.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }

        private static void TeachAnimalsCommand(CommandTracker tracker)
        {
            foreach (var animal in tracker.Animals)
            {
                animal.TrainCommand(tracker.Command);
            }
        }
    }
}