using AutoMapper;
using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YoYo_Web_App.Dtos;
using YoYo_Web_App.Models;
using YoYo_Web_App.Services.Interfaces.AthletServices;

namespace YoYo_Web_App.Controllers
{
    public class AthletController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAthletRepository _athletRepository;

        public AthletController(IMapper mapper, IAthletRepository athletRepository)
        {
            _mapper = mapper;
            _athletRepository = athletRepository;
        }

        public IActionResult Athlet()
        {
            var athlet = _mapper.Map<List<Athlet>, List<AthletDto>>(_athletRepository.GetAthlets());

            //var athlets = _athletService.GetAthlets();
            return View(athlet);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
