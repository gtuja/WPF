

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable IDE0290 // Use primary constructor

using System.Net.Http;
using System.Xml.Linq;
using Util;

/**
 * @brief A namespace for processing doxygen results.
 * @see Dxygn.Xml.Tag
 */
namespace Dxygn;

/**
 * @brief A public static class holding doxygen constants.
 * @see Dxygn.Xml.Tag
 * @see Dxygn.Xml.Attribute
 * @see Dxygn.Xml.Attribute
 */
public class Xml
{
  public class Tag
  {
    public class Compound
    {
      static readonly String Name = @"compound";
      
      static class Attribute
      {
        static class Name
        {
          static readonly String RefId  = @"refid";
          static readonly String Kind  = @"kind";
        }

        static class Value
        {
          static readonly String File  = @"file";
          static readonly String Dir  = @"dir";
          static readonly String Group  = @"group";
        };
      };

      public String strRefId;
      public String strKind;
      public XElement xeName;
      public List<XElement> lstMember;

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
      static readonly String Name = @"member";
      
      static class Attribute
      {
        static class Name
        {
          static readonly String RefId  = @"refid";
          static readonly String Kind  = @"kind";
        }

        static class Value
        {
          static readonly String Define  = @"define";
          static readonly String Function  = @"function";
          static readonly String Variable  = @"variable";
        };
      };

      public String strRefId;
      public String strKind;
      public XElement xeName;

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
      static readonly String Name = @"memberdef";
      static class Attribute
      {
        static class Name
        {
          static readonly String Kind  = @"kind";
          static readonly String Id  = @"id";
          static readonly String Prot = @"prot";
          static readonly String Static = @"static";
        }

        static class Value
        {
          static readonly String Define  = @"define";
          static readonly String Function  = @"function";
          static readonly String Variable  = @"variable";
        };
      };

      public class Location
      {
        static readonly String Name = @"location";
        static class Attribute
        {
          static class Name
          {
            static readonly String File  = @"file";
            static readonly String Line  = @"line";
            static readonly String Column = @"column";
            static readonly String BodyFile = @"bodyfile";
            static readonly String BodyStart = @"bodystart";
            static readonly String BodyEnd = @"bodyend";
          };
        };
        public String strFile;
        public String strLine;
        public String strColumn;
        public String strBodyFile;
        public String strBodyStart;
        public String strBodyEnd;

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
        static readonly String NameRef = @"references";
        static readonly String NameRefBy = @"referencedby";
        static class Attribute
        {
          static class Name
          {
            static readonly String RefId  = @"refid";
            static readonly String CompoundRef  = @"compoundref";
            static readonly String StartLine = @"startline";
            static readonly String EndLine = @"endline";
          };
        };
        public String strRefId;
        public String strCompoundRef;
        public String strStartLine;
        public String strEndLine;
        public String strValue;

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
};
