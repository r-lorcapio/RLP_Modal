using Microsoft.Extensions.DependencyInjection;
using RLP.App.Domain;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RLP.App
{
    class Program
    {
        private static ServiceProvider provider = null;
        private static string GetFileValidPath(string message = "")
        {
            Console.Clear();
            Console.WriteLine(message, Console.ForegroundColor = ConsoleColor.DarkBlue);
            Console.WriteLine("Informe o caminho do arquivo:");
            string path = Console.ReadLine();

            provider = provider ?? new ServiceCollection().AddSingleton(x => ActivatorUtilities.CreateInstance<Application>(x, new Pagination())).BuildServiceProvider();

            //Check if the file is valid.
            return provider.GetService<Application>().FileValidate(path);
        }
        static void Main(string[] args)
        {
            var fileIsValid = GetFileValidPath();

            while (!string.IsNullOrEmpty(fileIsValid))
            {
                fileIsValid =  GetFileValidPath(fileIsValid);
            }

            //Arquivo validado chama Application
            var taskKeys = new Task(provider.GetService<Application>().ReadKeys);
            taskKeys.Start();

            var tasks = new[] { taskKeys };
            Task.WaitAll(tasks);


        }

    }
}
