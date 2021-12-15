using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace DevOps_Case
{
    class Program
    {
        static void Main(string[] args)
        {

            // initialize variable
            string webChoice;

            // give small menu
            Console.WriteLine("Choose a website you want to scrape:");
            Console.WriteLine("Youtube = y or Y");
            Console.WriteLine("Indeed = i or I");
            Console.WriteLine("CoinMarketCap = c or C");
            Console.WriteLine();
            // Console.WriteLine("Your choice: ");
            System.Text.StringBuilder csvBuilder = new StringBuilder();

            // fill var webChoice
            webChoice = Console.ReadLine();

            if (webChoice == "y" || webChoice == "Y")
            {
                // Physical file path for the scv file (created and stored)
                string csvFilePath = "C:\\Users\\Bert\\Desktop\\School\\2ITF\\devops\\Case_studyYouTube.csv";

                // write some text on the screen and fill in var with search term
                Console.WriteLine("You chose to scrape YouTube!");
                Console.WriteLine("What Search term you want to scrape: ");
                string searchYt = Console.ReadLine();

                // make driver for youtube
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://www.youtube.com/");

                // set chrome on fullscreen
                driver.Manage().Window.Maximize();

                // accept the popup for youtubes ULA
                var accept = driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]/div[5]/div[2]/ytd-button-renderer[2]/a"));
                accept.Click();

                // get search bar and putt the search term in the bar and click enter
                var element = driver.FindElement(By.XPath("/html/body/ytd-app/div/div/ytd-masthead/div[3]/div[2]/ytd-searchbox/form/div[1]/div[1]/input"));
                element.Click();
                element.SendKeys(searchYt);
                element.Submit();

                // put on filter to get the newest videos on a given search
                var filter = driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/div/ytd-toggle-button-renderer/a/tp-yt-paper-button"));
                filter.Click();
                Thread.Sleep(1000);
                var sortLastHour = driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/iron-collapse/div/ytd-search-filter-group-renderer[1]/ytd-search-filter-renderer[1]/a"));
                sortLastHour.Click();
                Thread.Sleep(1000);
                filter.Click();
                Thread.Sleep(1000);
                var sortByDate = driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/iron-collapse/div/ytd-search-filter-group-renderer[5]/ytd-search-filter-renderer[2]/a"));
                sortByDate.Click();
                Console.Clear();
                Thread.Sleep(5000);

                for (int i = 1; i < 6; i++)
                {
                    // define number of video in YouTube
                    string a = i.ToString();

                    Console.WriteLine();
                    Console.WriteLine("*************************************");
                    Console.WriteLine();
                    // get video title
                    string title = "(//*[@id=\"video-title\"]/yt-formatted-string)[" + a + "]";
                    var videotitle = driver.FindElement(By.XPath(title)).Text;

                    // get amount of views
                    string views = "(//*[@id=\"metadata-line\"]/span[1])[" + a + "]";
                    var videoViews = driver.FindElement(By.XPath(views)).Text;

                    // get name of youtube channel
                    string channel = "/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[2]/ytd-item-section-renderer/div[3]/ytd-video-renderer[" + a + "]/div[1]/div/div[2]/ytd-channel-name/div/div/yt-formatted-string/a";
                    var videoChannel = driver.FindElement(By.XPath(channel)).Text;

                    // get url of youtube video
                    string url = "/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[2]/ytd-item-section-renderer/div[3]/ytd-video-renderer[" + a + "]/div[1]/ytd-thumbnail/a";
                    var url1 = driver.FindElement(By.XPath(url));
                    // get the value of the href tag inside the xpath element.
                    string videourl = url1.GetAttribute("href");

                    Console.WriteLine(videotitle);
                    Console.WriteLine(videoViews);
                    Console.WriteLine(videoChannel);
                    Console.WriteLine(videourl);

                    csvBuilder.Append("\"" + videotitle + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + videoViews + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + videoChannel + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + videourl + "\"");
                    csvBuilder.Append("\n");
                }
                File.AppendAllText(csvFilePath, csvBuilder.ToString());
            }
            else if (webChoice == "i" || webChoice == "I")
            {
                // Physical file path for the scv file (created and stored)
                string csvFilePath = "C:\\Users\\Bert\\Desktop\\School\\2ITF\\devops\\Indeed.csv";

                // write info test on the screen en fill in variable with job and place
                Console.WriteLine("You chose to scrape Indeed!");
                Console.WriteLine("What Job Advertisements you want to scrape: ");
                string searchIndeed = Console.ReadLine();
                Console.WriteLine("Where do you want to work? (Postal-code, Town, State):");
                string workplace = Console.ReadLine();

                // make a new webdriver for indeed.
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://be.indeed.com/");

                // put chrome window on full screen
                driver.Manage().Window.Maximize();

                // get input tags to fill in job and place to search for job advertisments
                var job = driver.FindElement(By.XPath("//*[@id=\"text-input-what\"]"));
                var place = driver.FindElement(By.XPath("//*[@id=\"text-input-where\"]"));

                // click on the job input and fill it with variable
                job.Click();
                job.SendKeys(searchIndeed);

                // click on the place input and fill it with variable
                place.Click();
                place.SendKeys(workplace);

                // press enter to search for job
                job.Submit();

                // filter job on date
                var datePlaced = driver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/div/div[2]/div/div[1]/button"));
                datePlaced.Click();

                // filter job on date and then select 3 days ago.
                var days = driver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/div/div[2]/div/div[1]/ul/li[2]/a"));
                days.Click();

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

                var closePopUp = driver.FindElement(By.XPath("/html/body/div[5]/div[1]/button"));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                closePopUp.Click();

                Thread.Sleep(2000);

                // define lists
                List<string> jobs = new List<string>();
                List<string> names = new List<string>();
                List<string> locations = new List<string>();
                List<string> links = new List<string>();

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,1000)");
                Thread.Sleep(1000);

                var jobTitle = driver.FindElements(By.CssSelector("h2.jobTitle > span"));

                foreach (var job2 in jobTitle)
                {
                    jobs.Add(job2.Text);
                }

                var companyName = driver.FindElements(By.CssSelector("pre > span.companyName"));

                foreach (var name in companyName)
                {
                    names.Add(name.Text);
                }

                var location = driver.FindElements(By.CssSelector("pre > div.companyLocation"));

                foreach (var loc in location)
                {
                    locations.Add(loc.Text);
                }

                var jobUrl1 = driver.FindElements(By.CssSelector("a.result"));

                foreach (var link in jobUrl1)
                {
                    string url5 = link.GetAttribute("href");
                    links.Add(url5);
                }

                Console.WriteLine();
                Console.WriteLine("******************************");

                for (int i = 0; i < jobs.Count; i++)
                {
                    Console.WriteLine();

                    Console.WriteLine(jobs[i]);
                    Console.WriteLine(names[i]);
                    Console.WriteLine(locations[i]);
                    Console.WriteLine(links[i]);

                    Console.WriteLine();
                    Console.WriteLine("******************************");

                    csvBuilder.Append("\"" + jobs[i] + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + names[i] + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + locations[i] + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + links[i] + "\"");
                    csvBuilder.Append("\n");
                }
                File.AppendAllText(csvFilePath, csvBuilder.ToString());
            }
            else if (webChoice == "c" || webChoice == "C")
            {
                // write text on screen and fill variable for search term
                Console.WriteLine("You chose to scrape CoinMarketCap!");
                Console.WriteLine("Do you want a particular Coin or a list of the top 100 coins? (Type 'COIN' or 'LIST')");
                string choice = Console.ReadLine();

                System.Text.StringBuilder svcBuilder = new StringBuilder();

                if (choice == "COIN" || choice == "coin" || choice == "Coin")
                {
                    // Physical file path for the scv file (created and stored)
                    string csvFilePath = "C:\\Users\\Bert\\Desktop\\School\\2ITF\\devops\\CoinMarketCapCoin.csv";

                    Console.WriteLine("You chose to scrape a particular coin.");
                    Console.WriteLine("What Coin you want to scrape (Name or Symbol): ");
                    string searchmarketcap = Console.ReadLine();

                    // create new webdriver for CoinMarketCap
                    IWebDriver driver = new ChromeDriver();
                    driver.Navigate().GoToUrl("https://coinmarketcap.com/");

                    // make chrome window at full screen
                    driver.Manage().Window.Maximize();
                    driver.Navigate().Refresh();

                    var searchbar = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[1]/div/div[1]/div[1]/div/div[2]/div[3]/div/div[1]"));
                    searchbar.Click();

                    var fillsearchbar = driver.FindElement(By.XPath("/html/body/div/div[1]/div/div[1]/div[1]/div/div[2]/div[4]/div/div/div/div/div[1]/div[1]/input"));
                    fillsearchbar.SendKeys(searchmarketcap + Keys.Enter);

                    // get all the nessecary data for the chosen coin
                    var coinName = driver.FindElement(By.XPath("/html/body/div/div[1]/div/div[2]/div/div[1]/div[2]/div/div[1]/div[1]/h2")).Text;
                    var coinPrice = driver.FindElement(By.XPath("/html/body/div/div[1]/div/div[2]/div/div[1]/div[2]/div/div[2]/div[1]/div/span")).Text;
                    var coinChange24H = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[1]/div/div[2]/div/div[3]/div/div[1]/div[2]/div[2]/div/div[1]/table/tbody/tr[2]/td/span")).Text;
                    var coinVolume24H = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[1]/div/div[2]/div/div[3]/div/div[1]/div[2]/div[2]/div/div[1]/table/tbody/tr[4]/td/span")).Text;
                    var coinRank = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[1]/div/div[2]/div/div[3]/div/div[1]/div[2]/div[2]/div/div[1]/table/tbody/tr[7]/td")).Text;
                    var coinMarketDominance = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[1]/div/div[2]/div/div[3]/div/div[1]/div[2]/div[2]/div/div[1]/table/tbody/tr[6]/td/span")).Text;
                    var coinMarketCap = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[1]/div/div[2]/div/div[3]/div/div[1]/div[2]/div[2]/div/div[2]/table/tbody/tr[1]/td/span")).Text;

                    // print all the data for the coin
                    Console.WriteLine(coinName);
                    Console.WriteLine(coinPrice);
                    Console.WriteLine(coinChange24H);
                    Console.WriteLine(coinVolume24H);
                    Console.WriteLine(coinRank);
                    Console.WriteLine(coinMarketDominance);
                    Console.WriteLine(coinMarketCap);


                    csvBuilder.Append("\"" + coinName + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + coinPrice + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + coinChange24H + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + coinVolume24H + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + coinRank + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + coinMarketDominance + "\"");
                    csvBuilder.Append(";");
                    csvBuilder.Append("\"" + coinMarketCap + "\"");
                    csvBuilder.Append("\n");

                    File.AppendAllText(csvFilePath, csvBuilder.ToString());
                }
                else if (choice == "LIST" || choice == "list" || choice == "List")
                {
                    // Physical file path for the scv file (created and stored)
                    string csvFilePath = "C:\\Users\\Bert\\Desktop\\School\\2ITF\\devops\\CoinMarketCapTop100List.csv";

                    Console.WriteLine("You chose to see the top 100 list.");

                    // create new webdriver for CoinMarketCap
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    IWebDriver driver = new ChromeDriver(chromeOptions);
                    driver.Navigate().GoToUrl("https://coinmarketcap.com/");

                    // make chrome window at full screen
                    driver.Manage().Window.Maximize();
                    driver.Navigate().Refresh();

                    // scroll down the page to load all the data to fetch the paths and selectors
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    for (int i = 0; i < 8; i++)
                    {
                        js.ExecuteScript("window.scrollBy(0,1000)");
                        Thread.Sleep(1000);
                    }

                    // loop over all Top 100 coins
                    for (int i = 1; i < 101; i++)
                    {
                        string a = i.ToString();
                        // get the list of the top 100 cryptocoins
                        Thread.Sleep(50); // sleep per coin 0.05 seconds
                        // get alle the data from XPath 
                        var coinName = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[1]/div[2]/div/div[1]/div[5]/table/tbody/tr[" + a + "]/td[3]/div/a/div/div/p")).Text;
                        var coinPrice = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[1]/div[2]/div/div[1]/div[5]/table/tbody/tr[" + a + "]/td[4]/div/a/span")).Text;
                        var coinMarketCap = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[1]/div[2]/div/div[1]/div[5]/table/tbody/tr[" + a + "]/td[7]/p/span[2]")).Text;
                        var coinVolume24H = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[1]/div[2]/div/div[1]/div[5]/table/tbody/tr[" + a + "]/td[8]/div/p")).Text;

                        // print all the data in console
                        Console.WriteLine(coinName);
                        Console.WriteLine(coinPrice);
                        Console.WriteLine(coinMarketCap);
                        Console.WriteLine(coinVolume24H);
                        Console.WriteLine("------------------");

                        csvBuilder.Append("\"" + coinName + "\"");
                        csvBuilder.Append(";");
                        csvBuilder.Append("\"" + coinPrice + "\"");
                        csvBuilder.Append(";");
                        csvBuilder.Append("\"" + coinVolume24H + "\"");
                        csvBuilder.Append(";");
                        csvBuilder.Append("\"" + coinMarketCap + "\"");
                        csvBuilder.Append("\n");
                    }
                    File.AppendAllText(csvFilePath, csvBuilder.ToString());
                }
            }
            else
            {
                // if none of the right letters are pressed show this message
                Console.WriteLine("You did not enter the right character, please try again.");
            }
        }
    }
}
