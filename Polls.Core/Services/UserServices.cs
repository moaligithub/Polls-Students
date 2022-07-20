using Polls.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using GemBox.Spreadsheet;
using NToastNotify;

namespace Polls.Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly ExcelFile workBook;
        private readonly IToastNotification _toastNotification;

        public UserServices(IHostingEnvironment hosting, IToastNotification toastNotification)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            workBook = ExcelFile.Load(hosting.WebRootPath + @"/Book1.xlsx");
            _toastNotification = toastNotification;
        }

        public ClaimsPrincipal Login(string UserId, string Password)
        {
            ExcelWorksheet workSheet = workBook.Worksheets[0];

            ExcelRow user = workSheet.Rows.Where(x => x.Cells[0].StringValue == UserId 
            && x.Cells[1].StringValue == Password).SingleOrDefault();

            if (user == null)
            {
                _toastNotification.AddErrorToastMessage("Invalid Account..");
                return null;
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, UserId),
                new Claim(ClaimTypes.Role, user.Cells[5].StringValue),
                new Claim("FirstName", user.Cells[2].StringValue), 
                new Claim("FullName", user.Cells[2].StringValue + user.Cells[3].StringValue),
                new Claim("Department", user.Cells[4].StringValue)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            _toastNotification.AddSuccessToastMessage($"Welcome, {user.Cells[2].StringValue}");
            return principal;
        }
    }
}