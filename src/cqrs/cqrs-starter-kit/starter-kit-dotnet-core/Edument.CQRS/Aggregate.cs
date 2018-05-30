using System;
using System.Collections;

namespace Edument.CQRS
{
    /// <summary>
    /// Aggregate base class, which factors out some common infrastructure that
    /// all aggregates have (ID and event application).
    /// </summary>
    public class Aggregate
    {
        /// <summary>
        /// The number of events loaded into this aggregate.
        /// </summary>
        public int EventsLoaded { get; private set; }

        /// <summary>
        /// The unique ID of the aggregate.
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Enumerates the supplied events and applies them in order to the aggregate.
        /// </summary>
        /// <param name="events"></param>
        public void ApplyEvents(IEnumerable events)
        {
            foreach (var e in events)
                GetType().GetMethod("ApplyOneEvent")
                    .MakeGenericMethod(e.GetType())
                    .Invoke(this, new[] { e });
        }

        /// <summary>
        /// Applies a single event to the aggregate.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="ev"></param>
        public void ApplyOneEvent<TEvent>(TEvent ev)
        {
            if (!(this is IApplyEvent<TEvent> applier))
            {
                throw new InvalidOperationException($"Aggregate {GetType().Name} does not know how to apply event {ev.GetType().Name}");
            }
                
            applier.Apply(ev);
            
            EventsLoaded++;
        }
    }
}
