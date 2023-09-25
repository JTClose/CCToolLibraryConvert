namespace CCToolLibraryConvert
{
    using System;
    using System.IO;
    using System.Security;
    using System.Windows.Forms;
    using System.Reflection;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System.Threading;
    using System.Globalization;
    using System.Collections;
    using System.Configuration;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
    using System.Text;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;
    using _360LibraryConverter;
    using Newtonsoft.Json.Linq;

    public partial class mainForm : Form
    {

        #region Not used
        private void camoticsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion


        #region mainForm
        public mainForm()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

            InitializeComponent();

            lvwColumnSorterTransactions = new ListViewColumnSorter();
            this.listViewCCToolLibrary.ListViewItemSorter = lvwColumnSorterTransactions;

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Set window location            
            this.Location = CCToolLibraryConvert.Properties.Settings.Default.WindowLocation;

            // Set window size
            this.Size = CCToolLibraryConvert.Properties.Settings.Default.WindowSize;

            // Set window size
            this.splitContainerMain.SplitterDistance = CCToolLibraryConvert.Properties.Settings.Default.SplitterDist;

            // Set window size
            this.splitContainerMain.SplitterDistance = CCToolLibraryConvert.Properties.Settings.Default.SplitterDist;

            // Parse Setting string collection
            rebuildListViewColumnInfo();

        }

        private void FormMain_Shown(object sender, EventArgs e)
        {

            // Create CCToolProperty collection
            buildCCToolPropertyCol();

            this.ToolStripMenuItemSave.Enabled = false;
            this.ToolStripMenuItemClose.Enabled = false;


            this.tabControlCCToolLib.TabPages.Clear();
            this.toolStripStatusLabelMessages.Text = "Ready";


        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Copy window location to app settings
            CCToolLibraryConvert.Properties.Settings.Default.WindowLocation = this.Location;

            // Copy window size to app settings
            if (this.WindowState == FormWindowState.Normal)
            {
                CCToolLibraryConvert.Properties.Settings.Default.WindowSize = this.Size;
            }
            else
            {
                CCToolLibraryConvert.Properties.Settings.Default.WindowSize = this.RestoreBounds.Size;
            }

            CCToolLibraryConvert.Properties.Settings.Default.SplitterDist = this.splitContainerMain.SplitterDistance;


            // Save settings
            CCToolLibraryConvert.Properties.Settings.Default.Save();

        }
        #endregion

        #region ToolStrip events

        #region Files

        private void ToolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            string newKeyName = string.Empty;
            string statusMessage = string.Empty;

            NCToolFile newCCToolFile = new NCToolFile();
            newCCToolFile.VendorSystem = CCToolSession.VendorEnum.CarbideCreate.ToString();
            newCCToolFile.bNewFile = true;

            CCToolSession.newFileCount++;
            newKeyName = "newfile" + CCToolSession.newFileCount.ToString();

            CCToolSession.curToolFiles.Add(newKeyName, newCCToolFile);
            CCToolSession.curToolFile = newCCToolFile;
            newCCToolFile.FullFilePath = newKeyName;

            //            bTabControlRefreshFlag = true;
            this.tabControlCCToolLib.TabPages.Add(newKeyName, "New File (" + CCToolSession.newFileCount + ")");

            // Make the Text Bold
            TabPage newTabPage = this.tabControlCCToolLib.TabPages[newKeyName];
            newTabPage.Text = newKeyName;


            newCCToolFile.FileTabPage = newTabPage;
            newTabPage.Tag = newCCToolFile;

            this.listViewCCToolLibrary.Parent = null;
            newTabPage.Controls.Add(this.listViewCCToolLibrary);
            this.tabControlCCToolLib.SelectedTab = newTabPage;

            if (this.bFirstTabPage == true)
            {
                newTabPage.Font = ccToolTextFontBold;
                this.bFirstTabPage = false;
            }

            this.listViewCCToolLibrary.Dock = DockStyle.Fill;
            updateCCToolLibraryListView(ref newCCToolFile);

            this.ToolStripMenuItemSave.Enabled = true;
            this.ToolStripMenuItemClose.Enabled = true;

            statusMessage = "Created New File (" + CCToolSession.newFileCount + ")";

            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            bool bSuccess = false;
            string errMsgStr = string.Empty;
            string caption = string.Empty;
            string curFullFilePath = string.Empty;
            string pathOnly = string.Empty;
            string message = string.Empty;
            string statusMessage = string.Empty;
            string oldFileName = string.Empty;

            if (CCToolSession.curToolFile.bNewFile)
            {
                oldFileName = CCToolSession.curToolFile.FullFilePath;
                // Open dialog to determine new file name
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = Properties.Settings.Default.LastCsvPath;
                    saveFileDialog.Filter = "CC Tool Library (*.csv)|*.csv";
                    //openFileDialog.FilterIndex = 2;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.Title = "Save Carbide Create Tool Library";
                    saveFileDialog.CreatePrompt = true;
                    saveFileDialog.OverwritePrompt = true;

                    DialogResult dr = saveFileDialog.ShowDialog();
                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                    curFullFilePath = saveFileDialog.FileName;

                }
            }
            else
            {
                curFullFilePath = CCToolSession.curToolFile.FullFilePath;
            }

            pathOnly = Path.GetFullPath(curFullFilePath);

            // On Success a new TabPage and item in curToolFile are added
            bSuccess = ToolFileHandling.SaveCCToolFile(curFullFilePath, ref CCToolSession.curToolFile, out errMsgStr);
            if (bSuccess == false)
            {
                caption = "Errors Saving Tool file";
                var result3 = MessageBox.Show(errMsgStr, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Warning);
                statusMessage = "Fail to save " + curFullFilePath;
            }
            else
            {
                CCToolSession.curToolFile.FullFilePath = curFullFilePath;

                if (CCToolSession.curToolFile.bNewFile == true)
                {
                    // Remove the new file from the Session dictionary and the Tab
                    CCToolSession.curToolFiles.Add(curFullFilePath, CCToolSession.curToolFile);
                    CCToolSession.curToolFiles.Remove(oldFileName);

                    // Remove ListView from Tabpage
                    this.listViewCCToolLibrary.Parent = null;


                    this.tabControlCCToolLib.TabPages.Add(curFullFilePath);
                    TabPage newTabPage = tabControlCCToolLib.TabPages[tabControlCCToolLib.TabPages.Count - 1];
                    CCToolSession.curToolFile.FileTabPage = newTabPage;
                    newTabPage.Tag = CCToolSession.curToolFile;

                    tabControlCCToolLib.TabPages.RemoveByKey(oldFileName);
                    tabControlCCToolLib.SelectedTab = newTabPage;

                    listViewCCToolLibrary.Parent = newTabPage;
                    listViewCCToolLibrary.Dock = DockStyle.Fill;

                    CCToolSession.curToolFile.bNewFile = false;

                }

                CCToolSession.curToolFile.FileTabPage.Text = CCToolSession.curToolFile.FileName;

                CCToolSession.curToolFile.bAnyChanges = false;

                // Save Path for future use
                Properties.Settings.Default.LastCsvPath = pathOnly;
                statusMessage = "Saved " + curFullFilePath;
            }

            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);


        }
        private void ToolStripMenuItemSaveAllChgs_Click(object sender, EventArgs e)
        {
            string statusMessage = string.Empty;

            List<NCToolFile> toolSuccessFile = new List<NCToolFile>();
            List<NCToolFile> toolErrorFile = new List<NCToolFile>();

            updateTabPagesOnSave(out toolSuccessFile, out toolErrorFile);
            statusMessage = "Saved " + toolSuccessFile.Count + " files Successfully and " + toolErrorFile.Count + " with Errors";

            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }

        private void ToolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            bool bChangesFlag = false;
            int changeCount = 0;
            int allCount = 0;
            string errMsgStr = string.Empty;
            string message = string.Empty;
            string caption = string.Empty;
            string statusMessage = string.Empty;
            List<NCToolFile> toolSuccessFile = new List<NCToolFile>();
            List<NCToolFile> toolErrorFile = new List<NCToolFile>();

            allCount = CCToolSession.curToolFiles.Count;

            bChangesFlag = CCToolSession.CheckChangesCurToolFiles(out changeCount);
            if (bChangesFlag)
            {
                // Offer options, ignore or saveall
                // Try to save file, then remove from current files

                caption = changeCount + " files have changes";
                message = "Ignore changed file(s)";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Information);
                if (result == DialogResult.No)
                {
                    caption = changeCount + " files have changes";
                    message = "OK to Saving changed file(s) or Cancel";
                    var result2 = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OKCancel,
                                                 MessageBoxIcon.Warning);
                    if (result2 == DialogResult.OK)
                    {
                        updateTabPagesOnSave(out toolSuccessFile, out toolErrorFile);
                        statusMessage = "Saved " + toolSuccessFile.Count + " files Successfully and " + toolErrorFile.Count + " with Errors";
                    }

                }

            }
            else
            {
                statusMessage = "Closed " + allCount + " file(s)";
            }

            CCToolSession.CloseAllCurToolFiles();
            clearAllCCToolFileGUI();
            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            bool bChangesFlag = false;
            int changeCount = 0;
            string errMsgStr = string.Empty;
            string message = string.Empty;
            string caption = string.Empty;
            string statusMessage = string.Empty;
            List<NCToolFile> toolSuccessFile = new List<NCToolFile>();
            List<NCToolFile> toolErrorFile = new List<NCToolFile>();

            bChangesFlag = CCToolSession.CheckChangesCurToolFiles(out changeCount);
            if (bChangesFlag)
            {
                // Offer options, ignore or saveall
                // Try to save file, then remove from current files

                caption = changeCount + " files have changes";
                message = "Ignore changed file(s) and Exit";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Information);
                if (result == DialogResult.No)
                {
                    caption = changeCount + " files have changes";
                    message = "OK to Saving changed file(s) or Cancel Exit";
                    var result2 = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OKCancel,
                                                 MessageBoxIcon.Warning);
                    if (result2 == DialogResult.OK)
                    {
                        updateTabPagesOnSave(out toolSuccessFile, out toolErrorFile);
                        statusMessage = string.Empty;
                        this.toolStripStatusLabelMessages.Text = "Saved " + toolSuccessFile.Count + " files Successfully and " + toolErrorFile.Count + " with Errors";
                    }

                }

            }
            CCToolSession.CloseAllCurToolFiles();
            clearAllCCToolFileGUI();

            this.Close();
        }

        #endregion

        #region ColumnSettings

        private void clearColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string statusMessage = string.Empty;
            CCToolLibraryConvert.Properties.Settings.Default.ColumnLayout.Clear();
            CCToolLibraryConvert.Properties.Settings.Default.Save();

            rebuildListViewColumnInfo();


            statusMessage = "Cleared Column Settings";
            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);
        }

        private void saveColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bSuccess = false;
            string statusMessage = string.Empty;

            bSuccess = saveUserColumnSettings(out statusMessage);

            // Build user defined column order and width map
            rebuildListViewColumnInfo();

            statusMessage = "Saved Column Settings";
            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }

        #endregion


        #region F360 File access

        private void OpenF360ToolLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bSuccess = false;
            bool bAnySuccess = false;
            TabPage curTabPage = new TabPage();
            string statusMessage = string.Empty;
            int successCount = 0;
            int errorCount = 0;
            JToken nextF360Token = null;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Properties.Settings.Default.LastCsvPath;
                openFileDialog.Filter = "Fusion Tool Library (*.json)|*.json";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "Import Fusion Tool Library(s)";

                DialogResult dr = openFileDialog.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    // Read the files
                    foreach (String curFullFilePath in openFileDialog.FileNames)
                    {
                        string onlyPath = Path.GetDirectoryName(curFullFilePath);
                        string onlyFileName = Path.GetFileName(curFullFilePath);

                        string caption = string.Empty;
                        string message = string.Empty;

                        // Check Dictionary for existing entry
                        if (CCToolSession.curToolFiles.TryGetValue(curFullFilePath, out NCToolFile chkToolFile))
                        {
                            // If so, option to Replace or Skip
                            caption = curFullFilePath + " already open";
                            message = "Select Yes to Reload file";
                            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.No)
                            {
                                // Skip this file
                                errorCount++;
                                continue;
                            }
                            else
                            {
                                // Remove this file from the Session dictionary and the Tab
                                CCToolSession.curToolFiles.Remove(curFullFilePath);
                                tabControlCCToolLib.TabPages.RemoveByKey(curFullFilePath);
                            }

                        }



                        // Create a Tool dictionary.
                        try
                        {
                            string errMsgStr = string.Empty;
                            _360LibraryConverter.F360ToolLibrary nextF360ToolLibrary = null;
                            nextF360Token = null;

                            bSuccess = ToolFileHandling.JsonInputFiles(curFullFilePath, out nextF360ToolLibrary, out nextF360Token);
                            if (bSuccess == true)
                            {
                                // Add F360 library to wrapper
                                NCToolFile nextCCToolFile = new NCToolFile();

                                nextCCToolFile.FullFilePath = curFullFilePath;
                                nextCCToolFile.F360Tools = nextF360ToolLibrary;

                                // Use the CC File name for Material and Machine
                                tabControlCCToolLib.TabPages.Add(curFullFilePath, nextCCToolFile.FileName);

                                // Select the new tabpage
                                curTabPage = tabControlCCToolLib.TabPages[tabControlCCToolLib.TabPages.Count - 1];
                                // use full path in name for key search
                                //curTabPage.Text = nextCCToolFile.FileName;
                                //curTabPage.Font = this.ccToolTextFontBold;

                                // Double point the File and Tabpage
                                curTabPage.Tag = nextCCToolFile;
                                nextCCToolFile.FileTabPage = curTabPage;

                                // use the file path as the document key
                                CCToolSession.curToolFiles.Add(nextCCToolFile.FullFilePath, nextCCToolFile);
                                CCToolSession.curToolFile = nextCCToolFile;
                                bAnySuccess = true;
                                successCount++;

                                // Save Path for future use
                                Properties.Settings.Default.LastJsonPath = onlyPath;


                            }
                            else
                            {
                                // Clean up
                                caption = curFullFilePath + " error";
                                message = errMsgStr + "\n\nSelect Ok to Cancel";
                                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                if (result == DialogResult.OK)
                                {
                                    // Skip this file
                                    errorCount++;
                                    continue;
                                }

                            }
                        }
                        catch (SecurityException ex)
                        {
                            // The user lacks appropriate permissions to read files, discover paths, etc.
                            MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                                "Error message: " + ex.Message + "\n\n" +
                                "Details (send to Support):\n\n" + ex.StackTrace

                            );
                            errorCount++;

                        }
                        catch (Exception ex)
                        {
                            // Could not load the image - probably related to Windows file system permissions.
                            MessageBox.Show("Cannot open JSON file " + curFullFilePath.Substring(curFullFilePath.LastIndexOf('\\'))
                                + ". You may not have permission to read the file, or " +
                                "it may be corrupt.\n\nReported error: " + ex.Message);
                            errorCount++;

                        }


                    }
                    // Update GUI
                    if (bAnySuccess == true)
                    {
                        this.tabControlCCToolLib.SelectTab(CCToolSession.curToolFile.FileTabPage);

                        // Remove possible ListView
                        // Remove possible TreeView
                        this.listViewCCToolLibrary.Parent = null;
                        this.treeViewF360Json.Parent = null;

                        CCToolSession.VendorEnum newVendor = CCToolSession.VendorEnum.F360;

                        switch (newVendor)
                        {
                            case CCToolSession.VendorEnum.CarbideCreate:
                                {
                                    curTabPage.Controls.Add(this.listViewCCToolLibrary);
                                    this.listViewCCToolLibrary.Dock = DockStyle.Fill;
                                    updateCCToolLibraryListView(ref CCToolSession.curToolFile);
                                    break;
                                }
                            case CCToolSession.VendorEnum.F360:
                                {
                                    curTabPage.Controls.Add(this.treeViewF360Json);
                                    this.treeViewF360Json.Dock = DockStyle.Fill;
                                    updateF360JsonTreeView(nextF360Token, CCToolSession.curToolFile.FileName, ref this.treeViewF360Json);
//                                    updateCCToolLibraryListView(ref CCToolSession.curToolFile);
                                    break;
                                }
                        }

                        if (this.bFirstTabPage == true)
                        {
                            CCToolSession.curToolFile.FileTabPage.Font = ccToolTextFontBold;
                            this.bFirstTabPage = false;
                        }


                        // Build GUI data
                        this.ToolStripMenuItemSave.Enabled = true;
                        this.ToolStripMenuItemClose.Enabled = true;

                    }


                }




            }
            statusMessage = "Successfully opened " + successCount + " file(s) and Errors with " + errorCount + " file(s)";
            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }
        private void Savef360ToolLibJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tempStr = null;

            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "d:\\";
                saveFileDialog.Filter = "F360 Tool Library (*.json)|*.json";
                //openFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Export F360 Tool Library(s)";

                DialogResult dr = saveFileDialog.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {

                    // Create a Tool Library file.
                    try
                    {
                        _360LibraryConverter.F360ToolLibrary f360ToolLibrary = CCToolSession.curF360File;
                        tempStr = saveFileDialog.FileName;
                        bool successFlag = ToolFileHandling.JsonOutputFiles(f360ToolLibrary, tempStr);
                        if (successFlag)
                        {
                            // Report statistics
                            // Build GUI data
                        }
                        else
                        {
                            // Clean up

                        }
                    }
                    catch (SecurityException ex)
                    {
                        // The user lacks appropriate permissions to read files, discover paths, etc.
                        MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                            "Error message: " + ex.Message + "\n\n" +
                            "Details (send to Support):\n\n" + ex.StackTrace
                        );
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show("Cannot Save JSON file " + tempStr.Substring(tempStr.LastIndexOf('\\'))
                            + ". You may not have permission to write the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }

            }
        }

        #endregion

        #region CCTool File access
        private void OpenCCToolLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open selected file(s)

            bool bSuccess = false;
            bool bAnySuccess = false;
            TabPage curTabPage = new TabPage();
            string statusMessage = string.Empty;
            int successCount = 0;
            int errorCount = 0;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Properties.Settings.Default.LastCsvPath;
                openFileDialog.Filter = "CC Tool Library (*.csv)|*.csv";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "Import Carbide Create Tool Library(s)";

                DialogResult dr = openFileDialog.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    // Read the files
                    foreach (String curFullFilePath in openFileDialog.FileNames)
                    {
                        string onlyPath = Path.GetDirectoryName(curFullFilePath);
                        string onlyFileName = Path.GetFileName(curFullFilePath);

                        string caption = string.Empty;
                        string message = string.Empty;

                        // Check Dictionary for existing entry
                        if (CCToolSession.curToolFiles.TryGetValue(curFullFilePath, out NCToolFile chkToolFile))
                        {
                            // If so, option to Replace or Skip
                            caption = curFullFilePath + " already open";
                            message = "Select Yes to Reload file";
                            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.No)
                            {
                                // Skip this file
                                errorCount++;
                                continue;
                            }
                            else
                            {
                                // Remove this file from the Session dictionary and the Tab
                                CCToolSession.curToolFiles.Remove(curFullFilePath);
                                tabControlCCToolLib.TabPages.RemoveByKey(curFullFilePath);
                            }

                        }


                        // Save Path for future use
                        Properties.Settings.Default.LastCsvPath = onlyPath;

                        // Create a Tool dictionary.
                        try
                        {
                            NCToolFile nextCCToolFile = new NCToolFile();
                            nextCCToolFile.VendorSystem = CCToolSession.VendorEnum.CarbideCreate.ToString();
                            string errMsgStr = string.Empty;

                            bSuccess = ToolFileHandling.ReadCCToolCSVFile(curFullFilePath, out nextCCToolFile, out errMsgStr);
                            if (bSuccess == true)
                            {
                                // Use the CC File name for Material and Machine
                                tabControlCCToolLib.TabPages.Add(curFullFilePath, nextCCToolFile.FileName);

                                // Select the new tabpage
                                curTabPage = tabControlCCToolLib.TabPages[tabControlCCToolLib.TabPages.Count - 1];
                                // use full path in name for key search
                                //curTabPage.Text = nextCCToolFile.FileName;
                                //curTabPage.Font = this.ccToolTextFontBold;

                                // Double point the File and Tabpage
                                curTabPage.Tag = nextCCToolFile;
                                nextCCToolFile.FileTabPage = curTabPage;

                                // use the file path as the document key
                                nextCCToolFile.FullFilePath = curFullFilePath;
                                CCToolSession.curToolFiles.Add(nextCCToolFile.FullFilePath, nextCCToolFile);
                                CCToolSession.curToolFile = nextCCToolFile;
                                bAnySuccess = true;
                                successCount++;


                            }
                            else
                            {
                                // Clean up
                                caption = curFullFilePath + " error";
                                message = errMsgStr + "\n\nSelect Ok to Cancel";
                                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                if (result == DialogResult.OK)
                                {
                                    // Skip this file
                                    errorCount++;
                                    continue;
                                }

                            }
                        }
                        catch (SecurityException ex)
                        {
                            // The user lacks appropriate permissions to read files, discover paths, etc.
                            MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                                "Error message: " + ex.Message + "\n\n" +
                                "Details (send to Support):\n\n" + ex.StackTrace

                            );
                            errorCount++;

                        }
                        catch (Exception ex)
                        {
                            // Could not load the image - probably related to Windows file system permissions.
                            MessageBox.Show("Cannot open CSV file " + curFullFilePath.Substring(curFullFilePath.LastIndexOf('\\'))
                                + ". You may not have permission to read the file, or " +
                                "it may be corrupt.\n\nReported error: " + ex.Message);
                            errorCount++;

                        }


                    }
                    // Update GUI
                    if (bAnySuccess == true)
                    {
                        this.tabControlCCToolLib.SelectTab(CCToolSession.curToolFile.FileTabPage);

                        this.listViewCCToolLibrary.Parent = null;
                        curTabPage.Controls.Add(this.listViewCCToolLibrary);
                        if (this.bFirstTabPage == true)
                        {
                            CCToolSession.curToolFile.FileTabPage.Font = ccToolTextFontBold;
                            this.bFirstTabPage = false;
                        }


                        this.listViewCCToolLibrary.Dock = DockStyle.Fill;
                        updateCCToolLibraryListView(ref CCToolSession.curToolFile);


                        // Build GUI data
                        this.ToolStripMenuItemSave.Enabled = true;
                        this.ToolStripMenuItemClose.Enabled = true;

                    }


                }




            }
            statusMessage = "Successfully opened " + successCount + " file(s) and Errors with " + errorCount + " file(s)";
            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);
        }

        #endregion

        #endregion

        #region ListView events

        private void listViewCCToolLibrary_ItemSelectionChanged_1(object sender, ListViewItemSelectionChangedEventArgs e)

        {

            string statusMessage = string.Empty;
            int selCount = listViewCCToolLibrary.SelectedItems.Count;

            if (selCount > 0)
            {


                // Populate the propertygrid objects to generate proper names/value pairs
                // The PropertyGrid will find common values

                CCToolCSV[] selCCToolCSV = new CCToolCSV[selCount];
                for (int i = 0; i < selCount; i++)
                {
                    selCCToolCSV[i] = (CCToolCSV)listViewCCToolLibrary.SelectedItems[i].Tag;

                }

                this.propertyGridCCToolLib.SelectedObjects = selCCToolCSV;

                // If the original objects are tied to the propertygrid you can't undo easily

                // Use a single dummy object with name/value pair to hold the values instead of the original instances 
                // The propertygrid already determined common parameters

                rebuildPropertiesTemplate();

                // Reset PropertyGrid object to this dummy
                propertyGridCCToolLib.SelectedObject = CCToolSession.templateCCTool;
                statusMessage = "Changed Selection";
            }
            else
            {
                propertyGridCCToolLib.SelectedObject = null;
                propertyGridCCToolLib.SelectedObjects = null;
                statusMessage = "Cleared Selection";
            }

            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }

        private void listViewCCToolLibrary_ColumnClick_1(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorterTransactions.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorterTransactions.Order == SortOrder.Ascending)
                {
                    lvwColumnSorterTransactions.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorterTransactions.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorterTransactions.SortColumn = e.Column;
                lvwColumnSorterTransactions.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listViewCCToolLibrary.Sort();

            string statusMessage = "Sorted Column";
            updateStatusStrip(listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }
        #endregion

        #region ListView context menu
        private void AddRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string statusMessage = string.Empty;
            CCToolSession.curToolFile.ToolIndex++;

            CCToolCSV newCCTool = new CCToolCSV();
            newCCTool.ToolIndex = CCToolSession.curToolFile.ToolIndex.ToString();

            // Add to curToolFile
            CCToolSession.curToolFile.CCTools.Add(newCCTool.ToolIndex, newCCTool);

            updateCCToolLibraryListView(ref CCToolSession.curToolFile);

            CCToolSession.curToolFile.bAnyChanges = true;

            // Update TabPage text
            this.tabControlCCToolLib.SelectedTab.Text = "* " + CCToolSession.curToolFile.FileName;

            statusMessage = "Added Row";
            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }

        private void CopyRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string statusMessage = string.Empty;
            int selCount = listViewCCToolLibrary.SelectedItems.Count;
            if (selCount > 0)
            {

                // Collect the ListItems and the associate CCToolCSV instances BEFORE
                ListViewItem[] selItems = new ListViewItem[selCount];
                CCToolCSV[] selCCToolCSV = new CCToolCSV[selCount];

                // Get the Tool from the ListView tag
                for (int i = 0; i < selCount; i++)
                {
                    selItems[i] = listViewCCToolLibrary.SelectedItems[i];
                    selCCToolCSV[i] = (CCToolCSV)selItems[i].Tag;

                }

                // Clear clipboard
                CCToolSession.clipBoardToolFile.CCTools.Clear();

                // Copy CCToolCSV instances from parent collection
                for (int i = 0; i < selCount; i++)
                {
                    // Create a shallow copy of instance and add copy to the clipboard
                    CCToolCSV newCCToolCSV = (CCToolCSV)selCCToolCSV[i].ShallowCopy();
                    CCToolSession.clipBoardToolFile.ToolIndex++;
                    CCToolSession.clipBoardToolFile.CCTools.Add(CCToolSession.clipBoardToolFile.ToolIndex.ToString(), newCCToolCSV);

                }

                statusMessage = "Copied " + selCount + " rows";
            }
            else
            {
                statusMessage = "Nothing Selected, No Action Taken";
            }

            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);


        }

        private void CutRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string statusMessage = string.Empty;
            int selCount = listViewCCToolLibrary.SelectedItems.Count;

            if (selCount > 0)
            {

                // Collect the ListItems and the associate CCToolCSV instances BEFORE
                ListViewItem[] selItems = new ListViewItem[selCount];
                CCToolCSV[] selCCToolCSV = new CCToolCSV[selCount];

                listViewCCToolLibrary.BeginUpdate();

                // Get the Tool from the ListView tag
                for (int i = 0; i < selCount; i++)
                {
                    selItems[i] = listViewCCToolLibrary.SelectedItems[i];
                    selCCToolCSV[i] = (CCToolCSV)selItems[i].Tag;

                }

                // Clear clipboard
                CCToolSession.clipBoardToolFile.CCTools.Clear();

                // Copy CCToolCSV instances from parent collection
                for (int i = 0; i < selCount; i++)
                {
                    // Create a shallow copy of instance and add copy to the clipboard
                    CCToolCSV newCCToolCSV = (CCToolCSV)selCCToolCSV[i].ShallowCopy();
                    CCToolSession.clipBoardToolFile.ToolIndex++;

                    CCToolSession.clipBoardToolFile.CCTools.Add(CCToolSession.clipBoardToolFile.ToolIndex.ToString(), newCCToolCSV);

                    // Remove the instance from the Session current file
                    CCToolSession.curToolFile.CCTools.Remove(selCCToolCSV[i].ToolIndex);
                }

                updateCCToolLibraryListView(ref CCToolSession.curToolFile);
                listViewCCToolLibrary.EndUpdate();

                propertyGridCCToolLib.SelectedObject = null;
                propertyGridCCToolLib.SelectedObjects = null;

                CCToolSession.curToolFile.bAnyChanges = true;
                // Update TabPage text
                this.tabControlCCToolLib.SelectedTab.Text = "* " + CCToolSession.curToolFile.FileName;

                statusMessage = "Cut " + selCount + " rows";
            }
            else
            {
                statusMessage = "Nothing Selected, No Action Taken";
            }


            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);


        }

        private void PasteRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string statusMessage = string.Empty;
            int clipCount = CCToolSession.clipBoardToolFile.CCTools.Count;

            if (clipCount > 0)
            {
                // Determine what row you are going to insert new records
                this.listViewCCToolLibrary.BeginUpdate();
                foreach (CCToolCSV copyCCToolCSV in CCToolSession.clipBoardToolFile.CCTools.Values)
                {
                    // Update next Index
                    CCToolSession.curToolFile.ToolIndex++;
                    CCToolSession.curToolFile.CCTools.Add(CCToolSession.curToolFile.ToolIndex.ToString(), copyCCToolCSV);
                }
                // Update ListView
                updateCCToolLibraryListView(ref CCToolSession.curToolFile);
                this.listViewCCToolLibrary.EndUpdate();

                statusMessage = "Pasted " + clipCount + " Rows";
                CCToolSession.curToolFile.bAnyChanges = true;
                // Update TabPage text
                this.tabControlCCToolLib.SelectedTab.Text = "* " + CCToolSession.curToolFile.FileName;

            }
            else
            {
                statusMessage = "Clipboard is empty, NO action taken";

            }
            propertyGridCCToolLib.SelectedObject = null;
            propertyGridCCToolLib.SelectedObjects = null;

            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);
        }

        private void DeleteRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string statusMessage = string.Empty;
            int selCount = listViewCCToolLibrary.SelectedItems.Count;
            if (selCount > 0)
            {

                // Collect the ListItems and the associate CCToolCSV instances BEFORE
                ListViewItem[] selItems = new ListViewItem[selCount];
                CCToolCSV[] selCCToolCSV = new CCToolCSV[selCount];

                listViewCCToolLibrary.BeginUpdate();

                // Get the Tool from the ListView tag
                for (int i = 0; i < selCount; i++)
                {
                    selItems[i] = listViewCCToolLibrary.SelectedItems[i];
                    selCCToolCSV[i] = (CCToolCSV)selItems[i].Tag;
                }

                // Remove instances from parent collections
                for (int i = 0; i < selCount; i++)
                {
                    // Remove the instance from the Session current file
                    CCToolSession.curToolFile.CCTools.Remove(selCCToolCSV[i].ToolIndex);
                }
                updateCCToolLibraryListView(ref CCToolSession.curToolFile);
                listViewCCToolLibrary.EndUpdate();

                propertyGridCCToolLib.SelectedObject = null;
                propertyGridCCToolLib.SelectedObjects = null;

                CCToolSession.curToolFile.bAnyChanges = true;
                // Update TabPage text
                this.tabControlCCToolLib.SelectedTab.Text = "* " + CCToolSession.curToolFile.FileName;

                statusMessage = "Deleted " + selCount + " rows";
            }
            else
            {
                statusMessage = "Nothing Selected, No Action Taken";
            }
            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);


        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string statusMessage = string.Empty;

            // Update the ListView
            listViewCCToolLibrary.BeginUpdate();
            foreach (ListViewItem item in listViewCCToolLibrary.Items)
            {
                item.Selected = true;

            }
            int selCount = listViewCCToolLibrary.SelectedItems.Count;
            listViewCCToolLibrary.EndUpdate();

            // Populate the propertygrid objects to generate proper names/value pairs
            // The PropertyGrid will find common values

            CCToolCSV[] selCCToolCSV = new CCToolCSV[selCount];
            for (int i = 0; i < selCount; i++)
            {
                selCCToolCSV[i] = (CCToolCSV)listViewCCToolLibrary.SelectedItems[i].Tag;
            }

            this.propertyGridCCToolLib.SelectedObjects = selCCToolCSV;

            // If the original objects are tied to the propertygrid you can't undo easily

            // Use a single dummy object with name/value pair to hold the values instead of the original instances 
            // The propertygrid already determined common parameters

            rebuildPropertiesTemplate();

            // Reset PropertyGrid object to this dummy
            propertyGridCCToolLib.SelectedObject = CCToolSession.templateCCTool;

            statusMessage = "Selected all Rows";
            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }

        private void unSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string statusMessage = string.Empty;

            listViewCCToolLibrary.SelectedItems.Clear();
            propertyGridCCToolLib.SelectedObject = null;
            propertyGridCCToolLib.SelectedObjects = null;

            statusMessage = "Cleared Selection";
            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }

        #endregion

        #region PropertyGrid events

        private void propertyGridCCToolLib_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.buttonApplyPropChanges.Enabled = true;
            this.buttonCancelPropChanges.Enabled = true;

            string displayName = null;
            object propValue = null;
            object oldValue = null;

            displayName = e.ChangedItem.Label;
            propValue = e.ChangedItem.Value;
            oldValue = e.OldValue;



            //            jtc add value validation here
            // use typeconvertor for validation

            // Collect DisplayName value pair to be used in Apply
            PropertyPairChange chkPropPair = null;
            if (CCToolSession.updatedCCToolCsvByDisplayNameCol.TryGetValue(displayName, out chkPropPair) == true)
            {
                // This will update a repeated change
                chkPropPair.PropValue = propValue;
                chkPropPair.ChangeFlag = true;
            }
            else
            {
                // This is the first time this property was changed
                chkPropPair = new PropertyPairChange();
                chkPropPair.DisplayName = displayName;
                chkPropPair.PropName = chkPropPair.PropertyInfoInst.Name;
                chkPropPair.PropValue = propValue;
                chkPropPair.ChangeFlag = true;
                CCToolSession.updatedCCToolCsvByPropertyCol.Add(chkPropPair.PropName, chkPropPair);
                CCToolSession.updatedCCToolCsvByDisplayNameCol.Add(chkPropPair.DisplayName, chkPropPair);
            }

        }

        #endregion

        #region Buttons events used with PropertyGrid
        private void buttonApplyPropChanges_Click(object sender, EventArgs e)
        {
            // Cycle through all selected transactions and apply new changed values
            bool bAnyChangesFlag = false;

            int selCount = listViewCCToolLibrary.SelectedItems.Count;
            if (selCount == 1)
            {
                // Get the single ListItem 
                ListViewItem selItem = listViewCCToolLibrary.SelectedItems[0];

                // Get the Tool instance from ListView 
                CCToolCSV selCCTool = (CCToolCSV)selItem.Tag;

                // Loop the the collection of changed name/value pairs
                foreach (PropertyPairChange chkPropPair in CCToolSession.updatedCCToolCsvByPropertyCol.Values)
                {
                    // Find only changed properties
                    if (chkPropPair.ChangeFlag == true)
                    {
                        ExportPropertyHelper.SetValueFromString(selCCTool, chkPropPair.PropName, chkPropPair.PropValue);
                        bAnyChangesFlag = true;
                    }
                }

            }
            else if (selCount > 1)
            {

                foreach (ListViewItem selItem in listViewCCToolLibrary.SelectedItems)
                {
                    // Items maybe null because of Clear() call.
                    if (selItem != null)
                    {
                        // Get Tool instance from ListView item tag
                        CCToolCSV selCCTool = (CCToolCSV)selItem.Tag;
                        // Loop through properties 
                        foreach (PropertyPairChange chkPropPair in CCToolSession.updatedCCToolCsvByPropertyCol.Values)
                        {
                            // Find only changed properties
                            if (chkPropPair.ChangeFlag == true)
                            {
                                ExportPropertyHelper.SetValueFromString(selCCTool, chkPropPair.PropName, chkPropPair.PropValue);
                                bAnyChangesFlag = true;
                            }
                        }
                    }
                }


            }
            if (bAnyChangesFlag == true)
            {
                // Flag the file as changed
                CCToolSession.curToolFile.bAnyChanges = true;

                // Update TabPage text
                this.tabControlCCToolLib.SelectedTab.Text = "* " + CCToolSession.curToolFile.FileName;
                // Rebuild Listview
                updateCCToolLibraryListView(ref CCToolSession.curToolFile);
            }

        }

        private void buttonCancelPropChanges_Click(object sender, EventArgs e)
        {
            // Clean up
            propertyCCToolCleanUp();
            // Give focus back to the ListView
            //this.listViewCCToolLibrary.Refresh();
            this.listViewCCToolLibrary.Focus();

        }
        #endregion


        #region TabControl

        private void tabControlCCToolLib_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = this.tabControlCCToolLib.TabPages[e.Index];
            var tabRect = this.tabControlCCToolLib.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);

            if (CCToolSession.curToolFiles.Count + CCToolSession.curF360Files.Count > 0)
            {
                //  Only add to Tabs with files
                var closeImage = Properties.Resources.DeleteButton_Image;
                e.Graphics.DrawImage(closeImage,
                    (tabRect.Right - closeImage.Width),
                    tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                    tabRect, tabPage.ForeColor, TextFormatFlags.Left);

            }
        }
        private void tabControlCCToolLib_MouseDown(object sender, MouseEventArgs e)
        {
            // Selecting the delete image
            string statusMessage = string.Empty;

            for (var i = 0; i < this.tabControlCCToolLib.TabPages.Count; i++)
            {
                var tabRect = this.tabControlCCToolLib.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                var closeImage = Properties.Resources.DeleteButton_Image;
                var imageRect = new Rectangle(
                    (tabRect.Right - closeImage.Width),
                    tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                    closeImage.Width,
                    closeImage.Height);
                if (imageRect.Contains(e.Location))
                {
                    // Remove selected files
                    bool bRemoveToolFile = false;
                    bool bSuccess = false;
                    string errMsgStr = string.Empty;

                    TabPage curTabPage = this.tabControlCCToolLib.TabPages[i];
                    NCToolFile curToolFile = curTabPage.Tag as NCToolFile;

                    if (curToolFile.bAnyChanges == true)
                    {

                        errMsgStr = string.Empty;
                        string message =
                            "Do you want to save before closing ?";
                        string caption = curToolFile.FullFilePath + " has changes";
                        var result = MessageBox.Show(message, caption,
                                                     MessageBoxButtons.YesNoCancel,
                                                     MessageBoxIcon.Warning);


                        if (result == DialogResult.Yes)
                        {
                            // Try to save file, then remove from current files
                            bSuccess = ToolFileHandling.SaveCCToolFile(curToolFile.FullFilePath, ref CCToolSession.curToolFile, out errMsgStr);

                        }
                        if (result == DialogResult.No)
                        {
                            // Report success
                            bSuccess = true;
                        }

                        if (bSuccess == true)
                        {
                            bRemoveToolFile = true;
                        }
                        else
                        {
                            caption = "Error overwriting Tool file";
                            var result3 = MessageBox.Show(errMsgStr, caption,
                                                         MessageBoxButtons.OK,
                                                         MessageBoxIcon.Warning);
                            statusMessage = caption + " " + curToolFile.FullFilePath;
                        }
                    }
                    else
                    {
                        bRemoveToolFile = true;
                        statusMessage = "Removed file " + curToolFile.FullFilePath;
                    }

                    if (bRemoveToolFile == true)
                    {
                        CCToolSession.curToolFiles.Remove(curToolFile.FullFilePath);
                        // Conotrol what TabPage will become current
                        if (CCToolSession.curToolFiles.Count > 0)
                        {
                            // Set a processing flag to the TabSelect doesn't go crazy
                            this.tabControlCCToolLib.TabPages.RemoveByKey(curToolFile.FullFilePath);
                            if (i == 0)
                            {
                                this.tabControlCCToolLib.SelectedIndex = i;

                            }
                            else
                            {
                                this.tabControlCCToolLib.SelectedIndex = i - 1;

                            }
                            // Get next Tab to select
                            curTabPage = this.tabControlCCToolLib.SelectedTab;

                            // This is to change the Current Tool file in the Session
                            curToolFile = curTabPage.Tag as NCToolFile;

                            // Move ListView to next TabPage
                            this.listViewCCToolLibrary.Parent = null;
                            curTabPage.Controls.Add(this.listViewCCToolLibrary);
                            this.listViewCCToolLibrary.Dock = DockStyle.Fill;

                            // Update ListView and PropertyGrid
                            updateCCToolLibraryListView(ref CCToolSession.curToolFile);
                        }
                        else
                        {

                            // No Tool files
                            clearAllCCToolFileGUI();
                            curToolFile = new NCToolFile();
                            statusMessage = statusMessage + "  **** No Tool files in Session ****";

                        }

                        CCToolSession.curToolFile = curToolFile;
                    }
                    propertyGridCCToolLib.SelectedObject = null;
                    propertyGridCCToolLib.SelectedObjects = null;

                    updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);
                    break;
                }
            }
        }

        private void tabControlCCToolLib_Selected(object sender, TabControlEventArgs e)
        {
            string statusMessage = string.Empty;

            if (CCToolSession.curToolFiles.Count > 0)
            {
                CCToolSession.curToolFile = (NCToolFile)e.TabPage.Tag;

                this.listViewCCToolLibrary.Parent = null;
                e.TabPage.Controls.Add(this.listViewCCToolLibrary);

                this.listViewCCToolLibrary.Dock = DockStyle.Fill;
                updateCCToolLibraryListView(ref CCToolSession.curToolFile);

                statusMessage = "Changed to file " + CCToolSession.curToolFile.FullFilePath;

                e.TabPage.Font = this.ccToolTextFontBold;
                tabControlCCToolLib.Refresh();
            }
            else
            {
                statusMessage = "No Tool files in Session";
            }

            propertyGridCCToolLib.SelectedObject = null;
            propertyGridCCToolLib.SelectedObjects = null;

            updateStatusStrip(this.listViewCCToolLibrary.SelectedItems.Count, statusMessage);

        }

        private void tabControlCCToolLib_Deselected(object sender, TabControlEventArgs e)
        {

            if (e.TabPage != null)
            {
                if (CCToolSession.curToolFiles.Count > 0)
                {
                    e.TabPage.Font = this.ccToolTextFontRegular;
                    tabControlCCToolLib.Refresh();
                }
            }
        }
        #endregion

        #region TreeView
        private void treeViewF360Json_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Determine by checking the Text property.  
            MessageBox.Show(e.Node.Text);
        }
        #endregion
    }
}