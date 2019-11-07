using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninject.Modules;
using TaskList.BLL.Interfaces;
using TaskList.BLL.Services;

namespace TaskList.WEB.Util
{
    public class ProjectServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectService>().To<ProjectService>();
        }
    }
}
