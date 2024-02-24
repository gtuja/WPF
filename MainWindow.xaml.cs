using System.Runtime.ExceptionServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Util;

#pragma warning disable IDE1006 // Naming Styles

namespace WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  private Button btnLoadCode;
  private TextBox tbTargetFolderCode; 
  private Button btnLoadXml; 
  private TextBox tbTargetFolderXml; 
  private Button btnExecute;
  private ProgressBar pbProgress;
  private RichTextBox rtbLog;
  private Label lblStatus;

  private String strTargetFolderCode;
  private String strTargetFolderXml;

  private Dxgn.Report rptDoxygenReport;

  public MainWindow()
  {
    InitializeComponent();
    this.btnLoadCode = ButtonLoadCode;
    this.tbTargetFolderCode = TextBoxPathCode;
    this.btnLoadXml = ButtonLoadXml;
    this.tbTargetFolderXml = TextBoxPathXml;
    this.btnExecute = ButtonExecute;
    this.pbProgress = ProgressBarExecute;
    this.rtbLog = RichTextBoxLog;
    this.lblStatus = LabelStatus;
    this.strTargetFolderCode = String.Empty;
    this.strTargetFolderXml = String.Empty;
    this.rptDoxygenReport = new Dxgn.Report();
  }

  public void vidBtnLoadCodeClick(
    object objSender,
    RoutedEventArgs reaEvent
  )
  {
    String strTargetFolder;

    strTargetFolder = Util.UI.strOpenFolderDialog("Select the path of target code..");
    if (strTargetFolder != String.Empty)
    {
      this.strTargetFolderCode = strTargetFolder;
      this.tbTargetFolderCode.Text = strTargetFolder;
      this.btnLoadXml.IsEnabled = true;
    }
  }

  public void vidBtnLoadXmlClick(
    object objSender,
    RoutedEventArgs reaEvent
  )
  {
    String strTargetFolder;

    strTargetFolder = Util.UI.strOpenFolderDialog("Select the path of xml..");
    if (strTargetFolder != String.Empty)
    {
      this.strTargetFolderXml = strTargetFolder;
      this.tbTargetFolderXml.Text = strTargetFolder;
      this.btnExecute.IsEnabled = true;
    }
  }

  public void vidBtnExecuteClick(
    object objSender,
    RoutedEventArgs reaEvent
  )
  {
    List<String>lstException = new();
    Int32 s32CountProgress = this.rptDoxygenReport.s32GetProgressCount(strTargetFolderXml, lstException);

    if (s32CountProgress != Dxgn.Report.s32ReturnInvalid)
    {
      this.pbProgress.Maximum = s32CountProgress;
      this.rptDoxygenReport.vidExecute(strTargetFolderXml, this.pbProgress, this.rtbLog, lstException);
    }
    else
    {
      if (lstException.Count > 0)
      {
        UI.vidAppendLog(rtbLog, "# There are exceptions[" + lstException.Count + "] below...");
        foreach(String s in lstException)
        {
          UI.vidAppendLog(rtbLog, s);
        }
      }
      else
      {
        UI.vidAppendLog(rtbLog, "# There is no target progress...");
      }
    }
  }
}
