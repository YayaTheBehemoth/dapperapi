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
           
        public  List<vagt_områderDTO> getAllVagtOmråder(){
            //Console.WriteLine("api nået");
          return  _service.getVagtOmråder();

          }
          [Route("api/status")]
          [HttpGet]
           
        public  List<vagt_statusDTO> getAllStatus(){
            //Console.WriteLine("api nået");
          return  _service.getStatus();

          }

          [Route("api/status/{id}")]
          [HttpGet]
          public List<vagtDTO> getVagtByStatus(int id)
          {
             return _service.filterByStatus(id);
          }
    }
}