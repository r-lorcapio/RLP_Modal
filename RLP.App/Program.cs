using Microsoft.Extensions.DependencyInjection;
using RLP.App.Domain;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RLP.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Informe o caminho do arquivo:");
            string path = Console.ReadLine();

            //TODO REMOVE.
            path = @"C:\Users\r_lor\OneDrive\Área de Trabalho\RODRIGO\WriteLines.txt";
            var instance = new ServiceCollection().AddSingleton(x => ActivatorUtilities.CreateInstance<Application>(x, new Pagination { PathFile = path })).BuildServiceProvider();

            //Check if the file is valid.
            var fileIsValid = instance.GetService<Application>().FileValidate();
            if (fileIsValid)
            {
                var taskKeys = new Task(instance.GetService<Application>().ReadKeys);
                taskKeys.Start();

                var tasks = new[] { taskKeys };
                Task.WaitAll(tasks);
            }

        }

    }
}
