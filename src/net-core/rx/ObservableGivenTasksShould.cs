using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Rx
{
    public class ObservableGivenTasksShould
    {
        [Fact]
        public void ReturnStreamWithThreeNumbers()
        {
            // Arrange
            var stream = Observable.Create<int>(async (observer) =>
            {
                observer.OnNext(await DoSomething(1));
                observer.OnNext(await DoSomething(2));
                observer.OnNext(await DoSomething(3));

                observer.OnCompleted();
            });

            var events = new List<int>();

            // Act
            stream.Subscribe(number => events.Add(number));
            stream.Wait();

            // Assert
            events.Count.ShouldBe(3);
            events[0].ShouldBe(1);
            events[1].ShouldBe(2);
            events[2].ShouldBe(3);
        }

        [Fact]
        public void SupportCancellation()
        {
            // Arrange
            var hasBeenCancelled = false;
            
            var stream = Observable.Create<int>(async (observer, ct) =>
            {
                ct.Register(() => hasBeenCancelled = true);

                await Task.Delay(1000);

                observer.OnCompleted();
            });
            
            // Act
            stream.Subscribe().Dispose();
            
            // Assert
            hasBeenCancelled.ShouldBeTrue();
        }

        private static Task<int> DoSomething(int returnValue)
        {
            return Task.FromResult(returnValue);
        }
    }
}
