using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Security;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CCToolLibraryConvert
{

    #region Helpers

    public class PropertyPairChange
    {
        private string _displayName = "";
        private string _propName = "";
        private object _propValue = "";
        private PropertyInfo _propertyInfo = null;
        private bool _changeFlag = false;
        private bool _browseAbleFlag  = false;

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public string PropName
        {
            get { return _propName; }
            set { _propName = value; }
        }
        public object PropValue
        {
            get { return _propValue; }
            set { _propValue = value; }
        }

        public PropertyInfo PropertyInfoInst
        {
            get { return _propertyInfo; }
            set { _propertyInfo = value; }
        }
        public bool ChangeFlag
        {
            get { return _changeFlag; }
            set { _changeFlag = value; }
        }
        public bool BrowseAbleFlag
        {
            get { return _browseAbleFlag; }
            set { _browseAbleFlag = value; }
        }

    }

    public static class ExportPropertyHelper
    {

        public static Dictionary<string, PropertyInfo> getAllInstancePropertyInfo(ref CCToolCSV chkCCTool)
        {
            //            var flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var map = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] allProps = chkCCTool.GetType().GetProperties();
            foreach (var prop in allProps)
            {
                if (prop.IsDefined(typeof(object), false))
                {
                    map.Add(prop.Name, prop);
                }
            }
            return map;
        }
        public static Dictionary<string, PropertyInfo> getAllInstancePropertyDisplayName(ref CCToolCSV chkCCTool)
        {
            //            var flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var map = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] allProps = chkCCTool.GetType().GetProperties();
            foreach (PropertyInfo prop in allProps)
            {
                if (prop.IsDefined(typeof(object), false))
                {
                    // Use Attribte DisplayName value for the key, not the property name
                    // That is what the propertyGrid uses
                    DisplayNameAttribute testAtt = prop.GetCustomAttribute<DisplayNameAttribute>();


                    map.Add(testAtt.DisplayName, prop);
                }
            }
            return map;
        }

        public static void SetValueFromString(object target, string propertyName, object propertyValue)
        {
            PropertyInfo oProp = target.GetType().GetProperty(propertyName);
            Type tProp = oProp.PropertyType;

            //Nullable properties have to be treated differently, since we 
            //  use their underlying property to set the value in the object
            if (tProp.IsGenericType
                && tProp.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                //if it's null, just set the value from the reserved word null, and return
                if (propertyValue == null)
                {
                    oProp.SetValue(target, null, null);
                    return;
                }

                //Get the underlying type property instead of the nullable generic
                tProp = new NullableConverter(oProp.PropertyType).UnderlyingType;
            }

            //use the converter to get the correct value
            oProp.SetValue(target, Convert.ChangeType(propertyValue, tProp), null);
            return;

        }
 
        public static bool GetValueFromString(object target, string propertyName, out object propertyValueObject)
        {
            propertyValueObject = null;
            PropertyInfo oProp = target.GetType().GetProperty(propertyName);

            if (oProp == null)
            {
                return false;
            }

            Type tProp = oProp.PropertyType;
            //use the converter to get the correct value
            propertyValueObject = oProp.GetValue(target, null);

            return true;
        }

        #region NotUsed
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }
        public static void CopyAllPropertyValues(object source, object destination, ref Dictionary<string, PropertyPairChange> propertyChangeCol, bool bNullOkFlag)
        {
            var sourceProperties = source.GetType().GetProperties();
            var destProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                string sourcePropName = sourceProperty.Name;
                PropertyPairChange chkPropPair = null;
                if (propertyChangeCol.TryGetValue(sourcePropName, out chkPropPair) == true)
                {
                    if (sourceProperty.IsDefined(typeof(object), false))
                    {

                        foreach (var destProperty in destProperties)
                        {
                            if (destProperty.Name == sourceProperty.Name &&
                    destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                            {
                                object sourceValue = sourceProperty.GetValue(source, new object[] { });
                                if (bNullOkFlag == true)
                                {
                                    destProperty.SetValue(destination, sourceValue, new object[] { });
                                }
                                else
                                {
                                    if (sourceValue != null)
                                    {
                                        destProperty.SetValue(destination, sourceValue, new object[] { });
                                    }
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }
        public class TypeHelpers
        {
            public T CastObject<T>(object input)
            {
                return (T)input;
            }

            public T ConvertObject<T>(object input)
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }

        }
        #endregion

    }
    #endregion


    // Open and Save file types    
    public class ToolFileHandling
    {
        #region F360 Tool File
        public static bool JsonInputFiles(string inputFileNameStr, out _360LibraryConverter.F360ToolLibrary f360ToolLibrary, out JToken nextJsonToken)
        {
            bool successFlag = false;

            nextJsonToken = JToken.Parse(System.IO.File.ReadAllText(inputFileNameStr));

            dynamic nextF360ToolLibrary = _360LibraryConverter.F360ToolLibrary.FromJson(System.IO.File.ReadAllText(inputFileNameStr));
            if (nextF360ToolLibrary != null)
            {
                successFlag = true;
            }
            f360ToolLibrary = nextF360ToolLibrary;

            return successFlag;


        }

        public static bool JsonOutputFiles(_360LibraryConverter.F360ToolLibrary f360ToolLibrary , string outputeFileNameStr)
        {
            bool successFlag = false;

            string jsonToolLib = _360LibraryConverter.Serialize.ToJson(f360ToolLibrary);
            if (jsonToolLib != null)
            {
                System.IO.File.WriteAllText(outputeFileNameStr, jsonToolLib);
                successFlag = true;
            }


            return successFlag;

        }
        #endregion

        #region CC Tool File

        public const string ccToolCSVHeader = "number,vendor,model,URL,name,type,diameter,cornerradius,flutelength,shaftdiameter,angle,numflutes,stickout,coating,metric,notes,machine,material,plungerate,feedrate,rpm,depth,cutpower,finishallowance,3dstepover,3dfeedrate,3drpm";


        public static bool SaveCCToolFile(string fullFilePath, ref NCToolFile saveCCToolFile, out string errMsgStr)
        {
            errMsgStr = string.Empty;
            bool bSuccess = false;
            if (saveCCToolFile.CCTools.Count > 0)
            {

                try
                {
                    bSuccess = WriteCCToolCSVFile(fullFilePath, CCToolSession.curToolFile, out errMsgStr);
                    if (bSuccess)
                    {
                        errMsgStr = "Success";
                        return (bSuccess);
                    }
                    else
                    {
                        // Clean up
                        // The user lacks appropriate permissions to read files, discover paths, etc.
                        errMsgStr = "Data error. Please contact the Author.\n" +
                            "Error message:\n" + errMsgStr + "\n" +
                            "Details (send to Support. Good Luck ! ):\n";
                        return (bSuccess);


                    }
                }
                catch (SecurityException ex)
                {
                    // The user lacks appropriate permissions to read files, discover paths, etc.
                    errMsgStr = "Security error. Please contact your administrator for details.\n" +
                        "Error message: " + ex.Message + "\n" +
                        "Details (send to Support):\n" + ex.StackTrace;
                
                    return (bSuccess);

                }
                catch (Exception ex)
                {
                    // Could not load the image - probably related to Windows file system permissions.
                    errMsgStr = "Cannot Save CSV file " + fullFilePath.Substring(fullFilePath.LastIndexOf('\\'))
                        + ". You may not have permission to write the file, or " +
                        "it may be corrupt.\nReported error: " + ex.Message;
                    return (bSuccess);
                }
            }
            else
            {
                errMsgStr = "No Tools to save";
                bSuccess = true;
                return (bSuccess);
            }
        }


        public static bool WriteCCToolCSVFile(string outputFileName, NCToolFile cCToolFile, out string errMsgStr)
        {
            errMsgStr = string.Empty;
            bool bSuccess = false;
            if (cCToolFile.CCTools.Count == 0)
            {
                errMsgStr = "No Tools defined in file " + outputFileName;
                return (bSuccess);
            }

            StringBuilder sb = new StringBuilder();
            // Add Header record
            sb.Append(ccToolCSVHeader + '\n');

            foreach (CCToolCSV nextToolCSV in cCToolFile.CCTools.Values)
            {
                sb.Append(nextToolCSV.Number.ToString() + ",");
                sb.Append(nextToolCSV.Vendor + ",");
                sb.Append(nextToolCSV.Model + ",");
                sb.Append(nextToolCSV.URL + ",");
                sb.Append(nextToolCSV.Name + ",");
                sb.Append(nextToolCSV.Type + ",");
                sb.Append(nextToolCSV.Diameter.ToString() + ",");
                sb.Append(nextToolCSV.Cornerradius.ToString() + ",");
                sb.Append(nextToolCSV.Flutelength.ToString() + ",");
                sb.Append(nextToolCSV.Shaftdiameter.ToString() + ",");
                sb.Append(nextToolCSV.Angle.ToString() + ",");
                sb.Append(nextToolCSV.Numflutes.ToString() + ",");
                sb.Append(nextToolCSV.Stickout.ToString() + ",");
                sb.Append(nextToolCSV.Coating + ",");
                sb.Append(nextToolCSV.Metric.ToString() + ",");
                sb.Append(nextToolCSV.Notes + ",");
                sb.Append(nextToolCSV.Machine + ",");
                sb.Append(nextToolCSV.Material + ",");
                sb.Append(nextToolCSV.Plungerate.ToString() + ",");
                sb.Append(nextToolCSV.Feedrate.ToString() + ",");
                sb.Append(nextToolCSV.Rpm.ToString() + ",");
                sb.Append(nextToolCSV.Depth.ToString() + ",");
                sb.Append(nextToolCSV.Cutpower + ",");
                sb.Append(nextToolCSV.Finishallowance.ToString() + ",");
                sb.Append(nextToolCSV.Stepover3d.ToString() + ",");
                sb.Append(nextToolCSV.Feedrate3d.ToString() + ",");
                sb.Append(nextToolCSV.Rpm3d.ToString());
                sb.Append('\n');

            }

            try
            {
                File.WriteAllText(outputFileName, sb.ToString());
                errMsgStr = "Updated CCToolFile " + outputFileName;
                cCToolFile.bAnyChanges = false;
                bSuccess = true;
            }
            catch (Exception e)
            {
                // some IO error
                errMsgStr = e.Message;
            }

            return (bSuccess);
        }

        public static bool ReadCCToolCSVFile(string inputFileNameStr, out NCToolFile newCCToolFile, out string errMSgStr)
        {
            bool bSuccess = true;
            errMSgStr = string.Empty;
            newCCToolFile = new NCToolFile();

            string[] dblQuoteArray;
            string[] commaArray;

            int j = 0;

            string[] ccToolRecords = File.ReadAllLines(inputFileNameStr);
          

            // Define delimiters to parse with

            char[] dblQuoteChar = new char[] { '"' };
            char[] commaChar = new char[] { ',' };

            int k = 0;
            bool headerFlag = true;
            string[] headerArray = null;

            foreach (string toolRecord in ccToolRecords)
            {
                if (toolRecord != null)
                {
                    k++;
                    // Find double quotes first, then commas
                    dblQuoteArray = toolRecord.Split(dblQuoteChar, StringSplitOptions.RemoveEmptyEntries);
                    commaArray = dblQuoteArray[0].Split(commaChar, StringSplitOptions.None);
                    int i = commaArray.Length;
                    if (i == 27)
                    {
                        // clean parse
                        if (headerFlag == true)
                        {
                            // Header ?
                            if (toolRecord == ccToolCSVHeader)
                            {
                                headerFlag = false;
                                // All good.  Save commaArray for use in error messages
                                headerArray = (String[]) commaArray.Clone();
                            }
                            else
                            {
                                // Failed header string check
                                errMSgStr = "Header record does not have match proper contents. " +  "\nBad " + toolRecord + "\nDefault " + ccToolCSVHeader;
                                bSuccess = false;
                                break;
                            }

                        }
                        else
                        {
                            bool bRecordSuccess = false;
                            // Read CSV data record
                            CCToolCSV newCCTool = new CCToolCSV();
                            bRecordSuccess = ParseCCToolRecord(commaArray, headerArray, k, ref newCCTool, out errMSgStr);
                            if (bRecordSuccess == true)
                            {
                                // Increment param position
                                j++;
                                newCCToolFile.ToolIndex++;
                                newCCTool.ToolIndex = newCCToolFile.ToolIndex.ToString();
                                newCCToolFile.CCTools.Add(newCCTool.ToolIndex, newCCTool);
                            }
                            else
                            {
                                bSuccess = false;
                                break;
                            }

                        }

                    }
                    else
                    {
                        // Illegal parameter count 
                        errMSgStr = "Record does not have 27 columns.  It has " + i + "\n " + toolRecord;
                        bSuccess = false;
                        break;
                    }
                }
                else
                {
                    // Empty record, bail out
                    errMSgStr = "Empty Record";
                    bSuccess = false;
                    break;

                }
            }

            if(bSuccess == true)
            {

                errMSgStr = "Successfully read " + j + " Records";
                newCCToolFile.FullFilePath = inputFileNameStr;
            }

            return bSuccess;

        }

        private static bool ParseCCToolRecord(string[] commaArray, string[] headerArray, int k, ref CCToolCSV newCCTool, out string errMSgStr)
        {
            int PPos = 0;
            errMSgStr = null;
            bool bSuccess = false;

            int nextInt = 0;
            decimal nextDecimal = 0;


            // Number
            if (!Int32.TryParse(commaArray[PPos], out nextInt))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not an Integer on Record " + k;
                return(bSuccess);
            }
            else 
                newCCTool.Number = nextInt;


            // Vendor    Null is ok
            PPos++;
            newCCTool.Vendor = commaArray[PPos];

            // Model    Null is ok
            PPos++;
            newCCTool.Model = commaArray[PPos];


            // URL    Null is ok
            PPos++;
            newCCTool.URL = commaArray[PPos];

            // name    Null is ok
            PPos++;
            newCCTool.Name = commaArray[PPos];

            // type   
            PPos++;
            if (commaArray[PPos] == null)
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value is NULL on Record " + k;
                return(bSuccess);
            }
            else
                newCCTool.Type = commaArray[PPos];

            // Diameter
            PPos++;

            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return (bSuccess);
            }
            else
                newCCTool.Diameter = nextDecimal;

            // CornerRadius
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return (bSuccess);
            }
            else
                newCCTool.Cornerradius = nextDecimal;

            // FluteLength
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return(bSuccess);
            }
            else
                newCCTool.Flutelength = nextDecimal;

            // ShaftDiameter
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return(bSuccess);
            }
            else
                newCCTool.Shaftdiameter = nextDecimal;

            // Angle
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return(bSuccess);
            }
            else
                newCCTool.Angle = nextDecimal;


            // NumFlutes
            PPos++;
            if (!Int32.TryParse(commaArray[PPos], out nextInt))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not an Integer on Record " + k;
                return (bSuccess);
            }
            else
                newCCTool.Numflutes = nextInt;

            // Stickout
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not an Integer on Record " + k;
                return(bSuccess);
            }
            else
                newCCTool.Stickout = nextDecimal;

            // Coating   Null is ok
            PPos++;
            newCCTool.Coating = commaArray[PPos];

            // Metric   0 = Inch    1 = Metric
            PPos++;
            if (!Int32.TryParse(commaArray[PPos], out nextInt))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not an Integer on Record " + k;
                return (bSuccess);
            }
            else
                newCCTool.Metric = nextInt;

            // Notes   Null is ok
            PPos++;
            newCCTool.Notes = commaArray[PPos];

            // Machine   Null is ok
            PPos++;
            newCCTool.Machine = commaArray[PPos];

            // Material   Null is ok
            PPos++;
            newCCTool.Material = commaArray[PPos];

            // PlungeRate
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return (bSuccess);
            }
            else
                newCCTool.Plungerate = nextDecimal;

            // FeedRate
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return(bSuccess);
            }
            else
                newCCTool.Feedrate = nextDecimal;

            // RPM
            PPos++;
            if (!Int32.TryParse(commaArray[PPos], out nextInt))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not an Integer on Record " + k;
                return (bSuccess);
            }
            else
                newCCTool.Rpm = nextInt;

            // Depth
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return (bSuccess);
            }
            else
                newCCTool.Depth = nextDecimal;

            // Cutpower    Null is ok
            PPos++;
            newCCTool.Cutpower = commaArray[PPos];

            // FinishAllowance
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return(bSuccess);
            }
            else
                newCCTool.Finishallowance = nextDecimal;

            // 3dStepover
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return (bSuccess);
            }
            else
                newCCTool.Stepover3d = nextDecimal;

            // 3dSFeedRate
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return(bSuccess);
            }
            else
                newCCTool.Feedrate3d = nextDecimal;

            // 3dRpm
            PPos++;
            if (!Decimal.TryParse(commaArray[PPos], out nextDecimal))
            {
                errMSgStr = "Param " + headerArray[PPos] + " Value " + commaArray[PPos] + " is not a Number on Record " + k;
                return(bSuccess);
            }
            else
                newCCTool.Rpm3d = nextDecimal;

            bSuccess = true;
            return (bSuccess);
        }

        #endregion
    }
}
