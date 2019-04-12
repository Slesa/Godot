using System;
using NHibernate;

namespace PersonalPlanung.Persistence.nh.Contracts
{
    public interface IHibernateSessionFactory : IDisposable
    {
        ISession CreateSession();
    }
}