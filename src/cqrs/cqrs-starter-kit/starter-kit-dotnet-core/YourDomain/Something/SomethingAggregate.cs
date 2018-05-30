using System.Collections;
using Edument.CQRS;
using Events.Something;

namespace YourDomain.Something
{
    public class SomethingAggregate : Aggregate,
        IHandleCommand<MakeSomethingHappen>,
        IApplyEvent<SomethingHappened>
    {
        private bool alreadyHappened;

        public IEnumerable Handle(MakeSomethingHappen c)
        {
            if (alreadyHappened)
                throw new SomethingCanOnlyHappenOnce();

            yield return new SomethingHappened
            {
                Id = c.Id,
                What = c.What
            };
        }

        public void Apply(SomethingHappened e)
        {
            alreadyHappened = true;
        }
    }
}