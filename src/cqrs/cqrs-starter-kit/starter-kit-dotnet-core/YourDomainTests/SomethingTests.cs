using System;
using Edument.CQRS;
using Events.Something;
using Xunit;
using YourDomain.Something;

namespace YourDomainTests
{
    public class SomethingTests : BDDTest<SomethingAggregate>
    {
        private Guid testId;

        public SomethingTests()
        {
            testId = Guid.NewGuid();
        }

        [Fact]
        public void SomethingCanHappen()
        {
            Test(
                Given(),
                When(new MakeSomethingHappen
                {
                    Id = testId,
                    What = "Boom!"
                }),
                Then(new SomethingHappened
                {
                    Id = testId,
                    What = "Boom!"
                }));
        }

        [Fact]
        public void SomethingCanHappenOnlyOnce()
        {
            Test(
                Given(new SomethingHappened
                {
                    Id = testId,
                    What = "Boom!"
                }),
                When(new MakeSomethingHappen
                {
                    Id = testId,
                    What = "Boom!"
                }),
                ThenFailWith<SomethingCanOnlyHappenOnce>());
        }
    }
}