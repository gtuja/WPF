

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0290 // Use primary constructor

using System.Windows.Controls;
using System.Xml.Linq;
using System.Reflection;
using Util;
using System.Windows;

/**
 * @brief A namespace for processing doxygen results.
 * @see Dxgn.Xml
 * @see Dxgn.Report
 */
namespace Dxgn;

/**
 * @brief A public class for reporting doxygen output.
 * @see Dxgn.Report.Reference
 * @see Dxgn.Report.Item
 */
public class Report
{
  public enum enuReportType : ushort
  {
    CSV = 0,
  };

  public static readonly Int32 s32ReturnInvalid = -1; /* A public static Int32 object represents invalid return of method. */

  /**\
   * @brief A public class for reporting reference fr\om doxygen output.
   * @see Dxgn.Report.Item
   */
  public class Reference
  {
    private String strName;  /**< A private String object holding the name of a reference. */
    public String strModule;  /**< A private String object holding the module name, e.g. file name, of a reference. */

    /**
     * @brief Constructor.
     * @param strName A String object of the name of a reference.
     * @param strName A strModule object of the module name, e.g. file name, of a reference.
     */
    public Reference(
      String strName,
      String strModule
    )
    {
      this.strName = strName;
      this.strModule = strModule;
    }

    /**
     * @brief A public method to to represent a Reference.
     * @return A String object to represent a Reference with csv style.
     */
    public String strToString(Report.enuReportType ReportType)
    {
      String strReturn = String.Empty;

      switch (ReportType)
      {
        case Report.enuReportType.CSV :
          strReturn = this.strName + "," + this.strModule + ",";
          break;
        default :
          strReturn = String.Empty;
          break;
      }
      return strReturn;
    }
  };

  /**
   * @brief A public class for reporting an item in doxygen output.
   * @see Dxgn.Report.Reference
   */
  public class Item
  {
    private readonly String strId;  /**< A private String object holding the id of an item. */
    private readonly String strFile;  /**< A private String object holding the file name of an item. */
    private readonly String strKind;  /**< A private String object holding the kind of an item. */
    private readonly String strName;  /**< A private String object holding the name of an item. */
    private readonly String strBodyStart;  /**< A private String object holding the start line of an item. */
    private readonly String strBodyEnd;  /**< A private String object holding the end line of an item. */
    private readonly String strModule;  /**< A private String object holding the module name, e.g. file name, of an item. */
    private readonly List<Reference> lstRef;  /**< A private list of Reference object holding the references of an item. */
    private readonly List<Reference> lstRefBy;  /**< A private list of Reference object holding the referenced by of an item. */

    /**
     * @brief Constructor.
     * @param strId A String object of the name of of an item.
     * @param strFile A String object holding the file name of an item.
     * @param strKind A String object holding the kind of an item.
     * @param strName A String object holding the name of an item.
     * @param strBodyStart A String object holding the start line of an item.
     * @param strBodyEnd A String object holding the end line of an item.
     * @param strModule A String object holding the module name, e.g. file name, of an item.
     * @param lstRef A list of Reference object holding the references of an item.
     * @param lstRefBy A list of Reference object holding the referenced by of an item.
     */
    public Item(
      String strId,
      String strFile,
      String strKind,
      String strName,
      String strBodyStart,
      String strBodyEnd,
      String strModule,
      List<Reference> lstRef,
      List<Reference> lstRefBy
    )
    {
      this.strId = strId;
      this.strFile = strFile;
      this.strKind = strKind;
      this.strName = strName;
      this.strBodyStart = strBodyStart;
      this.strBodyEnd = strBodyEnd;
      this.strModule = strModule;
      this.lstRef = [];
      foreach(Reference r in lstRef)
      {
        this.lstRef.Add(r);
      }
      this.lstRefBy = [];
      foreach(Reference r in lstRefBy)
      {
        this.lstRefBy.Add(r);
      }
    }

    /**
     * @brief A public method to get list of string for output(csv).
     * @return A list object holding string for csv.
     * @note The description of a CSV line is below.
     *  File,Kind,Name,BodyStart,BodyEnd,Module, [Item]
     *  Count,Name,Module, [Ref]
     *  Count,Name,Module, [RefBy]
     */
    public List<String> lstToCsv()
    {
      List<String> lstReturn = [];
      UInt16 u16Index;
      String strItemCsv = this.strFile + "," +
                          this.strKind + "," +
                          this.strName + "," +
                          this.strBodyStart + "," +
                          this.strBodyEnd + "," +
                          this.strModule + ",";
      if (this.lstRef.Count == 0 && this.lstRefBy.Count == 0)
      {
        lstReturn.Add(strItemCsv + ",,,,,,"); /* Count,Name,Module,[Ref, RefBy]*/
      }
      u16Index = 0;
      foreach(Reference r in this.lstRef)
      {
        u16Index++;
        lstReturn.Add(strItemCsv + u16Index.ToString() + "," +                /* Count, */
                                   r.strToString(Report.enuReportType.CSV) +  /* Name,Module, */
                                   ",,,");                                    /* Count,Name,Module,[RefBy] */
      }

      u16Index = 0;
      foreach(Reference r in this.lstRef)
      {
        u16Index++;
        lstReturn.Add(strItemCsv + ",,," +                                    /* Count,Name,Module,[Ref] */
                                   u16Index.ToString() + "," +                /* Count */
                                   r.strToString(Report.enuReportType.CSV));  /* Name,Module, */
      }
      return lstReturn;
    }
  };

  public Dictionary<String, Dxgn.Xml.Compound> dicCompound;  /**< A dictionary object of Compound object. */
  public Dictionary<String, Dxgn.Xml.MemberDef> dicMemberDef;  /**< A dictionary object of MemberDef object. */
  public Int32 s32CountProgress;  /**< A Int32 object holding the count to progress. */

  /**
   * @brief Constructor.
   */
  public Report()
  {
    this.dicCompound = [];
    this.dicMemberDef = [];
    this.s32CountProgress = -1;
  }

  /**
   * @brief A public method for getting the count to progress.
   * @param strPathXml A String object of the path of xml output from doxygen.
   * @param lstException A list of String object holding exception details.
   * @return s32Count A Int32 object holding the count to progress, shall be s32ReturnInvalid[-1] if any exception. 
   */
  public Int32 s32GetProgressCount(
    String strPathXml,
    List<String> lstException
  )
  {
    String strFileIndex = strPathXml + "\\" + Dxgn.Xml.strIndexFile;
    Int32 s32Count = s32ReturnInvalid;
    List<XElement> lstElement;

    try
    {
      lstElement = Util.Xml.lstGetDescendants(strFileIndex, @"compound", lstException);
      s32Count = lstElement.Count;  /* Add count for setup(dicCompound). */ 

      List<String> lstPathXml = [.. System.IO.Directory.GetFiles(strPathXml,
                                                         "*.xml",
                                                         System.IO.SearchOption.AllDirectories)];
      s32Count += (Int32)lstPathXml.Count;  /* Add count for set up(dicMemberDef). */
      s32Count += (Int32)lstPathXml.Count;  /* Add count for saving. */
    }
    catch (Exception e)
    {
      MethodBase? mb = MethodBase.GetCurrentMethod();
      if (lstException != null && mb != null )
      {
        lstException.Add(mb.ReflectedType + mb.Name + e.ToString());
      }
      s32Count = s32ReturnInvalid;  /* Return s32ReturnInvalid[-1], if there was any exception.*/
    }
    return s32Count;
  }

  /**
   * @brief A public method for executing.
   * @param strPathXml A String object of the path of xml output from doxygen.
   * @param pbProgress A ProgressBar object to refresh progress.
   * @param rtbLog A RichTextBox object to refresh log.
   * @param lstException A list of String object holding exception details.
   */
  public void vidExecute(
    String strPathXml,
    ProgressBar pbProgress,
    RichTextBox rtbLog,
    List<String> lstException
  )
  {
    String strFileIndex = strPathXml + "\\" + Dxgn.Xml.strIndexFile;
    List<XElement> lstElementCompound;

    try
    {
      UI.vidAppendLog(rtbLog, "### Started executing...");
      UI.vidAppendLog(rtbLog, "");
      UI.vidUpdateUI();

      lstElementCompound = Util.Xml.lstGetDescendants(strFileIndex, @"compound", lstException);

      List<String> lstPathXml = [.. System.IO.Directory.GetFiles(strPathXml,
                                                         "*.xml",
                                                         System.IO.SearchOption.AllDirectories)];
                                                         
      UI.vidAppendLog(rtbLog, "## Started extracting compounds(" + lstElementCompound.Count + ") from " + Dxgn.Xml.strIndexFile + "...");
      UI.vidUpdateUI();
      foreach(XElement xe in lstElementCompound)
      {
        UI.vidAppendLog(rtbLog, "[" + xe.Value + "] is in extracting...");
        pbProgress.Value++;
        UI.vidUpdateUI();
      }

      UI.vidAppendLog(rtbLog, "");
      UI.vidAppendLog(rtbLog, "## Started extracting memberdefs(" + lstPathXml.Count + ")...");
      UI.vidAppendLog(rtbLog, "");
      UI.vidUpdateUI();
      foreach(String str in lstPathXml)
      {
        UI.vidAppendLog(rtbLog, "[" + System.IO.Path.GetFileName(str) + "] is in processing...");
        pbProgress.Value++;
        UI.vidUpdateUI();
      }

      UI.vidAppendLog(rtbLog, "");
      UI.vidAppendLog(rtbLog, "## Started saving results(" + lstPathXml.Count + ")...");
      UI.vidAppendLog(rtbLog, "");
      UI.vidUpdateUI();
      foreach(String str in lstPathXml)
      {
        UI.vidAppendLog(rtbLog, "[" + System.IO.Path.GetFileName(str) + "] is in saving...");
        pbProgress.Value++;
        UI.vidUpdateUI();
      }
    }
    catch (Exception e)
    {
      MethodBase? mb = MethodBase.GetCurrentMethod();
      if (lstException != null && mb != null )
      {
        lstException.Add(mb.ReflectedType + mb.Name + e.ToString());
      }
    }
    UI.vidAppendLog(rtbLog, "### Execution is completed...");
    UI.vidAppendLog(rtbLog, "");
    UI.vidUpdateUI();
    MessageBox.Show( "Execution is completed...", @"Status", MessageBoxButton.OK , MessageBoxImage.Information);
    return;
  }
};
