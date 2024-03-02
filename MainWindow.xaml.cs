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
using System.ComponentModel;
using System.Windows.Threading;

#pragma warning disable IDE1006 // Naming Styles

namespace WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  private readonly Button btnLoadCode;
  private readonly TextBox tbTargetFolderCode; 
  private readonly Button btnLoadXml; 
  private readonly TextBox tbTargetFolderXml; 
  private readonly Button btnExecute;
  private readonly ProgressBar pbProgress;
  private readonly RichTextBox rtbLog;
  private readonly Label lblStatus;
  private readonly String strIdTaskDxoygenParser;
  private readonly Task.Container tcContainer;

  private String strTargetFolderCode;
  private String strTargetFolderXml;

  private Dxgn.Report? rptDoxygenReport;

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
    this.strIdTaskDxoygenParser = @"[DxgnParser]";
    this.tcContainer = new Task.Container(this.btnExecute, this.pbProgress, this.rtbLog, this.lblStatus);
    this.strTargetFolderCode = String.Empty;
    this.strTargetFolderXml = String.Empty;
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
      this.rptDoxygenReport = new Dxgn.Report(this.strIdTaskDxoygenParser, this.strTargetFolderCode, this.strTargetFolderXml);
      this.tcContainer.vidAdd(this.rptDoxygenReport);
    }
  }

  public void vidBtnExecuteClick(
    object objSender,
    RoutedEventArgs reaEvent
  )
  {
    if (this.tcContainer.bIsBusy(this.strIdTaskDxoygenParser))
    {
      this.tcContainer.vidCancel(this.strIdTaskDxoygenParser);
    }
    else
    {
      this.tcContainer.vidStart(this.strIdTaskDxoygenParser);
    }
  }
};
