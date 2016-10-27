using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class CompletionResult
    {
        public bool RanToCompletion { get; set; }

        public Exception Error { get; private set; }

        public CompletionResult(object result)
        {
            Result = result;
            RanToCompletion = true;
        }

        public CompletionResult(object result, Exception e)
        {
            Error = e;
            RanToCompletion = false;
            Result = result;
        }

        public object Result { get; set; }
    }
}
