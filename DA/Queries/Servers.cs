using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorCenter;
using ErrorCenter.Messages;
using BE.Entities.Response;

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

        public static List<ServiceProviderGeneralInfo> SelectMainPageByPageIndex(int pageIndex)
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {
                    List<ServiceProviderGeneralInfo> spList = new List<ServiceProviderGeneralInfo>();
                    var servers = (from s in dbCtx.Servers
                                   join c in dbCtx.Categories on s.CategoryId equals c.Id
                                   join sp in dbCtx.ServerPhotos on s.Id equals sp.ServerId into ssp
                                   from sp in ssp.DefaultIfEmpty()
                                   where s.IsActive && c.IsActive && (sp.IsPrimary || sp == null)
                                   orderby s.Id descending
                                   select new { s, c, sp.Photo}).Skip(ItemsPerPage * pageIndex).Take(ItemsPerPage).ToList();
                    foreach(var server in servers)
                    {
                        var temp = new ServiceProviderGeneralInfo()
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

        public static List<ServiceProviderGeneralInfo> SelectCategoryServiceByPageIndex(int categoryId, int pageIndex)
        {
            try
            {
                using (var dbCtx = new MSDbContext())
                {
                    List<ServiceProviderGeneralInfo> spList = new List<ServiceProviderGeneralInfo>();
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
                        var temp = new ServiceProviderGeneralInfo()
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
    }
}
