// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace festivalbooking.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using festivalbooking.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using festivalbooking.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "/Users/placeholder/Desktop/dapperapi/Client/_Imports.razor"
using festivalbooking.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : MainLayout1
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 177 "/Users/placeholder/Desktop/dapperapi/Client/Pages/Index.razor"
      


vagtDTO[] vagt;
vagtDTO[] vagt2;
opgaveDTO[] opgaver;

kompetenceDTO[] kompetencer;

kompetenceDTO[] mine_kompetencer;

private string felt;
private string felt1;

private bool isUpdate = false;


protected override async Task OnInitializedAsync()
{
  opgaver = await Http.GetFromJsonAsync<opgaveDTO[]>("api/omrader/frivillig");
   kompetencer = await Http.GetFromJsonAsync<kompetenceDTO[]>("api/kompetence");


}
            private async Task<frivilligDTO> login (string password, string username){
                 
             return  user = await Http.GetFromJsonAsync<frivilligDTO>($"api/login/{password}/{username}");
            }
            private frivilligDTO LogOut(frivilligDTO user){
              felt = "";
              felt1 = "";
              user.role_id = null;
              user.frivillig_navn ="";
              user.pw = "";
              user.frivillig_tlf = 0;
             
              return user;
            }

            private async Task updatetlf(int id){
             
                  await Http.PutAsJsonAsync<frivilligDTO>($"api/user/tlf/{id}", user);
                  
            }
                 private async Task updatenavn(int id){
            
                  await Http.PutAsJsonAsync<frivilligDTO>($"api/user/navn/{id}",user);
                   
            }
             private async Task updatepw(int id){
            
                  await Http.PutAsJsonAsync<frivilligDTO>($"api/user/pw/{id}",user);
                   
            }

                private async Task<vagtDTO[]> getvagterbymyid (int id){
                    await OnInitializedAsync();
                return vagt =  await Http.GetFromJsonAsync<vagtDTO[]>($"api/user/vagter/{id}");
                
                   
            }
            private async Task<vagtDTO[]> getvagterbyOpgave (int id, int uid){
         
                return vagt2 =  await Http.GetFromJsonAsync<vagtDTO[]>($"api/omrade/frivillig/{id}/{uid}");
                   
            }
                private async Task<kompetenceDTO[]> getmykompetencer(int id){
                    await OnInitializedAsync();
         
                return mine_kompetencer =  await Http.GetFromJsonAsync<kompetenceDTO[]>($"api/kompetence/{id}");
                   
            }
    private async Task tilmeldVagt(int fid, int? vid)
{
 tilmelding tilmelding = new tilmelding();
 tilmelding.frivillig_id = fid;
 tilmelding.vagt_id = vid;

    await Http.PostAsJsonAsync<tilmelding>($"api/user/tilmelding/{tilmelding.frivillig_id}", tilmelding);
    //await OnInitializedAsync();
}
private async Task kompetencebind(int uid, int kid){
kompetenceBinder tempbinder = new kompetenceBinder();
tempbinder.frivillig_id = uid;
tempbinder.kompetence_id = kid;
  await Http.PostAsJsonAsync($"api/kompetence/add/{uid}", tempbinder);
  await OnInitializedAsync();

}
private async Task deleteKompetence(int uid,int kid){
  await Http.DeleteAsync($"api/remove/kompetence/{uid}/{kid}");
}

private async Task deleteFrivillig(int id){
 
  await Http.DeleteAsync($"api/remove/frivillig/{id}");
  LogOut(user);

}
private bool toggleUpdate(){
  if (isUpdate == true){
     return isUpdate = false;
  }
 return isUpdate = true;
}


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private frivilligDTO user { get; set; }
    }
}
#pragma warning restore 1591
