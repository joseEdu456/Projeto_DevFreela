using Bogus;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Faker
{
    public class FakeDataHelper
    {
        private static readonly Faker<Project> _fakerProject = new Faker<Project>("pt_BR")
            .CustomInstantiator(f => new Project(
                f.Commerce.ProductName(),
                f.Lorem.Sentence(),
                f.Random.Int(1, 100),
                f.Random.Int(1, 100),
                f.Random.Decimal(1000, 10000)
             )
            );

        public static Project CreateFakeProject()
        {
            return _fakerProject.Generate();
        }

        private static readonly Faker<InsertProjectCommand> _fakerInsertProjectCommand = new Faker<InsertProjectCommand>("pt_BR")
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Descricao, f => f.Lorem.Sentence())
            .RuleFor(p => p.IdClient, f => f.Random.Int(1, 100))
            .RuleFor(p => p.IdFreelancer, f => f.Random.Int(1, 100))
            .RuleFor(p => p.TotalCost, f => f.Random.Decimal(1000, 10000));

        public static InsertProjectCommand CreateFakeInsertProjectCommand() => _fakerInsertProjectCommand.Generate();
    }
}
