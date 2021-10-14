using System.Net;
using System.Net.Mail;

namespace karma.Pages
{
    public partial class Contact
    {
        private string _subject, _description;
        
        private void sendEmail()
        {
            MailMessage mail = new MailMessage("CharieOrganization@gmail.com", "CharieOrganization@gmail.com", _subject, _description);
            NetworkCredential netCred = new NetworkCredential("CharieOrganization@gmail.com", DotNetEnv.Env.GetString("EMAIL_PASSWORD"));
            SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;

            smtpobj.Send(mail);
        }
    }
}
