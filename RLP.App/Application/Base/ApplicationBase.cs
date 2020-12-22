using Microsoft.Extensions.DependencyInjection;
using RLP.App.businessDelegate;
using RLP.App.Domain;
using RLP.App.Handler;
using System;
using System.Linq;

namespace RLP.App
{
    public abstract class ApplicationBase
    {
        public const string VALID_EXTENSIONS = ".txt,.xls,.xlsx";
        protected Pagination Pagination { get; set; }
        protected IServiceProvider Provider;
        
        public ApplicationBase(Pagination pagination)
        {
            //Dependency Injection.
            Provider = new ServiceCollection()
                .AddSingleton<IUpArrow, UpArrow>()
                .AddSingleton<IDownArrow, DownArrow>()
                .AddSingleton<IPageDown, PageDown>()
                .AddSingleton<IPageUp, PageUp>()
                .AddSingleton<ISearch, Search>()
                .AddSingleton<IBusinessBd, BusinessBd>()
                .AddSingleton<IBusinessBd>(x => ActivatorUtilities.CreateInstance<BusinessBd>(x, pagination))
                .BuildServiceProvider();

            this.Pagination = pagination;
        }
      
        protected void WriteLine(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
        }

        //Required Methods.
        public abstract void ReadKeys();

        protected static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("");
            Console.WriteLine("Arrow Down");
            Console.WriteLine("Arrow Up");
            Console.WriteLine("Page Down");
            Console.WriteLine("Page Up");

            return true;
        }
    }
}
