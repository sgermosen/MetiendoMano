using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsChatApplication.Controllers
{
    public class LetsChatController : Controller
    {
        // GET: LetsChat
        public ActionResult LetsChat()
        {
            return View();
        }
        public void SaveSession(string name,string sessionId)
        {
            // Call the addNewMessageToPage method to update clients.
            using (var context = new ChatDemoAppEntities())
            {
                var chatTbl = new ChatApp
                {
                    ChatSession = sessionId,
                    UserName = name
                };
                context.ChatApps.Add(chatTbl);
                context.SaveChanges();
            }
        }

        public bool CheckSession(string sessionValue)
        {
            using (var context = new ChatDemoAppEntities())
            {
                var checkSession = context.ChatApps.FirstOrDefault(x => x.ChatSession == sessionValue.Trim()) != null;
                return checkSession;
            }            
        }
    }
}