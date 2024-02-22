

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
 * @brief A public class holding doxygen xml components, e.g. Compound, Member, MemberDef.
 * @see Dxgn.Xml.Compound
 * @see Dxgn.Xml.Member
 * @see Dxgn.Xml.MemberDef
 */
public class Xml
{
  /**
  * @brief A public class holding doxygen xml component, i.e. Compound tag.
  * @see Dxgn.Xml.Compound
  * @see Dxgn.Xml.Member
  */
  public class Compound
  {
    public static readonly String Name = @"compound";
    
    /**
    * @brief A public static class holding constants of attributes on Compound tag.
    * @see Dxgn.Xml.Compound
    * @see Dxgn.Xml.Member
    */
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

    public readonly String strRefId;  /**< A public String object holding the id of a Compound tag. */
    public readonly String strKind;  /**< A public String object holding the kind of a Compound tag. */
    public readonly XElement xeName;  /**< A public XElement object holding the name element of a Compound tag. */
    public readonly List<XElement> lstMember;  /**< A public list of XElement object holding member elements of a Compound tag. */
    
    /**
    * @brief Constructor.
    * @param strRefId A String object holding the id of a Compound tag.
    * @param strKind A String object holding the kind of a Compound tag.
    * @param xeName A XElement object holding the name element of a Compound tag.
    * @param lstMember A list of XElement object holding member elements of a Compound tag.
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

  /**
  * @brief A public class holding doxygen xml component, i.e. Member, in a Compound tag.
  * @see Dxgn.Xml.Compound
  */
  public class Member
  {
    public static readonly String Name = @"member";
      
    /**
    * @brief A public static class holding constants of attributes on Member tag.
    * @see Dxgn.Xml.Compound
    */
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

    public readonly String strRefId;  /**< A public String object holding the id of a Member tag. */
    public readonly String strKind;  /**< A public String object holding the kind of a Member tag. */
    public readonly XElement xeName;  /**< A public String object holding the name element of a Member tag. */
    
    /**
    * @brief Constructor.
    * @param strRefId A String object holding the id of a Member tag.
    * @param strKind A String object holding the kind of a Member tag.
    * @param xeName A XElement object holding the name element of a Member tag.
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

  /**
  * @brief A public static class holding constants of attributes on MemberDef tag.
  * @see Dxgn.Xml.MemberDef.Location
  * @see Dxgn.Xml.MemberDef.Reference
  */
  public class MemberDef
  {
    public static readonly String Name = @"memberdef";

    /**
    * @brief A public static class holding constants of attributes on MemberDef tag.
    * @see Dxgn.Xml.Compound
    */
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

    /**
    * @brief A public class holding information of an element, i.e. Location, on a MemberDef tag.
    * @see Dxgn.Xml.MemberDef
    */
    public class Location
    {
      public static readonly String Name = @"location";
      
      /**
      * @brief A public static class holding constants of attributes of a element Location on MemberDef tag.
      * @see Dxgn.Xml.MemberDef
      */
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
      public readonly String strFile;  /**< A public String object holding the file name of a Location tag. */
      public readonly String strLine;  /**< A public String object holding the line of a Location tag. */
      public readonly String strColumn;  /**< A public String object holding the column of a Location tag. */
      public readonly String strBodyFile;  /**< A public String object holding the file name of a Location tag. */
      public readonly String strBodyStart;  /**< A public String object holding the start line of a Location tag. */
      public readonly String strBodyEnd;  /**< A public String object holding the end line of a Location tag. */

      /**
      * @brief Constructor.
      * @param strFile A String object holding the file name of a Location tag.
      * @param strLine A String object holding the line of a Location tag.
      * @param strColumn A String object holding the column of a Location tag.
      * @param strBodyFile A String object holding the file name of a Location tag.
      * @param strBodyStart A String object holding the start line of a Location tag.
      * @param strBodyEnd A String object holding the end line of a Location tag.
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

    /**
    * @brief A public class holding information of an element, i.e. References, ReferencedBy, on a MemberDef tag.
    * @see Dxgn.Xml.MemberDef
    */
    public class Reference
    {
      public static readonly String NameRef = @"references";
      public static readonly String NameRefBy = @"referencedby";
      
      /**
      * @brief A public static class holding constants of attributes of a element, i.e. References, ReferencedBy, on MemberDef tag.
      * @see Dxgn.Xml.MemberDef
      */
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

      public readonly String strRefId;  /**< A public String object holding the id of a Reference or ReferencedBy tag. */
      public readonly String strCompoundRef;  /**< A public String object holding the CompoundRef of a Reference or ReferencedBy tag. */
      public readonly String strStartLine;  /**< A public String object holding the file start line of a Reference or ReferencedBy tag. */
      public readonly String strEndLine;  /**< A public String object holding the end line of a Reference or ReferencedBy tag. */
      public readonly String strValue;  /**< A public String object holding the value of a Reference or ReferencedBy tag. */

      /**
      * @brief Constructor.
      * @param strRefId A String object holding the id of a Reference or ReferencedBy tag.
      * @param strCompoundRef A String object holding the CompoundRef of a Reference or ReferencedBy tag.
      * @param strStartLine A String object holding the file start line of a Reference or ReferencedBy tag.
      * @param strEndLine A String object holding the file end line of a Reference or ReferencedBy tag.
      * @param strValue A String object holding the value of a Reference or ReferencedBy tag.
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

    public readonly String strKind;  /**< A public String object holding the kind of a MemberDef tag. */
    public readonly String strId;  /**< A public String object holding the id of a MemberDef tag. */
    public readonly XElement xeName;  /**< A public XElement object holding the name element of a MemberDef tag. */
    public readonly XElement xeDefinition;  /**< A public XElement object holding the Definition element of a MemberDef tag. */
    public readonly XElement xeArgsstring;  /**< A public XElement object holding the Argsstring element of a MemberDef tag. */
    public readonly Location locLocation;  /**< A public Location object holding the Location element of a MemberDef tag. */
    public readonly List<Reference> lstReferences;  /**< A public list of Reference object holding references of a MemberDef tag. */
    public readonly List<Reference> lstReferencedby;  /**< A public list of Reference object holding referenced by of a MemberDef tag. */

    /**
    * @brief Constructor.
    * @param strKind A String object holding the kind of a MemberDef tag.
    * @param strId A String object holding the id of a MemberDef tag.
    * @param xeName A XElement object holding the name element of a MemberDef tag.
    * @param xeDefinition A XElement object holding the Definition element of a MemberDef tag.
    * @param xeArgsstring A XElement object holding the Argsstring element of a MemberDef tag.
    * @param locLocation A Location object holding the Location element of a MemberDef tag.
    * @param lstReferences A list of Reference object holding references of a MemberDef tag.
    * @param refReferencedby A list of Reference object holding referenced by of a MemberDef tag.
    */
    public MemberDef(
      String strKind,
      String strId,
      XElement xeName,
      XElement xeDefinition,
      XElement xeArgsstring,
      Location locLocation,
      List<Reference> lstReferences,
      List<Reference> lstReferencedby
    )
    {
      this.strKind = strKind;
      this.strId = strId;
      this.xeName = xeName;
      this.xeDefinition = xeDefinition;
      this.xeArgsstring = xeArgsstring;
      this.locLocation = locLocation;
      this.lstReferences = [];
      this.lstReferencedby = [];
      foreach(Reference r in lstReferences)
      {
        this.lstReferences.Add(r);
      }
      foreach(Reference r in lstReferencedby)
      {
        this.lstReferencedby.Add(r);
      }
    }
  };
};
