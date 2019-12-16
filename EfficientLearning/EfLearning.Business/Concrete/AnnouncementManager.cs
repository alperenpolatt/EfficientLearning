using AutoMapper;
using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Announcements;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class AnnouncementManager:IAnnouncementService
    {
        private IAnnouncementDal _announcementDal;
        private readonly IMapper _mapper;
        public AnnouncementManager(IAnnouncementDal announcementDal,IMapper mapper)
        {
            _announcementDal = announcementDal;
            _mapper = mapper;

        }

        public async Task<BasexResponse<ICollection<AnnouncementResponse>>> GetAllAsync(int givenClassroomId)
        {
            try
            {
                var announcements = await _announcementDal.FindAllAsync(a=>a.Material.GivenClassroomId==givenClassroomId);
                var response =_mapper.Map<ICollection<Announcement>, ICollection<AnnouncementResponse>>(announcements);
                return new BasexResponse<ICollection<AnnouncementResponse>>(response);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<AnnouncementResponse>>(ex.Message);
            }
        }
    }
}
