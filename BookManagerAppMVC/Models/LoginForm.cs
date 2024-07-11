using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookManagerAppMVC.Models
{
    public class LoginForm
    {
        [Required(ErrorMessage = "ユーザー名を入力してください")]
        public string UserName { get; set; } = "";
        [Required(ErrorMessage = "パスワードを入力してください")]
        public string Password { get; set; } = "";
    }
}
