#pragma checksum "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fa756a37d010ad10e2ae63cfc662732501ea8610"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Result_CandidateList), @"mvc.1.0.view", @"/Views/Result/CandidateList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\_ViewImports.cshtml"
using OnlineVotingSystem;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\_ViewImports.cshtml"
using OnlineVotingSystem.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa756a37d010ad10e2ae63cfc662732501ea8610", @"/Views/Result/CandidateList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be4ac2c64c0e964254eee68f6e1afbabfc884526", @"/Views/_ViewImports.cshtml")]
    public class Views_Result_CandidateList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OnlineVotingSystem.Models.ViewModels.ResultViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
  
    ViewData["Title"] = "Candidate List";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
            WriteLiteral("\r\n<a");
            BeginWriteAttribute("href", " href=\"", 134, "\"", 179, 1);
#nullable restore
#line 9 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
WriteAttributeValue("", 141, Url.Action("election-list", "result"), 141, 38, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary btn-md me-2\">\r\n    <i class=\"fa-solid fa-arrow-left me-1\"></i> Back\r\n</a>\r\n\r\n\r\n<div class=\"container mt-4\">\r\n    <div class=\"text-center\">\r\n        <h2 class=\"fw-bold\">Election Name: ");
#nullable restore
#line 16 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                                      Write(ViewBag.ElectionName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n    </div>\r\n    <br>\r\n    <h4>Total Candidates (");
#nullable restore
#line 19 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                     Write(Model.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(@")</h4>
    <div class=""table-responsive"">
        <table class=""table table-hover table-bordered text-center align-middle"">
            <thead style=""background-color: #000; color: #fff;"">
                <tr>
                    <th style=""width: 13%;"">Candidate ID</th>
                    <th>Candidate Name</th>
                    <th>Description</th>
                    <th>Total Votes</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 31 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                 if (Model != null && Model.Any())
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                     foreach (var candidate in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 36 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                           Write(candidate.CandidateID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 37 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                           Write(candidate.CandidateName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 38 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                           Write(candidate.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 39 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                           Write(candidate.TotalVotes.ToString("#,##0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 41 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                     
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td colspan=\"7\" class=\"text-center fw-bold text-muted\">No Records Found</td>\r\n                    </tr>\r\n");
#nullable restore
#line 48 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </tbody>
        </table>
    </div>
</div>
<br>


<script>
    var connection = new signalR.HubConnectionBuilder()
        .withUrl(""/electionResultHub"") // Must match the Hub route in Startup.cs
        .build();

    // Start the SignalR connection
    connection.start()
        .then(() => console.log('electionResultHub is started. No error found'))
        .catch(err => console.error(""An error has occurred in start(). Error Message: "" + err.toString()));



    // Listen for real-time updates from the server
    connection.on(""UpdateElectionResultView"", function () {
");
#nullable restore
#line 70 "C:\Users\IT Programmer\Desktop\JOSHUA ALL FOLDERS\OnlineVotingSystem\OnlineVotingSystem\Views\Result\CandidateList.cshtml"
          TempData["infoMessage"] = "Here is the latest results";

#line default
#line hidden
#nullable disable
            WriteLiteral("        location.reload();\r\n    });\r\n</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OnlineVotingSystem.Models.ViewModels.ResultViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
