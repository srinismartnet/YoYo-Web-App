using Core.Entities;
using Core.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Infrastructure.Data
{
    /// <summary>
    /// Athlet Repository
    /// </summary>
    public class AthletRepository : IAthletRepository
    {
        public List<Athlet> GetAthlets()
        {
            List<Athlet> athlets = new List<Athlet>();

            var athlet1 = new Athlet
            {
                id = 1,
                name = "Ashton Eaton",
                warn = false,
                stop = false,
                levelNumber = 0,
                shuttleNumber = 0
            };

            var athlet2 = new Athlet
            {
                id = 2,
                name = "Bryan Clay",
                warn = false,
                stop = false,
                levelNumber = 0,
                shuttleNumber = 0
            };

            var athlet3 = new Athlet
            {
                id = 3,
                name = "Dean Karnazes",
                warn = false,
                stop = false,
                levelNumber = 0,
                shuttleNumber = 0
            };

            var athlet4 = new Athlet
            {
                id = 4,
                name = "Usain Blot",
                warn = false,
                stop = false,
                levelNumber = 0,
                shuttleNumber = 0
            };

            athlets.Add(athlet1);
            athlets.Add(athlet2);
            athlets.Add(athlet3);
            athlets.Add(athlet4);

            return athlets;
        }

        /// <summary>
        /// Warn the Athlet
        /// </summary>
        /// <param name="athletId"></param>
        /// <returns></returns>
        public Athlet WarnAthlet(int athletId)
        {
            var playersList = GetAthlets();
            int editIndex = playersList.FindIndex(o => o.id == athletId);
            playersList[editIndex].warn = true;

            return playersList[editIndex];
        }

        /// <summary>
        /// Get the Fitness Data for Athlet
        /// </summary>
        /// <returns></returns>
        public List<FitnessScore> GetFitnessData()
        {
            var fitnessScoreData = new List<FitnessScore>();

            var jsonText = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{"AthletData\\fitnessrating_beeptest.json"}"));

            JArray jsonArray = JArray.Parse(jsonText);
            foreach (var item in jsonArray)
            {
                var jsonObj = JsonConvert.DeserializeObject<FitnessScore>(item.ToString());
                fitnessScoreData.Add(jsonObj);
            }

            return fitnessScoreData;
        }

    }
}
