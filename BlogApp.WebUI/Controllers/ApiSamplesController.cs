﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data.Concrete.EFCore;
using ProjectManagement.Entity;
using Newtonsoft.Json;
using ProjectManagement.Data.Abstract;
using System.Collections;

namespace ProjectManagement.WebUI.Controllers
{
    [Route("api/Sample/[action]")]
    [ApiController]
    public class ApiSamplesController : ControllerBase
    {
        private readonly PMContext _context;
        private ISampleRepository _sampleRepository;
        private IEmployeeRepository _employeeRepository;

        public ApiSamplesController(PMContext context, ISampleRepository sampleRepository, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _sampleRepository = sampleRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sample>>> GetSamples()
        {
            return await _context.Samples.ToListAsync();
        }

        [HttpGet]

        public ActionResult<SampleStatics> GetSamplesStatics()
        {
            return _sampleRepository.GetSampleStatics();
        }

        [HttpGet]

        public ActionResult<IEnumerable<EmployeeStatics>> GetEmployeeStatics()
        {
            List<EmployeeStatics> a1=new List<EmployeeStatics>();
            foreach (var employee in _employeeRepository.GetAll())
            {
                a1.Add(_employeeRepository.GetEmployeeStatics(employee.EmployeeId));
            }
            return a1;
        }



        [HttpGet("{id}")]
        public ActionResult<IEnumerable<GanttStatics>> GetEmployeeGantt(int id)
        {
            return _employeeRepository.GetGanttForEmp(id).ToList();
        }



        // GET: api/ApiSamples/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sample>> GetSample(int id)
        {
            var sample = await _context.Samples.FindAsync(id);

            if (sample == null)
            {
                return NotFound();
            }

            return sample;
        }

        // PUT: api/ApiSamples/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSample(int id, Sample sample)
        {
            if (id != sample.SampleId)
            {
                return BadRequest();
            }

            _context.Entry(sample).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SampleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiSamples
        [HttpPost]
        public async Task<ActionResult<Sample>> PostSample(Sample sample)
        {
            _context.Samples.Add(sample);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSample", new { id = sample.SampleId }, sample);
        }

        // DELETE: api/ApiSamples/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sample>> DeleteSample(int id)
        {
            var sample = await _context.Samples.FindAsync(id);
            if (sample == null)
            {
                return NotFound();
            }

            _context.Samples.Remove(sample);
            await _context.SaveChangesAsync();

            return sample;
        }

        private bool SampleExists(int id)
        {
            return _context.Samples.Any(e => e.SampleId == id);
        }
    }
}
