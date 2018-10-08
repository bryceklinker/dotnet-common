# Klinked.Gherkin

This library is a very light weight version of a Gherkin style testing framework that gets
the job done. 

**Unlike SpecFlow this library is tied to xUnit.**

## Sample Code

There is a more complete sample in the samples folder:

- [Gherkin Sample](./samples)

### Creating a Feature

```c#
public class NewHotnessFeature : Feature // <-- This is the key
{
    public NewHotnessFeature(ITestOutputHelper output)
        : base(output)
    {
    }
    
    [Fact]
    [Scenario("Provide the needful")]
    public async Task ProvideTheNeedful() 
    {
        await Given("I have some data setup");
        await When("I do the needful");
        await Then("I verify the needful was done");
    }
}
```

### Creating Steps

```c#
public class NewHotnessSteps
{
    [Given("I have some data setup")]
    public void IHaveSomeDataSetup()
    {
        // Setup some data
    }
    
    [When("I do the needful")]
    public void IDoTheNeedful()
    {
        // Do the needful
    }
    
    [Then("I verify the needful was done")]
    public void IVerifyTheNeedfulWasDone()
    {
        // Verify the needful was done
    }
}
```

### Using Scenario Context In Steps

```c#
public class SomeFeature : Feature
{
    public SomeFeature(ITestOutputHelper output)
        : base(output)
    {
    }
    
    [Fact]
    [Scenario("This is a feature")]
    public void ThisIsAFeature()
    {
        await Given("I use scenario context my-hot-value");
        await Then("I have my-hot-value in context");
    }
}
```

```c#
public class SomeSteps
{
    private readonly ScenarioContext _context;

    public SomeSteps(ScenarioContext context)
    {
        _context = context;
    }
    
    [Given("I use scenario context (.*)")]
    public void IUseScenarioContext(string value)
    {
        _context.Set("value", value);
    }
    
    [Then("I have (.*) in context")]
    public void IHaveValueInContext(string value) 
    {
        Assert.Equal(value, _context.Get<string>("value");
    }
}
```