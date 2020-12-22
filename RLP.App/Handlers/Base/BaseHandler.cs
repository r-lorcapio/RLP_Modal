using RLP.App.businessDelegate;
using RLP.App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLP.App.Handlers.Base
{
    public class BaseHandler
    {
        protected readonly int UnitIncrement;
        protected readonly int MultipleIncrement;
        protected IBusinessBd BusinessBd { get; set; }
        public BaseHandler(IBusinessBd businessBd)
        {
            this.UnitIncrement = 1;
            this.MultipleIncrement = 11;
            this.BusinessBd = businessBd;
        }

        protected async Task WriteLineAsync(string content)
        {
            Console.Clear();
            await Task.Run(() =>
            {
                Console.WriteLine(content);
                Console.WriteLine();
                Console.WriteLine("Digite E, para voltar ao menu.");
            });

        }
        protected async Task WriteLineAsync(string[] content)
        {
            Console.Clear();
            await Task.Run(() =>
            {
                content.ToList().ForEach(i => Console.WriteLine(i.ToString()));
                Console.WriteLine();
                Console.WriteLine("Digite E, para voltar ao menu.");
            });

        }

     
    }
}


