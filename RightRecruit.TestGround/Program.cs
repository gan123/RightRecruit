using Raven.Client.Document;

namespace RightRecruit.TestGround
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new DocumentStore {Url = "http://localhost:8082"};
            store.Initialize();
            
            using(var session  = store.OpenSession())
            {
                var trail = new Domain.Plan.ThirtyDayTrialPlan();
                var monthly = new Domain.Plan.MonthlyPlan();
                var annual = new Domain.Plan.AnnualPlan();
            }
        }
    }
}
