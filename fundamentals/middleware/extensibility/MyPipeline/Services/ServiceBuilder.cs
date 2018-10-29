using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPipeline.Services
{
    class ServiceBuilder
    {
        public static List<Func<RequestDelegate, RequestDelegate>> _list = new List<Func<RequestDelegate, RequestDelegate>>();
        public void Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _list.Add(middleware);
        }

        public void Run()
        {
            RequestDelegate end = (context) => {
                Console.WriteLine("ending ....");
                return Task.CompletedTask;
            };
            //倒置，使得后面的 requestdelegate 作为前面中间件的next
            _list.Reverse();
            //middleware.Invoke ，完成requestdelegate方法上的连接
            foreach (var middleware in _list)
            {
                end = middleware.Invoke(end);
            }

            end.Invoke(new Context());
            Console.ReadLine();
        }
    }
}
