﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flood
{
    public class CompletedEventArgs : EventArgs
    {
        public CompletionResult CompletionResult { get; set; }
    }
}
