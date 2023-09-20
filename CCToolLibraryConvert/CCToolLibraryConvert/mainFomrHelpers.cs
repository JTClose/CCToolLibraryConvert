using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CCToolLibraryConvert
{
    public partial class mainForm
    {

        #region Across controls
        private void clearAllCCToolFileGUI()
        {
            this.tabControlCCToolLib.TabPages.Clear();

            this.listViewCCToolLibrary.Clear();

            this.propertyGridCCToolLib.SelectedObject = new Object();
            this.propertyGridCCToolLib.Update();

            this.ToolStripMenuItemSave.Enabled = false;
            this.ToolStripMenuItemClose.Enabled = false;

            this.buttonApplyPropChanges.Enabled = false;
            this.buttonCancelPropChanges.Enabled = false;

            this.bFirstTabPage = true;
        }

        private void rebuildPropertiesTemplate()
        {
            // ReCreate a dummy object and fill out all common values found by PropertyGrid
            CCToolSession.templateCCTool = new CCToolCSV();
            // Create a collection of properties based on the dummy
            Dictionary<string, PropertyInfo> editProps = new Dictionary<string, PropertyInfo>();

            editProps = ExportPropertyHelper.getAllInstancePropertyDisplayName(ref CCToolSession.templateCCTool);

            // Search the PropertyGrid
            GridItemCollection propGridItems = null;
            propGridItems = GetAllGridEntries(propertyGridCCToolLib);
            if (propGridItems != null)
            {
                IEnumerator propGridEnum = propGridItems.GetEnumerator();
                int j = 0;
                while (propGridEnum.MoveNext())
                {
                    GridItem chkGridItem = (GridItem)propGridEnum.Current;
                    // Skip over Catagory items
                    if (chkGridItem.GridItemType != GridItemType.Category)
                    {
                        string chkGridPropName = chkGridItem.Label;         // This was defined from the property attribute DisplayName
                        object chkGridPropValue = chkGridItem.Value;


                        if (chkGridPropValue != null)
                        {
                            PropertyInfo editInstProp = null;
                            if (editProps.TryGetValue(chkGridPropName, out editInstProp) == true)
                            {
                                // PropertyGrid does not return correct numeric types consistently
                                Type targetType = editInstProp.PropertyType;


                                object correctedType = Convert.ChangeType(chkGridPropValue, targetType);
                                editInstProp.SetValue(CCToolSession.templateCCTool, correctedType, null);

                                // This is not a value check, no current limits
                            }
                        }
                    }
                }
            }
        }

        private Font ccToolTextFontRegular = new Font(mainForm.DefaultFont, FontStyle.Regular);
        private Font ccToolTextFontBold = new Font(mainForm.DefaultFont, FontStyle.Bold);
        private bool bFirstTabPage = true;

        #endregion

        #region StatusStrip

        private void updateStatusStrip(int selectCount, string statusMessage)
        {
            this.toolStripStatusLabelMessages.Text = statusMessage;

            this.toolStripStatusLabelSelectCount.Text = "Select count: " + selectCount.ToString();

            this.toolStripStatusLabelClipCount.Text = "Clip count: " + CCToolSession.clipBoardToolFile.Tools.Count.ToString();

            if (CCToolSession.curToolFile.FileName != null)
            {
                this.toolStripStatusLabelCurFile.Text = "File: " + CCToolSession.curToolFile.FileName;
            }
            else
            {
                this.toolStripStatusLabelCurFile.Text = "File: NA";
    
            }

            this.toolStripStatusLabelToolCount.Text = "Tool count: " + CCToolSession.curToolFile.Tools.Count.ToString();
            this.toolStripStatusLabelFileCount.Text = "File count: " + CCToolSession.curToolFiles.Count.ToString();
        }

        #endregion

        #region ListView

        private void updateCCToolLibraryListView(ref CCToolFile nextCCFile)
        {
            // Load ListView with Dictionary data
            listViewCCToolLibrary.View = System.Windows.Forms.View.Details;
            listViewCCToolLibrary.LabelEdit = false;
            listViewCCToolLibrary.AllowColumnReorder = true;
            listViewCCToolLibrary.CheckBoxes = false;
            listViewCCToolLibrary.FullRowSelect = true;
            listViewCCToolLibrary.GridLines = false;
            listViewCCToolLibrary.Sorting = SortOrder.None;
            listViewCCToolLibrary.MultiSelect = true;
            listViewCCToolLibrary.HideSelection = false;
            listViewCCToolLibrary.Font = this.ccToolTextFontRegular;


            // Collect ListView items that are selected
            // Save using Keys
            //Dictionary<string, string> selKeyTags = new Dictionary<string, string>();

            //object[] selObjs = new object[listViewCCToolLibrary.SelectedItems.Count];
            //foreach (ListViewItem selItem in listViewCCToolLibrary.SelectedItems)
            //{
            //    if (selItem != null)
            //    {
            //        selKeyTags.Add(selItem.Text, selItem.Text);
            //    }
            //}

            listViewCCToolLibrary.Clear();
            listViewCCToolLibrary.BeginUpdate();

            // Use the same value as a key for the Column and Property

            // Build columns based on available Properties
            Dictionary<string, PropertyInfo> templateProps = new Dictionary<string, PropertyInfo>();
            templateProps = ExportPropertyHelper.getAllInstancePropertyInfo(ref CCToolSession.templateCCTool);
            if (this.userColumnSettings.Count == 0)
            {
                // Use Template object for Column names
                foreach (PropertyInfo templatePropInfo in templateProps.Values)
                {
                    // Only if BrowseAble
                    BrowsableAttribute testAtt2 = templatePropInfo.GetCustomAttribute<BrowsableAttribute>();
                    if (testAtt2.Browsable == true)
                    {
                        listViewCCToolLibrary.Columns.Add(templatePropInfo.Name, -2, HorizontalAlignment.Left);
                        ColumnHeader newColumn = listViewCCToolLibrary.Columns[listViewCCToolLibrary.Columns.Count-1];
                        newColumn.Name = templatePropInfo.Name;
                        newColumn.Tag = templatePropInfo;
                    }
                }
                // Seems you have to do this AFTER multiple Columns in order for the first Column to size
                foreach (ColumnHeader lclColHeader in listViewCCToolLibrary.Columns)
                    lclColHeader.Width = -2;
            }
            else
            {
                // Build columns based on user defined order
                // Non BrowseAble properties will not be in the collection
                foreach (listViewColumnSettings userColm in this.userColumnSettings.Values)
                {
                    // Find matching PropertyInfo
                    if (templateProps.TryGetValue(userColm.Name, out PropertyInfo tempProp))
                    {
                        listViewCCToolLibrary.Columns.Add(userColm.Name, userColm.Width, HorizontalAlignment.Left);
                        ColumnHeader newColumn = listViewCCToolLibrary.Columns[listViewCCToolLibrary.Columns.Count-1];
                        newColumn.Name = tempProp.Name;
                        newColumn.Tag = userColm;
                    }
                    else
                    {
                        // Missing Column ?
                    }

                }
                foreach (ColumnHeader lclColHeader in listViewCCToolLibrary.Columns)
                {
                    if (this.userColumnSettings.TryGetValue(lclColHeader.Name, out listViewColumnSettings userColm))
                    {
                        lclColHeader.DisplayIndex = userColm.DisplayIndex;
                    }
                }

            }
            // Populate Listview values
            ListViewItem.ListViewSubItem CCToolSubItem = new ListViewItem.ListViewSubItem();

            // Loop thru each tool instance
            foreach (CCToolCSV lclCCTool in nextCCFile.Tools.Values)
            {
                int propIdx = 0;
                ListViewItem lclToolItem = null;


                // Use the Column Collection and select the Property by name
                foreach(ColumnHeader lclColumHeader in listViewCCToolLibrary.Columns)
                {
                    object instancePropInfo = null;
                    ExportPropertyHelper.GetValueFromString(lclCCTool, lclColumHeader.Name, out instancePropInfo);

                    if (propIdx == 0)
                    {
                        // Use the actual instance as the Tag for the ListView item
                        // This gives direct access from the ListView to the Tool object
                        lclToolItem = new ListViewItem(instancePropInfo.ToString());
                        lclToolItem.Tag = lclCCTool;
                    }
                    else
                    {
                        // This is the ListView sub item to match the property
                        CCToolSubItem = new ListViewItem.ListViewSubItem(lclToolItem, instancePropInfo.ToString());
                        CCToolSubItem.Name = instancePropInfo.ToString();
                        lclToolItem.SubItems.Add(CCToolSubItem);
                    }

                    propIdx++;
                }

                // Add to ListView
                listViewCCToolLibrary.Items.Add(lclToolItem);

                //// Re Select item if required
                //string itemKeyName = null;
                //if (selKeyTags.TryGetValue(lclCCTool.Number.ToString(), out itemKeyName) == true)
                //{
                //    listViewCCToolLibrary.Items[listViewIdx].Selected = true;
                //}

            }

            listViewCCToolLibrary.SelectedItems.Clear();
            listViewCCToolLibrary.EndUpdate();


        }

        private ListViewColumnSorter lvwColumnSorterTransactions;
        public class ListViewColumnSorter : IComparer
        {
            /// <summary>
            /// Specifies the column to be sorted
            /// </summary>
            private int ColumnToSort;
            /// <summary>
            /// Specifies the order in which to sort (i.e. 'Ascending').
            /// </summary>
            private SortOrder OrderOfSort;
            /// <summary>
            /// Case insensitive comparer object
            /// </summary>
            private CaseInsensitiveComparer ObjectCompare;

            /// <summary>
            /// Class constructor.  Initializes various elements
            /// </summary>
            public ListViewColumnSorter()
            {
                // Initialize the column to '0'
                ColumnToSort = 0;

                // Initialize the sort order to 'none'
                OrderOfSort = SortOrder.None;

                // Initialize the CaseInsensitiveComparer object
                ObjectCompare = new CaseInsensitiveComparer();
            }

            /// <summary>
            /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
            /// </summary>
            /// <param name="x">First object to be compared</param>
            /// <param name="y">Second object to be compared</param>
            /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
            public int Compare(object x, object y)
            {
                int compareResult;
                ListViewItem listviewX, listviewY;

                // Cast the objects to be compared to ListViewItem objects
                listviewX = (ListViewItem)x;
                listviewY = (ListViewItem)y;

                // Compare the two items
                compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

                // Calculate correct return value based on object comparison
                if (OrderOfSort == SortOrder.Ascending)
                {
                    // Ascending sort is selected, return normal result of compare operation
                    return compareResult;
                }
                else if (OrderOfSort == SortOrder.Descending)
                {
                    // Descending sort is selected, return negative result of compare operation
                    return (-compareResult);
                }
                else
                {
                    // Return '0' to indicate they are equal
                    return 0;
                }
            }

            /// <summary>
            /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
            /// </summary>
            public int SortColumn
            {
                set
                {
                    ColumnToSort = value;
                }
                get
                {
                    return ColumnToSort;
                }
            }

            /// <summary>
            /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
            /// </summary>
            public SortOrder Order
            {
                set
                {
                    OrderOfSort = value;
                }
                get
                {
                    return OrderOfSort;
                }
            }

        }

        #region ListView Column user settings

        // Session based ListView Column order and settings
        private Dictionary<string, listViewColumnSettings> userColumnSettings = new Dictionary<string, listViewColumnSettings>();

        private class listViewColumnSettings
        {
            private int _width;
            private string _name;
            private string _text;
            private int _displayIndex;
            private PropertyPairChange? _propertyPair;
            // name is used as key in parent collection


            public int Width
            {
                get { return this._width; }
                set { this._width = value; }
            }
            public string Name
            {
                get { return this._name; }
                set { this._name = value; }
            }
            public string Text
            {
                get { return this._text; }
                set { this._text = value; }
            }
            public int DisplayIndex
            {
                get { return this._displayIndex; }
                set { this._displayIndex = value; }
            }
            public PropertyPairChange? PropertyPair
            {
                get { return this._propertyPair; }
                set { this._propertyPair = value; }
            }
            public listViewColumnSettings()
            {
                _name = string.Empty;
                _width = 0;
                _text = string.Empty;
                _displayIndex = 0;
                _propertyPair = null;
            }

        }

        private bool saveUserColumnSettings(out string messageStr)
        {
            bool bSuccess = false;
            messageStr = string.Empty;

            // Clear old settings
            CCToolLibraryConvert.Properties.Settings.Default.ColumnLayout.Clear();

            // Collect ListView Column order and settings
            this.listViewCCToolLibrary.SuspendLayout();
            // Loop through and size each column header to fit the column header text.
            foreach (ColumnHeader lclColHeader in this.listViewCCToolLibrary.Columns)
            {
                StringBuilder columnInfo = new StringBuilder();
                columnInfo.Append(lclColHeader.Name);
                columnInfo.Append("#" + lclColHeader.Width);
                columnInfo.Append("#" + lclColHeader.DisplayIndex);
                CCToolLibraryConvert.Properties.Settings.Default.ColumnLayout.Add(columnInfo.ToString());
            }

            this.listViewCCToolLibrary.ResumeLayout();
            messageStr = "Saved User Column Settings";
            CCToolLibraryConvert.Properties.Settings.Default.Save();
            bSuccess = true;

            return bSuccess;
        }

        private void rebuildListViewColumnInfo()
        {
            StringCollection userSettings = new StringCollection();

            if (CCToolLibraryConvert.Properties.Settings.Default.ColumnLayout.Count > 0)
            {

                // Get info from Settings
                userSettings = CCToolLibraryConvert.Properties.Settings.Default.ColumnLayout;
                foreach (string userSet in userSettings)
                {
                    // Split and parse the Settings collection
                    listViewColumnSettings userColm = new listViewColumnSettings();
                    string[] colParm = userSet.Split('#');

                    userColm.Name = colParm[0];
                    userColm.Width = Convert.ToInt32(colParm[1]);
                    // Could change text to be from the DisplayAttribute
                    userColm.Text = colParm[0];
                    userColm.DisplayIndex = Convert.ToInt32(colParm[2]);

                    // Save in Session Dictionary
                    userColumnSettings.Add(userColm.Name, userColm);

                }
            }
            else
            {
                userColumnSettings.Clear();
            }


        }

        #endregion


        #endregion

        #region PropertyGrid
        // Builds a cache collection of property names and values
        private static void buildCCToolPropertyCol()
        {
            CCToolSession.templateCCTool = new CCToolCSV();

            Dictionary<string, PropertyInfo> editGridProps = new Dictionary<string, PropertyInfo>();
            editGridProps = ExportPropertyHelper.getAllInstancePropertyDisplayName(ref CCToolSession.templateCCTool);

            CCToolSession.updatedCCToolCsvByPropertyCol.Clear();
            CCToolSession.updatedCCToolCsvByDisplayNameCol.Clear();

            foreach (PropertyInfo chkPropInfo in editGridProps.Values)
            {
                PropertyPairChange chkPropPair = new();
                chkPropPair.PropName = chkPropInfo.Name;
                // This is for the PropertyGrid
                // Use DisplayNameAttribute as key, not the propertyname
                DisplayNameAttribute testAtt = chkPropInfo.GetCustomAttribute<DisplayNameAttribute>();
                chkPropPair.DisplayName = testAtt.DisplayName;
                BrowsableAttribute testAtt2 = chkPropInfo.GetCustomAttribute<BrowsableAttribute>();
                chkPropPair.BrowseAbleFlag = testAtt2.Browsable;
                chkPropPair.PropertyInfoInst = chkPropInfo;
                chkPropPair.ChangeFlag = false;

                CCToolSession.updatedCCToolCsvByPropertyCol.Add(chkPropPair.PropName, chkPropPair);
                CCToolSession.updatedCCToolCsvByDisplayNameCol.Add(chkPropPair.DisplayName, chkPropPair);
            }

        }

        private void propertyCCToolCleanUp()
        {
            this.propertyGridCCToolLib.SelectedObject = null;
            this.propertyGridCCToolLib.SelectedObjects = null;

            this.buttonApplyPropChanges.Enabled = false;
            this.buttonCancelPropChanges.Enabled = false;
        }

        private static GridItemCollection GetAllGridEntries(PropertyGrid grid)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");

            var field = grid.GetType().GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
            {
                field = grid.GetType().GetField("_gridView", BindingFlags.NonPublic | BindingFlags.Instance);
                if (field == null)
                    return null;
            }

            var view = field.GetValue(grid);
            if (view == null)
                return null;

            try
            {
                return (GridItemCollection)view.GetType().InvokeMember("GetAllGridEntries", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, view, null);
            }
            catch
            {
                return null;
            }

            //object view = grid.GetType().GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(grid);

            //return (GridItemCollection)view.GetType().InvokeMember("GetAllGridEntries", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, view, null);
        }


        #endregion

        #region TabStrip

        private static void updateTabPagesOnSave(out List<CCToolFile> toolFileSuccess, out List<CCToolFile> toolFileError)
        {
            bool bSuccess;
            string errMsgStr = string.Empty;
            string caption = string.Empty;
            toolFileSuccess = new List<CCToolFile>();
            toolFileSuccess = new List<CCToolFile>();
            bSuccess = CCToolSession.SaveAllCurToolFiles(out toolFileSuccess, out toolFileError, out errMsgStr);
            if (bSuccess == true)
            {
                foreach (CCToolFile toolFile in toolFileSuccess)
                {
                    toolFile.FileTabPage.Text = toolFile.FileName;
                }

            }
            else
            {
                foreach (CCToolFile toolFile in toolFileError)
                {
                    toolFile.FileTabPage.Text = "!! " + toolFile.FileName;
                }
                caption = "Errors Saving Tool files";
                var result3 = MessageBox.Show(errMsgStr, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Warning);
            }

            return;
        }

        #endregion


    }
}
