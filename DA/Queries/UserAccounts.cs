using DA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorCenter;
using ErrorCenter.Messages;

namespace DA.Queries
{
    public class UserAccounts
    {
        public static int InsertUser(BE.Entities.Requests.NewUser newUser)
        {
            try
            {
                var user = new User()
                {
                    IsActive = true,
                    Name = newUser.Name,
                    Tel = newUser.Tel
                };
                using (var dbCtx = new MSDbContext())
                {

                    dbCtx.Users.Add(user);
                    int result = dbCtx.SaveChanges();
                    if (result == 1)
                    {
                        return user.Id;
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
            }
            catch (Exception e)
            {
                var ex = new InsertIntoDataBaseException(ExceptionMessage.InsertIntoUsersException, e);
                Utils.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static bool DeleteUser(int id)
        {
            try
            {
                var user = new User()
                {
                    Id = id
                };
                using (var dbCtx = new MSDbContext())
                {

                    dbCtx.Users.Remove(user);
                    int result = dbCtx.SaveChanges();
                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
            }
            catch (Exception e)
            {
                var ex = new DeleteFromDataBaseException(ExceptionMessage.DeleteFromUsersException, e);
                Utils.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static int InsertActivation(BE.Entities.Requests.NewActivation newActivation)
        {
            try
            {
                var activation = new Activation()
                {
                    UserId = newActivation.UserId,
                    ActivationCode = newActivation.ActicationCode.ToString(),
                    IsActive = true
                };
                using (var dbCtx = new MSDbContext())
                {

                    dbCtx.Activations.Add(activation);
                    int result = dbCtx.SaveChanges();
                    if (result == 1)
                    {
                        return activation.Id;
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
            }
            catch (Exception e)
            {
                var ex = new InsertIntoDataBaseException(ExceptionMessage.InsertIntoActivationsException, e);
                Utils.Log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
