#pragma checksum "D:\MyProgram\Hobby\RLSimulation\RLSimulation\Shared\CommonLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dff06669820543a9a6f71dbae314320de059f10c"
// <auto-generated/>
#pragma warning disable 1591
namespace RLSimulation.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#line 1 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#line 2 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#line 3 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#line 4 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#line 5 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#line 6 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using RLSimulation;

#line default
#line hidden
#line 7 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\_Imports.razor"
using RLSimulation.Shared;

#line default
#line hidden
    public class CommonLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<RLSimulation.Shared.NavBar>(0);
            __builder.CloseComponent();
            __builder.AddMarkupContent(1, "\r\n\r\n");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "container pb-4");
            __builder.AddMarkupContent(4, "\r\n\t");
            __builder.AddContent(5, 
#line 6 "D:\MyProgram\Hobby\RLSimulation\RLSimulation\Shared\CommonLayout.razor"
     Body

#line default
#line hidden
            );
            __builder.AddMarkupContent(6, "\r\n\r\n\t");
            __builder.AddMarkupContent(7, "<footer>\r\n\t\t<p class=\"footer\">\r\n\t\t\tcopyright 2019 Rebel of White all rights reserved.\r\n\t\t</p>\r\n\t</footer>\r\n\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
