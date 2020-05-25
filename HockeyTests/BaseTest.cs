using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HockeyTests
{
    public class BaseTest
    {
        protected Fixture fixture = new Fixture();

        public BaseTest()
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
