using RLP.App.BusinessDelegate;
using RLP.App.Handlers.Base;
using System;


namespace RLP.App.Handler
{
    public interface IUpArrow
    {
        void Handle();
    }
    public class UpArrow : BaseHandler, IUpArrow
    {
        public UpArrow(IBusinessBd businessBd) : base(businessBd){}

        public async void Handle()
        {
            try
            {
                var result = await base.BusinessBd.RangeReturnFromCache(base.UnitIncrement, decrement: true);
                await WriteLineAsync(result);
            }
            catch (Exception ex)
            {
                await base.WriteLineAsync(ex.Message);
            }

        }
    }
}
