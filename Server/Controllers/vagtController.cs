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
    [Route("api/[controller]")]

    public class VagtController : ControllerBase {
        private vagtService _service;
        
        public VagtController(vagtService service){
            _service = service;
        }

        [HttpGet]
        public  List<testDTO> getAllVagt(){
            //Console.WriteLine("api nået");
          return  _service.getVagter();

          }
          [HttpDelete("{id}")]
          
        public async Task<IActionResult> DeleteVagt(int id)
        {  //Console.WriteLine("api nået" + id);
           await _service.deleteVagt(id);
           return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> postVagt(testDTO vagt){
              await _service.postVagt(vagt);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task <IActionResult> putVagt(testDTO test){
              await _service.putVagt(test);
              return NoContent();
        }
    }
}