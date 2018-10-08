using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Klinked.Gherkin.Common;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Klinked.Gherkin.Steps
{
    internal class StepDefinition
    {
        private Type Type { get; }
        private MethodInfo Method { get; }
        private StepAttribute Attribute { get; }
        private ParameterInfo[] MethodParameters => Method.GetParameters();
        
        public StepDefinition(Type type, MethodInfo method, StepAttribute attribute)
        {
            Type = type;
            Method = method;
            Attribute = attribute;
        }

        public bool IsMatch<T>(string text)
        {
            return Attribute.GetType() == typeof(T)
                   && Attribute.IsMatch(text);
        }

        public async Task Execute(string text, IServiceCollection services, ITestOutputHelper output)
        {
            var textParameters = Attribute.GetParameters(text);
            var instance = CreateInstance(services, output);
            OutputStep(text, output);
            await ExecuteStep(instance, textParameters);
        }

        private async Task ExecuteStep(object instance, string[] textParameters)
        {
            var parameters = ConvertParameters(textParameters).ToArray();
            if (Method.IsAsync())
                await ((Task) Method.Invoke(instance, parameters)).ConfigureAwait(false);
            else
                Method.Invoke(instance, parameters);
        }

        private IEnumerable<object> ConvertParameters(string[] values)
        {
            for (var i = 0; i < MethodParameters.Length; i++)
                yield return Convert.ChangeType(values[i], MethodParameters[i].ParameterType);
        }

        private object CreateInstance(IServiceCollection services, ITestOutputHelper output)
        {
            return services
                .AddTransient(Type)
                .BuildServiceProvider()
                .GetRequiredService(Type);
        }

        private void OutputStep(string text, ITestOutputHelper output)
        {
            var prefix = Attribute.GetType().Name.Replace("Attribute", "");
            output.WriteLine($"{prefix} {text}");
        }
    }
}