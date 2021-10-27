using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class FraudException: Exception //inherit from Exception class.
    {
        public FraudException()
            : base() { }//inheriting from base exception.
                        
        public FraudException(string message)//overloading above constructor.
            : base(message) { } //implementing what exists for Exception.
    }
}
