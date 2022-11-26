using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models
{
    public class UserModel
    {
        [BindProperty]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string name { get; set; }
        [BindProperty]
        public int profile { get; set; } = 0;
        public Dictionary<int, string> profiles { get; } = new Dictionary<int, string>() { { 0, "Patient" }, { 1, "Admin" }, { 2, "Doctor" } };
    }
}
