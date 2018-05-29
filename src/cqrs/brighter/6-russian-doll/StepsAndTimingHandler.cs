using System;
using Paramore.Brighter;

namespace RussianDoll
{
    public class StepsAndTimingHandler<TRequest>
        : RequestHandler<TRequest> where TRequest : class, IRequest
    {
        private int step;
        private HandlerTiming timing;

        public override void InitializeFromAttributeParams(params object[] initializerList)
        {
            step = (int) initializerList[0];
            timing = (HandlerTiming) initializerList[1];
        }

        public override TRequest Handle(TRequest command)
        {
            Console.WriteLine($"ENTER       : {this.GetType().Name} as step {step} {timing} the target handler.");
            
            var result = base.Handle(command);
            
            Console.WriteLine($"EXIT        : {this.GetType().Name} as step {step} {timing} the target handler.");
            
            return result;
        }
    }
}