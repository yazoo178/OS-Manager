using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace flood
{
    class ResetToken
    {
        private CancellationTokenSource source = new CancellationTokenSource();

        public CancellationTokenSource TokenSource
        {
            get
            {
                return source;
            }

            set
            {
                source = value;
            }
        }
        
        
       
        public void Reset()
        {
            TokenSource = new CancellationTokenSource();
        }
    }
}
