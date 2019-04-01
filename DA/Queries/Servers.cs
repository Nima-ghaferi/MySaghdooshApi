using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorCenter;
using ErrorCenter.Messages;
using BE.Entities.Response;
using BE.Entities.Requests;

namespace DA.Queries
{
    public class Servers
    {
        private static readonly int ItemsPerPage = 5;
        public static int InsertServerInfo(BE.Entities.Request.ServerInfoReq serverInfo)
        {
            try
            {
                var server = new Entities.Server()
                {
                    Name = serverInfo.Name,
                    Address = serverInfo.Address,
                    Tel = serverInfo.Tel,
                    IsActive = true,
                    CategoryId = serverInfo.CategoryId
                };
                using (var dbCtx = new MSDbContext())
                {
                    dbCtx.Servers.Add(server);
                    int result = dbCtx.SaveChanges();

                    if (result == 1)
                    {
                        foreach (var serverPhoto in serverInfo.Photos)
                        {
                            byte[] photoByte = Convert.FromBase64String(serverPhoto);
                            var serverPhotoEnt = new Entities.ServerPhoto()
                            {
                                Photo = photoByte,
                                ServerId = server.Id
                            };
                            dbCtx.ServerPhotos.Add(serverPhotoEnt);
                            dbCtx.SaveChanges();
                        }
                        return server.Id;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception e)
            {
                var ex = new InsertIntoDataBaseException(ExceptionMessage.InsertIntoServersException, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static List<ServiceProviderGeneralInfoResp> SelectMainPageByPageIndex(int pageIndex)
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {
                    List<ServiceProviderGeneralInfoResp> spList = new List<ServiceProviderGeneralInfoResp>();
                    var servers = (from s in dbCtx.Servers
                                   join c in dbCtx.Categories on s.CategoryId equals c.Id
                                   join sp in dbCtx.ServerPhotos on s.Id equals sp.ServerId into ssp
                                   from sp in ssp.DefaultIfEmpty()
                                   where s.IsActive && c.IsActive && (sp.IsPrimary || sp == null)
                                   orderby s.Id descending
                                   select new { s, c, sp.Photo }).Skip(ItemsPerPage * pageIndex).Take(ItemsPerPage).ToList();
                    foreach (var server in servers)
                    {
                        var temp = new ServiceProviderGeneralInfoResp()
                        {
                            Id = server.s.Id,
                            Name = server.s.Name,
                            CategoryId = server.c.Id,
                            CategoryName = server.c.Name,
                            PrimaryPhoto = server.Photo != null ? Convert.ToBase64String(server.Photo) : null
                        };
                        spList.Add(temp);
                    }
                    return spList;
                }
            }
            catch (Exception e)
            {
                var ex = new SelectFromDataBaseException(ExceptionMessage.SelectFromCategoriesServersException, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static List<ServiceProviderGeneralInfoResp> SelectCategoryServiceByPageIndex(int categoryId, int pageIndex)
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {
                    List<ServiceProviderGeneralInfoResp> spList = new List<ServiceProviderGeneralInfoResp>();
                    var servers = (from s in dbCtx.Servers
                                   join c in dbCtx.Categories on s.CategoryId equals c.Id
                                   join sp in dbCtx.ServerPhotos on s.Id equals sp.ServerId into ssp
                                   from sp in ssp.DefaultIfEmpty()
                                   where s.IsActive && c.IsActive && s.CategoryId == categoryId
                                        && (sp.IsPrimary || sp == null)
                                   orderby s.Id descending
                                   select new { s, c, sp.Photo }).Skip(ItemsPerPage * pageIndex).Take(ItemsPerPage).ToList();
                    foreach (var server in servers)
                    {
                        var temp = new ServiceProviderGeneralInfoResp()
                        {
                            Id = server.s.Id,
                            Name = server.s.Name,
                            CategoryId = server.c.Id,
                            CategoryName = server.c.Name,
                            PrimaryPhoto = server.Photo != null ? Convert.ToBase64String(server.Photo) : null
                        };
                        spList.Add(temp);
                    }
                    return spList;
                }
            }
            catch (Exception e)
            {
                var ex = new SelectFromDataBaseException(ExceptionMessage.SelectFromCategoriesIdServersException, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static ServiceProviderSpecificResp SelectServiceProviderById(int id)
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {

                    var server = (from s in dbCtx.Servers
                                  join c in dbCtx.Categories on s.CategoryId equals c.Id
                                  join sp in dbCtx.ServerPhotos on s.Id equals sp.ServerId into ssp
                                  from sp in ssp.DefaultIfEmpty()
                                  where s.IsActive && c.IsActive && s.Id == id
                                  && (sp.IsPrimary || sp == null)
                                  select new { s, c, sp.Photo }).FirstOrDefault();
                    if (server == null)
                    {
                        return null;
                    }
                    var photos = (from p in dbCtx.ServerPhotos
                                  where p.ServerId == id
                                  select p).ToList();
                    List<string> photoBase64List = new List<string>();
                    foreach (var photo in photos)
                    {
                        string temp = Convert.ToBase64String(photo.Photo);
                        photoBase64List.Add(temp);
                    }

                    ServiceProviderSpecificResp serviceProvider = new ServiceProviderSpecificResp()
                    {
                        Id = server.s.Id,
                        Name = server.s.Name,
                        Address = server.s.Address,
                        CategoryName = server.c.Name,
                        CategoryId = server.c.Id,
                        Tel = server.s.Tel,
                        PrimaryPhoto = server.Photo != null ? Convert.ToBase64String(server.Photo) : null,
                        PhotoList = photoBase64List
                    };
                    return serviceProvider;
                }
            }
            catch (Exception e)
            {
                var ex = new SelectFromDataBaseException(ExceptionMessage.SelectFromCategoriesServerIdException, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static int InsertServiceProviderLike(ServiceProviderLikeReq likeReq)
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {
                    var user = UserAccounts.SelectUserByTel(likeReq.UserMobileNumber);
                    var checkExistLike = (from likes in dbCtx.ServiceProviderLike
                                         where likes.ServerId == likeReq.ServerId && likes.UserId == user.Id
                                         select likes).Count();
                    if(checkExistLike > 0)
                    {
                        return 0;
                    }
                    var serviceProvideLike = new Entities.ServiceProviderLike()
                    {
                        ServerId = likeReq.ServerId,
                        UserId = user.Id
                    };
                    dbCtx.ServiceProviderLike.Add(serviceProvideLike);
                    int result = dbCtx.SaveChanges();

                    if (result == 1)
                    {
                        return serviceProvideLike.Id;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception e)
            {
                var ex = new InsertIntoDataBaseException(ExceptionMessage.InsertIntoServiceProviderLikeException, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public static bool DeleteServiceProviderLike(ServiceProviderLikeReq likeReq)
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {
                    dbCtx.ServiceProviderLike.RemoveRange(
                        dbCtx.ServiceProviderLike.Where(x => x.User.Tel == likeReq.UserMobileNumber && x.ServerId == likeReq.ServerId));
                    dbCtx.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                var ex = new InsertIntoDataBaseException(ExceptionMessage.DeleteServiceProviderLikeException, e);
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}