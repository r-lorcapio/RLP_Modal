
namespace RLP.App.Domain
{
    public class Pagination
    {
        public string PathFile { get; set; }


        public int CountMaxFile { get; set; }

        /// <summary>
        /// Como o cache, poderá armazenar somente 100 linhas. Vamos cachear sempre as proximas 50 e as 50 anteriores.
        /// </summary>
        public int HalfCacheQtd
        {
            get
            {
                return 50;
            }
        }

        /// <summary>
        /// Exibicao maxima de 11 em 11
        /// </summary>
        public int MaxNext
        {
            get
            {
                return 11;
            }
        }

        /// <summary>
        /// Limite maximo do cache, 100 linhas.
        /// </summary>
        public int LimitCacheRows
        {
            get
            {
                return 100;
            }
        }


        private int _CurrentRow { get; set; }
        public int CurrentRow
        {
            get { return _CurrentRow; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                _CurrentRow = value;
            }
        }

        private int _SkipRow { get; set; }
        public int SkipRow
        {
            get { return _SkipRow; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                _SkipRow = value;
            }
        }

        public string[] CacheRows { get; set; }


    }
}
