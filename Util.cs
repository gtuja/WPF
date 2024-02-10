using System.Windows.Controls;
using System.Windows.Documents;

/**
 * @brief A namespace for common utility.
 * @see UI
 */
namespace Util;

/**
 * @brief A public class holding utilities for windows controls on WPF.
 * @see UI
 */
public class UI
{
  /**
  * @brief A public static method to append text to RichTextBox control.
  * @see RichTextBox
  * @note
  *  The block height on RichTextBox shall be set "1" on xaml file, i.e. Block.LineHeight="1".
  */
  public static void vidAppendLog(
    RichTextBox rtb,
    String strLog
    )
  {
    var paragraph = new Paragraph();
    
    paragraph.Inlines.Add(new Run(strLog));
    rtb.Document.Blocks.Add(paragraph);

    rtb.Focus();
    rtb.ScrollToEnd();
  }

  /**
  * @brief A public static method to select a file using Microsoft.Win32 DLL.
  * @param strTitle A String object holding the title of dialog.
  * @param strFileName A String object holding the file name of dialog.
  * @param strFilter A String object holding filters.
  * @return strReturn A string object holding selected file name or String.Empty if canceled.
  * @see Microsoft.Win32.OpenFileDialog
  * @see https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-common-system-dialog-box?view=netdesktop-8.0
  * @note
  *  Usage
  *  Util.UI.strOpenFileDialog(@"Open File", @"Document", @"Text File|*.txt|" + @"Xml File|*.xml|" + @"Xlsx File|*.xlsx|" + "All Files|*.*");
  */
  public static String strOpenFileDialog(
    String strTitle,
    String strFileName,
    String strFilter
  )
  {
    String strReturn;
    var dialog = new Microsoft.Win32.OpenFileDialog();

    dialog.Title = strTitle;
    dialog.FileName = strFileName;
    dialog.Filter = strFilter;

    bool? result = dialog.ShowDialog();
    strReturn = (result == true) ? dialog.FileName : String.Empty;
    
    return strReturn;
  }

  /**
  * @brief A public static method to select a folder using Microsoft.Win32 DLL.
  * @param strTitle A String object holding the title of dialog.
  * @return strReturn A String object holding selected folder name or String.Empty if canceled.
  * @see Microsoft.Win32.OpenFolderDialog
  * @see https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-common-system-dialog-box?view=netdesktop-8.0
  * @note
  *  Usage
  *  Util.UI.strOpenFolderDialog(@"Open Folder");
  */
  public static String strOpenFolderDialog(
    String strTitle
  )
  {
    String strReturn;
    var dialog = new Microsoft.Win32.OpenFolderDialog();
    dialog.Title = strTitle;
    dialog.Multiselect = false;
    
    bool? result = dialog.ShowDialog();
    strReturn = (result == true) ? dialog.FolderName : String.Empty;
    
    return strReturn;
  }
};
