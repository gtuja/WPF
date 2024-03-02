#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable IDE1006 // Naming Styles

/**
 * @brief A namespace for common utility.
 * @see Util.UI
 * @see Util.Xml
 * @see Util.Debug
 */
namespace Util
{
  /**
  * @brief A public static class holding debug methods.
  * @see System.Diagnostics.StackTrace
  */
  public static class Debug
  {
    /**
    * @brief A public static method getting the name of caller method.
    * @see System.Diagnostics.StackTrace
    * @note
    *   https://stackoverflow.com/questions/171970/how-can-i-find-the-method-that-called-the-current-method
    */
    public static String strGetMethodNme()
    {
      return (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;
    }
  };
};
