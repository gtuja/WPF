using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

#pragma warning disable IDE1006 // Naming Styles

/**
 * @brief A namespace for common utility.
 * @see Util.UI
 * @see Util.Xml
 */
namespace Util;

/**
 * @brief A public class holding utilities for extracting XML, e.g. descendants, attribute, value, etc.
 * @see using System.Xml.Linq
 * @see using System.Reflection
 */
public class Xml
{
  /**
   * @brief A public class holding an attribute of a tag.
   * @param strName A String object holding the name of an attribute.
   * @param strValue A String object holding the value of an attribute.
   */
  public class Attribute
  {
    public String strName;
    public String strValue;

    /**
     * @brief A default constructor.
     */
    public Attribute()
    {
      this.strName = String.Empty;
      this.strValue = String.Empty;
    }

    /**
     * @brief A constructor.
     * @param strName A String object holding the name of attribute.
     * @param strValue A String object holding the value of attribute.
     */
    public Attribute(
      String strName,
      String strValue
      )
    {
      this.strName = strName;
      this.strValue = strValue;
    }

    /**
     * @brief A public method for getting attribute name and value.
     */
    public String strToString()
    {
      return this.strName + " : " + this.strValue;
    }
  };

  static readonly String Nothing = @"Nothing";

  /**
   * @brief A public static method to get descendants of dedicated tag(strTagName) on a xml file(strPath).
   * @param strPath A String object holding the file path of a xml file.
   * @param strTagName A String object holding the tag name to get.
   * @param lstException A list object to store exception information.
   * @return lstReturn A list object holding XElements extracted.
   * @see System.Reflection.MethodBase
   */
  public static List<XElement> lstGetDescendants(
    String strPath,
    String strTagName,
    List<String> lstException
    )
  {
    List<XElement> lstReturn = [];
    try
    {
      XDocument xd = XDocument.Load(strPath);
      foreach(XElement xe in xd.Descendants(strTagName))
      {
        lstReturn.Add(xe);
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
    return lstReturn;
  }

  /**
   * @brief A public static method to get descendants of dedicated tag(strTagName) on a XElement(xeElement).
   * @param xeElement A XElement object for extracting.
   * @param strTagName A String object holding the tag name to get.
   * @param lstException A list object to store exception information.
   * @return lstReturn A list object holding XElements extracted.
   * @see System.Reflection.MethodBase
   */
  public static List<XElement> lstGetDescendants(
    XElement xeElement,
    String strTagName,
    List<String> lstException
    )
  {
    List<XElement> lstReturn = [];
    try
    {
      foreach(XElement xe in xeElement.Descendants(strTagName))
      {
        lstReturn.Add(xe);
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
    return lstReturn;
  }

  /**
   * @brief A public static method to get the first descendants of dedicated tag(strTagName) on a XElement(xeElement).
   * @param xeElement A XElement object for extracting.
   * @param strTagName A String object holding the tag name to get.
   * @param lstException A list object to store exception information.
   * @return xeReturn A XElement object holding the first descendant.
   * @see System.Reflection.MethodBase
   */
  public static XElement xeGetFirstDescendant(
    XElement xeElement,
    String strTagName,
    List<String> lstException
    )
  {
    XElement xeReturn = new (Xml.Nothing);
    try
    {
      foreach(XElement xe in xeElement.Descendants(strTagName))
      {
        if (xe.Name.ToString() == strTagName)
        {
          xeReturn = xe;
          break;
        }
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
    return xeReturn;
  }

  /**
   * @brief A public static method to get value of attributes of a dedicated XElement(xeElement).
   * @param xeElement A XElement object for extracting.
   * @param strAttributeName A String object holding the name of attribute to get.
   * @param lstException A list object to store exception information.
   * @return lstReturn A list object holding value of attributes extracted.
   * @see System.Reflection.MethodBase
   */
  public static List<Attribute> lstGetAttributes(
    XElement xeElement,
    List<String> lstException
    )
  {
    List<Attribute> lstReturn = [];
    try
    {
      foreach(XAttribute attribute in xeElement.Attributes())
      {
        lstReturn.Add(new Attribute(attribute.Name.ToString(), attribute.Value));
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
    return lstReturn;
  }

  /**
   * @brief A public static method to get the first attribute of a dedicated XElement(xeElement).
   * @param xeElement A XElement object for extracting.
   * @param strAttributeName A String object holding the name of attribute to get.
   * @param lstException A list object to store exception information.
   * @return strReturn A String object holding the first value of attribute extracted.
   * @see Xml.lstGetAttributes
   */
  public static Attribute attrGetAttribute(
    XElement xeElement,
    String strAttributeName,
    List<String> lstException
    )
  {
    Attribute attrReturn = new() { strName = strAttributeName, strValue = Xml.Nothing };
    try
    {
      foreach(XAttribute attribute in xeElement.Attributes())
      {
        if (attribute.Name.ToString() == strAttributeName)
        {
          attrReturn.strValue = attribute.Value;
          break;
        }
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
    return attrReturn;
  }
};

