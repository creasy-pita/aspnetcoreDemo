using MyPipeline.Services;
using System;
using System.Threading.Tasks;

namespace MyPipeline
{
    class Program
    {
        static ServiceBuilder _serviceBuilder;
        static void Main(string[] args)
        {
            _serviceBuilder = new ServiceBuilder();
            ConfigurePipeline();
            _serviceBuilder.Run();
            Console.ReadKey();
        }
        /// <summary>
        /// 自定义管道 middleware 处理逻辑 直接使用 lambda 表达式方式传入
        /// </summary>
        static void ConfigurePipeline()
        {
            _serviceBuilder.Use((next) =>
                {
                    return 
                        (context) =>
                        {
                            Console.WriteLine("filter specific character");
                            Task task = next.Invoke(context);
                            Console.WriteLine("filter specific character");
                            return task;
                        };
                }
            );

            _serviceBuilder.Use((next) => {
                return (context) =>
                {
                    Console.WriteLine("tran begin");
                    Task t = next.Invoke(context);
                    Console.WriteLine("tran end");
                    return t;
                };
            });

            _serviceBuilder.Use((next) => {
                return (context) =>
                {
                    Console.WriteLine("log begin");
                    Task t = next.Invoke(context);
                    Console.WriteLine("log end");
                    return t;
                };
            });
            RequestDelegate end = (context) => {
                Console.WriteLine("ending ....");
                return Task.CompletedTask;
            };
        }
    }


}
