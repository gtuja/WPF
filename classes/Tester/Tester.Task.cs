using System.Reflection;
using System.ComponentModel;
using Task;

#pragma warning disable IDE1006 // Naming Styles

namespace Tester
{
  public class TesterBackground(String strName) : Task.Background(strName)
  {
    protected override void vidDoWork(object? sender, DoWorkEventArgs e)
    {
      MethodBase? mb = MethodBase.GetCurrentMethod();
      String strMethodName = (mb != null) ? mb.ReflectedType + mb.Name : String.Empty;
      
      vidOnTaskPre(new TaskEventArgs(Task.Worker.strCancel, 0, strMethodName));

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
    
    protected override void vidProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
      MethodBase? mb = MethodBase.GetCurrentMethod();
      String strMethodName = (mb != null) ? mb.ReflectedType + mb.Name : String.Empty;

      vidOnTaskProgressChanged(new TaskEventArgs(String.Empty, e.ProgressPercentage, strMethodName));
      vidOnTaskAppendLog(new TaskEventArgs(String.Empty, e.ProgressPercentage, strMethodName));
      return;
    }

    protected override void vidCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
      MethodBase? mb = MethodBase.GetCurrentMethod();
      String strMethodName = (mb != null) ? mb.ReflectedType + mb.Name : String.Empty;
      vidOnTaskPost(new TaskEventArgs(Task.Worker.strExecute, 0, strMethodName));
      return;
    }
  }
}
