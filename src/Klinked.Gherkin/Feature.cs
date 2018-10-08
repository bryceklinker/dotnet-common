using System;
using System.Linq;
using System.Threading.Tasks;
using Klinked.Gherkin.Common;
using Klinked.Gherkin.Scenarios;
using Klinked.Gherkin.Steps;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Klinked.Gherkin
{
    public class Feature
    {
        private readonly IServiceCollection _services;
        
        private StepDefinition[] Steps { get; }
        private bool HasPrintedScenario { get; set; }
        private ITest Test { get; }
        private string ScenarioName { get; }
        protected ScenarioContext ScenarioContext { get; }
        protected ITestOutputHelper Output { get; }

        protected Feature(ITestOutputHelper output)
            : this(output, new object[0])
        {
            
        }

        protected Feature(ITestOutputHelper output, params object[] services)
        {
            Output = output;
            Steps = StepLocator.Instance.GetDefinitions();
            Test = output.GetTest();
            ScenarioName = Test.GetScenarioName();
            ScenarioContext = new ScenarioContext();
            _services = CreateServiceCollection(services);
        }

        protected async Task Given(string text)
        {
            await ExecuteStep<GivenAttribute>(text).ConfigureAwait(false);;
        }

        protected async Task When(string text)
        {
            await ExecuteStep<WhenAttribute>(text).ConfigureAwait(false);;
        }

        protected async Task Then(string text)
        {
            await ExecuteStep<ThenAttribute>(text).ConfigureAwait(false);;
        }

        private async Task ExecuteStep<T>(string text)
        {
            var step = GetMatchingStep<T>(text);

            if (!HasPrintedScenario)
                OutputScenario();

            await step.Execute(text, _services, Output).ConfigureAwait(false);
        }

        private void OutputScenario()
        {
            HasPrintedScenario = true;
            Output.WriteLine($"Scenario: {ScenarioName}");
        }

        private StepDefinition GetMatchingStep<T>(string text)
        {
            var matchingSteps = Steps.Where(s => s.IsMatch<T>(text)).ToArray();

            if (matchingSteps.Length > 1)
                throw new InvalidOperationException($"Multiple steps found matching: {text}");
            
            if (matchingSteps.Length == 0)
                throw new InvalidOperationException($"Could not find matching step for: {text}");

            return matchingSteps[0];
        }

        private IServiceCollection CreateServiceCollection(object[] serviceInstances)
        {
            var services = new ServiceCollection()
                .AddSingleton(Output)
                .AddSingleton(ScenarioContext);
                
            foreach (var service in serviceInstances)
                services.AddSingleton(service.GetType(), service);
            return services;
        }
    }
}