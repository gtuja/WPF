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
  public class TaskEventArgs(String strId) : System.EventArgs
  {
    public String strId {get; set;} = strId;            /**< A string object holding the ID of task. */
  };

  /**
  * @brief A public class holding task entry event arguments.
  * @see System.EventArgs
  * @see TaskEventArgs
  */
  public class TaskEventEntryArgs(String strId, String strContent, Int32 s32ProgressMax, String strLog) : TaskEventArgs(strId)
  {
    public String strContent {get; set;} = strContent;        /**< A String object holding content, e.g. Button. */
    public Int32 s32ProgressMax {get; set;} = s32ProgressMax; /**< A Int32 object holding the max of progress, e.g. ProgressBar. */
    public String strLog {get; set;} = strLog;                /**< A string object holding log, e.g. RichTextBox. */
  };

  /**
  * @brief A public class holding task progress event arguments.
  * @see System.EventArgs
  * @see TaskEventArgs
  */
  public class TaskEventProgressArgs(String strId, Int32 s32Progress, String strStatus) : TaskEventArgs(strId)
  {
    public Int32 s32Progress {get; set;} = s32Progress; /**< A Int32 object holding current progress, e.g. ProgressBar. */
    public String strStatus {get; set;} = strStatus;    /**< A string object holding status, e.g. Label. */
  };

  /**
  * @brief A public class holding task log event arguments.
  * @see System.EventArgs
  * @see TaskEventArgs
  */
  public class TaskEventLogArgs(String strId, String strLog) : TaskEventArgs(strId)
  {
    public String strLog {get; set;} = strLog;  /**< A string object holding status, e.g. Label. */
  };

  /**
  * @brief A public class holding task exit event arguments.
  * @see System.EventArgs
  * @see TaskEventArgs
  */
  public class TaskEventExitArgs(String strId, String strContent, String strLog) : TaskEventArgs(strId)
  {
    public String strContent {get; set;} = strContent;  /**< A Int32 object holding content, e.g. Button. */
    public String strLog {get; set;} = strLog;          /**< A string object holding status, e.g. Label. */
  };
};
