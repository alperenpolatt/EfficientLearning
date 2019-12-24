using AutoMapper;
using EfLearning.Business.Abstract;
using EfLearning.Business.Common;
using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using EfLearning.Core.Practices;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class GivenPracticeManager: IGivenPracticeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IGivenPracticeDal _givenPracticeDal;
        private readonly IMapper _mapper;
        public GivenPracticeManager(IUnitOfWork unitOfWork, IGivenPracticeDal givenPracticeDal, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _givenPracticeDal = givenPracticeDal;
            _mapper = mapper;
        }

        public async Task<BasexResponse<ICollection<LevelResponse>>> GetLevelsAsync(int programmingType)
        {
            try
            {
                var givenPractices = await _givenPracticeDal.FindByAsync(g=>g.ProgrammingType==(ProgrammingType)programmingType);
                var response = _mapper.Map<ICollection<GivenPractice>, ICollection<LevelResponse>>(givenPractices);
                return new BasexResponse<ICollection<LevelResponse>>(response);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<LevelResponse>>(ex.Message);
            }
        }

        public async Task<BasexResponse<LevelDetailResponse>> GetLevelDetailAsync(int id)
        {
            try
            {
                var givenPractice = await _givenPracticeDal.GetAsync(id);
                var response = _mapper.Map<GivenPractice, LevelDetailResponse>(givenPractice);
                return new BasexResponse<LevelDetailResponse>(response);
            }
            catch (Exception ex)
            {

                return new BasexResponse<LevelDetailResponse>(ex.Message);
            }
        }

        public async Task<BasexResponse<AnswerResponse>> CheckAnswerAsync(int id, string answer)
        {
            try
            {
                var givenPractices = await _givenPracticeDal.GetAsync(id);
                var answerSuccess = false;
                char[] charsToTrim = { '\"', '\n' , '\r' };
                var repSol = new String[charsToTrim.Length+1];
                repSol[0] =givenPractices.Solution;
                for (int i = 0; i < charsToTrim.Length; i++)
                {
                    repSol[i+1] = repSol[i].Replace(charsToTrim[i], ' ');
                }
                var trimSol = repSol[repSol.Length - 1];
                if (trimSol.Replace(" ","") == answer.Replace(" ", ""))
                    answerSuccess = true;
                return new BasexResponse<AnswerResponse>(new AnswerResponse { AnswerSuccess=answerSuccess});
            }
            catch (Exception ex)
            {
                return new BasexResponse<AnswerResponse>(ex.Message);
            }
        }

        public async Task<BasexResponse<HintResponse>> GetAHintAsync(int id)
        {
            try
            {
                var givenPractices = await _givenPracticeDal.GetAsync(id);
                return new BasexResponse<HintResponse>(new HintResponse { Hint = givenPractices.Hint });
            }
            catch (Exception ex)
            {
                return new BasexResponse<HintResponse>(ex.Message);
            }
        }
    }
}
