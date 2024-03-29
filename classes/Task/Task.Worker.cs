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
    public event EventHandler<TaskEventEntryArgs>? ehWorkerEntry;    /**< An event handler for event entry invoked from task. */
    public event EventHandler<TaskEventProgressArgs>? ehWorkerProgress; /**< An event handler for event progress invoked from task. */
    public event EventHandler<TaskEventLogArgs>? ehWorkerLog;      /**< An event handler for event log invoked from task. */
    public event EventHandler<TaskEventExitArgs>? ehWorkerExit;     /**< An event handler for event exit invoked from task. */

    /**
    * @brief A public abstract method to start task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    * @note
    *   The class inherited should override this method.  
    */
    public abstract void vidStart();

    /**
    * @brief A public abstract method to stop task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    * @note
    *   The class inherited should override this method.  
    */
    public abstract void vidStop();
    
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
      TaskEventEntryArgs e
      )
    {
      ehWorkerEntry?.Invoke(this, e);
    }

    /**
    * @brief A protected virtual method to invoke progress event for inherited classes, e.g., Background, Service.  
    * @param sender A object? object holding caller object.
    * @param e A TaskEventEntryArgs object holding event arguments.
    * @see Task.Background
    * @see Task.Service
    */
    protected virtual void vidOnWorkerProgress(
      object? sender,
      TaskEventProgressArgs e
      )
    {
      ehWorkerProgress?.Invoke(this, e);
    }

    /**
    * @brief A protected virtual method to invoke log event for inherited classes, e.g., Background, Service.  
    * @param sender A object? object holding caller object.
    * @param e A TaskEventLogArgs object holding event arguments.
    * @see Task.Background
    * @see Task.Service
    */
    protected virtual void vidOnWorkerLog(
      object? sender,
      TaskEventLogArgs e
      )
    {
      ehWorkerLog?.Invoke(this, e);
    }

    /**
    * @brief A protected virtual method to invoke exit event for inherited classes, e.g., Background, Service.  
    * @param sender A object? object holding caller object.
    * @param e A TaskEventExitArgs object holding event arguments.
    * @see Task.Background
    * @see Task.Service
    */
    protected virtual void vidOnWorkerExit(
      object? sender,
      TaskEventExitArgs e
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
        vidOnWorkerLog(this, new TaskEventLogArgs(this.strId?? String.Empty, @"[" + this.strId + @"] : try to start..."));
      }
      else
      {
        vidOnWorkerLog(this, new TaskEventLogArgs(this.strId?? String.Empty, @"[" + this.strId + @"] : no way to start, it's busy..."));
      }
    }

    /**
    * @brief A public abstract method to pause task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    * @note
    *   The class inherited should override this method.  
    */
    public virtual void vidPause()
    {
      vidOnWorkerLog(this, new TaskEventLogArgs(strId, "[NI] " + Util.Debug.strGetMethodNme() + " -> is not implemented.."));      
    }

    /**
    * @brief A public abstract method to resume task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    * @note
    *   The class inherited should override this method.  
    */
    public virtual void vidResume()
    {
      vidOnWorkerLog(this, new TaskEventLogArgs(strId, "[NI] " + Util.Debug.strGetMethodNme() + " -> is not implemented.."));      
    }

    /**
    * @brief An public abstract method to stop task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    */
    public override void vidStop()
    {
      if (this.bgwWorker.IsBusy)
      {
        this.bgwWorker.CancelAsync();
        vidOnWorkerLog(this, new TaskEventLogArgs(this.strId?? String.Empty, @"[" + this.strId + @"] : try to cancel..."));
      }
      else
      {
        vidOnWorkerLog(this, new TaskEventLogArgs(this.strId?? String.Empty, @"[" + this.strId + @"] : no way to cancel, it's not busy..."));
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
      vidOnWorkerExit(this, new TaskEventExitArgs(this.strId?? String.Empty, Constants.strExecute, @"[" + this.strId + @"] : invoke event exit..."));
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
      this.strId = String.Empty;
    }
    
    public Service(
      String strId
    )
    {
      this.strId = strId;
      vidOnWorkerLog(this, new TaskEventLogArgs(strId, "[NI] " + Util.Debug.strGetMethodNme() + " -> is not implemented.."));      
    }

    public override void vidStart()
    {
      vidOnWorkerLog(this, new TaskEventLogArgs(strId, "[NI] " + Util.Debug.strGetMethodNme() + " -> is not implemented.."));      
    }

    /**
    * @brief A public abstract method to pause task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    * @note
    *   The class inherited should override this method.  
    */
    public virtual void vidPause()
    {
      vidOnWorkerLog(this, new TaskEventLogArgs(strId, "[NI] " + Util.Debug.strGetMethodNme() + " -> is not implemented.."));      
    }

    /**
    * @brief A public abstract method to resume task, e.g., Background, Service.  
    * @see Task.Background
    * @see Task.Service
    * @note
    *   The class inherited should override this method.  
    */
    public virtual void vidResume()
    {
      vidOnWorkerLog(this, new TaskEventLogArgs(strId, "[NI] " + Util.Debug.strGetMethodNme() + " -> is not implemented.."));      
    }

    public override void vidStop()
    {
      vidOnWorkerLog(this, new TaskEventLogArgs(strId, "[NI] " + Util.Debug.strGetMethodNme() + " -> is not implemented.."));      
    }

    public override Boolean bIsBusy()
    {
      Boolean bReturn = false;
      vidOnWorkerLog(this, new TaskEventLogArgs(strId, "[NI] " + Util.Debug.strGetMethodNme() + " -> is not implemented.."));      
      
      return bReturn;
    }

    protected abstract void vidDoWork(object? sender, EventArgs e);
    
    protected abstract void vidCompleted(object? sender, EventArgs e);
  };
};


