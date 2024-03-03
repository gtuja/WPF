using System.ComponentModel;

#pragma warning disable IDE1006 // Naming Styles

namespace Task
{
  public class TemplateBackground(String strName) : Task.Background(strName)
  {
    protected override void vidDoWork(object? sender, DoWorkEventArgs e)
    {
      vidOnWorkerEntry(this, new TaskEventEntryArgs(this.strId?? String.Empty, Constants.strCancel, 100, @"[" + this.strId + @"] : invoke event entry..."));

      for(Int32 i = 0; i < 100; i++)
      {
        if (this.bgwWorker.CancellationPending)
        {
          e.Cancel = true;
          break;
        }
        vidOnWorkerProgress(this, new TaskEventProgressArgs(this.strId?? String.Empty, i, @"Extracting compounds[" + i + "/" + 100 + "]"));
        System.Threading.Thread.Sleep(1000);
      }
      return;
    }
  }
}
