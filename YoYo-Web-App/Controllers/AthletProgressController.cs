using AutoMapper;
using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YoYo_Web_App.Dtos;
using YoYo_Web_App.Models;
using YoYo_Web_App.Services.Interfaces.AthletServices;
using YoYo_Web_App.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoYo_Web_App.Controllers
{
    [ApiController]
    public class AthletProgressController : ControllerBase
    {
        public IAthletService _athletService; 
        private readonly IMapper _mapper;
        private readonly IAthletRepository _athletRepository;

        public AthletProgressController(IAthletService athletService, IMapper mapper, IAthletRepository athletRepository)
        {
            _athletService = athletService;
            _mapper = mapper;
            _athletRepository = athletRepository;
        }
        [Route("[controller]/GetAthlets")]
        [HttpGet]
        public ActionResult GetAthlets()
        {
            return Ok(_athletRepository.GetAthlets());
        }
        [Route("[controller]/GetFitnessScoreData")]
        [HttpGet]
        public ActionResult GetFitnessData()
        {
            var fitnessScoreData = _mapper.Map<List<FitnessScore>, List<FitnessScoreDto>>(_athletRepository.GetFitnessData());

            return Ok(fitnessScoreData);
        }

        [HttpPost("AthletResult/{id}")]
        public ActionResult AthletResult([FromForm] AthletViewModel athletResults)
        {
            var athletResult = _athletService.GetAthletFitness(athletResults.id, athletResults.result);
            Console.WriteLine(athletResult.id + " : " + athletResult.result);

            return Ok(athletResult);
        }


        [HttpGet("WarnAthlet/{id}")]
        public ActionResult WarnAthlet(int id)
        {
            var allAthlets = _athletRepository.GetAthlets();

            int editIndex = allAthlets.FindIndex(o => o.id == id);
            allAthlets[editIndex].warn = true;
            return Ok(allAthlets[editIndex]);
        }
    }
}
