using System;
using Paramore.Brighter;

namespace RussianDoll
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class StepsAndTimingAttribute : RequestHandlerAttribute
    {
        public StepsAndTimingAttribute(int step, HandlerTiming timing)
            : base(step, timing)
        {
        }

        public override Type GetHandlerType()
        {
            return typeof(StepsAndTimingHandler<>);
        }

        public override object[] InitializerParams()
        {
            return new object[]
            {
                this.Step,
                this.Timing
            };
        }
    }
}