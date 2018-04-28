using IdentityServer4.Services;
using IdentityServer4.Stores;
using MyCookieAuthSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace MyCookieAuthSample.Services
{
    public class ConsentService
    {
        private readonly IClientStore clientStore;
        private readonly IResourceStore resourceStore;
        private readonly IIdentityServerInteractionService identityServerInteractionService;


        public ConsentService(IClientStore clientStore,
        IResourceStore resourceStore,
        IIdentityServerInteractionService identityServerInteractionService)
        {
            this.clientStore = clientStore;
            this.resourceStore = resourceStore;
            this.identityServerInteractionService = identityServerInteractionService;
        }


        private ConsentViewModel CreateConsentViewModel(AuthorizationRequest request, Client client, Resources resources,InputViewModel inputViewModel=null)
        {
            var vm = new ConsentViewModel();
            if(inputViewModel!=null)
            {
                vm.Button = inputViewModel.Button;
                vm.RememberConsent = inputViewModel.RememberConsent;
                vm.ScopesConsented = inputViewModel.ScopesConsented;
            }

            vm.ClientId = client.ClientId;
            vm.ClientName = client.ClientName;
            vm.ClientUrl = client.ClientUri;
            vm.ClientLogUrl = client.LogoUri;
            vm.RememberConsent = client.AllowRememberConsent;
            vm.IdentityScopes = resources.IdentityResources.Select(i => CreateScopeViewModel(i));
            vm.ResourceScopes = resources.ApiResources.SelectMany(i => i.Scopes).Select(x => CreateScopeViewModel(x));
            return vm;
        }

        private ScopeViewModel CreateScopeViewModel(IdentityResource identityResource)
        {
            return new ScopeViewModel
            {
                Name = identityResource.Name
                ,
                Description = identityResource.Description
                ,
                Emphasize = identityResource.Emphasize
                ,
                DisplayName = identityResource.DisplayName
                ,
                Required = false//identityResource.Required
                ,
                Checked = identityResource.Required
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

        public async Task<ConsentViewModel> BuildConsentViewModel(string returnUrl)
        {

            //根据returnurl client信息
            var request = await identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            var client = await clientStore.FindClientByIdAsync(request.ClientId);
            var resources = await resourceStore.FindResourcesByScopeAsync(client.AllowedScopes);

            ConsentViewModel vm = CreateConsentViewModel(request, client, resources);


            vm.ReturnUrl = returnUrl;
            return vm;
        }

        public async Task<ConsentProcessResult> ProcessResult(InputViewModel viewModel)
        {
            ConsentProcessResult result = new ConsentProcessResult();
            ConsentResponse consentResponse = null;
            if (viewModel.Button == "no")
            {
                consentResponse = ConsentResponse.Denied;
            }
            else
            {
                if (viewModel.ScopesConsented != null && viewModel.ScopesConsented.Any())
                {
                    var scopes = viewModel.ScopesConsented;


                    consentResponse = new ConsentResponse
                    {
                        RememberConsent = viewModel.RememberConsent,
                        ScopesConsented = scopes
                    };
                }
                else
                {
                    result.ValidateError = "请至少选择一个选择";
                }
            }
            var request = await identityServerInteractionService.GetAuthorizationContextAsync(viewModel.ReturnUrl);
            if (consentResponse != null)
            {
                await identityServerInteractionService.GrantConsentAsync(request, consentResponse);//使用service 验证同意的部分
                result.RedirectUrl = viewModel.ReturnUrl;
            }
            var client = await clientStore.FindClientByIdAsync(request.ClientId);
            var resources = await resourceStore.FindResourcesByScopeAsync(client.AllowedScopes);
            ConsentViewModel consentViewModel = CreateConsentViewModel(request, client, resources);
            result.consentViewModel = consentViewModel;
            return result;
        }

    }
}
