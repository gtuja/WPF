
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;
using Util;

#pragma warning disable IDE1006 // Naming Styles

/**
 * @brief A namespace for task control.
 * @see Task.Constants
 * @see Task.TaskEventArgs
 * @see Task.Manager
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
  public class Manager(Button btnExecute, ProgressBar pbProgress, RichTextBox rtbLog, Label lblStatus)
  {
    private readonly Dictionary<String, Task.Worker> dictWorker = []; /**< A private Dictionary object holding tasks(workers). */
    private readonly ProgressBar pbProgress = pbProgress;             /**< A private ProgressBar object to be updated by event(progress) invoked by task. */
    private readonly RichTextBox rtbLog = rtbLog;                     /**< A private RichTextBox object to be updated by event(log) invoked by task. */
    private readonly Button btnExecute = btnExecute;                  /**< A private Button object to be updated by event(entry, exit) invoked by task. */
    private readonly Label lblStatus = lblStatus;                  /**< A private Button object to be updated by event(entry, exit) invoked by task. */

    /**
    * @brief A public method to add a worker.  
    * @param wrkrWorker A Task.Worker object to be added.
    * @return enuReturn A Task.enuReturnType object represent the return of method.
    * @see Task.enuReturnType
    * @see vidHandleTaskEntry
    * @see vidHandleTaskProgress
    * @see vidHandleTaskLog
    * @see vidHandleTaskExit
    */
    public enuReturnType enuRegister(
      Task.Worker wrkrWorker
      )
    {
      enuReturnType enuReturn;
      
      if (this.dictWorker.ContainsKey(wrkrWorker.strId))
      {
        vidHandleTaskLog(this, new TaskEventLogArgs(wrkrWorker.strId, "[XCPTN] " + Util.Debug.strGetMethodNme() + " -> [" + wrkrWorker.strId  + "] is already exist.."));
        enuReturn = enuReturnType.NotRegistered;
      }
      else
      {
        wrkrWorker.ehWorkerEntry += vidHandleTaskEntry;
        wrkrWorker.ehWorkerProgress += vidHandleTaskProgress;
        wrkrWorker.ehWorkerLog += vidHandleTaskLog;
        wrkrWorker.ehWorkerExit += vidHandleTaskExit;
        this.dictWorker.Add(wrkrWorker.strId, wrkrWorker);
        enuReturn = enuReturnType.True;
      }
      return enuReturn;
    }

    /**
    * @brief A public method to start worker by ID.
    * @param strId A String object holding the ID of worker.
    * @return enuReturn A Task.enuReturnType object represent the return of method.
    * @see Task.enuReturnType
    */
    public enuReturnType enuStart(
      String strId
      )
    {
      enuReturnType enuReturn;

      if (this.dictWorker.ContainsKey(strId))
      {
        this.dictWorker[strId].vidStart();
        enuReturn = enuReturnType.True;
      }
      else
      {
        vidHandleTaskLog(this, new TaskEventLogArgs(strId, "[XCPTN] " + Util.Debug.strGetMethodNme() + " -> [" + strId  + "] is not registered.."));
        enuReturn = enuReturnType.NotRegistered;
      }
      return enuReturn;
    }

    /**
    * @brief A public method to pause worker by ID.
    * @param strId A String object holding the ID of worker.
    * @return enuReturn A Task.enuReturnType object represent the return of method.
    * @see Task.enuReturnType
    */
    public enuReturnType enuPause(
      String strId
      )
    {
      vidHandleTaskLog(this, new TaskEventLogArgs(strId, "[NI] " + Util.Debug.strGetMethodNme() + " -> is not implemented.."));
      return enuReturnType.NotImplemented;
    }

    /**
    * @brief A public method to resume worker by ID.
    * @param strId A String object holding the ID of worker.
    * @return enuReturn A Task.enuReturnType object represent the return of method.
    * @see Task.enuReturnType
    */
    public enuReturnType enuResume(
      String strId
      )
    {
      vidHandleTaskLog(this, new TaskEventLogArgs(strId, "[NI] " + Util.Debug.strGetMethodNme() + " -> is not implemented.."));
      return enuReturnType.NotImplemented;
    }

    /**
    * @brief A public method to stop worker by ID.
    * @param strId A String object holding the ID of worker.
    * @return enuReturn A Task.enuReturnType object represent the return of method.
    * @see Task.enuReturnType
    */
    public enuReturnType enuStop(
      String strId
      )
    {
      enuReturnType enuReturn;

      if (this.dictWorker.ContainsKey(strId))
      {
        this.dictWorker[strId].vidStop();
        enuReturn = enuReturnType.True;
      }
      else
      {
        enuReturn = enuReturnType.NotRegistered;
      }
      return enuReturn;
    }

    /**
    * @brief A public method to get the state of worker, i.e., busy or not, by ID.
    * @param strId A String object holding the ID of worker.
    * @return Boolean worker is busy or not.
    */
    public enuReturnType enuIsBusy(
      String strId
      )
    {
      enuReturnType enuReturn;

      if (this.dictWorker.ContainsKey(strId))
      {
        this.dictWorker[strId].vidStop();
        enuReturn = enuReturnType.True;
      }
      else
      {
        enuReturn = enuReturnType.NotRegistered;
      }
      return enuReturn;
    }

    /**
    * @brief A public method to deregister worker by ID.
    * @param strId A String object holding the ID of worker.
    * @return enuReturn A Task.enuReturnType object represent the return of method.
    * @see Task.enuReturnType
    * @see vidHandleTaskEntry
    * @see vidHandleTaskProgress
    * @see vidHandleTaskLog
    * @see vidHandleTaskExit
    * @note
    *   This method is not tested currently, it might be used further feature.
    */
    private Task.enuReturnType enuDeregister(
      String strId
      )
    {
      enuReturnType enuReturn;

      if (this.dictWorker.ContainsKey(strId))
      {
        this.dictWorker[strId].ehWorkerEntry -= vidHandleTaskEntry;
        this.dictWorker[strId].ehWorkerProgress -= vidHandleTaskProgress;
        this.dictWorker[strId].ehWorkerLog -= vidHandleTaskLog;
        this.dictWorker[strId].ehWorkerExit -= vidHandleTaskExit;
        this.dictWorker.Remove(strId);
        enuReturn = enuReturnType.True;
      }
      else
      {
        enuReturn = enuReturnType.NotRegistered;
      }
      return enuReturn;
    }

    /**
    * @brief A public method to get worker is registered or not by ID.
    * @param strId A String object holding the ID of worker.
    * @return True/False
    */
    public Boolean bIsRegistered(String strId)
    {
      return this.dictWorker.ContainsKey(strId);
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
