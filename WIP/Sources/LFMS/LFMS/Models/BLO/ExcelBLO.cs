using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

namespace LFMS.Models.BLO
{
  
    public enum TemplatePattern
    {
        HEAD,
        LIST,
        FULL
    };



    public class ExcelBLO : IDisposable
    {
        #region 

        public const string START_ROW = "StartRow";
        public const string STEP = "Step";
        public const string KEY = "Key";
        public const string STEP_NODE_NAME = "STEP";

        #endregion

        
        TemplatePattern _templatePattern;

        //Excel
        Application _excelApplication;

        //Excel Workbook
        Workbook _workbook;

        //ExcelWorksheet
        Worksheet _worksheet;

        string _templatePath;

        string _templateSheetName;

        string _mappingPath;

        //HEAD
        Dictionary<string, string> _headMap;

        //LIST
        Dictionary<string, string> _listMap;

        int _startRow;

        int _step;

        string _keyColumn;

        int _keyStep;

        #region 

     
        public ExcelBLO(TemplatePattern pattern, string mappingPath, string templatePath, string templateSheetName)
        {

            _mappingPath = mappingPath;
            _templatePath = templatePath;
            _templateSheetName = templateSheetName;
            _templatePattern = pattern;

           
            _excelApplication = new Application();
        }

     
        public void Dispose()
        {

            
            CloseExcelApplication();

        }

        #endregion

        #region 

        private Boolean FileIsExist(string filePath)
        {
            try
            {
                return File.Exists(filePath);
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

        
        private Boolean FileIsOpening(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                FileStream stream = null;
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        private void OpenExcelFile()
        {
            try
            {
                if (!FileIsExist(_templatePath))
                {
                    throw new Exception("temple error");
                    //return;
                }

                if (_excelApplication.Workbooks.Count == 0)
                {
                    _workbook = _excelApplication.Workbooks.Open(_templatePath, Type.Missing, false);
                }

                _worksheet = _excelApplication.Workbooks[1].Worksheets[_templateSheetName];

            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }


    
        private void CopyTemplateSheet()
        {
            try
            {

                Workbook newWorkbook = _excelApplication.Workbooks.Add();

                _worksheet.Copy(newWorkbook.Worksheets[1]);


                _workbook.Saved = true;
                _workbook.Close();

                _workbook = newWorkbook;
                _worksheet = _workbook.Worksheets[1];

            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

 
        private void CloseExcelApplication()
        {
            try
            {
                if (_excelApplication != null)
                {

                    if (_workbook != null)
                    {
                        _workbook.Saved = true;
                        _workbook.Close();
                    }

                    _excelApplication.Quit();
                    _excelApplication = null;

                }
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        private XmlNodeList ReadXmlByTag(string tagName, string path)
        {
            XmlNodeList nodelist = null;

            try
            {
                string xmlPath = path;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                nodelist = xmlDoc.GetElementsByTagName(tagName);

            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }

            return nodelist;
        }

       
        private void GetHeadMapping()
        {
            try
            {
                _headMap = new Dictionary<string, string>();

                XmlNodeList headNodeList = ReadXmlByTag(TemplatePattern.HEAD.ToString(), _mappingPath);

                if (headNodeList == null)
                {
                    throw new Exception("Head mapping file is not valid ");
                }

                XmlNode headNode = headNodeList[0];

                foreach (XmlNode dtoNode in headNode)
                {
                    
                    string dtoName = dtoNode.Name;

                    foreach (XmlNode propertyNode in dtoNode.ChildNodes)
                    {
                        string propertyName = propertyNode.Name;
                        string headNodeKey = string.Format("{0}.{1}", dtoName, propertyName);

                        string cellId = propertyNode.InnerText;

                        _headMap.Add(headNodeKey, cellId);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

     
        private void GetListMapping()
        {
            try
            {
                _listMap = new Dictionary<string, string>();

                XmlNodeList listNodeList = ReadXmlByTag(TemplatePattern.LIST.ToString(), _mappingPath);

                if (listNodeList == null)
                {
                    throw new Exception("List mapping file is not valid");
                }

                XmlNode listNode = listNodeList[0];

                foreach (XmlNode StepNode in listNode)
                {
                    switch (StepNode.Name)
                    {
                        case START_ROW:
                            int startRow = Convert.ToInt32(StepNode.InnerText.Trim());
                            _startRow = startRow;
                            break;

                        case STEP:
                            int step = Convert.ToInt32(StepNode.InnerText.Trim());
                            _step = step;
                            break;

                        case KEY:
                            _keyColumn = StepNode.ChildNodes[0].InnerText.Trim();
                            _keyStep = Convert.ToInt32(StepNode.ChildNodes[1].InnerText.Trim());
                            break;

                        default:
                            string stepNodeName = StepNode.Name;
                            string stepIndex = stepNodeName.Replace(STEP_NODE_NAME, string.Empty).Trim();

                            SetNodeListMapping(StepNode, stepIndex);

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

        
        private void SetNodeListMapping(XmlNode stepNode, string stepIndex)
        {
            try
            {
                foreach (XmlNode dtoNode in stepNode.ChildNodes)
                {
                    string dtoName = dtoNode.Name;
                    foreach (XmlNode propertyNode in dtoNode.ChildNodes)
                    {
                        string keyFormat = "{0}.{1}.{2}";
                        string propertyName = propertyNode.Name;
                        string listNodeKey = string.Format(keyFormat, stepIndex, dtoName, propertyName);

                        string listNodeValue = propertyNode.InnerText;

                        _listMap.Add(listNodeKey, listNodeValue);

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }


        private void GetMapping()
        {
            try
            {
                if (!FileIsExist(_mappingPath))
                {
                    return;
                }

                if (_templatePattern == TemplatePattern.HEAD)
                {
                    GetHeadMapping();
                }

                if (_templatePattern == TemplatePattern.LIST)
                {
                    GetListMapping();
                }

                if (_templatePattern == TemplatePattern.FULL)
                {
                    GetHeadMapping();
                    GetListMapping();
                }
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }


  
        private Dictionary<string, string> CreateFixMap(Dictionary<string, string> listMap, int loopIndex)
        {
            try
            {
                Dictionary<string, string> fixMap = new Dictionary<string, string>();

                foreach (var pair in listMap)
                {
                    string listMapKey = pair.Key;
                    string listMapValue = pair.Value;

                    string[] parts = listMapKey.Split('.');

                    string keyFormat = "{0}.{1}";
                    string fixMapKey = string.Format(keyFormat, parts[1], parts[2]);

                    int stepIndex = Convert.ToInt32(parts[0].Replace(STEP_NODE_NAME, string.Empty).Trim());
                    int rowId = loopIndex + stepIndex;
                    string fixMapValue = String.Format("{0}{1}", listMapValue, rowId);

                    fixMap.Add(fixMapKey, fixMapValue);

                }
                return fixMap;
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

  
        private Dictionary<string, object> CreateObjectMap(Dictionary<string, IList> listObjectMap, int index)
        {
            Dictionary<string, object> objectMap = new Dictionary<string, object>();

            foreach (var pair in listObjectMap)
            {

                string objectType = pair.Key;


                object dto;

                if ((pair.Value.Count) <= index)
                {
                    Type t = pair.Value[0].GetType();
                    dto = Activator.CreateInstance(t);

                    listObjectMap[objectType].Add(dto);
                }
                else
                {
                    dto = (pair.Value)[index];
                }

                objectMap.Add(objectType, dto);
            }
            return objectMap;
        }

    
        private void GetDataOfFixArea(Dictionary<string, string> mapping, Dictionary<string, object> objectMap)
        {
            try
            {
                foreach (var pair in mapping)
                {
                    string[] parts = pair.Key.Split('.');
                    string dtoTypeName = parts[0];
                    string propertyName = parts[1];
                    string cellId = pair.Value;

                    object dataObject = objectMap[dtoTypeName];

                    PropertyInfo property = dataObject.GetType().GetProperty(propertyName);

                    string cellValue = Convert.ToString(_worksheet.get_Range(cellId).Value);
                    property.SetValue(dataObject, cellValue, null);

                }

            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }


        private void SetDataToFixArea(Dictionary<string, string> mapping, Dictionary<string, object> objectMap)
        {
            try
            {

                foreach (var pair in mapping)
                {
                    string[] parts = pair.Key.Split('.');
                    string dtoTypeName = parts[0];
                    string propertyName = parts[1];
                    string cellId = pair.Value;

                    object dataObject = objectMap[dtoTypeName];

                    PropertyInfo property = dataObject.GetType().GetProperty(propertyName);

                    _worksheet.get_Range(cellId).Value = property.GetValue(dataObject, null);

                }
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

      
        private void GetDataOfListArea(Dictionary<string, string> listMap, Dictionary<string, IList> listObjectMap)
        {
            try
            {

                Range usedRange = _worksheet.UsedRange;
                int usedRowCount = usedRange.Rows.Count;

                int loopNumber = 0;

                for (int i = _startRow; i < usedRowCount; i += _step)
                {
                    Dictionary<string, object> objectMap = new Dictionary<string, object>();
                    Dictionary<string, string> fixMap = new Dictionary<string, string>();

                    int loopIndex = (_startRow - 1) + loopNumber * _step;

                    var keyData = usedRange.Cells[loopIndex + _keyStep, _keyColumn];

                    if (keyData.Value2 == null)
                        break;


                    fixMap = CreateFixMap(listMap, loopIndex);

                    objectMap = CreateObjectMap(listObjectMap, loopNumber);

                    //
                    GetDataOfFixArea(fixMap, objectMap);
                    loopNumber++;

                }

            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }


   
        private void SetDataToListArea(Dictionary<string, string> listMap, Dictionary<string, IList> listObjectMap, bool isAppend = false)
        {
            try
            {
                int indexToCopyRow = _startRow;

                IList listObject = listObjectMap.Values.First();

                for (int i = 0; i < listObject.Count; i++)
                {
                    Dictionary<string, object> objectMap = new Dictionary<string, object>();
                    Dictionary<string, string> fixMap = new Dictionary<string, string>();

                    int insertRowCount;

                    if (isAppend)
                    {
                        insertRowCount = listObject.Count;
                    }
                    else
                    {
                        insertRowCount = listObject.Count - 1;
                    }

                    if (i < insertRowCount)
                    {
                        string range = "{0}:{1}";
                        range = string.Format(range, indexToCopyRow, indexToCopyRow + _step - 1);
                        _worksheet.Rows[range].Select();
                        _worksheet.Rows[range].Copy();
                        _worksheet.Rows[range].Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, false);
                    }

                    int loopIndex = (_startRow - 1) + i * _step;
                    fixMap = CreateFixMap(listMap, loopIndex);

                    objectMap = CreateObjectMap(listObjectMap, i);

                    SetDataToFixArea(fixMap, objectMap);

                    indexToCopyRow = indexToCopyRow + _step;
                }
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }


        private void SetDataObjectsToHeadTemplate(bool isCopyTemplate = true, params object[] dataObjects)
        {
            try
            {

                Dictionary<string, object> headObjects = new Dictionary<string, object>();

                foreach (var headObject in dataObjects)
                {
                    headObjects.Add(headObject.GetType().Name.ToString(), headObject);
                }

                GetMapping();

                if (isCopyTemplate)
                {
                    OpenExcelFile();

                    CopyTemplateSheet();
                }

                SetDataToFixArea(_headMap, headObjects);

            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

      
        public void SetDataListObjectsToListTemplate(IList<object> dataObjects, bool isCopyTemplate = true, bool isAppend = false)
        {
            try
            {
                Dictionary<string, IList> listObjects = new Dictionary<string, IList>();

                foreach (var listObject in dataObjects)
                {
                    IList list = (IList)listObject;
                    listObjects.Add(list[0].GetType().Name.ToString(), list);
                }

                GetMapping();

                if (isCopyTemplate)
                {
                    OpenExcelFile();

                    CopyTemplateSheet();

                    SetDataToListArea(_listMap, listObjects);
                }
                else
                {
                    SetDataToListArea(_listMap, listObjects, isAppend);
                }
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

        
        public void SetDataObjectsToFullTemplate(IList<object> listObjects, bool isAppend = false, params object[] headObjects)
        {
            try
            {
                if (isAppend)
                {
                    SetDataObjectsToHeadTemplate(false, headObjects);
                    SetDataListObjectsToListTemplate(listObjects, false, isAppend);
                }
                else
                {
                    SetDataObjectsToHeadTemplate(true, headObjects);
                    SetDataListObjectsToListTemplate(listObjects, false);
                }

                //ExportObjectsToHeadArea(savePath, headObjects);
                //ExportListObjectsToListArea(savePath, objectList);
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

     
        private void ErrorHandle()
        {
            CloseExcelApplication();
        }

        #endregion

        #region 

       
        public void ImportHeadAreaToObjects(params object[] objectList)
        {
            try
            {
                Dictionary<string, object> dataObjects = new Dictionary<string, object>();

                // paramsをループして、<Type, object>のマップを作成
                foreach (var objectSub in objectList)
                {
                    Type typeObj = objectSub.GetType();

                    if (dataObjects.ContainsKey(typeObj.Name))
                    {
                        continue;
                    }
                    dataObjects.Add(typeObj.Name.ToString(), objectSub);
                }

                GetMapping();

                OpenExcelFile();

                GetDataOfFixArea(_headMap, dataObjects);


            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

    
        public void ImportListAreaToListObjects(IList<IList> objectLists)
        {
            try
            {
                Dictionary<string, IList> listObjectMap = new Dictionary<string, IList>();

                foreach (var objectList in objectLists)
                {
                    listObjectMap.Add(objectList[0].GetType().Name.ToString(), objectList);
                }

                GetMapping();

                OpenExcelFile();

                GetDataOfListArea(_listMap, listObjectMap);

            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }


        
        public void ImportFullAreaToObjects(IList<IList> listObjects, params object[] headObjects)
        {
            try
            {
                ImportHeadAreaToObjects(headObjects);
                ImportListAreaToListObjects(listObjects);
                CloseExcelApplication();
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }


        
        public void ExportObjectsToHeadArea(string savePath, params object[] headObjects)
        {
            try
            {
                if (FileIsExist(savePath))
                {

                    _workbook = _excelApplication.Workbooks.Open(savePath, Type.Missing, false);

                    _worksheet = _excelApplication.Workbooks[1].Worksheets[_templateSheetName];

                    SetDataObjectsToHeadTemplate(false, headObjects);

                    _workbook.Save();
                }
                else
                {

                    SetDataObjectsToHeadTemplate(true, headObjects);

                    _workbook.SaveAs(savePath, Type.Missing, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                }

                CloseExcelApplication();
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

       
        public void ExportListObjectsToListArea(string savePath, IList<object> listObjects)
        {
            try
            {

                if (FileIsExist(savePath))
                {
                    _workbook = _excelApplication.Workbooks.Open(savePath, Type.Missing, false);

                    _worksheet = _excelApplication.Workbooks[1].Worksheets[_templateSheetName];

                    SetDataListObjectsToListTemplate(listObjects, false, true);

                    _workbook.Save();
                }
                else
                {
                    SetDataListObjectsToListTemplate(listObjects);

                    _workbook.SaveAs(savePath);
                }

                CloseExcelApplication();

            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

      
        public void ExportObjectsToFullArea(string savePath, IList<object> listObjects, params object[] headObjects)
        {
            try
            {
                if (FileIsExist(savePath))
                {

                    _workbook = _excelApplication.Workbooks.Open(savePath, Type.Missing, false);

                    _worksheet = _excelApplication.Workbooks[1].Worksheets[_templateSheetName];

                    SetDataObjectsToFullTemplate(listObjects, true, headObjects);

                    _workbook.Save();
                }
                else
                {
                    SetDataObjectsToFullTemplate(listObjects, false, headObjects);

                    _workbook.SaveAs(savePath);
                }

                CloseExcelApplication();
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

     
        public void PrintHeadTemplate(params object[] headObject)
        {
            try
            {
                SetDataObjectsToHeadTemplate(true, headObject);

                _worksheet.PrintOutEx();

                CloseExcelApplication();
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

        
        public void PrintListTemplate(IList<object> listObjects)
        {
            try
            {
                SetDataListObjectsToListTemplate(listObjects);

                _worksheet.PrintOutEx();

                CloseExcelApplication();
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

      
        public void PrintFullTemplate(IList<object> listObjects, params object[] headObject)
        {
            try
            {
                SetDataObjectsToFullTemplate(listObjects, false, headObject);

                _worksheet.PrintOutEx();

                CloseExcelApplication();
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

       
        public void PrintOnly()
        {
            try
            {
                OpenExcelFile();

                _worksheet.PrintOutEx();

                CloseExcelApplication();
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

     
        public Workbook GetOpenWorkbook()
        {
            try
            {
                return _workbook;
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

       
        public Worksheet GetWorksheet()
        {
            try
            {
                return _worksheet;
            }
            catch (Exception ex)
            {
                ErrorHandle();
                throw ex;
            }
        }

      
        public bool IsValidTemPlate(Dictionary<string, string> conditions)
        {
            try
            {
                OpenExcelFile();

                foreach (var condition in conditions)
                {
                    string cellValue = Convert.ToString(_worksheet.get_Range(condition.Key).Value);

                    if (!cellValue.Equals(condition.Value))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        #endregion
    }
}