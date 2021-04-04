using System;
using System.Collections.Generic;
using System.Text;

namespace TBBProject.Core.Common.ServiceException
{
    public class ServiceControlledException : Exception
    {
        public ServiceControlledException()
        {

        }

        public ServiceControlledException(string exception)
            : base(String.Format("Service Controlled Exception : {0}", exception))
        {

        }

    }

}
