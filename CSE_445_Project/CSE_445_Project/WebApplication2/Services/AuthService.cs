using System;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Xml.Linq;
using SecurityUtils.Services;


namespace WebApplication2.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICryptoService _crypto;
        private readonly string _memberXmlPath;

        public AuthService()
        {
            
            _crypto = new PasswordHasher();
            _memberXmlPath = HostingEnvironment.MapPath("~/App_Data/Member.xml");
        }

        public bool Register(string email, string password)
        {
        
            XDocument doc;
            if (File.Exists(_memberXmlPath))
                doc = XDocument.Load(_memberXmlPath);
            else
                doc = new XDocument(new XElement("Members"));

  
            bool exists = doc.Root
                .Elements("Member")
                .Any(x => (string)x.Attribute("Email") == email);
            if (exists)
                return false;

            string hash = _crypto.Hash(password);
            var newMember = new XElement("Member",
                new XAttribute("Email", email),
                new XAttribute("PasswordHash", hash));
            doc.Root.Add(newMember);
            doc.Save(_memberXmlPath);

            return true;
        }

        public bool Authenticate(string email, string password)
        {
            if (!File.Exists(_memberXmlPath))
                return false;

            var doc = XDocument.Load(_memberXmlPath);


            var node = doc.Root
                .Elements("Member")
                .FirstOrDefault(x => (string)x.Attribute("Email") == email);
            if (node == null)
                return false;

            string storedHash = (string)node.Attribute("PasswordHash");
            return _crypto.Verify(password, storedHash);
        }
    }
}