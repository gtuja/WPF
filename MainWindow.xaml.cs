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

  private readonly Task.Manager tmManager;
  private readonly String strTaskId;
  public MainWindow()
  {
    InitializeComponent();
    this.lblStatus = LabelStatus;
    this.pbProgress = ProgressBarExecute;
    this.rtbLog = RichTextBoxLog;
    this.btnExecute = ButtonExecute;
    this.strTaskId = @"TesterBackground";
    this.tmManager = new Task.Manager(this.btnExecute, this.pbProgress, this.rtbLog, this.lblStatus);
    this.tmManager.enuRegister(new Task.TemplateBackground(this.strTaskId));
  }

  private void vidBtnExecuteClick(
    object objSender,
    RoutedEventArgs reaEvent
  )
  {
    if (tmManager.bIsRegistered(this.strTaskId))
    {
      switch(this.tmManager.enuIsBusy(this.strTaskId))
      {
        case Task.enuReturnType.True :
          this.tmManager.enuStop(this.strTaskId);
          break;
        case Task.enuReturnType.False :
          this.tmManager.enuStart(this.strTaskId);
          break;
        default :
          MessageBox.Show("Something wrong check logs", "SOFT ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
          break;
      }
    }
  }
}
