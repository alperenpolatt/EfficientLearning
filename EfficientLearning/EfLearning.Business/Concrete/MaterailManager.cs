using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Announcements;
using EfLearning.Core.Classrooms;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class MaterailManager:IMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMaterialDal _materialDal;
        private IAnnouncementDal _announcementDal;
        public MaterailManager(IUnitOfWork unitOfWork, IMaterialDal materialDal, IAnnouncementDal announcementDal)
        {
            _unitOfWork = unitOfWork;
            _materialDal = materialDal;
            _announcementDal = announcementDal;
        }

        public async Task<BasexResponse<Material>> CreateAsync(Material material,string description)
        {
            try
            {
                material.CreationTime = DateTime.UtcNow;
                var resultMaterial=await _materialDal.AddAsync(material);
                await _unitOfWork.CompleteAsync();
                var announcement=await _announcementDal.AddAsync(new Announcement
                {
                    CreationTime = DateTime.UtcNow,
                    Description=description,
                    MaterialId=resultMaterial.Id
                });
                await _unitOfWork.CompleteAsync();
                resultMaterial.AnnouncementId = announcement.Id;
                await _materialDal.UpdateAsync(resultMaterial, resultMaterial.Id);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<Material>(material);
            }
            catch (Exception ex)
            {
                return new BasexResponse<Material>(ex.Message);
            }
        }

        public async Task<BasexResponse<Material>> DeleteByIdAsync(int materialId)
        {
            try
            {
                var material = await _materialDal.GetAsync(materialId);
                _materialDal.Delete(material);
                var announcement=await _announcementDal.GetAsync(material.AnnouncementId);
                _announcementDal.Delete(announcement);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<Material>(material);
            }
            catch (Exception ex)
            {

                return new BasexResponse<Material>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<Material>>> GetMaterialsByGivenClassroomId(int givenClassroomId)
        {
            try
            {
                var givenClassrooms = await _materialDal.FindAllAsync(m => m.GivenClassroomId == givenClassroomId);
                return new BasexResponse<ICollection<Material>>(givenClassrooms);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<Material>>(ex.Message);
            }
        }

        public Task<BasexResponse<ICollection<Material>>> GetMaterialsByGivenClassroomIdAndUserId(int givenClassroomId, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<BasexResponse<Material>> UpdateAsync(Material material,string description)
        {
            try
            {
                var resultMaterial = await _materialDal.GetAsync(material.Id);
                resultMaterial.MaterialScale = material.MaterialScale;
                resultMaterial.Question = material.Question;
                resultMaterial.Hint = material.Hint;
                var resultAnnouncement = await _announcementDal.GetAsync(resultMaterial.AnnouncementId);
                resultAnnouncement.Description = description;

                var updatedMaterial = await _materialDal.UpdateAsync(resultMaterial, resultMaterial.Id);
                 await _announcementDal.UpdateAsync(resultAnnouncement, resultAnnouncement.Id);
                await _unitOfWork.CompleteAsync();
                updatedMaterial.Announcement = null;//can't parse JSON
                return new BasexResponse<Material>(updatedMaterial);
            }
            catch (Exception ex)
            {
                return new BasexResponse<Material>(ex.Message);
            }
        }
    }
}
