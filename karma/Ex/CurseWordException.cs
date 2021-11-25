using System;


namespace karma.Ex
{
        public class CurseWordException : Exception
        {
            //string message = "The title contains curse words";
            public CurseWordException() : base()
            {
                throw new CurseWordException("The title contains curse words");
            }
            public CurseWordException(string message): base(message) {
                
            }
        }
}