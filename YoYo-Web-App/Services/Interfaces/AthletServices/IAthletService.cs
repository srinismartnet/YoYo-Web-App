using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoYo_Web_App.ViewModel;

namespace YoYo_Web_App.Services.Interfaces.AthletServices
{
    public interface IAthletService
    {
        AthletViewModel GetAthletFitness(int athletId, string result);
    }
}
