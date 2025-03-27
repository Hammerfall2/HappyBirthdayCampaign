using HappyBirthdayCampain.BOL;
using HappyBirthdayCampain.UI.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace HappyBirthdayCampain.UI.Maps
{
    public static class ReportMapper
    {
       
        public static Report MapReportDtoToReport(ReportDTO reportDTO)
        {
            Report report = new Report();

            reportDTO.summary.giftEmployeeDto.ForEach(giftEmployee => { giftEmployee.Value.ForEach(value => { report.GiftPresentName.Add(giftEmployee.Key.GiftName); }) ; });
            reportDTO.noVoteEmployees.ForEach(key => { report.GiftPresentName.Add("No Vote"); });


            reportDTO.summary.giftEmployeeDto.Values.ForEach(key => { key.ForEach(name => { report.VoteUserFullName.Add(name.FirstName + " " + name.LastName); }); });
            reportDTO.noVoteEmployees.ForEach(key => { report.VoteUserFullName.Add(key.FirstName + " " + key.LastName); });

            report.GiftPresentEmployees = reportDTO.summary.giftEmployeeDto;
            report.GiftPresentWinner = reportDTO.WiningGiftPresent.GiftName;
            report.CampaignYear = reportDTO.header.CampaignYear;

            report.BirthDayUserFullName = reportDTO.header.BirthDayUser.FirstName + " " + reportDTO.header.BirthDayUser.LastName;
            report.Title  = "Report for Birthday User:" + report.BirthDayUserFullName + "for Campaign Year " + report.CampaignYear + " is ready"; ;

            return report;
        }



    }
}