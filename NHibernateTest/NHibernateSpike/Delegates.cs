using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateSpike
{
    public delegate void Int32Action(int x);

    public interface IInte32Action
    {
        void DoIt(int x);
    }

    public class Delegates : IInte32Action
    {
        public void DoIt(int x)
        {
            Console.WriteLine("This is number {0}", x);
        }

        public void RandomRob(int x)
        {
            Console.WriteLine("Come from random: {0}",x);
        }

        public static void StaticBill(int x)
        {
            Console.WriteLine("from static : {0}", x);
        }
    }
}
