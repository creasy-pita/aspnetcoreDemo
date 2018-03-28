using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using MyCookieAuthSample.Models;
using MyCookieAuthSample.ViewModels;

namespace MyCookieAuthSample.Controllers
{
    public class ConsentController : Controller
    {
        private readonly IClientStore clientStore;
        private readonly IResourceStore resourceStore;
        private readonly IIdentityServerInteractionService identityServerInteractionService;


        public ConsentController(IClientStore clientStore,
        IResourceStore resourceStore,
        IIdentityServerInteractionService identityServerInteractionService)
        {
            this.clientStore = clientStore;
            this.resourceStore = resourceStore;
            this.identityServerInteractionService = identityServerInteractionService;
        }

        private async Task<ConsentViewModel> BuildConsentViewModel(string returnUrl)
        {

            //根据returnurl client信息
            var request = await identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            var client = await clientStore.FindClientByIdAsync(request.ClientId);
            var resources = await resourceStore.FindResourcesByScopeAsync(client.AllowedScopes);


            return CreateConsentViewModel(request,client, resources);
        }

        private ConsentViewModel CreateConsentViewModel(AuthorizationRequest request, Client client, Resources resources)
        {
            var vm = new ConsentViewModel();
            vm.ClientId = client.ClientId;
            vm.ClientName = client.ClientName;
            vm.ClientLogUrl = client.LogoUri;
            vm.AllowRemeberConsent = client.AllowRememberConsent;

            vm.IdentityScopes = resources.IdentityResources.Select(i => CreateScopeViewModel(i));
            vm.ResourceScopes = resources.ApiResources.SelectMany(i=>i.Scopes).Select(x => CreateScopeViewModel(x));
            return vm;
        }

        private ScopeViewModel CreateScopeViewModel(IdentityResource identityResource)
        {
            return new ScopeViewModel
            {
                Name = identityResource.Name
                ,Description = identityResource.Description
                ,Emphasize = identityResource.Emphasize
                ,DisplayName = identityResource.DisplayName
                ,Required = identityResource.Required
                ,Checked = identityResource.Required
            };
        }

        private ScopeViewModel CreateScopeViewModel(Scope identityResource)
        {


            return new ScopeViewModel
            {
                Name = identityResource.Name
                ,
                Description = identityResource.Description
                ,
                DisplayName = identityResource.DisplayName
                ,
                Required = identityResource.Required
                ,
                Checked = identityResource.Required
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            //根据returnurl client信息
            var model = await BuildConsentViewModel(returnUrl);
            return View(model);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
