using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace KNCore.Comm.Quartz
{
    public class TestTask
    {
        public async Task StartTestAsync()
        {
            try
            {
                // 从工厂中获取调度程序实例
                NameValueCollection props = new NameValueCollection {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);

                IScheduler scheduler = await factory.GetScheduler();
                // 开启调度器
                await scheduler.Start();
                // 定义这个工作，并将其绑定到我们的IJob实现类
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();
                // 触发作业立即运行，然后每10秒重复一次，无限循环
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1","group1")
                    .StartNow()
                    .WithSimpleSchedule( x=> 
                    x.WithIntervalInSeconds(10)
                    .RepeatForever())
                    .Build();

                // 告诉Quartz使用我们的触发器来安排作业
                await scheduler.ScheduleJob(job,trigger);
                // 等待60秒
                await Task.Delay(TimeSpan.FromSeconds(60));
                // 关闭调度程序
                await scheduler.Shutdown();

            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteAsync(se.ToString());
            }
        }

        public async Task StartSimpleTrigger()
        {
            NameValueCollection props = new NameValueCollection {
                    { "quartz.serializer.type", "binary" }
                };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // 触发器构建器默认创建一个简单的触发器，实际上返回一个ITrigger
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(DateTime.Now) //指定开始时间为当前系统时间
                .ForJob("job1", "group1") //通过JobKey识别作业
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

    }
}
