using NHibernateSpike;
using NUnit.Framework;


namespace NHibernateTest
{
    [TestFixture]
    class Delegatestest
    {
        [Test]
        public void SingleMethodInterface()
        {
            IInte32Action action = new Delegates();
            action.DoIt(10);
        }

        [Test]
        public void SimpleDelegatesFromMethod()
        {
            var mytarget = new Delegates();
            Int32Action action = mytarget.DoIt;

            Int32Action action2 = action + mytarget.RandomRob;

            action2.Invoke(4);

        }

        [Test]
        public void Delegatesfromstaticmethod()
        {
            Int32Action staticdelegate = Delegates.StaticBill;

            staticdelegate(34);
        }
    }
}
