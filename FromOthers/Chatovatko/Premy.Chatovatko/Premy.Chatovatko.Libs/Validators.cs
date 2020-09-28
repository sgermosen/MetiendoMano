using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Premy.Chatovatko.Libs
{
    public static class Validators
    {
        public static readonly string REGEX_USERNAME = "^[a-zA-Z][-a-zA-Z0-9_]+$";
        public static bool ValidateRegexUserName(String userName)
        {
            return Regex.IsMatch(userName, REGEX_USERNAME);
        }
    }
}
