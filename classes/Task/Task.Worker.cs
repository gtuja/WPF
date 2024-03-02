using System.ComponentModel;

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
  * @brief An abstract class holding abstract or virtual methods for inherited classes, e.g., Background, Service.  
  * @see Task.Background
  * @see Task.Service
  */
  public abstract class Worker
  {
    public String strId = String.Empty; /**< A String object holding the ID of a Worker. */
    public event EventHandler<TaskEventArgs>? ehWorkerEntry;    /**< An event handler for event entry invoked from task. */
    public event EventHandler<TaskEventArgs>? ehWorkerProgress; /**< An event handler for event progress invoked from task. */
    public event EventHandler<TaskEventArgs>? ehWorkerLog;      /**< An event handler for event log invoked from task. */
    public event EventHandler<TaskEventArgs>? ehWorkerExit;     /**< An event handler for event exit invoked from task. */

    /**
    * @brief A public abstract method to start task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    * @note
    *   The class inherited should override this method.  
    */
    public abstract void vidStart();
    
    /**
    * @brief A public abstract method to cancel task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    * @note
    *   The class inherited should override this method.  
    */
    public abstract void vidCancel();
    
    /**
    * @brief A public abstract method to get task is busy or not, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    * @note
    *   The class inherited should override this method.  
    */
    public abstract Boolean bIsBusy();

    /**
    * @brief A protected virtual method to invoke entry event for inherited classes, e.g., Background, Service.  
    * @param sender A object? object holding caller object.
    * @param TaskEventArgs A TaskEventArgs object holding event arguments.
    * @see Task.Background
    * @see Task.Service
    */
    protected virtual void vidOnWorkerEntry(
      object? sender,
      TaskEventArgs e
      )
    {
      ehWorkerEntry?.Invoke(this, e);
    }

    /**
    * @brief A protected virtual method to invoke progress event for inherited classes, e.g., Background, Service.  
    * @param sender A object? object holding caller object.
    * @param TaskEventArgs A TaskEventArgs object holding event arguments.
    * @see Task.Background
    * @see Task.Service
    */
    protected virtual void vidOnWorkerProgress(
      object? sender,
      TaskEventArgs e
      )
    {
      ehWorkerProgress?.Invoke(this, e);
    }

    /**
    * @brief A protected virtual method to invoke log event for inherited classes, e.g., Background, Service.  
    * @param sender A object? object holding caller object.
    * @param TaskEventArgs A TaskEventArgs object holding event arguments.
    * @see Task.Background
    * @see Task.Service
    */
    protected virtual void vidOnWorkerLog(
      object? sender,
      TaskEventArgs e
      )
    {
      ehWorkerLog?.Invoke(this, e);
    }

    /**
    * @brief A protected virtual method to invoke exit event for inherited classes, e.g., Background, Service.  
    * @param sender A object? object holding caller object.
    * @param TaskEventArgs A TaskEventArgs object holding event arguments.
    * @see Task.Background
    * @see Task.Service
    */
    protected virtual void vidOnWorkerExit(
      object? sender,
      TaskEventArgs e
      )
    {
      ehWorkerExit?.Invoke(this, e);
    }
  };

  /**
  * @brief A class processing a background task.  
  * @see Task.Worker
  */
  public abstract class Background : Worker
  {
    /**
    * @brief Default constructor.  
    */
    public Background()
    {
      this.strId = String.Empty;
      this.bgwWorker = new BackgroundWorker
      {
        WorkerSupportsCancellation = true
      };
      this.bgwWorker.DoWork += new DoWorkEventHandler(vidDoWork);
      this.bgwWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(vidCompleted);
    }

    /**
    * @brief Constructor.  
    * @param strId A String object holding the ID of task.  
    */
    public Background(
      String strId
    ) : this()
    {
      this.strId = strId;
    }

    /**
    * @brief An public abstract method to start task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    */
    public override void vidStart()
    {
      if (!this.bgwWorker.IsBusy)
      {
        this.bgwWorker.WorkerReportsProgress = true;
        this.bgwWorker.WorkerSupportsCancellation = true;
        this.bgwWorker.RunWorkerAsync();
        vidOnWorkerLog(this, new TaskEventArgs(this.strId?? String.Empty, String.Empty, Constants.s32ProgressCountInvalid, @"[" + this.strId + @"] : try to start..."));
      }
      else
      {
        vidOnWorkerLog(this, new TaskEventArgs(this.strId?? String.Empty, String.Empty, Constants.s32ProgressCountInvalid, @"[" + this.strId + @"] : no way to start, it's busy..."));
      }
    }

    /**
    * @brief An public abstract method to cancel task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    */
    public override void vidCancel()
    {
      if (this.bgwWorker.IsBusy)
      {
        this.bgwWorker.CancelAsync();
        vidOnWorkerLog(this, new TaskEventArgs(this.strId?? String.Empty, String.Empty, Constants.s32ProgressCountInvalid, @"[" + this.strId + @"] : try to cancel..."));
      }
      else
      {
        vidOnWorkerLog(this, new TaskEventArgs(this.strId?? String.Empty, String.Empty, Constants.s32ProgressCountInvalid, @"[" + this.strId + @"] : no way to cancel, it's not busy..."));
      }
    }

    /**
    * @brief An public abstract method to get task is busy or not, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    */
    public override Boolean bIsBusy()
    {
      return this.bgwWorker.IsBusy;
    }

    /**
    * @brief A protected virtual method to do something with background task.
    * @param sender A object? object holding sender.  
    * @param e A DoWorkEventArgs object holding event arguments.  
    * @note
    *   The class inherited should override this method.  
    */
    protected abstract void vidDoWork(
      object? sender,
      DoWorkEventArgs e
      );
    
    /**
    * @brief A protected virtual method to do something after background task completed.
    * @param sender A object? object holding sender.  
    * @param e A DoWorkEventArgs object holding event arguments.  
    */
    protected virtual void vidCompleted(
      object? sender,
      RunWorkerCompletedEventArgs e
      )
    {
      vidOnWorkerExit(this, new TaskEventArgs(this.strId?? String.Empty, Constants.strExecute, Constants.s32ProgressCountInvalid, @"[" + this.strId + @"] : invoke event exit..."));
      return;
    }
    protected readonly BackgroundWorker bgwWorker;  /**< A BackgroundWorker object to process background task. */
  };

  /**
  * @brief A class processing a service task.  
  * @see Task.Worker
  * @note
  *   TBD, Shall implement on further feature.
  */
  public abstract class Service : Worker
  {
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

    public override void vidStart()
    {
      /* TBD */
    }

    public override void vidCancel()
    {
      /* TBD */
    }

    public override Boolean bIsBusy()
    {
      Boolean bReturn = false;
      /* TBD */
      return bReturn;
    }

    protected abstract void vidDoWork(object? sender, DoWorkEventArgs e);
    
    protected virtual void vidCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
      /* TBD */
    }

    public String strName { get; set; }
  };
};


