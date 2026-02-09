using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeleteKindleBookmarks
{
    public class SignIn
    {
       public string email { get; set; }
       public string password { get; set; }
       public IWebElement emailInput { get; set; }
       public IWebElement passwordInput { get; set; }
       public IWebElement signInButton { get; set; }

        public SignIn(IWebDriver driver)
        {
            var creds = ReadLabeledFile(File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Credentials.txt")));
            email = creds.userName;
            password = creds.pass;
            emailInput = driver.FindElement(By.Id("ap_email"));
            passwordInput = driver.FindElement(By.Id("ap_password"));
            signInButton = driver.FindElement(By.Id("signInSubmit"));
        }

        private static Credentials ReadLabeledFile(string path)
        {
            var result = new Credentials();

            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(new[] { ':' }, 2); // split into label + value
                if (parts.Length < 2)
                    continue;

                string label = parts[0].Trim();
                string value = parts[1].Trim();

                switch (label)
                {
                    case "userName":
                        result.userName = value;
                        break;
                    case "password":
                        result.pass = value;
                        break;
                }
            }

            return result;
        }
        private class Credentials
        {
            public string userName { get; set; }
            public string pass { get; set; }

        }
    }
}
