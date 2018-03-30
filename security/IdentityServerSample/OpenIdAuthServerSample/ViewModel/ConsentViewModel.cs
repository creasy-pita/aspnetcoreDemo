using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace MyCookieAuthSample.ViewModels
{//����ʾ�ĸ�����Ϣ
    public class ConsentViewModel:InputViewModel
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public string ClientLogUrl { get; set; }

        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }

        public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }

    }
}