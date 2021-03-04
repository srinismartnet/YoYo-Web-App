using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interface
{
    /// <summary>
    /// Interface Repository
    /// </summary>
    public interface IAthletRepository
    {
        List<Athlet> GetAthlets();

        Athlet WarnAthlet(int athlet);

        List<FitnessScore> GetFitnessData();
    }
}
