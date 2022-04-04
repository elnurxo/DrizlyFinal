using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required,DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required,DataType(DataType.Password),Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
        public string Id { get; set; }
        public string Token { get; set; }
    }
}
