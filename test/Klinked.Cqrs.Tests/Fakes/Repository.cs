using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Klinked.Cqrs.Commands;
using Klinked.Cqrs.Queries;

namespace Klinked.Cqrs.Tests.Fakes
{
    public class Model
    {
        public Guid Id { get; set; }
    }
    
    public class Repository : IQueryHandler<Guid, Model>, ICommandHandler<Model>
    {
        private static readonly List<Model> _models = new List<Model>();

        public static Model[] Models => _models.ToArray();

        public Task<Model> Execute(Guid args)
        {
            return Task.FromResult(Models.SingleOrDefault(m => m.Id == args));
        }

        public Task Execute(Model args)
        {
            _models.Add(args);
            return Task.CompletedTask;
        }

        public static void Clear()
        {
            _models.Clear();
        }
    }
}