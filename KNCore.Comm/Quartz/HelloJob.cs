using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNCore.Comm.Quartz
{
    public class HelloJob:IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.Out.WriteAsync("quartz test");
            return Task.CompletedTask;
        }
    }
}
