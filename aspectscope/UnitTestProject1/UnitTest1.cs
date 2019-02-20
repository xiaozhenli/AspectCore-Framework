using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build();
            //创建ICanService的代理类  CreateInterfaceProxy<T> 只会mock接口  不会调用PeopleCanServcie的方法
            //                        CreateInterfaceProxy<Tinterface,TImplate> 会模拟接口也会调用PeopleCanService的方法
            ICanService canServiceProxy = proxyGenerator.CreateInterfaceProxy<ICanService, PeopleCanServcie>();
            Console.WriteLine(canServiceProxy.GetType().ToString());
            canServiceProxy.SayHello();
            canServiceProxy.SayBye();
        }


        [CustomInterceptor]
        public interface ICanService
        {
            /// <summary>
            /// 注入拦截器
            /// </summary>
            void SayHello();
            void SayBye();
        }

        class PeopleCanServcie : ICanService
        {
            public void SayHello()
            {
                Console.WriteLine("开始测试");
            }

            public void SayBye()
            {
                Console.WriteLine("原始的再见");
            }

        }






        /// <summary>
        /// 自定义拦截器属性
        /// </summary>
        public class CustomInterceptor : AbstractInterceptorAttribute
        {
            //拦截器
            public override Task Invoke(AspectContext context, AspectDelegate next)
            {
                Console.WriteLine("拦截的方法");
                return context.Invoke(next);
            }
        }
    }
}
