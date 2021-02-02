// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Localization;
using System.ComponentModel.DataAnnotations;

namespace Exadel.CrazyPrice.IdentityServer.ViewModels
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "emailRequired")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "passwordRequired")]
        [StringLength(100, ErrorMessage = "passwordStringLength", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}