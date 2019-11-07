using System;
using Ninject.Modules;
using TaskList.DAL.Interfaces;
using TaskList.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TaskList.BLL.Infrastructure
{
    public class RepositoryModule : NinjectModule
    {
        private DbContextOptions Options { get; set; }

        public RepositoryModule(DbContextOptions options)
        {
            Options = options;
        }

        public override void Load()
        {
            {
                Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(Options);
            }
        }
    }
}
