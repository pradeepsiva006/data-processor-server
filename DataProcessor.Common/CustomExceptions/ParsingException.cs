using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor.Common.CustomExceptions
{
    public class ParsingException : Exception
    {
        public ParsingException() { }

        public ParsingException(string message) : base(message) { }

        public ParsingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
