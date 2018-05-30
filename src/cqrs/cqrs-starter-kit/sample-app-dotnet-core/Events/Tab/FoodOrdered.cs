using System;
using System.Collections.Generic;

namespace Events.Tab
{
    public class FoodOrdered
    {
        public Guid Id;
        public List<OrderedItem> Items;
    }
}
