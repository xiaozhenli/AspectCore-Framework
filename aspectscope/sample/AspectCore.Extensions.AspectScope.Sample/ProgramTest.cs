using AspectCore.Injector;
using System;
using System.Collections.Generic;
using System.Text;
using AspectCore.Configuration;
using AspectCore.DynamicProxy;
using System.Threading.Tasks;

namespace AspectCore.Extensions.AspectScope.Sample
{
    class ProgramTest
    {
        public static void Main()
        {
            IServiceContainer services = new ServiceContainer();
            services.AddType<ICanService, PeopleService>();

            services.Configure(configure => {
                configure.Interceptors.Add(new CustomInterceptorFactory());
            });

           var serviceResolver =  services.Build();
           var service= (ICanService)serviceResolver.GetService(typeof(ICanService));

           service.CanSayHello("测试");


        }

    }


    /// <summary>
    ///自定义拦截器工厂
    /// </summary>
    class CustomInterceptorFactory : InterceptorFactory
    {
        public override IInterceptor CreateInstance(IServiceProvider serviceProvider)
        {
            return new CustomInterceptor();
        }
    }


    /// <summary>
    /// 自定义拦截器
    /// </summary>
    class CustomInterceptor : IInterceptor
    {
        public bool AllowMultiple { get; set; }
        public bool Inherited { get; set; }
        public int Order { get ; set; }
        public Task Invoke(AspectContext context, AspectDelegate next)
        {
            Console.WriteLine("执行自定义拦截器,调用之后的拦截器");
            return context.Invoke(next);
        }
    }


    interface ICanService {
        void CanSayHello(string words);
    }


    class PeopleService : ICanService
    {
        public void CanSayHello(string words)
        {
            Console.WriteLine("测试");
        }
    }
}
