namespace CCToolLibraryConvert
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            statusStripMain = new StatusStrip();
            toolStripStatusLabelMessages = new ToolStripStatusLabel();
            toolStripStatusLabelSelectCount = new ToolStripStatusLabel();
            toolStripStatusLabelClipCount = new ToolStripStatusLabel();
            toolStripStatusLabelCurFile = new ToolStripStatusLabel();
            toolStripStatusLabelToolCount = new ToolStripStatusLabel();
            toolStripStatusLabelFileCount = new ToolStripStatusLabel();
            menuStripMain = new MenuStrip();
            ToolStripMenuItemFile = new ToolStripMenuItem();
            ToolStripMenuItemNew = new ToolStripMenuItem();
            toolStripMenuItemOpen = new ToolStripMenuItem();
            cCToolLibraryToolStripMenuItem = new ToolStripMenuItem();
            f360ToolLibraryToolStripMenuItem = new ToolStripMenuItem();
            camoticsToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItemSave = new ToolStripMenuItem();
            toolStripMenuItemSaveCurrent = new ToolStripMenuItem();
            toolStripMenuItemSaveAllChgs = new ToolStripMenuItem();
            ToolStripMenuItemClose = new ToolStripMenuItem();
            ToolStripMenuItemExit = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            columnLayoutToolStripMenuItem = new ToolStripMenuItem();
            saveColumnToolStripMenuItem = new ToolStripMenuItem();
            clearColumnToolStripMenuItem = new ToolStripMenuItem();
            splitContainerMain = new SplitContainer();
            tabControlCCToolLib = new TabControl();
            tabPage1 = new TabPage();
            treeViewF360Json = new TreeView();
            listViewCCToolLibrary = new ListView();
            contextMenuStripListView = new ContextMenuStrip(components);
            addRowToolStripMenuItem = new ToolStripMenuItem();
            copyRowsToolStripMenuItem = new ToolStripMenuItem();
            cutRowsToolStripMenuItem = new ToolStripMenuItem();
            pasteRowsToolStripMenuItem = new ToolStripMenuItem();
            deleteRowsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            selectAllToolStripMenuItem = new ToolStripMenuItem();
            unSelectAllToolStripMenuItem = new ToolStripMenuItem();
            splitContainerPropGrid = new SplitContainer();
            propertyGridCCToolLib = new PropertyGrid();
            buttonCancelPropChanges = new Button();
            buttonApplyPropChanges = new Button();
            statusStripMain.SuspendLayout();
            menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            tabControlCCToolLib.SuspendLayout();
            tabPage1.SuspendLayout();
            contextMenuStripListView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerPropGrid).BeginInit();
            splitContainerPropGrid.Panel1.SuspendLayout();
            splitContainerPropGrid.Panel2.SuspendLayout();
            splitContainerPropGrid.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripMain
            // 
            statusStripMain.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelMessages, toolStripStatusLabelSelectCount, toolStripStatusLabelClipCount, toolStripStatusLabelCurFile, toolStripStatusLabelToolCount, toolStripStatusLabelFileCount });
            statusStripMain.Location = new Point(0, 426);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new Size(800, 24);
            statusStripMain.TabIndex = 0;
            statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMessages
            // 
            toolStripStatusLabelMessages.Name = "toolStripStatusLabelMessages";
            toolStripStatusLabelMessages.Size = new Size(425, 19);
            toolStripStatusLabelMessages.Spring = true;
            toolStripStatusLabelMessages.Text = "Messages";
            toolStripStatusLabelMessages.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelSelectCount
            // 
            toolStripStatusLabelSelectCount.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            toolStripStatusLabelSelectCount.Name = "toolStripStatusLabelSelectCount";
            toolStripStatusLabelSelectCount.Size = new Size(75, 19);
            toolStripStatusLabelSelectCount.Text = "SelectCount";
            // 
            // toolStripStatusLabelClipCount
            // 
            toolStripStatusLabelClipCount.Name = "toolStripStatusLabelClipCount";
            toolStripStatusLabelClipCount.Size = new Size(92, 19);
            toolStripStatusLabelClipCount.Text = "ClipBoardCount";
            // 
            // toolStripStatusLabelCurFile
            // 
            toolStripStatusLabelCurFile.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            toolStripStatusLabelCurFile.Name = "toolStripStatusLabelCurFile";
            toolStripStatusLabelCurFile.Size = new Size(69, 19);
            toolStripStatusLabelCurFile.Text = "CurrentFile";
            // 
            // toolStripStatusLabelToolCount
            // 
            toolStripStatusLabelToolCount.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            toolStripStatusLabelToolCount.Name = "toolStripStatusLabelToolCount";
            toolStripStatusLabelToolCount.Size = new Size(66, 19);
            toolStripStatusLabelToolCount.Text = "ToolCount";
            // 
            // toolStripStatusLabelFileCount
            // 
            toolStripStatusLabelFileCount.Name = "toolStripStatusLabelFileCount";
            toolStripStatusLabelFileCount.Size = new Size(58, 19);
            toolStripStatusLabelFileCount.Text = "FileCount";
            // 
            // menuStripMain
            // 
            menuStripMain.Items.AddRange(new ToolStripItem[] { ToolStripMenuItemFile, settingsToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(800, 24);
            menuStripMain.TabIndex = 1;
            menuStripMain.Text = "menuStrip1";
            // 
            // ToolStripMenuItemFile
            // 
            ToolStripMenuItemFile.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItemNew, toolStripMenuItemOpen, ToolStripMenuItemSave, ToolStripMenuItemClose, ToolStripMenuItemExit });
            ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
            ToolStripMenuItemFile.Size = new Size(37, 20);
            ToolStripMenuItemFile.Text = "&File";
            // 
            // ToolStripMenuItemNew
            // 
            ToolStripMenuItemNew.Name = "ToolStripMenuItemNew";
            ToolStripMenuItemNew.Size = new Size(180, 22);
            ToolStripMenuItemNew.Text = "New";
            ToolStripMenuItemNew.Click += ToolStripMenuItemNew_Click;
            // 
            // toolStripMenuItemOpen
            // 
            toolStripMenuItemOpen.DropDownItems.AddRange(new ToolStripItem[] { cCToolLibraryToolStripMenuItem, f360ToolLibraryToolStripMenuItem, camoticsToolStripMenuItem });
            toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            toolStripMenuItemOpen.Size = new Size(180, 22);
            toolStripMenuItemOpen.Text = "&Open";
            // 
            // cCToolLibraryToolStripMenuItem
            // 
            cCToolLibraryToolStripMenuItem.Name = "cCToolLibraryToolStripMenuItem";
            cCToolLibraryToolStripMenuItem.Size = new Size(180, 22);
            cCToolLibraryToolStripMenuItem.Text = "CC Tool Library";
            cCToolLibraryToolStripMenuItem.Click += OpenCCToolLibraryToolStripMenuItem_Click;
            // 
            // f360ToolLibraryToolStripMenuItem
            // 
            f360ToolLibraryToolStripMenuItem.Name = "f360ToolLibraryToolStripMenuItem";
            f360ToolLibraryToolStripMenuItem.Size = new Size(180, 22);
            f360ToolLibraryToolStripMenuItem.Text = "F360 Tool Library";
            f360ToolLibraryToolStripMenuItem.Click += OpenF360ToolLibraryToolStripMenuItem_Click;
            // 
            // camoticsToolStripMenuItem
            // 
            camoticsToolStripMenuItem.Enabled = false;
            camoticsToolStripMenuItem.Name = "camoticsToolStripMenuItem";
            camoticsToolStripMenuItem.Size = new Size(180, 22);
            camoticsToolStripMenuItem.Text = "Camotics";
            camoticsToolStripMenuItem.Click += camoticsToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemSave
            // 
            ToolStripMenuItemSave.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItemSaveCurrent, toolStripMenuItemSaveAllChgs });
            ToolStripMenuItemSave.Name = "ToolStripMenuItemSave";
            ToolStripMenuItemSave.Size = new Size(180, 22);
            ToolStripMenuItemSave.Text = "&Save";
            // 
            // toolStripMenuItemSaveCurrent
            // 
            toolStripMenuItemSaveCurrent.Name = "toolStripMenuItemSaveCurrent";
            toolStripMenuItemSaveCurrent.Size = new Size(164, 22);
            toolStripMenuItemSaveCurrent.Text = "Save Current File";
            toolStripMenuItemSaveCurrent.Click += ToolStripMenuItemSave_Click;
            // 
            // toolStripMenuItemSaveAllChgs
            // 
            toolStripMenuItemSaveAllChgs.Name = "toolStripMenuItemSaveAllChgs";
            toolStripMenuItemSaveAllChgs.Size = new Size(164, 22);
            toolStripMenuItemSaveAllChgs.Text = "Save All Changes";
            toolStripMenuItemSaveAllChgs.Click += ToolStripMenuItemSaveAllChgs_Click;
            // 
            // ToolStripMenuItemClose
            // 
            ToolStripMenuItemClose.Name = "ToolStripMenuItemClose";
            ToolStripMenuItemClose.Size = new Size(180, 22);
            ToolStripMenuItemClose.Text = "&Close All Files";
            ToolStripMenuItemClose.Click += ToolStripMenuItemClose_Click;
            // 
            // ToolStripMenuItemExit
            // 
            ToolStripMenuItemExit.Name = "ToolStripMenuItemExit";
            ToolStripMenuItemExit.Size = new Size(180, 22);
            ToolStripMenuItemExit.Text = "&Exit";
            ToolStripMenuItemExit.Click += ToolStripMenuItemExit_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { columnLayoutToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // columnLayoutToolStripMenuItem
            // 
            columnLayoutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveColumnToolStripMenuItem, clearColumnToolStripMenuItem });
            columnLayoutToolStripMenuItem.Name = "columnLayoutToolStripMenuItem";
            columnLayoutToolStripMenuItem.Size = new Size(153, 22);
            columnLayoutToolStripMenuItem.Text = "ColumnLayout";
            // 
            // saveColumnToolStripMenuItem
            // 
            saveColumnToolStripMenuItem.Name = "saveColumnToolStripMenuItem";
            saveColumnToolStripMenuItem.Size = new Size(101, 22);
            saveColumnToolStripMenuItem.Text = "Save";
            saveColumnToolStripMenuItem.Click += saveColumnToolStripMenuItem_Click;
            // 
            // clearColumnToolStripMenuItem
            // 
            clearColumnToolStripMenuItem.Name = "clearColumnToolStripMenuItem";
            clearColumnToolStripMenuItem.Size = new Size(101, 22);
            clearColumnToolStripMenuItem.Text = "Clear";
            clearColumnToolStripMenuItem.Click += clearColumnToolStripMenuItem_Click;
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(0, 24);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(tabControlCCToolLib);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(splitContainerPropGrid);
            splitContainerMain.Panel2MinSize = 250;
            splitContainerMain.Size = new Size(800, 402);
            splitContainerMain.SplitterDistance = 400;
            splitContainerMain.TabIndex = 2;
            splitContainerMain.TabStop = false;
            // 
            // tabControlCCToolLib
            // 
            tabControlCCToolLib.Controls.Add(tabPage1);
            tabControlCCToolLib.Dock = DockStyle.Fill;
            tabControlCCToolLib.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlCCToolLib.Location = new Point(0, 0);
            tabControlCCToolLib.Name = "tabControlCCToolLib";
            tabControlCCToolLib.Padding = new Point(12, 4);
            tabControlCCToolLib.SelectedIndex = 0;
            tabControlCCToolLib.Size = new Size(400, 402);
            tabControlCCToolLib.TabIndex = 4;
            tabControlCCToolLib.TabStop = false;
            tabControlCCToolLib.DrawItem += tabControlCCToolLib_DrawItem;
            tabControlCCToolLib.Selected += tabControlCCToolLib_Selected;
            tabControlCCToolLib.Deselected += tabControlCCToolLib_Deselected;
            tabControlCCToolLib.MouseDown += tabControlCCToolLib_MouseDown;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(treeViewF360Json);
            tabPage1.Controls.Add(listViewCCToolLibrary);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(392, 372);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // treeViewF360Json
            // 
            treeViewF360Json.Location = new Point(33, 38);
            treeViewF360Json.Name = "treeViewF360Json";
            treeViewF360Json.Size = new Size(121, 261);
            treeViewF360Json.TabIndex = 1;
            treeViewF360Json.AfterSelect += treeViewF360Json_AfterSelect;
            // 
            // listViewCCToolLibrary
            // 
            listViewCCToolLibrary.ContextMenuStrip = contextMenuStripListView;
            listViewCCToolLibrary.Location = new Point(173, 30);
            listViewCCToolLibrary.Name = "listViewCCToolLibrary";
            listViewCCToolLibrary.Size = new Size(219, 342);
            listViewCCToolLibrary.TabIndex = 0;
            listViewCCToolLibrary.UseCompatibleStateImageBehavior = false;
            listViewCCToolLibrary.ColumnClick += listViewCCToolLibrary_ColumnClick_1;
            listViewCCToolLibrary.ItemSelectionChanged += listViewCCToolLibrary_ItemSelectionChanged_1;
            // 
            // contextMenuStripListView
            // 
            contextMenuStripListView.Items.AddRange(new ToolStripItem[] { addRowToolStripMenuItem, copyRowsToolStripMenuItem, cutRowsToolStripMenuItem, pasteRowsToolStripMenuItem, deleteRowsToolStripMenuItem, toolStripSeparator1, selectAllToolStripMenuItem, unSelectAllToolStripMenuItem });
            contextMenuStripListView.Name = "contextMenuStripListView";
            contextMenuStripListView.Size = new Size(139, 164);
            // 
            // addRowToolStripMenuItem
            // 
            addRowToolStripMenuItem.Name = "addRowToolStripMenuItem";
            addRowToolStripMenuItem.Size = new Size(138, 22);
            addRowToolStripMenuItem.Text = "Add Row";
            addRowToolStripMenuItem.Click += AddRowToolStripMenuItem_Click;
            // 
            // copyRowsToolStripMenuItem
            // 
            copyRowsToolStripMenuItem.Name = "copyRowsToolStripMenuItem";
            copyRowsToolStripMenuItem.Size = new Size(138, 22);
            copyRowsToolStripMenuItem.Text = "Copy Rows";
            copyRowsToolStripMenuItem.Click += CopyRowsToolStripMenuItem_Click;
            // 
            // cutRowsToolStripMenuItem
            // 
            cutRowsToolStripMenuItem.Name = "cutRowsToolStripMenuItem";
            cutRowsToolStripMenuItem.Size = new Size(138, 22);
            cutRowsToolStripMenuItem.Text = "Cut Rows";
            cutRowsToolStripMenuItem.Click += CutRowsToolStripMenuItem_Click;
            // 
            // pasteRowsToolStripMenuItem
            // 
            pasteRowsToolStripMenuItem.Name = "pasteRowsToolStripMenuItem";
            pasteRowsToolStripMenuItem.Size = new Size(138, 22);
            pasteRowsToolStripMenuItem.Text = "Paste Rows";
            pasteRowsToolStripMenuItem.Click += PasteRowsToolStripMenuItem_Click;
            // 
            // deleteRowsToolStripMenuItem
            // 
            deleteRowsToolStripMenuItem.Name = "deleteRowsToolStripMenuItem";
            deleteRowsToolStripMenuItem.Size = new Size(138, 22);
            deleteRowsToolStripMenuItem.Text = "Delete Rows";
            deleteRowsToolStripMenuItem.Click += DeleteRowsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(135, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.Size = new Size(138, 22);
            selectAllToolStripMenuItem.Text = "Select All";
            selectAllToolStripMenuItem.Click += selectAllToolStripMenuItem_Click;
            // 
            // unSelectAllToolStripMenuItem
            // 
            unSelectAllToolStripMenuItem.Name = "unSelectAllToolStripMenuItem";
            unSelectAllToolStripMenuItem.Size = new Size(138, 22);
            unSelectAllToolStripMenuItem.Text = "UnSelect All";
            unSelectAllToolStripMenuItem.Click += unSelectAllToolStripMenuItem_Click;
            // 
            // splitContainerPropGrid
            // 
            splitContainerPropGrid.Dock = DockStyle.Fill;
            splitContainerPropGrid.FixedPanel = FixedPanel.Panel2;
            splitContainerPropGrid.IsSplitterFixed = true;
            splitContainerPropGrid.Location = new Point(0, 0);
            splitContainerPropGrid.Name = "splitContainerPropGrid";
            splitContainerPropGrid.Orientation = Orientation.Horizontal;
            // 
            // splitContainerPropGrid.Panel1
            // 
            splitContainerPropGrid.Panel1.Controls.Add(propertyGridCCToolLib);
            // 
            // splitContainerPropGrid.Panel2
            // 
            splitContainerPropGrid.Panel2.Controls.Add(buttonCancelPropChanges);
            splitContainerPropGrid.Panel2.Controls.Add(buttonApplyPropChanges);
            splitContainerPropGrid.Size = new Size(396, 402);
            splitContainerPropGrid.SplitterDistance = 350;
            splitContainerPropGrid.TabIndex = 0;
            // 
            // propertyGridCCToolLib
            // 
            propertyGridCCToolLib.Dock = DockStyle.Fill;
            propertyGridCCToolLib.Location = new Point(0, 0);
            propertyGridCCToolLib.Name = "propertyGridCCToolLib";
            propertyGridCCToolLib.Size = new Size(396, 350);
            propertyGridCCToolLib.TabIndex = 2;
            propertyGridCCToolLib.PropertyValueChanged += propertyGridCCToolLib_PropertyValueChanged;
            // 
            // buttonCancelPropChanges
            // 
            buttonCancelPropChanges.Enabled = false;
            buttonCancelPropChanges.Location = new Point(140, 12);
            buttonCancelPropChanges.Name = "buttonCancelPropChanges";
            buttonCancelPropChanges.Size = new Size(80, 24);
            buttonCancelPropChanges.TabIndex = 3;
            buttonCancelPropChanges.Text = "Cancel";
            buttonCancelPropChanges.UseVisualStyleBackColor = true;
            buttonCancelPropChanges.Click += buttonCancelPropChanges_Click;
            // 
            // buttonApplyPropChanges
            // 
            buttonApplyPropChanges.Enabled = false;
            buttonApplyPropChanges.Location = new Point(30, 12);
            buttonApplyPropChanges.Name = "buttonApplyPropChanges";
            buttonApplyPropChanges.Size = new Size(80, 24);
            buttonApplyPropChanges.TabIndex = 2;
            buttonApplyPropChanges.Text = "Apply";
            buttonApplyPropChanges.UseVisualStyleBackColor = true;
            buttonApplyPropChanges.Click += buttonApplyPropChanges_Click;
            // 
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainerMain);
            Controls.Add(statusStripMain);
            Controls.Add(menuStripMain);
            MainMenuStrip = menuStripMain;
            MinimumSize = new Size(750, 400);
            Name = "mainForm";
            Text = "CC Tool Library Editor";
            FormClosed += FormMain_FormClosed;
            Load += FormMain_Load;
            Shown += FormMain_Shown;
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            tabControlCCToolLib.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            contextMenuStripListView.ResumeLayout(false);
            splitContainerPropGrid.Panel1.ResumeLayout(false);
            splitContainerPropGrid.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerPropGrid).EndInit();
            splitContainerPropGrid.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private StatusStrip statusStripMain;
        private MenuStrip menuStripMain;
        private SplitContainer splitContainerMain;
        private ToolStripMenuItem ToolStripMenuItemFile;
        private ToolStripMenuItem toolStripMenuItemOpen;
        private ToolStripMenuItem ToolStripMenuItemSave;
        private ToolStripMenuItem ToolStripMenuItemClose;
        private ToolStripMenuItem ToolStripMenuItemExit;
        private ToolStripMenuItem f360ToolLibraryToolStripMenuItem;
        private ToolStripMenuItem cCToolLibraryToolStripMenuItem;
        private ToolStripMenuItem camoticsToolStripMenuItem;
        private SplitContainer splitContainerPropGrid;
        private PropertyGrid propertyGridCCToolLib;
        private Button buttonCancelPropChanges;
        private Button buttonApplyPropChanges;
        private ContextMenuStrip contextMenuStripListView;
        private ToolStripMenuItem copyRowsToolStripMenuItem;
        private ToolStripMenuItem pasteRowsToolStripMenuItem;
        private ToolStripMenuItem deleteRowsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem unSelectAllToolStripMenuItem;
        private TabControl tabControlCCToolLib;
        private ToolStripMenuItem toolStripMenuItemSaveCurrent;
        private ToolStripMenuItem toolStripMenuItemSaveAllChgs;
        private ToolStripStatusLabel toolStripStatusLabelMessages;
        private ToolStripMenuItem ToolStripMenuItemNew;
        private ToolStripStatusLabel toolStripStatusLabelCurFile;
        private ToolStripStatusLabel toolStripStatusLabelToolCount;
        private ToolStripStatusLabel toolStripStatusLabelSelectCount;
        private ToolStripMenuItem cutRowsToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabelClipCount;
        private ToolStripMenuItem addRowToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem columnLayoutToolStripMenuItem;
        private ToolStripMenuItem saveColumnToolStripMenuItem;
        private ToolStripMenuItem clearColumnToolStripMenuItem;
        private TabPage tabPage1;
        private ListView listViewCCToolLibrary;
        private ToolStripStatusLabel toolStripStatusLabelFileCount;
        private TreeView treeViewF360Json;
    }
}