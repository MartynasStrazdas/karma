using System;


namespace karma.Ex
{
        public class TitleTooLongException : Exception
        {
            public TitleTooLongException()
            {
                Console.WriteLine("The title is too long.");
            }
        }
}