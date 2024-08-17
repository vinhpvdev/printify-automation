using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace PrintifyAutomation
{
    public class PrintifyAutomation
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public PrintifyAutomation()
        {
            var options = new EdgeOptions();
            //var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--no-sandbox");
            options.DebuggerAddress = "localhost:9444";
            driver = new EdgeDriver(options);
            //driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(1000),
            };
        }

        public void PublishProduct(string product, string bulletPoint)
        {
            try
            {
                string searchBoxXPath = "//pfy-input[contains(@class,'input-search')]//input";
                IWebElement searchBoxElement = wait.Until(driver => driver.FindElement(By.XPath(searchBoxXPath)));

                //Searching product by product name then Click on its Action button
                searchBoxElement.Clear();
                Thread.Sleep(500);
                searchBoxElement.SendKeys(product);
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Search for product", "Failed to search for the product.", ex);
            }

            //wait.Until(driver => driver.FindElement(By.XPath("//pfa-products-list-item[@data-testid='item']")));

            try
            {
                string productLinkXPath = $"//pfa-products-list-item//a[.//p[contains(text(),\"{product.Trim()}\")]]";
                IWebElement productLinkElement = wait.Until(driver => driver.FindElement(By.XPath(productLinkXPath)));
                productLinkElement.Click();
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Click on product link", "Failed to click on product link.", ex);
            }

            try
            {
                //Check on mockup
                string mockupCarouselItemsXPath = "//pfy-carousel[contains(@class,'mockups-carousel')][1]//pfy-carousel-item//label[@data-testid='checkboxWrapper']";
                var mockupCarouselItemElements = wait.Until(driver => driver.FindElements(By.XPath(mockupCarouselItemsXPath)));
                if (mockupCarouselItemElements.Count >= 5)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        if (mockupCarouselItemElements[i].GetAttribute("aria-checked") == "false")
                        {
                            mockupCarouselItemElements[i].Click();
                            Thread.Sleep(500);
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < mockupCarouselItemElements.Count; i++)
                    {
                        if (mockupCarouselItemElements[i].GetAttribute("aria-checked") == "false")
                        {
                            mockupCarouselItemElements[i].Click();
                            Thread.Sleep(500);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Click on product mockups checkbox", "Failed to find and click on product mockups checkbox.", ex);
            }

            try
            {
                //Add bullet point
                string bulletPointXPath = "//div[@formarrayname='bulletPoints']//input[@placeholder='Add new']";
                IWebElement bulletPointElement = wait.Until(driver => driver.FindElement(By.XPath(bulletPointXPath)));
                bulletPointElement.SendKeys(bulletPoint);
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Add text on Bullet Point textbox", "Failed to find Bullet Point input textbox and add text on Bullet Point textbox.", ex);
            }

            try
            {
                string publishBtnXPath = "//pfy-button[@data-analyticsid='publishProduct']/button";
                IWebElement publishBtnElement = wait.Until(driver => driver.FindElement(By.XPath(publishBtnXPath)));
                publishBtnElement.Click();
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Click on Publish Product button", "Failed to find and click on Publish Product button.", ex);
            }

        }

        public void SelectProductAndCopy(string product, string targetStore)
        {
            try
            {
                string searchBoxXPath = "//pfy-input[contains(@class,'input-search')]//input";
                IWebElement searchBoxElement = wait.Until(driver => driver.FindElement(By.XPath(searchBoxXPath)));

                //Searching product by product name then Click on its Action button
                searchBoxElement.Clear();
                Thread.Sleep(500);
                searchBoxElement.SendKeys(product);
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Search for product", "Failed to search for the product.", ex);
            }

            IWebElement productActionElement = null;
            try
            {
                string productActionXPath = $"//pfa-products-list-item[.//p[contains(text(),\"{product.Trim()}\")]]//div[@class='action-wrapper']";
                productActionElement = wait.Until(driver => driver.FindElement(By.XPath(productActionXPath)));
                productActionElement.Click();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Click on the product action button", "Failed to find and click on the action button for the product.", ex);
            }

            try
            {
                //Click on "Copy To..." button
                string copyActionXPath = ".//pfy-dropdown-option[@data-analyticsid='productsCopy']";
                IWebElement copyToButtonElement = productActionElement.FindElement(By.XPath(copyActionXPath));
                copyToButtonElement.Click();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Click on the product 'Copy To...' button", "Failed to find and click on the 'Copy To...' button.", ex);
            }
            
            try
            {
                //Check on the target store's checkbox
                string modalDialogContentXPath = "//div[contains(@class,'modal-dialog-wrapper')]/div[@data-testid='modalDialogContent']";
                IWebElement modalDialogContentElement = wait.Until(driver => driver.FindElement(By.XPath(modalDialogContentXPath)));
                string targetStoreCheckboxXPath = $"//div[contains(@class, 'store-item')]//p[contains(text(), '{targetStore}')]//ancestor::label//div[@class='checkbox-checkbox']";
                IWebElement targetStoreCheckboxElement = modalDialogContentElement.FindElement(By.XPath(targetStoreCheckboxXPath));
                targetStoreCheckboxElement.Click();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Check on the target store checkbox", "Failed to check on the target store checkbox.", ex);
            }

            try
            {
                //Click on Copy Products Button
                string modalDialogFooterXPath = "//div[@data-testid='modalDialogFooter']";
                IWebElement modalDialogFooterElement = wait.Until(driver => driver.FindElement(By.XPath(modalDialogFooterXPath)));
                string copyProductButtonXPath = ".//button[@class='button primary medium']";
                IWebElement copyProductButtonElement = modalDialogFooterElement.FindElement(By.XPath(copyProductButtonXPath));
                if (copyProductButtonElement.Displayed && copyProductButtonElement.Enabled)
                {
                    copyProductButtonElement.Click();
                    Thread.Sleep(3000);
                }
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Click on Copy Product button", "Failed to find and click on Copy Product button.", ex);
            }
        }

        public void GoToStore(string store)
        {
            try
            {
                //Click to trigger dropdown Stores
                string selectedStoreXPath = "//span[@data-testid='selectedStoreName']";
                var selectedStoreElement = wait.Until(driver => driver.FindElement(By.XPath(selectedStoreXPath)));
                Console.WriteLine(selectedStoreElement.Text);
                selectedStoreElement.Click();
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Click on Dropdown Store Menu button", "Failed to find and click on Dropdown Store Menu button.", ex);
            }
            
            try
            {
                string sourceStoreXPath = $"//button[.//p[contains(text(), '{store}')]]";
                var sourceStoreElement = wait.Until(driver => driver.FindElement(By.XPath(sourceStoreXPath)));
                sourceStoreElement.Click();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                throw new AutomationStepException("Click on Target Store label", "Failed to find and click on Target Store label.", ex);
            }

        }

        public void AccessWorkingArea()
        {
            string url = "https://printify.com/app/store/products/1";
            driver.Navigate().GoToUrl(url);
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
        public void Close()
        {
            driver.Quit();
        }
    }

    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}
