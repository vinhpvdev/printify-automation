using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System.Data;
using System.Text.Json;

namespace PrintifyAutomation
{
    public partial class Form1 : Form
    {
        private readonly string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIzN2Q0YmQzMDM1ZmUxMWU5YTgwM2FiN2VlYjNjY2M5NyIsImp0aSI6IjdiMGI1OWI3ZDEyNWRlNjFlYTQwYjY5MjZkNGNlYzkwYzAwZTM1MTRmODUzMWZhZDVjNDM4MDFhMGZjYzcxY2M2ZmFlZGI0OGYxOGM3MDBhIiwiaWF0IjoxNzIzNjU3MDM0LjE0NzAyNSwibmJmIjoxNzIzNjU3MDM0LjE0NzAyNywiZXhwIjoxNzU1MTkzMDM0LjEzNzc4OCwic3ViIjoiMTUzNDI3NzIiLCJzY29wZXMiOlsic2hvcHMubWFuYWdlIiwic2hvcHMucmVhZCIsImNhdGFsb2cucmVhZCIsIm9yZGVycy5yZWFkIiwib3JkZXJzLndyaXRlIiwicHJvZHVjdHMucmVhZCIsInByb2R1Y3RzLndyaXRlIiwid2ViaG9va3MucmVhZCIsIndlYmhvb2tzLndyaXRlIiwidXBsb2Fkcy5yZWFkIiwidXBsb2Fkcy53cml0ZSIsInByaW50X3Byb3ZpZGVycy5yZWFkIiwidXNlci5pbmZvIl19.AjOyp9D7JkJgAiIv_39KgneTwWg6Rr8ri6izxb-_0DKerkR4trlTt5LMxUm-E_iiKI05nquuPQk7yaH6aPQ";
        List<string> products = new List<string>();
        List<string> stores = new List<string>();
        string bulletPoint = "";
        private PrintifyAutomation automation;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Printify Automation";
            BrowserLauncher.StartBrowserWithDebugging();
            automation = new PrintifyAutomation();
            automation.AccessWorkingArea();
            this.Load += new EventHandler(MainForm_Load);
            btnCopy.Click += new EventHandler(btnCopy_Click);
            btnPublish.Click += new EventHandler(btnPublish_Click);
            btnCopyAndPublish.Click += new EventHandler(btnCopyAndPublish_Click);
        }
        private async void MainForm_Load(object? sender, EventArgs e)
        {
            progressLabel.BackColor = Color.Transparent;
            await FetchSourceStoresAsync();
            await FetchTargetStoresAsync();
        }

        private async Task FetchTargetStoresAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                    var response = await client.GetAsync("https://api.printify.com/v1/shops.json");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        stores = ParseStoreNames(responseString);
                        cmbTargetStore.DataSource = stores;
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch stores. Please check your access token or internet connection.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private async Task FetchSourceStoresAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                    var response = await client.GetAsync("https://api.printify.com/v1/shops.json");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        stores = ParseStoreNames(responseString);
                        cmbSourceStore.DataSource = stores;
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch stores. Please check your access token or internet connection.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private List<string> ParseStoreNames(string jsonResponse)
        {
            var stores = new List<string>();

            try
            {
                // Parse the JSON response into a dynamic object
                var json = JToken.Parse(jsonResponse);

                // Iterate through each store item in the JSON response
                foreach (var store in json)
                {
                    stores.Add(store["title"].ToString());
                }
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Failed to parse JSON response: {ex.Message}");
            }

            return stores;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select file";
            openFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            progressBar1.Visible = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 50;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    progressBar1.Visible = true;
                    products.Clear();
                    ImportExcel(openFileDialog.FileName);
                    progressBar1.Style = ProgressBarStyle.Blocks;
                    MessageBox.Show($"Imported Successfully! Total {products.Count} records");
                    txtPath.Text = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnPublish_Click(object? sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = products.Count;
            progressBar1.Step = 10;
            progressLabel.Text = "Processing...";
            var failedValue = 0;
            automation.GoToStore(cmbTargetStore.SelectedItem.ToString());
            for (int i = 0; i < products.Count; i++)
            {
                try
                {
                    automation.PublishProduct(products[i], txtBulletPoint.Text);
                    UpdateProgress(i + 1, products.Count);
                    progressLabel.Text = $"{i}/{products.Count}";
                }
                catch (AutomationStepException ex)
                {
                    Logger.LogException(products[i], ex);
                    Logger.LogProductThatFailedOnProcess(products[i].ToString());
                    failedValue++;
                    continue;
                }
            }
            progressBar1.Invoke((Action)(() =>
                {
                    progressBar1.Value = 0;
                    progressLabel.Text = "Done!";
                }));
            MessageBox.Show($"Publish products successfully!. Total: {products.Count} - Failed: {failedValue}");
        }

        private void btnCopy_Click(object? sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = products.Count;
            progressBar1.Step = 10;
            progressLabel.Text = "Processing...";
            var failedValue = 0;
            automation.GoToStore(cmbSourceStore.SelectedItem.ToString());
            for (int i = 0; i < products.Count; i++)
            {
                try
                {
                    automation.SelectProductAndCopy(products[i], cmbTargetStore.SelectedItem.ToString());
                    UpdateProgress(i + 1, products.Count);
                    progressLabel.Text = $"{i}/{products.Count}";
                }
                catch (AutomationStepException ex) 
                {
                    Logger.LogException(products[i], ex);
                    Logger.LogProductThatFailedOnProcess(products[i].ToString());
                    failedValue++;
                    continue;
                }
            }
            UpdateProgress(products.Count, products.Count);
            progressLabel.Invoke((Action)(() =>
            {
                progressLabel.Text = "Copy products successfully!";
            }));
            MessageBox.Show($"Copy products successfully!. Total: {products.Count} - Failed: {failedValue}");
        }

        private void btnCopyAndPublish_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = products.Count * 2;
            progressBar1.Step = 10;
            progressLabel.Text = "Processing...";
            var process = 0;
            var copyFailedValue = 0;
            var publishFailedValue = 0;
            automation.GoToStore(cmbSourceStore.SelectedItem.ToString());
            for (int i = 0; i < products.Count; i++)
            {
                try
                {
                    automation.SelectProductAndCopy(products[i], cmbTargetStore.SelectedItem.ToString());
                    UpdateProgress(i + 1, products.Count*2);
                    process++;
                    progressLabel.Text = $"{process}/{products.Count * 2}";
                }
                catch (AutomationStepException ex)
                {
                    Logger.LogException(products[i], ex);
                    Logger.LogProductThatFailedOnProcess(products[i].ToString());
                    copyFailedValue++;
                    continue;
                }
            }

            automation.GoToStore(cmbTargetStore.SelectedItem.ToString());
            for (int i = 0; i < products.Count; i++)
            {
                try
                {
                    automation.PublishProduct(products[i], txtBulletPoint.Text);
                    UpdateProgress(i + 1, products.Count*2);
                    process++;
                    progressLabel.Text = $"{process}/{products.Count * 2}";
                }
                catch (AutomationStepException ex)
                {
                    Logger.LogException(products[i], ex);
                    Logger.LogProductThatFailedOnProcess(products[i].ToString());
                    publishFailedValue++;
                    continue;
                }

            }
            progressBar1.Invoke((Action)(() =>
            {
                progressBar1.Value = 0;
                progressLabel.Text = "Done!";
            }));
            MessageBox.Show($"Copy and publish products successfully!. Total: {products.Count} - Failed: {copyFailedValue} on copy, {publishFailedValue} on publish");
        }

        private void ImportExcel(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var productNameColumnHeader = "Title";
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorkSheet = excelPackage.Workbook.Worksheets[0];
                CleanUpExcelSheet(excelWorkSheet);
                for (int i = excelWorkSheet.Dimension.Start.Column; i <= excelWorkSheet.Dimension.End.Column; i++)
                {
                    if (excelWorkSheet.Cells[1, i].Value.ToString() == productNameColumnHeader)
                    {
                        for (int j = excelWorkSheet.Dimension.Start.Row + 1; j <= excelWorkSheet.Dimension.End.Row; j++)
                        {
                            if (!string.IsNullOrEmpty(excelWorkSheet.Cells[j, i].Value.ToString()))
                            {
                                products.Add(excelWorkSheet.Cells[j, i].Value.ToString());
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void UpdateProgress(int currentValue, int maxValue)
        {
            // Calculate the percentage
            double percentage = (double)currentValue / maxValue * 100;

            // Update the ProgressBar and Label on the UI thread
            if (progressBar1.InvokeRequired || progressLabel.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = currentValue;
                }));
            }
            else
            {
                progressBar1.Value = currentValue;
            }
        }

        private void txtBulletPoint_TextChanged(object sender, EventArgs e)
        {

        }

        private void CleanUpExcelSheet(ExcelWorksheet excelWorkSheet)
        {
            // Remove empty rows
            for (int row = excelWorkSheet.Dimension.End.Row; row >= excelWorkSheet.Dimension.Start.Row; row--)
            {
                if (IsRowEmpty(excelWorkSheet, row))
                {
                    excelWorkSheet.DeleteRow(row);
                }
            }

            // Remove empty columns
            for (int col = excelWorkSheet.Dimension.End.Column; col >= excelWorkSheet.Dimension.Start.Column; col--)
            {
                if (IsColumnEmpty(excelWorkSheet, col))
                {
                    excelWorkSheet.DeleteColumn(col);
                }
            }
        }

        // Helper function to check if a row is empty
        private bool IsRowEmpty(ExcelWorksheet worksheet, int row)
        {
            for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
            {
                if (worksheet.Cells[row, col].Value != null && !string.IsNullOrEmpty(worksheet.Cells[row, col].Value.ToString().Trim()))
                {
                    return false;
                }
            }
            return true;
        }

        // Helper function to check if a column is empty
        private bool IsColumnEmpty(ExcelWorksheet worksheet, int col)
        {
            for (int row = worksheet.Dimension.Start.Row; row <= worksheet.Dimension.End.Row; row++)
            {
                if (worksheet.Cells[row, col].Value != null && !string.IsNullOrEmpty(worksheet.Cells[row, col].Value.ToString().Trim()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
