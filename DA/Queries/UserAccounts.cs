using DA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorCenter;
using ErrorCenter.Messages;
using BE.Entities.Requests;

namespace DA.Queries
{
    public class UserAccounts
    {
        public static int InsertUser(NewUserReq newUser)
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
                Logger.Log.Error(ex.Message, ex);
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
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static int InsertActivation(NewActivationReq newActivation)
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
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static bool ExistActivationByUser(ActivationCodeReq activationCode)
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {
                    var userActiovation = (from a in dbCtx.Activations 
                                      join u in dbCtx.Users on a.UserId equals u.Id
                                      where a.ActivationCode == activationCode.Code 
                                      && u.Tel == activationCode.Tel
                                      && a.IsActive
                                      select new {a, u}).FirstOrDefault();
                    return userActiovation != null;
                }
            }
            catch (Exception e)
            {
                var ex = new SelectFromDataBaseException(ExceptionMessage.ExistActivationByUser, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static int InsertToken(NewTokenReq newToken)
        {
            try
            {
                var token = new Token()
                {
                    IsActive = true,
                    UserId = newToken.UserId,
                    TokenStr = newToken.TokenStr,
                    LastUsageTime = newToken.TokenTime
                };
                using (var dbCtx = new MSDbContext())
                {

                    dbCtx.Tokens.Add(token);
                    int result = dbCtx.SaveChanges();
                    if (result == 1)
                    {
                        return token.Id;
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
            }
            catch (Exception e)
            {
                var ex = new InsertIntoDataBaseException(ExceptionMessage.InsertIntoTokensException, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static User SelectUserByTel(string tel)
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {
                    var user = (from u in dbCtx.Users 
                                    where u.Tel == tel
                                    select u).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception e)
            {
                var ex = new SelectFromDataBaseException(ExceptionMessage.SelectFromUsersByTelException, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static bool ValidateToken(string phoneNumber, string token)
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {
                    var userToken = (from t in dbCtx.Tokens
                                           join u in dbCtx.Users on t.UserId equals u.Id
                                           where t.TokenStr == token
                                           && u.Tel == phoneNumber
                                           && t.IsActive
                                           select new { t, u }).FirstOrDefault();
                    if(userToken == null)
                    {
                        return false;
                    }
                    var now = DateTime.Now;
                    var diff = now - userToken.t.LastUsageTime;
                    if (diff.Days > 30)
                    {
                        return false;
                    }
                    userToken.t.LastUsageTime = now;
                    dbCtx.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                var ex = new SelectFromDataBaseException(ExceptionMessage.ValidatingToken, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
