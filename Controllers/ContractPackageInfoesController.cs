using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClownsCRMAPI.Models;
using ClownsCRMAPI.CustomModels;

namespace ClownsCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractPackageInfoesController : ControllerBase
    {
        private readonly ClownsContext _context;

        public ContractPackageInfoesController(ClownsContext context)
        {
            _context = context;
        }

        // GET: api/ContractPackageInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractPackageInfo>>> GetContractPackageInfos()
        {
            return await _context.ContractPackageInfos.ToListAsync();
        }

        // GET: api/ContractPackageInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractPackageInfo>> GetContractPackageInfo(int id)
        {
            var contractPackageInfo = await _context.ContractPackageInfos.FindAsync(id);

            if (contractPackageInfo == null)
            {
                return NotFound();
            }

            return contractPackageInfo;
        }

        // PUT: api/ContractPackageInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractPackageInfo(int id, ContractPackageInfo contractPackageInfo)
        {
            if (id != contractPackageInfo.PackageInfoId)
            {
                return BadRequest();
            }

            _context.Entry(contractPackageInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractPackageInfoExists(id))
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

        // POST: api/ContractPackageInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("SavePackageInfo")]
        public async Task<ActionResult<ContractPackageInfo>> SavePackageInfo(ContractPackageInfoModel contractPackageInfo)
        {
            //_context.ContractPackageInfos.Add(contractPackageInfo);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetContractPackageInfo", new { id = contractPackageInfo.PackageInfoId }, contractPackageInfo);
        }

        // DELETE: api/ContractPackageInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractPackageInfo(int id)
        {
            var contractPackageInfo = await _context.ContractPackageInfos.FindAsync(id);
            if (contractPackageInfo == null)
            {
                return NotFound();
            }

            _context.ContractPackageInfos.Remove(contractPackageInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractPackageInfoExists(int id)
        {
            return _context.ContractPackageInfos.Any(e => e.PackageInfoId == id);
        }
    }
}
