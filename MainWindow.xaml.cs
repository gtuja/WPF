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
    String strFileXml;
    List<XElement> lstElement;
    List<String> lstException = [];
    UI.vidAppendLog(rtbLog, @"Test....................");
    strFileXml = UI.strOpenFileDialog(@"Open File", @"Document", @"Xml File|*.xml|" + @"Text File|*.txt|" + @"Xlsx File|*.xlsx|" + "All Files|*.*");
    UI.vidAppendLog(rtbLog, strFileXml);

    lstElement = Xml.lstGetDescendants(strFileXml, @"compound", lstException);

    this.pbProgress.Maximum = lstElement.Count;
    
    foreach(XElement xeCompound in lstElement)
    {
      UI.vidAppendLog(rtbLog, xeCompound.Name.ToString());
      UI.vidAppendLog(rtbLog, xeCompound.Value);
      
      List<Xml.Attribute> lstAttribute = [];

      lstElement = Xml.lstGetDescendants(xeCompound, @"member", lstException);
      
      foreach (XElement xeMember in lstElement)
      {
        UI.vidAppendLog(rtbLog, xeMember.Name.ToString() + " : " + xeMember.Value);
        foreach (Xml.Attribute attribute in Xml.lstGetAttributes(xeMember, lstException))
        {
          UI.vidAppendLog(rtbLog, attribute.ToString());
        }
        UI.vidUpdateUI();
      }
      this.pbProgress.Value++;
      UI.vidUpdateUI();
    }
  }
}
