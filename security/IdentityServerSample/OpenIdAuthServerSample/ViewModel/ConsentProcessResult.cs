using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace MyCookieAuthSample.ViewModels
{//����ʾ�ĸ�����Ϣ
    public class ConsentProcessResult
    {
        public string RedirectUrl { get; set; }

        public bool IsRedirect { get { return RedirectUrl != null; } }

        public string ValidateError { get; set; }
        public ConsentViewModel consentViewModel { get; set; }

    }
}