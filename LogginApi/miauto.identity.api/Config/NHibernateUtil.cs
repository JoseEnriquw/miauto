using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Mapping.Attributes;
using NHibernate.Cfg;
using System.Reflection;
using miauto.identity.api.Models.Entity;
using System;

namespace miauto.identity.api.Config
{
    public class NHibernateUtil
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory BuilSessionFactory()
        {
            try
            {

                if(NHibernateUtil._sessionFactory == null)
                {
                    Configuration configuration = new Configuration();
                    configuration.Configure();

                    HbmSerializer.Default.Validate = true;
                    configuration.AddInputStream(HbmSerializer.Default.Serialize(Assembly.GetExecutingAssembly()));    

                    configuration.AddAssembly(typeof(User).Assembly);
           
                    new SchemaExport(configuration).Execute(true,true,false);

                    NHibernateUtil._sessionFactory = configuration.BuildSessionFactory();

                }
                return NHibernateUtil._sessionFactory;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static ISessionFactory GetISessionFactory() { return NHibernateUtil._sessionFactory == null?  BuilSessionFactory(): NHibernateUtil._sessionFactory; }

        public static void Start() { BuilSessionFactory(); }
        public static void Stop() { NHibernateUtil.GetISessionFactory().Close(); }

    }
}
