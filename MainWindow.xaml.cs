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
  private readonly ProgressBar pbProgress;
  private readonly RichTextBox rtbLog;
  private readonly Label lblStatus;
  private readonly Button btnExecute;

  private readonly Task.Container tcContainer;
  private readonly Tester.TesterBackground tbTester;
  private readonly String strTaskId;
  public MainWindow()
  {
    InitializeComponent();
    this.lblStatus = LabelStatus;
    this.pbProgress = ProgressBarExecute;
    this.rtbLog = RichTextBoxLog;
    this.btnExecute = ButtonExecute;
    this.strTaskId = @"TesterBackground";
    this.tbTester = new (this.strTaskId);
    this.tcContainer = new Task.Container(this.btnExecute, this.pbProgress, this.rtbLog, this.lblStatus);
    this.tcContainer.vidAdd(this.tbTester);
  }

  public void vidBtnExecuteClick(
    object objSender,
    RoutedEventArgs reaEvent
  )
  {
    if (this.tcContainer.bIsBusy(this.strTaskId))
    {
      this.tcContainer.vidCancel(this.strTaskId);
    }
    else
    {
      this.tcContainer.vidStart(this.strTaskId);
    }
  }
}
