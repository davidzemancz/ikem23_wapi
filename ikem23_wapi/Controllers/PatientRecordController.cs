﻿using ikem23_wapi.DTOs;
using ikem23_wapi.Models;
using ikem23_wapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ikem23_wapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRecordController : ControllerBase
    {
        private readonly PatientRecordDataService _dataService;
        private readonly HttpClient _httpClient;

        public PatientRecordController(PatientRecordDataService dataService, HttpClient httpClient)
        {
            _dataService = dataService;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IEnumerable<PatientRecord>> Get()
        {
            return await _dataService.Get();
        }

        [HttpPost("create")]
        public async Task Create(IFormCollection data)
        {
            // TODO
            // 1. read excel files
            // Create PatientRecords

            var dto = new PatientRecordCreateDto();

           
        }


    }
}
