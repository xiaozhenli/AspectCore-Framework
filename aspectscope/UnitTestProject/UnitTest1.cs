using AspectCore.DynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build();
            //����ICanService�Ĵ�����  CreateInterfaceProxy<T> ֻ��mock�ӿ�  �������PeopleCanServcie�ķ���
            //                        CreateInterfaceProxy<Tinterface,TImplate> ��ģ��ӿ�Ҳ�����PeopleCanService�ķ���
            ICanService canServiceProxy = proxyGenerator.CreateInterfaceProxy<ICanService, PeopleCanServcie>();
            Console.WriteLine(canServiceProxy.GetType().ToString());
            canServiceProxy.SayHello();
            canServiceProxy.SayBye();

        }

    }


    [CustomInterceptor]
    public interface ICanService
    {
        /// <summary>
        /// ע��������
        /// </summary>
    
        void SayHello();

        void SayBye();
    }

    class PeopleCanServcie : ICanService
    {

        public void SayHello()
        {
            Console.WriteLine("��ʼ����");
        }

        public void SayBye()
        {
            Console.WriteLine("ԭʼ���ټ�");
        }


    }






    /// <summary>
    /// �Զ�������������
    /// </summary>
    public class CustomInterceptor : AbstractInterceptorAttribute
    {
        //������
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            Console.WriteLine("���صķ���");
            return context.Invoke(next);
        }
    }
}
