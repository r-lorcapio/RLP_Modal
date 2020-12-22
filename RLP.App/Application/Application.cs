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

        public string FileValidate(string pathFile)
        {

            try
            {
                if (string.IsNullOrEmpty(pathFile))
                {
                    return "Caminho inválido";
                }

                if (!File.Exists(pathFile))
                {
                    return "Arquivo não encontrado!";
                }

                if (!(bool)Path.GetExtension(pathFile).ExtensionIsValid(VALID_EXTENSIONS))
                {
                    return $"Extensão inválida de arquivo. Aceito somente: {VALID_EXTENSIONS}";
                }

                //Arquivo validado, mostra o menu.
                base.Pagination.PathFile = pathFile;
                //base.Pagination.CountMaxFile = File.ReadAllLines(pathFile).Length;
                MainMenu();
            }
            catch (IOException ioEx)
            {
                return $"{ioEx.Message}";
            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
            }

            return string.Empty;
        }

        public override void ReadKeys()
        {
            var key = new ConsoleKeyInfo();

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

                        if (string.IsNullOrWhiteSpace(row))
                        {
                            Console.WriteLine("Informe um valor válido.");
                        }
                        else
                        {
                            if (int.TryParse(row, out int currentRow))
                            {
                                //if (currentRow <= base.Pagination.CountMaxFile)
                                //{
                                base.Pagination.CurrentRow = currentRow;
                                Provider.GetRequiredService<ISearch>().Handle();
                                //}
                                //else
                                //{
                                //    Console.WriteLine("Linha não encontrada no documento.");
                                //}

                            }
                            else
                            {
                                Console.WriteLine("Número inválido.");
                            }

                        }

                        break;
                    case ConsoleKey.E:
                        base.Pagination.SkipRow = 0;
                        base.Pagination.CurrentRow = 0;
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

