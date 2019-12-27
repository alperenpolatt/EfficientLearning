using AutoMapper;
using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Announcements;
using EfLearning.Core.Classrooms;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class MaterailManager:IMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMaterialDal _materialDal;
        private IAnnouncementDal _announcementDal;
        private ITakenClassroomDal _takenClassroomDal;
        private readonly IMapper _mapper;
        public MaterailManager(IUnitOfWork unitOfWork, IMaterialDal materialDal, IAnnouncementDal announcementDal, ITakenClassroomDal takenClassroomDal, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _materialDal = materialDal;
            _announcementDal = announcementDal;
            _takenClassroomDal = takenClassroomDal;
            _mapper = mapper;
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

        public async Task<BasexResponse<MaterialQuestionResponse>> GetByIdAsync(int id)
        {
            try
            {
                var resMaterial = await _materialDal.GetAsync(id);
                var material = _mapper.Map<Material, MaterialQuestionResponse>(resMaterial);
                return new BasexResponse<MaterialQuestionResponse>(material);
            }
            catch (Exception ex)
            {

                return new BasexResponse<MaterialQuestionResponse>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<CloseMaterialResponse>>> GetCloseMaterials(int userId)
        {
            try
            {
                var materials = (await _takenClassroomDal.GetByUserIdWithGivenClassroomAndItsMaterialsAsync(userId)).SelectMany(a => a.GivenClassroom.Materials);
                var closeMaterial = materials.Where(m => (m.Deadline.Value.Subtract(DateTime.UtcNow)).TotalMinutes <= (5*24*60) 
                && (m.Deadline.Value.Subtract(DateTime.UtcNow)).TotalMilliseconds > 0);

                var list = new Collection<CloseMaterialResponse>();

                foreach (var item in closeMaterial)
                {
                    list.Add(new CloseMaterialResponse
                    {
                        Id=item.Id,
                        Deadline=item.Deadline.Value,
                        Description=item.Announcement.Description,
                        GivenClassroomId=item.GivenClassroomId,
                        CreationTime=item.CreationTime
                    });
                }


                return new BasexResponse<ICollection<CloseMaterialResponse>>(list);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<CloseMaterialResponse>>(ex.Message);
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

        public async Task<BasexResponse<CountResponse>> GetMaterialCountAsync(int userId)
        {
            try
            {

                var count =await  _materialDal.GetNumberOfMaterialAsync(userId);
                return new BasexResponse<CountResponse>(new CountResponse { Count = count });
            }
            catch (Exception ex)
            {
                return new BasexResponse<CountResponse>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<CloseMaterialResponse>>> GetFreshMaterials(int userId)
        {
            try
            {
                var materials = (await _takenClassroomDal.GetByUserIdWithGivenClassroomAndItsMaterialsAsync(userId)).SelectMany(a => a.GivenClassroom.Materials);
                var freshMaterial = materials.Where(m => DateTime.UtcNow.Subtract(m.CreationTime).TotalDays<=1);


                var list = new Collection<CloseMaterialResponse>();

                foreach (var item in freshMaterial)
                {
                    list.Add(new CloseMaterialResponse
                    {
                        Id = item.Id,
                        Deadline = item.Deadline.Value,
                        Description = item.Announcement.Description,
                        GivenClassroomId = item.GivenClassroomId,
                        CreationTime = item.CreationTime
                    });
                }


                return new BasexResponse<ICollection<CloseMaterialResponse>>(list);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<CloseMaterialResponse>>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<MaterialResultResponse>>> GetMaterialsAndAnwers(int userId, int givenClassroomId)
        {
            try
            {
                var materials = await _materialDal.GetWithAnnouncementAndAnswerAsync(givenClassroomId);
                var usersAnswers = materials.Where(m => m.MaterialAnswers.Any(ma => ma.UserId == userId)).SelectMany(m=>m.MaterialAnswers);
                var list = new Collection<MaterialResultResponse>();
                foreach (var item in materials)
                {
                    list.Add(new MaterialResultResponse
                    {
                        Id = item.Id,
                        CreationTime = item.CreationTime,
                        Deadline = item.Deadline.Value,
                        Description = item.Announcement.Description,
                        MaterialType = item.MaterialType,
                        Score = usersAnswers.FirstOrDefault(m => m.MaterialId == item.Id)?.Score
                    }) ;
                }
                


                return new BasexResponse<ICollection<MaterialResultResponse>>(list);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<MaterialResultResponse>>(ex.Message);
            }
        }
    }
}
