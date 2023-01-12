using OpenTools.Mediator;
using OpenTools.Mediator.App;
using OpenTools.Mediator.Abstrations;
using SimpleInjector;

Container container = new();
DependencyResolver dependencyResolver = new(container);
dependencyResolver.Register<TestCommand, TestCommandHandler>();
dependencyResolver.Register<TestQuery, TestResult, TestQueryHandler>();
dependencyResolver.RegisterFrom(typeof(TestQuery).Assembly);
Mediator mediator = new(dependencyResolver);
var result = await mediator.Send(new TestQuery(), default);
await mediator.Send(new TestCommand(), default);
