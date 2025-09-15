using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DataAccess;
using WindowsFormsApp1.Dtos;

namespace WindowsFormsApp1.Controller
{
    internal class StatusController
    {
        private StatusRepository repo;

        public StatusController()
        {
            repo = new StatusRepository();
        }
        public List<StatusDto> GetStatus()
        {
            return repo.GetStatus();
        }
    }
}
