using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCenter
{
    public class SelectFromDataBaseException : Exception
    {
        public SelectFromDataBaseException(string message, Exception ex) : base(message, ex)
        {

        }

    }

    public class InsertIntoDataBaseException : Exception
    {
        public InsertIntoDataBaseException(string message, Exception ex) : base(message, ex)
        {

        }

    }
    public class DeleteFromDataBaseException : Exception
    {
        public DeleteFromDataBaseException(string message, Exception ex) : base(message, ex)
        {

        }

    }
}
