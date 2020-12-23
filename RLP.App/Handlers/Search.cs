using RLP.App.BusinessDelegate;
using RLP.App.Handlers.Base;
using System;

namespace RLP.App.Handler
{
    public interface ISearch
    {
        void Handle();
    }
    public class Search : BaseHandler, ISearch
    {
        public Search(IBusinessBd businessBd) : base(businessBd) { }

        public async void Handle()
        {
            try
            {
                var result = await base.BusinessBd.RangeReturnFromCache(0, forceReloadCache: true);
                await WriteLineAsync(result);

            }
            catch (Exception ex)
            {
                await base.WriteLineAsync(ex.Message);
            }

        }
    }
}
