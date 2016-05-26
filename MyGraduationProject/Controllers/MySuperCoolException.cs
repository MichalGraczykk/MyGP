using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGraduationProject.Controllers
{

    [Serializable]
    public class MySuperException : Exception
    {
        public MySuperException() { }
        public MySuperException(string message) 
            : base(message) { }
        public MySuperException(string message, Exception inner) 
            : base(message, inner) { }
        protected MySuperException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}