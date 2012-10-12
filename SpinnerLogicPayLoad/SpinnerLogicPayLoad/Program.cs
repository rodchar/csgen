using System;

namespace SpinnerLogicPayLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            BLL bll = new BLL(null, null);
            bll.Start();
        }
    }
}
