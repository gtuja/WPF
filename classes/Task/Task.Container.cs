
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using Util;

#pragma warning disable IDE1006 // Naming Styles

/**
 * @brief A namespace for task control.
 * @see Task.Constants
 * @see Task.TaskEventArgs
 * @see Task.Container
 * @see Task.Worker
 * @see Task.Background
 * @see Task.Service
 */
namespace Task
{
  /**
  * @brief A public class maintains and controls tasks, e.g., Task.Background, Task.Service, through Task.Worker.  
  * @see Task.Worker
  * @see Task.Background
  * @see Task.Service
  */
  public class Container(Button btnExecute, ProgressBar pbProgress, RichTextBox rtbLog, Label lblStatus)
  {
    private readonly Dictionary<String, Task.Worker> dictWorker = []; /**< A private Dictionary object holding tasks(workers). */
    private readonly ProgressBar pbProgress = pbProgress;             /**< A private ProgressBar object to be updated by event(progress) invoked by task. */
    private readonly RichTextBox rtbLog = rtbLog;                     /**< A private RichTextBox object to be updated by event(log) invoked by task. */
    private readonly Button btnExecute = btnExecute;                  /**< A private Button object to be updated by event(entry, exit) invoked by task. */
    private readonly Label lblStatus = lblStatus;                  /**< A private Button object to be updated by event(entry, exit) invoked by task. */

    /**
    * @brief A public method to add a worker.  
    * @param wrkrWorker A Task.Worker object to be added.
    * @see vidHandleTaskEntry
    * @see vidHandleTaskProgress
    * @see vidHandleTaskLog
    * @see vidHandleTaskExit
    */
    public void vidAdd(
      Task.Worker wrkrWorker
      )
    {
      wrkrWorker.ehWorkerEntry += vidHandleTaskEntry;
      wrkrWorker.ehWorkerProgress += vidHandleTaskProgress;
      wrkrWorker.ehWorkerLog += vidHandleTaskLog;
      wrkrWorker.ehWorkerExit += vidHandleTaskExit;
      this.dictWorker.Add(wrkrWorker.strId, wrkrWorker);
    }

    /**
    * @brief A public method to start worker by ID.
    * @param strId A String object holding the ID of worker.
    */
    public void vidStart(
      String strId
      )
    {
      this.dictWorker[strId].vidStart();
    }

    /**
    * @brief A public method to cancel worker by ID.
    * @param strId A String object holding the ID of worker.
    */
    public void vidCancel(
      String strId
      )
    {
      this.dictWorker[strId].vidCancel();
    }

    /**
    * @brief A public method to get the state of worker, i.e., busy or not, by ID.
    * @param strId A String object holding the ID of worker.
    * @return Boolean worker is busy or not.
    */
    public Boolean bIsBusy(
      String strId
      )
    {
      return this.dictWorker[strId].bIsBusy();
    }

    /**
    * @brief A public method to remove worker by ID.
    * @param strId A String object holding the ID of worker.
    * @see vidHandleTaskEntry
    * @see vidHandleTaskProgress
    * @see vidHandleTaskLog
    * @see vidHandleTaskExit
    * @note
    *   This method is not tested currently, it might be used further feature.
    */
    private void vidRemove(
      String strId
      )
    {
      this.dictWorker[strId].ehWorkerEntry -= vidHandleTaskEntry;
      this.dictWorker[strId].ehWorkerProgress -= vidHandleTaskProgress;
      this.dictWorker[strId].ehWorkerLog -= vidHandleTaskLog;
      this.dictWorker[strId].ehWorkerExit -= vidHandleTaskExit;
      this.dictWorker.Remove(strId);
      return;
    }

    /**
    * @brief A public method to handle entry event invoked by task.  
    * @param sender A object? object of sender.
    * @param e A TaskEventEntryArgs object holding event arguments.
    */
    private void vidHandleTaskEntry(
      object? sender,
      TaskEventEntryArgs e
      )
    {
      Application.Current.Dispatcher.BeginInvoke(
        DispatcherPriority.Background,
        new Action(() => { 
          this.pbProgress.Maximum = e.s32ProgressMax;
          this.btnExecute.Content = e.strContent;
          UI.vidAppendLog(this.rtbLog, e.strLog);
        }));
      return;
    }

    /**
    * @brief A public method to handle progress event invoked by task.  
    * @param sender A object? object of sender.
    * @param e A TaskEventProgressArgs object holding event arguments.
    * @note
    */
    private void vidHandleTaskProgress(
      object? sender,
      TaskEventProgressArgs e
      )
    {
      Application.Current.Dispatcher.BeginInvoke(
        DispatcherPriority.Background,
        new Action(() => { 
          this.pbProgress.Value = e.s32Progress;
          this.lblStatus.Content = e.strStatus;
        }));
    }

    /**
    * @brief A public method to handle log event invoked by task.  
    * @param sender A object? object of sender.
    * @param e A TaskEventLogArgs object holding event arguments.
    */
    private void vidHandleTaskLog(
      object? sender,
      TaskEventLogArgs e
      )
    {
      Application.Current.Dispatcher.BeginInvoke(
        DispatcherPriority.Background,
        new Action(() => { 
          UI.vidAppendLog(this.rtbLog, e.strLog);
        }));
    }

    /**
    * @brief A public method to handle exit event invoked by task.  
    * @param sender A object? object of sender.
    * @param e A TaskEventExitArgs object holding event arguments.
    */
    private void vidHandleTaskExit(
      object? sender,
      TaskEventExitArgs e
      )
    {
      Application.Current.Dispatcher.BeginInvoke(
        DispatcherPriority.Background,
        new Action(() => { 
          this.btnExecute.Content = e.strContent;
          UI.vidAppendLog(this.rtbLog, e.strLog);
        }));
    }
  };
};
