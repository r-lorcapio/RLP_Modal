using RLP.App.businessDelegate;
using RLP.App.Domain;
using RLP.App.Handlers.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RLP.App.Handler
{
    public interface IPageUp
    {
        void Handle();
    }
    public class PageUp : BaseHandler, IPageUp
    {
        public PageUp(IBusinessBd businessBd) : base(businessBd)
        {
        }

        public async void Handle()
        {
            try
            {
                var result = await base.BusinessBd.RangeReturnFromCache(base.MultipleIncrement, decrement: true);
                await WriteLineAsync(result);
            }
            catch (Exception ex)
            {
                await base.WriteLineAsync(ex.Message);
            }

        }
    }
}
