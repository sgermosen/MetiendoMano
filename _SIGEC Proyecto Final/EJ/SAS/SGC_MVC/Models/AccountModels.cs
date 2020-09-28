using SGC_MVC.CustomCode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web;
using System.Web.Security;

namespace SGC_MVC.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        //[System.Web.Mvc.Remote("CheckUserName", "Account")]
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string name { get; set; }

        public int status { get; set; }

        public bool superUser { get; set; }

        [Required]
        public int? department_id { get; set; }

        public int company_id { get; set; }

        [Required]
        public int position_id { get; set; }

        public DateTime create_date { get; set; }

        public DateTime update_date { get; set; }

        [Required]
        public string[] roles { get; set; }

        [MaxFileSize(1024 * 1024, ErrorMessage = "El archivo no puede exceder los {0} bytes")]
        [FileTypes("jpeg,jpg,png,gif,bmp")]
        public HttpPostedFileBase image { get; set; }

        //public string imageName { get; set; }
    }

    public class EditAccountModel
    {
        public EditAccountModel() { }

        public EditAccountModel(User user)
        {
            UserName = user.username;
            Password = user.password;
            name = user.name;
            status = user.status;
            superUser = user.superUser;
            department_id = user.departmentID;
            company_id = user.companyID;
            position_id = user.positionID;
            create_date = (DateTime)user.createDate;
            update_date = (DateTime)user.updateDate;
            imageUrl = user.imageUrl;
            Password = user.password;
            ConfirmPassword = user.password;
            userID = user.ID;
            activeUser = Convert.ToBoolean(user.status);
        }

        public int userID { get; set; }

        [Required]
        [Display(Name = "E-Mail")]
        //[System.Web.Mvc.Remote("CheckUserName", "Account")]
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        public string UserName { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string name { get; set; }

        public int status { get; set; }

        public bool superUser { get; set; }

        [Required]
        public int? department_id { get; set; }

        public int? company_id { get; set; }

        [Required]
        public int? position_id { get; set; }

        public DateTime create_date { get; set; }

        public DateTime update_date { get; set; }

        [Required]
        public string[] roles { get; set; }

        [MaxFileSize(1024 * 1024, ErrorMessage = "El archivo no puede exceder los {0} bytes")]
        [FileTypes("jpeg,jpg,png,gif,bmp")]
        public HttpPostedFileBase image { get; set; }

        public string imageUrl { get; set; }

        public bool passwordChanged { get; set; }

        public bool activeUser { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
