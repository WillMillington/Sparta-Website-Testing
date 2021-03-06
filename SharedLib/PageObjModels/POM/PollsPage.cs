﻿using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PageObjModels.POM
{
    public class PollsPage : TablePage
    {
        public List<PollsKeyValues> pollsList = new List<PollsKeyValues>();
        public ReadOnlyCollection<IWebElement> thElements => _seleniumDriver.FindElements(By.CssSelector("tbody tr th"));
        public ReadOnlyCollection<IWebElement> tdElements => _seleniumDriver.FindElements(By.CssSelector("tbody tr td"));
        private IWebElement tableBody => _seleniumDriver.FindElement(By.CssSelector(".table > tbody:nth-child(2)"));
        private IWebElement tableHeaderRow => _seleniumDriver.FindElement(By.CssSelector("html body div.container-fluid table.table thead.thead-dark tr.d-flex"));

        public PollsPage(IWebDriver seleniumDriver) : base(seleniumDriver)
        {
            _url = PagesConfigReader.PollsUrl;
        }

        public void GetTableContent()
        {
            var splitTds = tdElements.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 3)
                .Select(x => x.Select(v => v.Value).ToList()).ToList();

            for (int i = 0; i < thElements.Count; i++)
            {
                pollsList.Add(
                    new PollsKeyValues(
                        thElements[i].Text,
                        splitTds[i][0].Text,
                        splitTds[i][1].Text,
                        splitTds[i][2].Text
                    )
                );
            }
        }
        public List<List<string>> GetTableData(int numOfRows = -1) => ConvertTable(tableBody, numOfRows);

        public List<string> GetTableHeaders() => ConvertTableHeaders(tableHeaderRow);

        public bool CheckDateFormatInTable(int numOfRows)
        {
            bool dateCorrectformat = false;

            List<List<string>> pollsData = GetTableData(numOfRows);

            for (int i = 0; i < pollsData.Count; i++)
            {
                string date = pollsData[i][0].ToString();
                if (date.Contains("am") || date.Contains("pm"))
                {
                    dateCorrectformat = true;
                }
                else
                {
                    dateCorrectformat = false;
                }
            }
            return dateCorrectformat;
        }

        public string CheckStatus(int numOfRows)
        {
            int completedCount = 0;
            int inProgressCount = 0;
            int waitingCount = 0;
            int abortedCount = 0;
            int numOfRowStatusSkipped = 0;

            List<List<string>> pollsData = GetTableData(numOfRows);

            for (int i = 0; i < pollsData.Count; i++)
            {
                string currentStatus = pollsData[i][1].ToString();

                switch (currentStatus)
                {
                    case "completed":
                        completedCount++;
                        break;                                         
                    case "in progress":
                        inProgressCount++;
                        break;
                    case "waiting":
                        waitingCount++;
                        break;
                    case "aborted":
                        abortedCount++;
                        break;
                    default:
                        numOfRowStatusSkipped++;
                        break;
                }
            }
            return $"completed: {completedCount}, in progress: {inProgressCount}, waiting: {waitingCount}, aborted: {abortedCount}, number of rows skipped: {numOfRowStatusSkipped}";
        }

    }
}