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
#line 3 "/Users/placeholder/Desktop/dapperapi/Client/Pages/Frivillig.razor"
using festivalbooking.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/frivillig")]
    public partial class Frivillig : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 121 "/Users/placeholder/Desktop/dapperapi/Client/Pages/Frivillig.razor"
      
    private string felt1; 
    private int felt;

    private string felt2;
      private doneFrivilligDTO[] frivillige; 
    private roletest[] roller;
    private kompetenceDTO[] kompetencer;
    private kompetenceDTO kompetence = new kompetenceDTO();

    private frivilligDTO user = new frivilligDTO();
protected override async Task OnInitializedAsync()
{

    frivillige = await Http.GetFromJsonAsync<doneFrivilligDTO[]>("api/vagt/frivillig");
        roller = await Http.GetFromJsonAsync<roletest[]>("api/roles");
             kompetencer = await Http.GetFromJsonAsync<kompetenceDTO[]>("api/kompetence");
            
    
}
    private async Task opret(string navn, int tlf, string pw )
{   
    frivilligDTO user1 = new frivilligDTO();
    user1.frivillig_navn = navn;
    user1.frivillig_tlf = tlf;
    user1.pw = pw;
    user1.role_id = user.role_id;
    

    await Http.PostAsJsonAsync<frivilligDTO>($"api/user/opret/{navn}", user1);
    await OnInitializedAsync();
}
private void bindRID(roletest role){
     user.role_id = role.id;
}
private async Task sletkompetence(int id){
    await Http.DeleteAsync($"api/slet/kompetence/{id}");
    await OnInitializedAsync();

}
    private async Task opretkompetence(string navn)
{   
 
kompetence.kompetence_navn = navn; 

    await Http.PostAsJsonAsync<kompetenceDTO>($"api/kompetence/opret/{navn}", kompetence);
    await OnInitializedAsync();
}

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
