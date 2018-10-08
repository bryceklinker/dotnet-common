# Klinked.Cqrs

Over the years I've come to enjoy the way CQRS would have you organize your code. This library
takes advantage of dependency injection quite heavily so if that's not your deal you may want to
look into a different solution.

## Sample Code

Below is a small walk through of using the library. There are also two sample applications that
use `Klinked.Cqrs`. One is a console application the other is an ASP.NET Core application.

- [AspNetCore App](./samples/Klinked.Cqrs.AspNetCore)
- [Console App](./samples/Klinked.Cqrs.Console)

### Setting up with ASP.NET Core App

```c#
// Startup.cs

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Setup other services
        services.AddKlinkedCqrs(b => b.UseAssemblyFor<Startup>());
        // Setup other services
    }
}
```

### Setting up with Console App

```c#
class Program
{
    public static void Main(string[] args)
    {
        // Setup CQRS without any other DI setup
        var simple = CqrsBus.UseAssemblyFor<Program>()
                        .Build();
        
        // Setup CQRS with an existing DI setup
        var services = new ServiceCollection()
            .AddHttpClient();
            
        var advanced = CqrsBus.UseAssemblyFor<Program>()
                        .UseServices(services)
                        .Build();
        
    }
}
```

### Creating A QueryHandler

```c#
// GetAllPlayersQueryHandler.cs

public class GetAllPlayersQueryArgs
{
}

public class GetAllPlayersQueryHandler : IQueryHandler<GetAllPlayersQueryArgs, Player[]>
{
    private readonly FootballContext _context;
    
    public GetAllPlayersQueryHandler(FootballContext context) 
    {
        _context = context;
    }

    public async Task<Player[]> Execute(GetAllPlayersQueryArgs query) 
    {
        return await _context.Players.ToArrayAsync();
    }
}
```

### Creating a CommandHandler

```c#
// AddPlayerCommandHandler.cs

public class AddPlayerCommandArgs
{
    public string Name { get; }
    
    public AddPlayerCommandArgs(string name)
    {
        Name = name;
    }
}

public class AddPlayerCommandHandler : IQueryHandler<AddPlayerCommandArgs>
{
    private readonly FootballContext _context;
    
    public AddPlayerCommandHandler(FootballContext context) 
    {
        _context = context;
    }

    public async Task Execute(AddPlayerCommandArgs query) 
    {
        _context.Add(new Player
            {
                Name = query.Name
            });
        await _context.SaveChangesAsync();
    }
}
```

### Executing a Query And Command

```c#
// PlayersController.cs
[Route("[controller]")]
public class PlayersController : Controller
{
    private readonly ICqrsBus _cqrsBus;
    
    public PlayersController(ICqrsBus cqrsBus) 
    {
        _cqrsBus = cqrsBus
    }
    
    [HttpGet]
    public async Task<Player[]> GetAll() 
    {
        // Execute a IQueryHandler that accepts a GetAllPlayersQuery as an argument and returns Task<Player[]>.
        var players = await _cqrsBus.Execute<GetAllPlayersQueryArgs, Player[]>(new GetAllPlayersQueryArgs());
        return Ok(players);
    }
    
    [HttpGet]
    public async Task<IActionResult> Add([FromBody] AddPlayerCommandArgs args) 
    {
        // Execute a IQueryHandler that accepts a GetAllPlayersQuery as an argument and returns Task<Player[]>.
        await _cqrsBus.Execute(args);
        return Created();
    }
}
```