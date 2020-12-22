using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLP.App.Domain;
using RLP.App.Extensions;

namespace RLP.App.businessDelegate
{
    public interface IBusinessBd
    {
        Task<string[]> RangeReturnFromCache(int skipRow, bool decrement = false, bool forceReloadCache = false);
    }

    public class BusinessBd : IBusinessBd
    {
        public readonly Pagination Pagination;
        public BusinessBd(Pagination pagination)
        {
            this.Pagination = pagination;
        }

        public async Task<string[]> RangeReturnFromCache(int skipRow, bool decrement = false, bool forceReloadCache = false)
        {
            string[] result = null;

            if (!forceReloadCache)
            {
                if (!decrement)
                {
                    Pagination.CurrentRow += skipRow;
                    Pagination.SkipRow += skipRow;
                }

                else
                {
                    Pagination.CurrentRow -= skipRow;
                    Pagination.SkipRow -= skipRow;
                }
            }

            await Task.Run(() =>
            {
                //Load inicial dos valores.
                if (Pagination.CacheRows is null)
                {
                    Pagination.CacheRows = System.IO.File.ReadLines(Pagination.PathFile).Skip(0).Take(Pagination.LimitCacheRows).ToArray();
                }

                if (Pagination.SkipRow.AutomaticReloadCache(Pagination.MaxNext) || forceReloadCache)
                {

                    //Como podemos cachear somente 100. Pego os 50 anteriores e os proximos 50 do arquivo.
                    var skip = (Pagination.CurrentRow - Pagination.HalfCacheQtd);
                    Pagination.CacheRows = System.IO.File.ReadLines(Pagination.PathFile).Skip(skip).Take(Pagination.LimitCacheRows).ToArray();

                    //Procura os valores no intervalo das 100 linhas.
                    var skipNext = (forceReloadCache ? 5 : Pagination.MaxNext);
                    var halfCache = (forceReloadCache ? 45 : Pagination.HalfCacheQtd);

                    Pagination.SkipRow = ((Pagination.CurrentRow >= Pagination.HalfCacheQtd) ? halfCache : (Pagination.CurrentRow - skipNext));
                }


                //Retorna o resultado de acordo com o skip.
                result = Pagination.CacheRows.Skip(Pagination.SkipRow).Take(Pagination.MaxNext).ToArray();
            });

            return result;
        }


    }
}
