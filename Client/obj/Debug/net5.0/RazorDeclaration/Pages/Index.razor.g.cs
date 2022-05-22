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
#line 1 "/Users/placeholder/Desktop/festivalbooking/Client/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/placeholder/Desktop/festivalbooking/Client/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/placeholder/Desktop/festivalbooking/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/placeholder/Desktop/festivalbooking/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/placeholder/Desktop/festivalbooking/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/Users/placeholder/Desktop/festivalbooking/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/Users/placeholder/Desktop/festivalbooking/Client/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/placeholder/Desktop/festivalbooking/Client/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/Users/placeholder/Desktop/festivalbooking/Client/_Imports.razor"
using festivalbooking.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/Users/placeholder/Desktop/festivalbooking/Client/_Imports.razor"
using festivalbooking.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/placeholder/Desktop/festivalbooking/Client/Pages/Index.razor"
using festivalbooking.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 120 "/Users/placeholder/Desktop/festivalbooking/Client/Pages/Index.razor"
      
private vagtDTO[] vagter;

private opgaveDTO[] områder;

private vagt_statusDTO [] status;
private vagtDTO vagt = new vagtDTO();
protected override async Task OnInitializedAsync()
{
    vagter = await Http.GetFromJsonAsync<vagtDTO[]>("api/vagt");
    områder = await Http.GetFromJsonAsync<opgaveDTO[]>("api/omrader");
    status = await Http.GetFromJsonAsync<vagt_statusDTO[]>("api/status");
}
 private async Task sortVagterByområde()
    {
        vagter = await Http.GetFromJsonAsync<vagtDTO[]>("api/omrader/sort");
        //await OnInitializedAsync();

    }

   
       private async Task sortVagterByAntal()
    {
        vagter = await Http.GetFromJsonAsync<vagtDTO[]>("api/antal/sort");
        //await OnInitializedAsync();

    }


 private async Task DeleteVagt(int id)
    {
     
    
    await Http.DeleteAsync($"api/vagt/{id}");
    await OnInitializedAsync();
    }

private async Task postVagt(vagtDTO vagt)
{
    await Http.PostAsJsonAsync<vagtDTO>("api/vagt", vagt);
    await OnInitializedAsync();
}
private async Task patchVagt(int vagt_id)
{
          vagt.vagt_id = vagt_id; 
    await Http.PutAsJsonAsync<vagtDTO>($"api/vagt/{vagt_id}", vagt);
    await OnInitializedAsync();
}
   private void bindOid(opgaveDTO område)
    {
        vagt.opgave_id = område.opgave_id;
        vagt.opgave_navn = område.opgave_navn;
        //Console.WriteLine($"{vagt.område_id}");


    }
    
        
    private async Task låsEllerÅbenVagt(vagtDTO vagt)
    {
    await Http.PutAsJsonAsync<vagtDTO>($"api/las/{vagt.vagt_id}", vagt);
   await OnInitializedAsync();
    }

     
    
       private async Task getVagterByOmråde(int id){
        vagter = await Http.GetFromJsonAsync<vagtDTO[]>($"api/omrade/{id}");
        //await OnInitializedAsync();

    }
    

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
