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
using Util;

namespace WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
    lblStatus.Content = @"Status?";
  }

  public void vidBtnExecuteClick(
    object objSender,
    RoutedEventArgs reaEvent
  )
  {
    Util.UI.vidAppendLog(rtbLog, @"Test....................");
    Util.UI.vidAppendLog(rtbLog, Util.UI.strOpenFileDialog(@"Open File", @"Document", @"Text File|*.txt|" + @"Xml File|*.xml|" + @"Xlsx File|*.xlsx|" + "All Files|*.*"));
    Util.UI.vidAppendLog(rtbLog, Util.UI.strOpenFolderDialog(@"Open Folder"));
  }
}
