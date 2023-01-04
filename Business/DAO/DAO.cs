using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vapeur.Business.DAO
{
    public abstract class DAO<T>
    {
        protected string connectionString = null;

        public DAO() {
            this.connectionString = ConfigurationManager.ConnectionStrings["VapeurDB"].ConnectionString;
        }

        public abstract bool Create(T obj);
        public abstract bool Delete(T obj);
        public abstract bool Update(T obj);
        public abstract T Read(int id);
        public abstract T Read(int id1,int id2);
        public abstract T Read(string chain);
        public abstract List<T> GetAll();

    }
}
