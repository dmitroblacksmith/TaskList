using System;
using Ninject.Modules;
using TaskList.BLL.Interfaces;
using TaskList.BLL.Services;

namespace TaskList.WEB.Util
{
    public class TaskServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITaskService>().To<TaskService>();
        }
    }
}
