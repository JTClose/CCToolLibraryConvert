using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCToolLibraryConvert
{

    // Session Static definitions
    public class CCToolSession
    {
        // CC Tools
        // Collection of all current CC Tool CSV files
        public static Dictionary<string, NCToolFile> curToolFiles = new Dictionary<string, NCToolFile>();
        // Current CC Tool file
        public static NCToolFile curToolFile = new NCToolFile();
        // Temp CC Tool file for copying and paste
        public static NCToolFile clipBoardToolFile = new NCToolFile();

        // Temp Tool instance for PropertyGrid values
        // This should probably be an object to make it generic
        public static CCToolCSV templateCCTool = new CCToolCSV();

        // CC Tool Properties values by keyed by Property name
        public static Dictionary<string, PropertyPairChange> updatedCCToolCsvByPropertyCol = new Dictionary<string, PropertyPairChange>();
        // CC Tool Properties values by keyed by DisplayName Attribute
        public static Dictionary<string, PropertyPairChange> updatedCCToolCsvByDisplayNameCol = new Dictionary<string, PropertyPairChange>();


        // F360 Tools
        // Collection of all current F360 json files
        public static Dictionary<string, _360LibraryConverter.F360ToolLibrary> curF360Files = new Dictionary<string, _360LibraryConverter.F360ToolLibrary>();
        // Current F360 json file
        public static _360LibraryConverter.F360ToolLibrary curF360File = new _360LibraryConverter.F360ToolLibrary();
        // Temp F360 json file for copy and paste
        public static _360LibraryConverter.F360ToolLibrary copyF360File = new _360LibraryConverter.F360ToolLibrary();

        // Session based next new file index
        public static int newFileCount = 0;


        public static bool CheckChangesCurToolFiles(out int changeCount)
        {
            bool bChangeWarning = false;
            changeCount = 0;
            // Check all files for changes
            foreach (NCToolFile toolFile in CCToolSession.curToolFiles.Values)
            {
                if (toolFile.bAnyChanges == true)
                {
                    changeCount++;
                    bChangeWarning = true;
                }
            }
            return bChangeWarning;

        }

        public static void CloseAllCurToolFiles()
        {
            // Remove all CCToolFiles from Session
            CCToolSession.curToolFiles.Clear();
            CCToolSession.curToolFile = new NCToolFile();
            CCToolSession.clipBoardToolFile = new NCToolFile();
        }

        public static bool SaveAllCurToolFiles(out List<NCToolFile> ccToolFileSuccess, out List<NCToolFile> ccToolFileError, out string allErrMsgStr)
        {
            bool bExitWarning = false;
            bool bChangeWarning = false;
            bool bSuccess = false;
            string errMsgStr = string.Empty;
            allErrMsgStr = string.Empty;
            ccToolFileSuccess = new List<NCToolFile>();
            ccToolFileError = new List<NCToolFile>();

            // Check all files for changes
            foreach (NCToolFile toolFile in CCToolSession.curToolFiles.Values)
            {
                if (toolFile.bAnyChanges == true)
                {
                    bSuccess = ToolFileHandling.SaveCCToolFile(toolFile.FullFilePath, ref CCToolSession.curToolFile, out errMsgStr);
                    if (bSuccess == false)
                    {
                        allErrMsgStr = string.Concat(allErrMsgStr + "\n", errMsgStr);
                        bExitWarning = true;
                        ccToolFileError.Add(toolFile);
                    }
                    else
                    {
                        ccToolFileSuccess.Add(toolFile);
                        toolFile.bAnyChanges = false;
                        toolFile.bNewFile = false;

                    }
                }
            }
            if (bExitWarning == true)
            {
                allErrMsgStr = string.Concat(ccToolFileError.Count + " files had errors saving", allErrMsgStr);
            }
            return (bSuccess);


        }

        public enum VendorEnum
        {
            CarbideCreate,
            F360
        }

    }

    // Wrapper around CC Tool CSV file
    public class NCToolFile
    {
        private string _fileName;
        private string _fullFilePath;
        private bool _bAnyChanges;
        private string _vendorSystem;
        private bool _bNewFile;
        private int _toolIndex;
        private TabPage? _fileTabPage;

        public NCToolFile()
        {
            _fileName = string.Empty;
            _fullFilePath = string.Empty;
            _bAnyChanges = false;
            _bNewFile = false;
            _vendorSystem = string.Empty;
            _fileTabPage = null;
            // Index is incremented BEFORE inserting.  This gives a 0 based index
            _toolIndex = -1;
            CCTools = new Dictionary<string, CCToolCSV>();
            F360Tools = new _360LibraryConverter.F360ToolLibrary();
        }

        public string FullFilePath
        {
            get { return _fullFilePath; }
            set
            {
                _fullFilePath = value;
                _fileName = Path.GetFileName(_fullFilePath);
            }

        }
        public string FileName
        {
            get { return _fileName; }
        }

        public bool bAnyChanges
        {
            get { return _bAnyChanges; }
            set { _bAnyChanges = value; }
        }
        public bool bNewFile
        {
            get { return _bNewFile; }
            set { _bNewFile = value; }
        }

        public string VendorSystem
        {
            get { return _vendorSystem; }
            set { _vendorSystem = value; }

        }

        public TabPage FileTabPage 
        {
            get { return _fileTabPage; }
            set { _fileTabPage = value; } 
        }

        public int ToolIndex
        {
            get { return _toolIndex; }
            set { _toolIndex = value; }
        }

        public Dictionary<string, CCToolCSV> CCTools = new Dictionary<string, CCToolCSV>();

        public _360LibraryConverter.F360ToolLibrary F360Tools = new _360LibraryConverter.F360ToolLibrary();
    }



}
