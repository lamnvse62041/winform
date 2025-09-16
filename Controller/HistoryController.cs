using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Controller
{
    internal class HistoryController
    {
        private DataAccess.HistoryRepository historyRepo;
        public HistoryController()
        {
            historyRepo = new DataAccess.HistoryRepository();
        }
        public List<Dtos.HistoryDto> GetAllHistory()
        {
            return historyRepo.GetAllUser();
        }

        public void AddHistory(Dtos.HistoryDto history)
        {
            historyRepo.AddHistory(history);
        }
    }
}
