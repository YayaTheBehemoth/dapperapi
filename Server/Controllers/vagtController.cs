using System;
using System.Collections.Generic;
using System.Linq;
using festivalbooking.Shared;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using festivalbooking.Server.Services;

namespace festivalbooking.Server.Controllers {
    [ApiController]
 

    public class VagtController : ControllerBase {
        private vagtService _service;
        
        public VagtController(vagtService service){
            _service = service;
        }
        [Route("api/[controller]")]
        [HttpGet]
        public  List<vagtDTO> getAllVagt(){
            //Console.WriteLine("api nået");
          return  _service.getVagter();

          }
          [Route("api/[controller]/{id}")]
          [HttpDelete]
          
        public async Task<IActionResult> DeleteVagt(int id)
        {  //Console.WriteLine("api nået" + id);
           await _service.deleteVagt(id);
           return NoContent();
        }
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<IActionResult> postVagt(vagtDTO vagt){
              await _service.postVagt(vagt);
            return NoContent();
        }
        [Route("api/[controller]/{id}")]
        [HttpPut]
        public async Task <IActionResult> putVagt(vagtDTO test){
              await _service.putVagt(test);
              return NoContent();
        }
        [Route("api/omrader")]
        [HttpGet]
           
        public  List<opgaveDTO> getAllVagtOpgaver(){
            //Console.WriteLine("api nået");
          return  _service.getAllOpgaver();

          }
          [Route("api/status")]
          [HttpGet]
           
        public  List<vagt_statusDTO> getAllStatus(){
            //Console.WriteLine("api nået");
          return  _service.getStatus();

          }

          [Route("api/status/{id}")]
          [HttpGet]
          public List<opgaveDTO> getOpgaverByStatus(int id)
          {
             return _service.filterByStatus(id);
          }
         [Route("api/omrade/{id}")]
          [HttpGet]
          public List<vagtDTO> getVagtByOpgave(int id)
          {
             return _service.filterByOpgave(id);
          }
          [Route("api/[controller]/frivillig")]
          [HttpGet]
          public List<doneFrivilligDTO> getFrivillige()
          {
             return _service.getFrivillige();
          }
          [Route("api/omrader/sort")]
          [HttpGet]
          public List<vagtDTO> getVagterSortOpgave()
          {
             return _service.getVagterSortOpgave();
          }

          [Route("api/status/sort")]
          [HttpGet]
          public List<opgaveDTO> getOpgaverSortStatus()
          {
             return _service.getOpgaverSortStatus();
          }

          [Route("api/antal/sort")]
          [HttpGet]
          public List<vagtDTO> getVagterSortAntal()
          {
             return _service.getVagterSortAntal();
          }
         [Route("api/las/{id}")]
         [HttpPut]
          public async Task<IActionResult> låsVagt(vagtDTO vagt)
          {
             await _service.låsVagt(vagt);
              return NoContent();
          }
           [Route("api/opgaver/las/{id}")]
         [HttpPut]
          public async Task<IActionResult> låsOpgave(opgaveDTO opgave)
          {
             await _service.låsOpgave(opgave);
              return NoContent();
          }

        [Route("api/opgaver")]
        [HttpPost]
        public async Task<IActionResult> postOpgave(opgaveDTO opgave){
              await _service.postOpgave(opgave);
            return NoContent();
        }

        [Route("api/opgaver/{id}")]
        [HttpPut]
        public async Task <IActionResult> putVagt(opgaveDTO test){
              await _service.putOpgave(test);
              return NoContent();
        }
        [Route("api/opgaver/{id}")]
      [HttpDelete]
          
        public async Task<IActionResult> DeleteOpgave(int id)
        {  //Console.WriteLine("api nået" + id);
           await _service.deleteOpgave(id);
           return NoContent();
        }
         
    }
}