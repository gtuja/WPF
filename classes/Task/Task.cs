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
  * @brief A public static class holding constants.
  */
  public static class Constants
  {
    public const Int32 s32ProgressCountInvalid = -1;  /**< A Int32 object holding invalid progress count. */
    public const String strExecute = @"Execute";      /**< A String object holding content of "execute", e.g., Button. */
    public const String strCancel = @"Cancel";        /**< A String object holding content of "cancel", e.g., Button. */
  };

  /**
  * @brief A public class holding task event arguments.
  * @see System.EventArgs
  */
  public class TaskEventArgs(String strId, String strContent, Int32 s32Progress, String strLog) : System.EventArgs
  {
    public String strId {get; set;} = strId;            /**< A string object holding the ID of task. */
    public String strContent {get; set;} = strContent;  /**< A string object holding content, e.g. Button. */
    public Int32 s32Progress {get; set;} = s32Progress; /**< A Int32 object holding current progress, e.g. ProgressBar. */
    public String strLog {get; set;} = strLog;          /**< A string object holding log, e.g. RichTextBox. */
  };
};
