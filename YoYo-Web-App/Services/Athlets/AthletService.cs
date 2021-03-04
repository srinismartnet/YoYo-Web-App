using AutoMapper;
using Core.Interface;
using Infrastructure.Data;
using YoYo_Web_App.Services.Interfaces.AthletServices;
using YoYo_Web_App.ViewModel;

namespace YoYo_Web_App.Services.Athlets
{
    public class AthletService : IAthletService
    {
        private readonly IAthletRepository _athletRepository;

        public AthletService(IAthletRepository athletRepository)
        {
            _athletRepository = athletRepository;
        }
        /// <summary>
        /// Get Athlet Fitness for showing on view
        /// </summary>
        /// <param name="athletId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public AthletViewModel GetAthletFitness(int athletId, string result)
        {
            var athletResult = new AthletViewModel();
            var athletList = _athletRepository.GetAthlets();
            int editIndex = athletList.FindIndex(o => o.id == athletId);
            athletResult.id = athletList[editIndex].id;
            athletResult.result = result;
             
            return athletResult;
        }

    }
}
