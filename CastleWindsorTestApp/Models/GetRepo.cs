using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CastleWindsorTestApp.Models
{
    /// <summary>
    /// Get Repositories
    /// </summary>
    public class GetRepo : IDisposable
    {
        private static CustleWindsorContext custleWindsorContext = new CustleWindsorContext();

        internal static IRepository<Employee> EmployeeRepository => new Repository<Employee>(custleWindsorContext);
        internal static IRepository<Department> DepartmentRepository => new Repository<Department>(custleWindsorContext);


        #region Disposing

        bool disposed = false;
        void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    custleWindsorContext.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}