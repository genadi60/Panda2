using System;
using Microsoft.AspNetCore.Identity;
using Panda2.Models;

namespace Panda2.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AuthenticatedAttribute : Attribute
    {
        private readonly SignInManager<PandaUser> _signInManager;
        public AuthenticatedAttribute(SignInManager<PandaUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public bool IsAuthenticated
        {
            get; 
            set;
        }
    }
}
