using System.ComponentModel;
using System.Reflection;

#pragma warning disable IDE1006 // Naming Styles

/**
 * @brief A namespace for Task called through Task.Container.
 * @see Task.Container
 */
namespace Task
{
  /**
  * @brief A public class holding task event arguments.
  */
  public class TaskEventArgs(String strContent, Int32 s32Progress, String strLog) : System.EventArgs
  {
    public String strContent {get; set;} = strContent;  /**< A string object holding content, e.g. Button.  */
    public Int32 s32Progress {get; set;} = s32Progress; /**< A Int32 object holding current progress, e.g. ProgressBar.  */
    public String strLog {get; set;} = strLog;          /**< A string object holding log, e.g. RichTextBox. */
  };

  public abstract class Worker
  {
    public static readonly Int32 s32ProgressCountInvalid = -1;
    public static readonly String strExecute = @"Execute";
    public static readonly String strCancel = @"Cancel";
    public event EventHandler<TaskEventArgs>? ehWorkerEntry;
    public event EventHandler<TaskEventArgs>? ehWorkerProgress;
    public event EventHandler<TaskEventArgs>? ehWorkerLog;
    public event EventHandler<TaskEventArgs>? ehWorkerExit;
    protected virtual void vidOnWorkerEntry(TaskEventArgs e)
    {
      ehWorkerEntry?.Invoke(this, e);
    }
    protected virtual void vidOnWorkerProgress(TaskEventArgs e)
    {
      ehWorkerProgress?.Invoke(this, e);
    }
    protected virtual void vidOnWorkerLog(TaskEventArgs e)
    {
      ehWorkerLog?.Invoke(this, e);
    }
    protected virtual void vidOnWorkerExit(TaskEventArgs e)
    {
      ehWorkerExit?.Invoke(this, e);
    }
    public abstract Boolean vidStart();
    public abstract Boolean vidCancel();
    public abstract Boolean bIsBusy();
  };

  public class Background : Worker
  {
    protected readonly BackgroundWorker bgwWorker;

    public String strName { get; set; }
    public Int32 s32ProgressCount { get; set; }

    public Background()
    {
      this.bgwWorker = new BackgroundWorker();
      this.strName = String.Empty;
      this.s32ProgressCount = Worker.s32ProgressCountInvalid;
      this.bgwWorker.DoWork += new DoWorkEventHandler(vidDoWork);
      this.bgwWorker.ProgressChanged += new ProgressChangedEventHandler(vidProgressChanged);
      this.bgwWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(vidCompleted);
    }

    public Background(
      String strName
    ) : this()
    {
      this.strName = strName;
    }

    protected virtual void vidDoWork(object? sender, DoWorkEventArgs e)
    {
      MethodBase? mb = MethodBase.GetCurrentMethod();
      String strMethodName = (mb != null) ? mb.ReflectedType + mb.Name : String.Empty;
      
      vidOnWorkerEntry(new TaskEventArgs(Task.Worker.strCancel, 0, strMethodName));

      for(UInt32 i = 0; i < 100; i++)
      {
        Console.WriteLine("do work... [" + i.ToString() + "]....");
        
        if (this.bgwWorker.CancellationPending)
        {
          Console.WriteLine("canceled... [" + i.ToString() + "]....");
          break;
        }
        this.bgwWorker.ReportProgress((int)i);
        System.Threading.Thread.Sleep(1000);
      }
      return;
    }
    
    protected virtual void vidProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
      MethodBase? mb = MethodBase.GetCurrentMethod();
      String strMethodName = (mb != null) ? mb.ReflectedType + mb.Name : String.Empty;

      vidOnWorkerProgress(new TaskEventArgs(String.Empty, e.ProgressPercentage, strMethodName));
      vidOnWorkerLog(new TaskEventArgs(String.Empty, e.ProgressPercentage, strMethodName));
      return;
    }

    protected virtual void vidCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
      MethodBase? mb = MethodBase.GetCurrentMethod();
      String strMethodName = (mb != null) ? mb.ReflectedType + mb.Name : String.Empty;
      vidOnWorkerExit(new TaskEventArgs(Task.Worker.strExecute, 0, strMethodName));
      return;
    }

    public override Boolean vidStart()
    {
      Boolean bReturn = false;

      if (!this.bIsBusy())
      {
        this.bgwWorker.WorkerReportsProgress = true;
        this.bgwWorker.WorkerSupportsCancellation = true;
        this.bgwWorker.RunWorkerAsync();
        bReturn = true;
      }
      else
      {
        bReturn = false;
      }
      return bReturn;
    }

    public override Boolean vidCancel()
    {
      Boolean bReturn = false;

      if (this.bgwWorker != null)
      {
        this.bgwWorker.CancelAsync();
        bReturn = true;
      }
      else
      {
        bReturn = false;
      }
      return bReturn;
    }

    public override Boolean bIsBusy()
    {
      return this.bgwWorker.IsBusy;
    }
  };
  
  public class Service : Worker
  {
    public String strName { get; set; }

    public Service()
    {
      this.strName = String.Empty;
    }
    
    public Service(
      String strName
    )
    {
      this.strName = strName;
    }

    protected virtual void vidDoWork(object? sender, DoWorkEventArgs e)
    {
      /* TBD */
    }
    
    protected virtual void vidProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
      /* TBD */
    }

    protected virtual void vidCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
      /* TBD */
    }

    public override Boolean vidStart()
    {
      Boolean bReturn = false;
      /* TBD */
      return bReturn;
    }

    public override Boolean vidCancel()
    {
      Boolean bReturn = false;
      /* TBD */
      return bReturn;
    }

    public override Boolean bIsBusy()
    {
      Boolean bReturn = false;
      /* TBD */
      return bReturn;
    }
  };
}

