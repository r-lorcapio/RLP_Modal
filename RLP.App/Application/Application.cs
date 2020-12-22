using Microsoft.Extensions.DependencyInjection;
using RLP.App.businessDelegate;
using RLP.App.Domain;
using RLP.App.Extensions;
using RLP.App.Handler;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RLP.App
{
    public class Application : ApplicationBase
    {
        public Application(Pagination pagination) : base(pagination) { }

        public bool FileValidate()
        {

            try
            {
                if (string.IsNullOrEmpty(base.Pagination.PathFile))
                {
                    base.WriteLine("Invalid path!");
                    return false;
                }

                if (!File.Exists(base.Pagination.PathFile))
                {
                    base.WriteLine("File not exists!");
                    return false;
                }

                if (!(bool)Path.GetExtension(base.Pagination.PathFile).ExtensionIsValid(VALID_EXTENSIONS))
                {
                    base.WriteLine($"Extension is not valid! Accepted only {VALID_EXTENSIONS}");
                    return false;
                }

                //Arquivo validado, mostra o menu.
                MainMenu();
            }
            catch (IOException ioEx)
            {
                base.WriteLine($"{ioEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                base.WriteLine($"{ex.Message}");
                return false;
            }

            return true;
        }

        public override void ReadKeys()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        Provider.GetService<IUpArrow>().Handle();
                        break;
                    case ConsoleKey.DownArrow:
                        Provider.GetRequiredService<IDownArrow>().Handle();
                        break;
                    case ConsoleKey.PageDown:
                        Provider.GetRequiredService<IPageDown>().Handle();
                        break;
                    case ConsoleKey.PageUp:
                        Provider.GetRequiredService<IPageUp>().Handle();
                        break;
                    case ConsoleKey.L:
                        
                        Console.WriteLine("Informe a linha desejada:");
                        string row = Console.ReadLine();
                        base.Pagination.CurrentRow = int.Parse(row);
                        Provider.GetRequiredService<ISearch>().Handle();
                        break;
                    case ConsoleKey.E:
                        MainMenu();
                        break;

                    default:
                        if (Console.CapsLock && Console.NumberLock)
                        {
                            Console.WriteLine(key.KeyChar);
                        }
                        break;
                }
            }
        }

    }
}
