using DryLightning.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DryLightning.Controllers
{
    public class EmailTimerController : Controller
    {
        private IEmailTimeRepository _repo;

        public EmailTimerController(IEmailTimeRepository repo)
        {
            _repo = repo;
        }

        // Update the most recent time an email notification was sent out
        public void UpdateLastEmailTime(int id)
        {
            _repo.UpdateLastEmailTime(id);
        }
    }
}