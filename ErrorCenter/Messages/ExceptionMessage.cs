using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCenter.Messages
{
    public class ExceptionMessage
    {
        public static readonly string SelectFromCategoriesException = "Select data from MS_Categories failed. Ex:00.001";
        public static readonly string InsertIntoUsersException = "Insert into MS_Users failed. Ex:00.002";
        public static readonly string InsertIntoActivationsException = "Insert into MS_Activations failed. Ex:00.003";
        public static readonly string InsertIntoServersException = "Insert into MS_Servers failed. Ex:00.004";
        public static readonly string DeleteFromUsersException = "Delete from MS_Users failed. Ex:00.005";
        public static readonly string ExistActivationByUser = "Select from ActivationByUser failed. Ex:00.006";
        public static readonly string InsertIntoTokensException = "Insert into MS_Tokens failed. Ex:00.007";
        public static readonly string SelectFromUsersByTelException = "Select from  MS_Users failed. Ex:00.008";
        public static readonly string ValidatingToken = "Validation token failed. Ex:00.007";
        public static readonly string SelectFromCategoriesServersException = "Select data from MS_Categories, MS_Servers failed. Ex:00.008";
        public static readonly string SelectFromCategoriesIdServersException = "Select data from MS_Categories, MS_Servers failed. Ex:00.009";
    }
}
