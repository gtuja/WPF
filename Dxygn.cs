

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0290 // Use primary constructor

using System.Xml.Linq;
using Util;

/**
 * @brief A namespace for processing doxygen results.
 * @see Dxygn.Xml
 * @see Dxygn.Report
 */
namespace Dxygn;

/**
 * @brief A public class holding doxygen xml component(tag).
 * @see Dxygn.Xml.Compound
 * @see Dxygn.Xml.Member
 * @see Dxygn.Xml.MemberDef
 */
public class Xml
{
  public class Compound
  {
    public static readonly String Name = @"compound";
    
    public static class Attribute
    {
      public static class Name
      {
        public static readonly String RefId  = @"refid";
        public static readonly String Kind  = @"kind";
      }

      public static class Value
      {
        public static readonly String File  = @"file";
        public static readonly String Dir  = @"dir";
        public static readonly String Group  = @"group";
      };
    };
    public String strRefId;
    public String strKind;
    public XElement xeName;
    public List<XElement> lstMember;
    
    /**
    * @brief Constructor.
    */
    public Compound(
      String strRefId,
      String strKind,
      XElement xeName,
      List<XElement> lstMember
    )
    {
      this.strRefId = strRefId;
      this.strKind = strKind;
      this.xeName = xeName;
      this.lstMember = [];
      foreach(XElement xe in lstMember)
      {
        this.lstMember.Add(xe);
      }
    }
  };

  public class Member
  {
    public static readonly String Name = @"member";
      
    public static class Attribute
    {
      public static class Name
      {
        public static readonly String RefId  = @"refid";
        public static readonly String Kind  = @"kind";
      }

      public static class Value
      {
        public static readonly String Define  = @"define";
        public static readonly String Function  = @"function";
        public static readonly String Variable  = @"variable";
      };
    };

    public String strRefId;
    public String strKind;
    public XElement xeName;
    
    /**
    * @brief Constructor.
    */
    public Member(
      String strRefId,
      String strKind,
      XElement xeName
    )
    {
      this.strRefId = strRefId;
      this.strKind = strKind;
      this.xeName = xeName;
    }
  };

  public class MemberDef
  {
    public static readonly String Name = @"memberdef";
    public static class Attribute
    {
      public static class Name
      {
        public static readonly String Kind  = @"kind";
        public static readonly String Id  = @"id";
        public static readonly String Prot = @"prot";
        public static readonly String Static = @"static";
      }
      public static class Value
      {
        public static readonly String Define  = @"define";
        public static readonly String Function  = @"function";
        public static readonly String Variable  = @"variable";
      };
    };

    public class Location
    {
      public static readonly String Name = @"location";
      public static class Attribute
      {
        public static class Name
        {
          public static readonly String File  = @"file";
          public static readonly String Line  = @"line";
          public static readonly String Column = @"column";
          public static readonly String BodyFile = @"bodyfile";
          public static readonly String BodyStart = @"bodystart";
          public static readonly String BodyEnd = @"bodyend";
        };
      };
      public String strFile;
      public String strLine;
      public String strColumn;
      public String strBodyFile;
      public String strBodyStart;
      public String strBodyEnd;

      /**
      * @brief Constructor.
      */
      public Location(
        String strFile, 
        String strLine,
        String strColumn,
        String strBodyFile,
        String strBodyStart,
        String strBodyEnd
      )
      {
        this.strFile = strFile; 
        this.strLine = strLine;
        this.strColumn = strColumn;
        this.strBodyFile = strBodyFile;
        this.strBodyStart = strBodyStart;
        this.strBodyEnd = strBodyEnd;
      }
    };

    public class Reference
    {
      public static readonly String NameRef = @"references";
      public static readonly String NameRefBy = @"referencedby";
      public static class Attribute
      {
        public static class Name
        {
          public static readonly String RefId  = @"refid";
          public static readonly String CompoundRef  = @"compoundref";
          public static readonly String StartLine = @"startline";
          public static readonly String EndLine = @"endline";
        };
      };
      public String strRefId;
      public String strCompoundRef;
      public String strStartLine;
      public String strEndLine;
      public String strValue;

      /**
      * @brief Constructor.
      */
      public Reference(
        String strRefId,
        String strCompoundRef,
        String strStartLine,
        String strEndLine,
        String strValue
      )
      {
        this.strRefId = strRefId;
        this.strCompoundRef = strCompoundRef;
        this.strStartLine = strStartLine;
        this.strEndLine = strEndLine;
        this.strValue = strValue;
      }
    };

    public String strKind;
    public String strId;
    public XElement xeName;
    public XElement xeDefinition;
    public XElement xeArgsstring;
    public Location locLocation;
    public List<Reference> refReferences;
    public List<Reference> refReferencedby;

    /**
    * @brief Constructor.
    */
    public MemberDef(
      String strKind,
      String strId,
      XElement xeName,
      XElement xeDefinition,
      XElement xeArgsstring,
      Location locLocation,
      List<Reference> refReferences,
      List<Reference> refReferencedby
    )
    {
      this.strKind = strKind;
      this.strId = strId;
      this.xeName = xeName;
      this.xeDefinition = xeDefinition;
      this.xeArgsstring = xeArgsstring;
      this.locLocation = locLocation;
      this.refReferences = [];
      this.refReferencedby = [];
      foreach(Reference r in refReferences)
      {
        this.refReferences.Add(r);
      }
      foreach(Reference r in refReferencedby)
      {
        this.refReferencedby.Add(r);
      }
    }
  };
};

/**
 * @brief A public class for reporting doxygen output.
 * @see Dxygn.Report.Item
 * @see Dxygn.Report.Reference
 */
public class Report
{
  public class Reference
  {
    public String strName;
    public String strModule;

    /**
    * @brief Constructor.
    */
    public Reference(
      String strName,
      String strModule
    )
    {
      this.strName = strName;
      this.strModule = strModule;
    }
    public override String ToString()
    {
      return this.strName + "," + this.strModule + ",";
    }
  };

  public class Item
  {
    public String strId;
    public String strFile;
    public String strKind;
    public String strName;
    public String strBodyStart;
    public String strBodyEnd;
    public String strModule;
    public List<Reference> lstRef;
    public List<Reference> lstRefBy;

    /**
    * @brief Constructor.
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
        lstReturn.Add(strItemCsv + u16Index.ToString() + "," +  /* Count */
                                   r.strName + "," +            /* Name */
                                   r.strModule + "," +          /* Module */
                                   ",,,");  /* Count,Name,Module,[RefBy] */
      }

      u16Index = 0;
      foreach(Reference r in this.lstRef)
      {
        u16Index++;
        lstReturn.Add(strItemCsv + ",,," +  /* Count,Name,Module,[Ref] */
                                   u16Index.ToString() + "," +  /* Count */
                                   r.strName + "," +            /* Name */
                                   r.strModule + ",");          /* Module */
      }
      return lstReturn;
    }
  };
};
