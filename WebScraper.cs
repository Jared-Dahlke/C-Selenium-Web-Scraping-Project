using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace WebScrape
{
    

    class Program
    {
        static void Main(string[] args)
        {      
            TestIt();
        }


        

        private static void TestIt()
        {

         
            string processHandle = "";

            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    if (process.MainWindowTitle.ToString().Contains("econciliations"))
                    {
                        processHandle = process.Handle.ToString();
                    }
                  

                  
                }
            }
         

            ChromeDriver cd = new ChromeDriver();
        

            string currentUrl = @"https://xeroxtechnology.us2.blackline.com/Modules/Reconciliations/Certify.aspx?x=DaRhsyQWiMqADMa6qXD01CDlYHTyoDNDHURmFtoUIlpzkhMssdyhvMU4xBIYEyJ2EXwHo%2fEaLl1m6zFErv%2fSZ7pEDaK6gtKOVL3dtxcmAsZZxTeFYFDXXP%2fMovUtBbbI5TnoDBke0DrZApLhouPyrgO6aspghXOO%2bFF%2b1VyAM1It7UIJFRSKPrdmDCL8bwwLkUY%2f5vAauyaUXYf8xjSYTzy2bH6S67CCZPqye9A%2bFAPGE3eF0JCudGefOum80gH6cUNwL07iKNLziXpW0Gkwuh7m3BUBOAgXaLMbSBJA1GIae1IrISSO94uvgoCmbSsdgsd3rdOa1pZwWCtemxmDYG4xfwpRswh7zZ9%2fJGOKPEpMy%2bjjGSbMTHh8BLw6E7NFdimNcvx5eOuDdqdddvBV31msq4cKG5kW4paUvDbca%2fNe1d19PrbvcXibcWJVgA8B";


            cd.Navigate().GoToUrl(currentUrl);

           
            cd.FindElementById("ctl00_ctl00_contentBody_cphMain_theme_tbUserName").SendKeys("myUserName");
            System.Threading.Thread.Sleep(1000);
            cd.FindElementById("ctl00_ctl00_contentBody_cphMain_theme_tbPassword").SendKeys("myPassword");
            System.Threading.Thread.Sleep(1000);
            cd.FindElementById("ctl00_ctl00_contentBody_cphMain_theme_btnSubmit").Click();

            System.Threading.Thread.Sleep(8000);
            cd.FindElementById("modules--navigation-user-role").Click();

            string activateApproverURL = "https://xeroxtechnology.us2.blackline.com/user/active-role/edit?roleId=2";
           
             cd.Navigate().GoToUrl(activateApproverURL);
            System.Threading.Thread.Sleep(5000);

            //set period, 40 = 10/31/2018
            string period = "43";
            string monthDate = "https://xeroxtechnology.us2.blackline.com/period/selected-period/edit?periodId="+period+"";
            cd.Navigate().GoToUrl(monthDate);
            Console.WriteLine("went to proper month");

            //set to team items
            System.Threading.Thread.Sleep(3000);
            cd.FindElementById("teamToggle").Click();
            Console.WriteLine("clicked on the team assignments toggle");


            System.Threading.Thread.Sleep(3000);
            string numRecsRef = "https://xeroxtechnology.us2.blackline.com/Modules/Reconciliations/ExecGrid.aspx?x=OTxXbD0gMhgFe76YMWVg9pTFEyyH%2botk2vEXztJ4bZo%3d";
            cd.Navigate().GoToUrl(numRecsRef);
            Console.WriteLine("clicked on the number of recs button");

           int filterFlag =  0;

            //loop through each rec and certify
            for (int rec = 1; rec < 300; rec++)
            {

                


                Console.WriteLine("start new loop");
                System.Threading.Thread.Sleep(3000);



                if (filterFlag == 0)
                {
                    string filterDropdownXPath = "//body[@class='prime-2016 bl--viewport-layout None bl--header-redesigned   ']/div[@class='bl--app-outer']/div[@class='bl--page-content bl--layout-2-columns-collapsible']/div[@class='bl--layout-primary-col bl--col-no-pad']/div[@id='modules--execGrid']/div[@id='modules--execGrid-grid']/div[@class='atlas-grid--header']/div[3]/div[@id='modules--execGrid-viewOptions']/div[@class='atlas--ui-dropdown']/button[@*]/span[@class='atlas--ui-dropdown-title']";

                    cd.FindElementByXPath(filterDropdownXPath).Click();
                    Console.WriteLine("clicked dropdown filter button");

                    System.Threading.Thread.Sleep(3000);


                
                    string preparedOnlyFilterXpath = "//html[@class='bl--viewport-layout webkit webkit537 chrome chrome72']/body[@class='prime-2016 bl--viewport-layout None bl--header-redesigned   ']/div[@class='bl--app-outer']/div[@class='bl--page-content bl--layout-2-columns-collapsible']/div[@class='bl--layout-primary-col bl--col-no-pad']/div[@id='modules--execGrid']/div[@id='modules--execGrid-grid']/div[@class='atlas-grid--header']/div[3]/div[@id='modules--execGrid-viewOptions']/div[@class='atlas--ui-dropdown atlas--ui-dropdown-active']/ul[@class='atlas--ui-dropdown-menu atlas--ui-dropdown-group']/li[@class='atlas--ui-dropdown-menu-group']/div[@class='atlas--ui-dropdown-menu-group-container']/div[@class='atlas--ui-dropdown-submenu']/ul/li[@class='atlas--ui-dropdown-menu-item-container']/span[@class='atlas--ui-dropdown-menu-item']";
                    cd.FindElementByXPath(preparedOnlyFilterXpath).Click();
                    Console.WriteLine("clicked prepared only button");
                    System.Threading.Thread.Sleep(3000);

                    filterFlag = 1;
                }
                
               
                string viewXpath = @"//div[@id='modules--execGrid']/div[@id='modules--execGrid-grid']/
div[@class='atlasGrid--grid-elements']/div[@class='atlasGrid--scroller atlas--limitless-scroller']/div[@class='atlas--ui-grid-body']
/div[@class='atlas--ui-grid-row atlas--limitless-item'][1]/div[@class='atlas--ui-grid-cell atlasGrid-cell #modules--execGrid-grid-col-action']"+
"/button[@class='button small btnViewAction atlas--ui-button']";


                try
                {
                    cd.FindElementByXPath(viewXpath).Click();
                    Console.WriteLine("went to a rec");
                }
                catch
                {
                    return;
                }
               

                //click the certify button
                System.Threading.Thread.Sleep(3000);
                try
                {
                    cd.FindElementByXPath("//input[@id='ctl00_ctl00_contentBody_cphMain_SaveCertifyButtons1_btnSaveCertify']").SendKeys(Keys.Enter);
                    Console.WriteLine("clicked  on certify#1 button");

                    
                }
                catch
                {
                    try
                    {
                        Console.WriteLine("couldn't click on certify#1 button using xpath");
                        try
                        {

                            cd.FindElementById("ctl00_ctl00_contentBody_cphMain_SaveCertifyButtons1_btnSaveCertify");
                                           //"id='ctl00_ctl00_contentBody_cphMain_SaveCertifyButtons1_btnSaveCertify'"
                            Console.WriteLine("couldn't click on certify#1 button using id");

                            //cant find certify button, need to figure out how to find
                        }
                        catch
                        {
                            cd.Navigate().GoToUrl("https://xeroxtechnology.us2.blackline.com/Modules/Reconciliations/ExecGrid.aspx");
                            Console.WriteLine("went to reconciliations list page");
                            System.Threading.Thread.Sleep(2000);
                        }
                       
                        System.Threading.Thread.Sleep(2000);
                        continue;
                    }
                    catch
                    {
                        Console.WriteLine("cant find main recs view url");
                        Console.ReadLine();
                    }

                }


                //click the checkboxes
                System.Threading.Thread.Sleep(2000);

                try
                {
                    string xPathQ1 = @"//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_tblCurrentChecklist']/tbody/tr[2]/td/div[@id='ctl00_ctl00_contentBody_cphMain_Checklist_dvCheckBoxList']/span[@class='textSmall'][1]/input[@id='ctl00_ctl00_contentBody_cphMain_Checklist_93_question']";
                    cd.FindElementByXPath(xPathQ1).Click();
                   
                }
                catch
                {
                    cd.FindElementByXPath("//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/div[@id='ctl00_ctl00_contentBody_cphMain_pnlTemplateControls']/table/tbody/tr[17]/td/div[@class='webcontrols_saveCertify webcontrols_saveCertify_ParentContainer textSmall']/div[@id='dvSaveCertify']/input[@id='ctl00_ctl00_contentBody_cphMain_SaveCertifyButtons1_btnSaveCertify']").SendKeys(Keys.Enter);
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("couldnt find q1 box so hit certify ");
                }


                string xPathQ2 = @"//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_tblCurrentChecklist']/tbody/tr[2]/td/div[@id='ctl00_ctl00_contentBody_cphMain_Checklist_dvCheckBoxList']/span[@class='textSmall'][2]/input[@id='ctl00_ctl00_contentBody_cphMain_Checklist_94_question']";
                cd.FindElementByXPath(xPathQ2).Click();

                string xPathQ3 = @"//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_tblCurrentChecklist']/tbody/tr[2]/td/div[@id='ctl00_ctl00_contentBody_cphMain_Checklist_dvCheckBoxList']/span[@class='textSmall'][3]/input[@id='ctl00_ctl00_contentBody_cphMain_Checklist_95_question']";
                cd.FindElementByXPath(xPathQ3).Click();

                string xPathQ4 = @"//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_tblCurrentChecklist']/tbody/tr[2]/td/div[@id='ctl00_ctl00_contentBody_cphMain_Checklist_dvCheckBoxList']/span[@class='textSmall'][4]/input[@id='ctl00_ctl00_contentBody_cphMain_Checklist_97_question']";
                cd.FindElementByXPath(xPathQ4).Click();

                string xPathQ5 = @"//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_tblCurrentChecklist']/tbody/tr[2]/td/div[@id='ctl00_ctl00_contentBody_cphMain_Checklist_dvCheckBoxList']/span[@class='textSmall'][5]/input[@id='ctl00_ctl00_contentBody_cphMain_Checklist_98_question']";
                cd.FindElementByXPath(xPathQ5).Click();

                string xPathQ6 = @"//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_tblCurrentChecklist']/tbody/tr[2]/td/div[@id='ctl00_ctl00_contentBody_cphMain_Checklist_dvCheckBoxList']/span[@class='textSmall'][6]/input[@id='ctl00_ctl00_contentBody_cphMain_Checklist_99_question']";
                cd.FindElementByXPath(xPathQ6).Click();

                string xPathQ7 = @"//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_tblCurrentChecklist']/tbody/tr[2]/td/div[@id='ctl00_ctl00_contentBody_cphMain_Checklist_dvCheckBoxList']/span[@class='textSmall'][7]/input[@id='ctl00_ctl00_contentBody_cphMain_Checklist_100_question']";
                cd.FindElementByXPath(xPathQ7).Click();

                string xPathQ8 = @"//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_tblCurrentChecklist']/tbody/tr[2]/td/div[@id='ctl00_ctl00_contentBody_cphMain_Checklist_dvCheckBoxList']/span[@class='textSmall'][8]/input[@id='ctl00_ctl00_contentBody_cphMain_Checklist_101_question']";
                cd.FindElementByXPath(xPathQ8).Click();

                string xPathQ9 = @"//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_tblCurrentChecklist']/tbody/tr[2]/td/div[@id='ctl00_ctl00_contentBody_cphMain_Checklist_dvCheckBoxList']/span[@class='textSmall'][9]/input[@id='ctl00_ctl00_contentBody_cphMain_Checklist_102_question']";
                cd.FindElementByXPath(xPathQ9).Click();

                string xPathQ10 = @"//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_tblCurrentChecklist']/tbody/tr[2]/td/div[@id='ctl00_ctl00_contentBody_cphMain_Checklist_dvCheckBoxList']/span[@class='textSmall'][10]/input[@id='ctl00_ctl00_contentBody_cphMain_Checklist_103_question']";
                cd.FindElementByXPath(xPathQ10).Click();

                Console.WriteLine("checked all of the boxes");

               



                string certifyPath = "//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_Table3']/tbody/tr[1]/td[2]/div[@id='dvSaveCertify']/input[@id='ctl00_ctl00_contentBody_cphMain_btnSubmit']";
                string backPath = "//body[@id='ctl00_ctl00_ctl07']/form[@id='aspnetForm']/div[@class='masterPages_ParentMaster_ContentContainer core_clearfix']/div[@id='mainContentPanel']/table[@id='ctl00_ctl00_contentBody_cphMain_CertTable']/tbody/tr[3]/td/table[@id='ctl00_ctl00_contentBody_cphMain_Table3']/tbody/tr[1]/td[2]/input[@id='ctl00_ctl00_contentBody_cphMain_btnCancel']";


                System.Threading.Thread.Sleep(1000);

                try
                {
                    cd.FindElementByXPath(certifyPath).Click();
                    Console.WriteLine("found certify #2");
                }
                catch
                {
                    cd.FindElementByXPath(backPath).Click();
                    Console.WriteLine("could not find certify #2 so clicked back");
                }
                   
                   
                System.Threading.Thread.Sleep(2000);
            }


            Console.ReadLine();

        }




    }

}
