using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Shouldly;

namespace Edument.CQRS
{
    /// <summary>
    /// Provides infrastructure for a set of tests on a given aggregate.
    /// </summary>
    /// <typeparam name="TAggregate"></typeparam>
    public class BDDTest<TAggregate>
        where TAggregate : Aggregate, new()
    {
        private readonly TAggregate sut;

        public BDDTest()
        {
            sut = new TAggregate();
        }

        protected void Test(IEnumerable given, Func<TAggregate, object> when, Action<object> then)
        {
            then(when(ApplyEvents(sut, given)));
        }

        protected IEnumerable Given(params object[] events)
        {
            return events;
        }

        protected Func<TAggregate, object> When<TCommand>(TCommand command)
        {
            return agg =>
            {
                try
                {
                    return DispatchCommand(command).Cast<object>().ToArray();
                }
                catch (Exception e)
                {
                    return e;
                }
            };
        }

        protected Action<object> Then(params object[] expectedEvents)
        {
            return got =>
            {
                var gotEvents = got as object[];
                if (gotEvents != null)
                {
                    if (gotEvents.Length == expectedEvents.Length)
                    {
                        for (var i = 0; i < gotEvents.Length; i++)
                        {
                            if (gotEvents[i].GetType() == expectedEvents[i].GetType())
                            {
                                Serialize(gotEvents[i]).ShouldBe(Serialize(expectedEvents[i]));
                            }
                            else
                            {
                                var actualName = gotEvents[i].GetType().Name;
                                var expectedName = expectedEvents[i].GetType().Name;
                                
                                actualName.ShouldBe(
                                    expectedName,
                                    $"Incorrect event in results; expected a {expectedName} but got a {actualName}");
                            }
                        }
                    }
                    else if (gotEvents.Length < expectedEvents.Length)
                    {
                        throw new Exception(
                            $"Expected event(s) missing: {string.Join(", ", EventDiff(expectedEvents, gotEvents))}");
                    }
                    else
                    {
                        throw new Exception(
                            $"Unexpected event(s) emitted: {string.Join(", ", EventDiff(gotEvents, expectedEvents))}");
                    }
                }
                else if (got is CommandHandlerNotDefiendException)
                {
                    throw new Exception((got as Exception).Message);
                }
                else
                {
                    throw new Exception($"Expected events, but got exception {got.GetType().Name}");
                }
            };
        }

        private string[] EventDiff(object[] a, object[] b)
        {
            var diff = a.Select(e => e.GetType().Name).ToList();
            foreach (var remove in b.Select(e => e.GetType().Name))
                diff.Remove(remove);
            return diff.ToArray();
        }

        protected Action<object> ThenFailWith<TException>()
        {
            return got =>
            {
                if (got is TException)
                {
                    // Got correct exception type
                }
                else if (got is CommandHandlerNotDefiendException)
                {
                    throw new Exception((got as Exception).Message);
                }
                else if (got is Exception)
                {
                    throw new Exception($"Expected exception {typeof(TException).Name}, but got exception {got.GetType().Name}");
                }
                else
                {
                    throw new Exception($"Expected exception {typeof(TException).Name}, but got event result");
                }
            };
        }

        private IEnumerable DispatchCommand<TCommand>(TCommand c)
        {
            var handler = sut as IHandleCommand<TCommand>;
            if (handler == null)
                throw new CommandHandlerNotDefiendException(string.Format(
                    "Aggregate {0} does not yet handle command {1}",
                    sut.GetType().Name, c.GetType().Name));
            return handler.Handle(c);
        }

        private TAggregate ApplyEvents(TAggregate agg, IEnumerable events)
        {
            agg.ApplyEvents(events);
            return agg;
        }

        private string Serialize(object obj)
        {
            var ser = new XmlSerializer(obj.GetType());
            var ms = new MemoryStream();
            ser.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            return new StreamReader(ms).ReadToEnd();
        }

        private class CommandHandlerNotDefiendException : Exception
        {
            public CommandHandlerNotDefiendException(string msg) : base(msg) { }
        }
    }
}