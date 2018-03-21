using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeluxeOM.Utils.Config;
//using System.Configuration;
using DeluxeOM.Services;
using DeluxeOM.Models.Common;

namespace DeluxeOM.Tests
{
    [TestClass]
    public class ConfigTest
    {
        [TestMethod]
        public void VerifyConfigValues()
        {
            Assert.IsTrue(App.Config.SmtpMail.Server == "smtpMail.server", "smtp server name is incorrect");
            Assert.IsTrue(App.Config.SmtpMail.Port == "smtpMail.port", "smtp port is incorrect");
            Assert.IsTrue(App.Config.SmtpMail.UserName == "smtpMail.username", "smtp username is incorrect");
            Assert.IsTrue(App.Config.SmtpMail.Password == "smtpMail.password", "smtp pasword is incorrect");

            Assert.IsTrue(App.Config.FtpSettings[Provider.ITuneStore].Provider == Provider.ITuneStore, "ftpSettings element(enum) provider mismatch");
            Assert.IsTrue(App.Config.FtpSettings["ITuneStore"].Provider == Provider.ITuneStore, "ftpSettings element(string) provider mismatch");

            Assert.IsTrue(App.Config.FtpSettings[Provider.ITuneStore].FileType == DownloadFileType.MsExcel, "ftpSettings element filetypr mismatch");
            Assert.IsTrue(App.Config.FtpSettings[Provider.ITuneStore].UserName == "msExcelUsername", "ftpSettings element username mismatch");
            Assert.IsTrue(App.Config.FtpSettings[Provider.WB].UserName == "csvUsername", "ftpSettings element(WB) username mismatch");
            Assert.IsTrue(App.Config.FtpSettings[Provider.WB].Password == "csv.Password", "ftpSettings element(WB) password mismatch");
            Assert.IsTrue(App.Config.FtpSettings[Provider.WB].FtpLocation == "csv.locartion", "ftpSettings element(WB) ftpLocation mismatch");

            // Assert.IsTrue(App.Config.Key1 == "Value1", "Appsettings one mismatch");

        }

        [TestMethod]
        public void VerifySmtpEmail()
        {
            INotificationService ntfn = new NotificationService();

            dlxMailMessage dlxMessage = new dlxMailMessage()
            {
                From = "amoljadh@evolvingsols.com",
                To = "amoljadh@cybage.com",
                SuccessSubject = "Test email",
                SuccessBody = "test body"
            };

            ntfn.SendEmail(dlxMessage);

            Assert.IsTrue(true);
        }
    }
}
