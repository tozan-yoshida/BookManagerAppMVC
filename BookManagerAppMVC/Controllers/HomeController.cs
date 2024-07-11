using System.ComponentModel.DataAnnotations;
using BookManagerAppMVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Reflection.PortableExecutable;
using System.DirectoryServices;
using DirectoryEntry = System.DirectoryServices.DirectoryEntry;

namespace BookManagerAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("UserName,Password")] LoginForm loginForm)
        {
            try
            {

                _ = HttpContext ?? throw new InvalidOperationException("Static SSR �Ŏ��s���Ă�������");

                ClaimsIdentity? identity = null;

                var authenticateUserName = AuthenticateUser(loginForm.UserName, loginForm.Password);

                // �{���ł���΂����Ń��O�C������������悤�ȋ@�\���Ăяo��
                if (!string.IsNullOrWhiteSpace(authenticateUserName))
                {
                    identity = new(
                        [new Claim(ClaimTypes.Name, authenticateUserName), new Claim(ClaimTypes.Role, "User")],
                        CookieAuthenticationDefaults.AuthenticationScheme);
                }

                if (identity != null)
                {
                    // ���O�C������
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity)
                    );
                    return RedirectToAction("FindBooks", "Books");
                }
                else
                {
                    // ���O�C�����s

                    return View();
                }
            }
            catch (Exception e) when (e is not NavigationException)
            {
                string message = e.Message;
                return View();
            }
        }
        /// <summary>
        /// �A�N�e�B�u�f�B���N�g�� ���[�U�[��F��
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string AuthenticateUser(string username, string password)
        {
            try
            {
                // Active Directory �T�[�o�[�� DirectoryEntry �I�u�W�F�N�g���쐬
                string domain = "LDAP://192.168.1.6/DC=tozan,DC=co,DC=jp";
                DirectoryEntry root1 = new DirectoryEntry(domain, username, password);

                // Active Directory �Ń��[�U�[������
                DirectorySearcher searcher = new DirectorySearcher(root1);
                searcher.Filter = "(&(objectClass=user)(sAMAccountName=" + username + "))";
                searcher.SearchScope = SearchScope.Subtree;

                // ���[�U�[�������������ǂ������m�F
                SearchResult result = searcher.FindOne();

                if (result != null)
                {
                    //���O�iname�j���擾����
                    string name = result.Properties["displayname"][0].ToString();
                    return name;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
            );
            return RedirectToAction("Index");
        }
    }
}
