

using System.Windows.Controls;
using System.Xml.Linq;
using System.Reflection;
using Util;
using System.Windows;
using System.ComponentModel;

#pragma warning disable IDE1006 // Naming Styles

/**
 * @brief A namespace for processing doxygen results.
 * @see Dxgn.Xml
 * @see Dxgn.Report
 */
namespace Dxgn
{
  /**\
   * @brief A public class for reporting reference fr\om doxygen output.
   * @see Dxgn.Report.Item
   */
  public class Reference(
    String strName,
    String strModule
    )
  {
    private readonly String strName = strName;      /**< A private String object holding the name of a reference. */
    private readonly String strModule = strModule;  /**< A private String object holding the module of a reference. */

    /**
     * @brief A public method to to represent a Reference.
     * @param ReportType A Report.enuReportType object of ReportType.
     * @return A String object to represent a Reference with ReportType.
     */
    public String strToString(
      Report.enuReportType ReportType
      )
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
    private readonly String strId;              /**< A private String object holding the id of an item. */
    private readonly String strFile;            /**< A private String object holding the file name of an item. */
    private readonly String strKind;            /**< A private String object holding the kind of an item. */
    private readonly String strName;            /**< A private String object holding the name of an item. */
    private readonly String strBodyStart;       /**< A private String object holding the start line of an item. */
    private readonly String strBodyEnd;         /**< A private String object holding the end line of an item. */
    private readonly String strModule;          /**< A private String object holding the module name, e.g. file name, of an item. */
    private readonly List<Reference> lstRef;    /**< A private list of Reference object holding the references of an item. */
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

  /**
  * @brief A public class for reporting doxygen output.
  * @see Dxgn.Report.Reference
  * @see Dxgn.Report.Item
  */
  public class Report : Task.Background
  {
    public enum enuReportType : ushort
    {
      CSV = 0,
    };

    public static readonly Int32 s32ReturnInvalid = -1; /* A public static Int32 object represents invalid return of method. */
    private readonly Dictionary<String, Dxgn.Xml.Compound> dicCompound;  /**< A dictionary object of Compound object. */
    private readonly Dictionary<String, Dxgn.Xml.MemberDef> dicMemberDef;  /**< A dictionary object of MemberDef object. */
    private readonly String strPathSource;
    private readonly String strPathXml;
    private readonly Int32 s32ProgressMax;  /**< A Int32 object holding the count to progress. */

    public Report(
      String strId,
      String strPathSource,
      String strPathXml
    ) : base (strId)
    {
      this.dicCompound = [];  /**< A dictionary object of Compound object. */
      this.dicMemberDef = [];  /**< A dictionary object of MemberDef object. */
      this.strPathSource = strPathSource;
      this.strPathXml = strPathXml;
      this.s32ProgressMax = s32GetProgressMax(strPathXml);  /**< A Int32 object holding the count to progress. */
    }

    /**
    * @brief A private method to get the count to progress.
    * @param strPathXml A String object of the path of xml output from doxygen.
    * @return s32Count A Int32 object holding the count to progress, shall be s32ReturnInvalid[-1] if any exception. 
    */
    private Int32 s32GetProgressMax(
      String strPathXml
    )
    {
      String strFileIndex = strPathXml + @"\" + Dxgn.Xml.strIndexFile;
      Int32 s32Count = s32ReturnInvalid;
      List<XElement> lstElement;
      List<String>lstException = [];
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
        lstException.Add(e.ToString());
        foreach(String exception in lstException)
        {
          vidOnWorkerLog(this, new Task.TaskEventLogArgs(this.strId?? String.Empty, "[Exception][" + Util.Debug.strGetMethodNme() + "] " + exception.ToString()));
        }
        s32Count = s32ReturnInvalid;  /* Return s32ReturnInvalid[-1], if there was any exception.*/
      }
      return s32Count;
    }

    protected override void vidDoWork(
      object? sender,
      DoWorkEventArgs e
      )
    {
      String strFileIndex = strPathXml + "\\" + Dxgn.Xml.strIndexFile;
      List<XElement> lstElementCompound;
      List<String> lstException = [];
      Int32 s32Progress;
      Int32 s32Index;

      vidOnWorkerEntry(this, new Task.TaskEventEntryArgs(this.strId?? String.Empty, Task.Constants.strCancel, this.s32ProgressMax, @"[" + this.strId + @"] : invoke event entry..."));
      lstElementCompound = Util.Xml.lstGetDescendants(strFileIndex, @"compound", lstException);
      vidOnWorkerLog(this, new Task.TaskEventLogArgs(this.strId?? String.Empty, "## Started extracting compounds(" + lstElementCompound.Count + ")..."));
      List<String> lstPathXml = [.. System.IO.Directory.GetFiles(strPathXml,
                                                                  "*.xml",
                                                                  System.IO.SearchOption.AllDirectories)];
      s32Progress = 0;
      s32Index = 0;
      foreach(XElement xe in lstElementCompound)
      {
        s32Progress++;
        s32Index++;
        /* Code needed, extracting compound from index.xml */
        System.Threading.Thread.Sleep(100);
        vidOnWorkerProgress(this, new Task.TaskEventProgressArgs(this.strId?? String.Empty, s32Progress, @"Extracting compounds [" + s32Index + "/" + lstElementCompound.Count + "]"));
      }

      vidOnWorkerLog(this, new Task.TaskEventLogArgs(this.strId?? String.Empty, "## Started extracting memberdefs on xml files(" + lstPathXml.Count + ")..."));
      s32Index = 0;
      foreach(String str in lstPathXml)
      {
        s32Progress++;
        s32Index++;
        /* Code needed, extracting memberdefs from *.xml */
        System.Threading.Thread.Sleep(100);
        vidOnWorkerProgress(this, new Task.TaskEventProgressArgs(this.strId?? String.Empty, s32Progress, @"Extracting memberdefs [" + s32Index + "/" + lstPathXml.Count + "]"));
      }

      vidOnWorkerLog(this, new Task.TaskEventLogArgs(this.strId?? String.Empty, "## Saving memberdefs on xml files(" + lstPathXml.Count + ")..."));
      s32Index = 0;
      foreach(String str in lstPathXml)
      {
        s32Progress++;
        s32Index++;
        /* Code needed, saving memberdef items on *.xml */
        System.Threading.Thread.Sleep(100);
        vidOnWorkerProgress(this, new Task.TaskEventProgressArgs(this.strId?? String.Empty, s32Progress, @"Saving memberdefs[" + s32Index + "/" + lstPathXml.Count + "]"));
      }

      foreach(String exception in lstException)
      {
        vidOnWorkerLog(this, new Task.TaskEventLogArgs(this.strId?? String.Empty, "[Exception][" + Util.Debug.strGetMethodNme() + "] " + exception.ToString()));
      }
    }
  };
};
