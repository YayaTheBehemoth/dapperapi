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
        [Route("api/user/vagter/{id}")]
        [HttpGet]
        public  List<vagtDTO> getvagterbyfrivilligid(int id){
            //Console.WriteLine("api nået");
          return  _service.getVagterbyfrivilligid(id);

          }
            [Route("api/roles")]
        [HttpGet]
        public  List<roletest> getroles(){
            //Console.WriteLine("api nået");
          return  _service.getroles();

          }
        
           [Route("api/user/tlf/{id}")]
          [HttpPut]
          
        public async Task<IActionResult> updatetlf(frivilligDTO user)
        {  //Console.WriteLine("api nået" + id);
           await _service.updatetlf(user);
           return NoContent();
        }
           [Route("api/user/navn/{id}")]
          [HttpPut]
          
        public async Task<IActionResult> updatenavn(frivilligDTO user)
        {  //Console.WriteLine("api nået" + id);
           await _service.updateNavn(user);
           return NoContent();
        }
           [Route("api/user/pw/{id}")]
          [HttpPut]
          
        public async Task<IActionResult> updatepw(frivilligDTO user)
        {  //Console.WriteLine("api nået" + id);
           await _service.updatepw(user);
           return NoContent();
        }

        [Route("api/login/{password}/{username}")]
        [HttpGet]
        public  frivilligDTO login(string password, string username){
            //Console.WriteLine("api nået");
          return  _service.getvagtbyid(password, username);

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
            [Route("api/omrader/frivillig")]
        [HttpGet]
           
        public  List<opgaveDTO> getAllulåstOpgaver(){
            //Console.WriteLine("api nået");
          return  _service.getOpgaverUlåst();

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
             [Route("api/omrade/frivillig/{id}/{uid}")]
          [HttpGet]
          public List<vagtDTO> getVagtByOpgaveUlåst(int id, int uid)
          {
             return _service.filterByUlåstOpgave(id, uid);
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

        [Route("api/remove/frivillig/{id}")]
      [HttpDelete]
          
        public async Task<IActionResult> Deletefrivillig(int id)
        {  //Console.WriteLine("api nået" + id);
           await _service.deleteFrivillig(id);
           return NoContent();
        }

        [Route("api/test/{id}")]
        [HttpGet]
        public frivilligDTO getfrivilligbyid(int id){
           return _service.getfrivilligbyid(id);
        }
        [Route("api/kompetence")]
        [HttpGet]
        public List<kompetenceDTO> getAllkompetencer(){
           return _service.getkompetencer();
        }
        [Route("api/kompetence/{id}")]
        [HttpGet]
        public List<kompetenceDTO> getAllmykompetencer(int id){
           return _service.getmykompetencer(id);
        }
        [Route("api/user/tilmelding/{id}")]
        [HttpPost]
        public async Task<IActionResult> tilmeld(tilmelding tilmelding){
              await _service.tilmelding(tilmelding);
            return NoContent();
        }
          [Route("api/kompetence/add/{uid}")]
        [HttpPost]
        public async Task<IActionResult> addmykompetence(kompetenceBinder binder){
              await _service.addmykompetencer(binder.frivillig_id, binder.kompetence_id);
            return NoContent();
        }
         [Route("api/remove/kompetence/{uid}/{kid}")]
        [HttpDelete]
        public async Task<IActionResult> removemykompetence(int uid, int kid){
              await _service.removemykompetencer(uid,kid);
            return NoContent();
        }
          [Route("api/slet/kompetence/{id}")]
        [HttpDelete]
        public async Task<IActionResult> removefromkompetence(int id){
              await _service.removefromkompetencer(id);
            return NoContent();
        }
         [Route("api/user/opret/{navn}")]
        [HttpPost]
        public async Task<IActionResult> opret(frivilligDTO user){
              await _service.opret(user);
            return NoContent();
        }
          [Route("api/kompetence/opret/{navn}")]
        [HttpPost]
        public async Task<IActionResult> opret(kompetenceDTO kompetence){
              await _service.addtokompetencer(kompetence);
            return NoContent();
        }
         
    }
}