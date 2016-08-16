using System;
using System.Diagnostics;
using System.Linq;

namespace LINQtoObjects
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            (from types in typeof(int).Assembly.GetTypes()
                where types.IsInterface && types.IsPublic
                orderby types.Name
                select new
                {
                    interfaceName = types.Name,
                    NumOfMethods = types.GetMethods().Length
                }).ToList().ForEach(x => Console.WriteLine($"name: {x.interfaceName}" +
                                                           $"Number of methods: {x.NumOfMethods}"));

            Console.WriteLine("\n");

            Process.GetProcesses()
                .Where(process =>
                {
                    try
                    {
                        return process.Handle != IntPtr.Zero && process.Threads.Count < 5;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .OrderBy(process => process.Id)
                .Select(process => new
                {
                    name = process.ProcessName,
                    id = process.Id,
                    time = process.StartTime
                }).ToList().ForEach(x =>
                {
                    Console.WriteLine($"Process name {x.name} " +
                                      $"Process id {x.id} " +
                                      $"Process start time {x.time}");
                });

            Console.WriteLine("\ngroup\n");

            var processGroups = Process.GetProcesses()
                .Where(process =>
                {
                    try
                    {
                        return process.Handle != IntPtr.Zero && process.Threads.Count < 5;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .OrderBy(process => process.Id)
                .GroupBy(process => process.BasePriority, process => new
                {
                    name = process.ProcessName,
                    id = process.Id,
                    time = process.StartTime
                });

            foreach (var process in processGroups.SelectMany(group => group))
                Console.WriteLine($"Process name {process.name} " +
                                  $"Process id {process.id} " +
                                  $"Process start time {process.time}");

            Console.WriteLine("\n");

            Console.WriteLine("The total number of threads in the system is " +
                              $"{Process.GetProcesses().Sum(process => process.Threads.Count)}");

            Console.WriteLine("\n");

            People p = new People("David",20,123456);
            Pet   t = new Pet("Mitzi",3,12);
            Console.WriteLine($"{t.Name} {t.Age} {t.Id}");
            p.CopyTo(t);
            Console.WriteLine($"{t.Name} {t.Age} {t.Id}");
        }
    }
}