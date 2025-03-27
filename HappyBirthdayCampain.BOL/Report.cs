using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBirthdayCampain.BOL
{
    public class ReportHeader
    {
     
        public int CampaignYear { get; set; }
        public EmployeeDTO BirthDayUser { get; set; } = new EmployeeDTO();

    }

    public class VotePresentSummary
    {
        public Dictionary<GiftPresentDTO, List<EmployeeDTO>> giftEmployeeDto = new Dictionary<GiftPresentDTO, List<EmployeeDTO>>();
    }


    public class ReportDTO
    {
        public ReportHeader header = new ReportHeader();
        public VotePresentSummary summary = new VotePresentSummary();
        public List<EmployeeDTO> noVoteEmployees = new List<EmployeeDTO>();
        public GiftPresentDTO WiningGiftPresent = new GiftPresentDTO();

    }
}
