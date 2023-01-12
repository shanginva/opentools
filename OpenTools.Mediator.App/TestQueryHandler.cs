using OpenTools.Mediator.Abstrations;

namespace OpenTools.Mediator.App;

public class TestResult { }

public class TestQuery : IQuery<TestResult> { }

public class TestQueryHandler : IQueryHandler<TestQuery, TestResult>
{
    public Task<TestResult> Handle(TestQuery query, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Executed: {query.GetType().Name}");
        return Task.FromResult(new TestResult());
    }
}
