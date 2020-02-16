using CommonLayer.DTO;
using CommonLayer.DTO.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class DALUtility
    {
        private AppDBContext context;
        public DALUtility()
        {
            context = new AppDBContext();
        }

        public PlatformWellViewModel GetPlatformWellList()
        {
            PlatformWellViewModel platformWellViewModel = new PlatformWellViewModel();
            platformWellViewModel.Platforms = context.Platforms.ToList();
            platformWellViewModel.Wells = context.Wells.Join(context.Platforms, w => w.platformId, p => p.id, (w, p) => new { w, p })
                                                 .Select (result => new Well
                                                 {
                                                     PlatformName = result.p.uniqueName ,
                                                     uniqueName = result.w.uniqueName,
                                                     latitude = result.w.latitude,
                                                     longitude = result.w.longitude,
                                                     createdAt = result.w.createdAt,
                                                     updatedAt = result.w.updatedAt
                                                 }).ToList();
                                                  

            return platformWellViewModel;
        }
        public void InsertOrUpdatePlatformWellData(List<Platform> platformList)
        {
            foreach (Platform platform in platformList)
            {
                var platformDto = context.Platforms.Find(platform.id);

                // if entity does not already exist -> create new
                if (platformDto == null)
                {
                    platformDto = new Platform();
                    platformDto.id = platform.id;
                    context.Platforms.Add(platformDto);
                }

                // map received values
                platformDto.uniqueName = platform.uniqueName;
                platformDto.latitude = platform.latitude;
                platformDto.longitude = platform.longitude;
                platformDto.createdAt = platform.createdAt;
                platformDto.updatedAt = platform.updatedAt;

                foreach (var item in platform.well)
                {
                    var wellDto = context.Wells.Find(item.id);

                    // if entity does not already exist -> create new
                    if (wellDto == null)
                    {
                        wellDto = new Well();
                        wellDto.id = item.id;
                        context.Wells.Add(wellDto);
                    }

                    wellDto.platformId = item.platformId;
                    wellDto.uniqueName = item.uniqueName;
                    wellDto.latitude = item.latitude;
                    wellDto.longitude = item.longitude;
                    wellDto.createdAt = item.createdAt;
                    wellDto.updatedAt = item.updatedAt;

                }

            }

            context.SaveChanges();
        }
    }
}
