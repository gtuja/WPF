

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0290 // Use primary constructor

using System.Xml.Linq;
using Util;

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
  /**
  * @brief A public class for reporting reference from doxygen output.
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
    * @brief A public override method ToString.
    * @return A String object to represent a Reference with csv style.
    */
    public override String ToString()
    {
      return this.strName + "," + this.strModule + ",";
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
    public List<String> ToCsv()
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
        lstReturn.Add(strItemCsv + u16Index.ToString() + "," +  /* Count, */
                                   r.ToString() +               /* Name,Module, */
                                   ",,,");                      /* Count,Name,Module,[RefBy] */
      }

      u16Index = 0;
      foreach(Reference r in this.lstRef)
      {
        u16Index++;
        lstReturn.Add(strItemCsv + ",,," +  /* Count,Name,Module,[Ref] */
                                   u16Index.ToString() + "," +  /* Count */
                                   r.ToString());               /* Name,Module, */
      }
      return lstReturn;
    }
  };
};
