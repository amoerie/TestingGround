using System;

namespace TestingGround.Tests
{
    public static class Log
    {
        public static void Write(string output)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0:HH:mm:ss.fffffff} - {1}", DateTime.Now, output));
        }
    }
}
