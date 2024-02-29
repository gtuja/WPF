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

  private Tester.TesterBackground bgTask;

  private readonly Task.Container tcContainer;

  public MainWindow()
  {
    InitializeComponent();
    this.lblStatus = LabelStatus;
    this.pbProgress = ProgressBarExecute;
    this.rtbLog = RichTextBoxLog;
    this.btnExecute = ButtonExecute;
    this.tcContainer = new Task.Container(this.btnExecute, this.pbProgress, this.rtbLog);
    this.bgTask = new Tester.TesterBackground(@"Tester(Background)");
    this.tcContainer.vidAdd(this.bgTask);
  }

  public void vidBtnExecuteClick(
    object objSender,
    RoutedEventArgs reaEvent
  )
  {
    foreach(Task.Worker worker in this.tcContainer.lstWorker)
    {
      if (worker.bIsBusy())
      {
        worker.vidCancel();
        this.btnExecute.Content = @"Execute";
      }
      else
      {
        worker.vidStart();
        this.btnExecute.Content = @"Cancel";
      }
    }
  }
}
