using RLP.App.BusinessDelegate;
using RLP.App.Handlers.Base;
using System;

namespace RLP.App.Handler
{
    public interface IPageDown
    {
        void Handle();
    }
    public class PageDown : BaseHandler, IPageDown
    {
        public PageDown(IBusinessBd businessBd) : base(businessBd)
        {
        }

        public async void Handle()
        {
            try
            {
                var result = await base.BusinessBd.RangeReturnFromCache(base.MultipleIncrement);
                await WriteLineAsync(result);
            }
            catch (Exception ex)
            {
                await base.WriteLineAsync(ex.Message);
            }

        }
    }
}
