using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace WebAdministrator.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
            MsgErros = new List<KeyValuePair<string, string>>();
        }
        public string RequestId { get; set; }
        public Exception ErrorException { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public List<KeyValuePair<string, string>> MsgErros { get; set; }
    }
}
