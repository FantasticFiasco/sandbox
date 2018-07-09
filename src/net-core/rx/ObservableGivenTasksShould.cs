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
            // Act
            var stream = Observable.Create<int>(async (observer) =>
            {
                observer.OnNext(await DoSomething(1));
                observer.OnNext(await DoSomething(2));
                observer.OnNext(await DoSomething(3));

                observer.OnCompleted();
            });

            // Assert
            stream
                .SequenceEqual(Observable.Range(1, 3))
                .Wait()
                .ShouldBeTrue();
        }

        [Fact]
        public void SupportCancellation()
        {
            // Arrange
            var hasBeenCancelled = false;
            
            var stream = Observable.Create<int>(async (observer, ct) =>
            {
                ct.Register(() => hasBeenCancelled = true);

                await Task.Delay(1000, ct);

                observer.OnCompleted();
            });
            
            // Act
            stream.Subscribe().Dispose();
            
            // Assert
            hasBeenCancelled.ShouldBeTrue();
        }

        [Fact]
        public void SupportExceptions()
        {
            // Arrange
            var hasExceptionBeenCaught = false;

            var stream = Observable.Create<int>(async (observer) =>
            {
                await Task.FromException(new Exception("This is a exception!"));
            });

            // Act
            stream.Subscribe(
                number => { },
                e => hasExceptionBeenCaught = true,
                () => { });
            
            // Assert
            hasExceptionBeenCaught.ShouldBeTrue();
        }

        private static Task<int> DoSomething(int returnValue)
        {
            return Task.FromResult(returnValue);
        }
    }
}
