
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using Util;

#pragma warning disable IDE1006 // Naming Styles

namespace Task
{
  public class Container(Button btnExecute, ProgressBar pbProgress, RichTextBox rtbLog)
  {
    public readonly List<Task.Worker> lstWorker = [];
    private readonly ProgressBar pbProgress = pbProgress;
    private readonly RichTextBox rtbLog = rtbLog;
    private readonly Button btnExecute = btnExecute;

    public void vidAdd(Task.Worker worker)
    {
      worker.ehWorkerEntry += vidHandleWorkerEntry;
      worker.ehWorkerProgress += vidHandleWorkerProgress;
      worker.ehWorkerLog += vidHandleWorkerLog;
      worker.ehWorkerExit += vidHandleWorkerExit;
      this.lstWorker.Add(worker);
    }
    private void vidHandleWorkerEntry(object? sender, TaskEventArgs e)
    {
      Application.Current.Dispatcher.BeginInvoke(
        DispatcherPriority.Background,
        new Action(() => { 
          this.pbProgress.Value = e.s32Progress;
          this.btnExecute.Content = e.strContent;
        }));
      return;
    }
    private void vidHandleWorkerProgress(object? sender, TaskEventArgs e)
    {
      Application.Current.Dispatcher.BeginInvoke(
        DispatcherPriority.Background,
        new Action(() => { 
          this.pbProgress.Value = e.s32Progress;
          UI.vidAppendLog(this.rtbLog, e.strLog);
        }));
      return;
    }

    private void vidHandleWorkerLog(object? sender, TaskEventArgs e)
    {
      Application.Current.Dispatcher.BeginInvoke(
        DispatcherPriority.Background,
        new Action(() => { 
          UI.vidAppendLog(this.rtbLog, e.strLog);
        }));
      return;
    }
    private void vidHandleWorkerExit(object? sender, TaskEventArgs e)
    {
      Application.Current.Dispatcher.BeginInvoke(
        DispatcherPriority.Background,
        new Action(() => { 
          this.btnExecute.Content = e.strContent;
          UI.vidAppendLog(this.rtbLog, e.strLog);
        }));
      return;
    }
  };
};
