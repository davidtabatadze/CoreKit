using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreKit.Sync
{

    /// <summary>
    /// Represents functionality to run async methods within a sync process.
    /// </summary>
    public static class SyncKit
    {

        /// <summary>
        /// TaskFactory object
        /// </summary>
        private static readonly TaskFactory Factory = new TaskFactory(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default
        );

        /// <summary>
        /// Executes an async method synchronously
        /// </summary>
        /// <param name="task">Task/Method to execute</param>
        public static void Run(Func<Task> task) =>
            Factory.StartNew(task)
                   .Unwrap()
                   .GetAwaiter()
                   .GetResult();

        /// <summary>
        /// Executes an async method synchronously
        /// </summary>
        /// <typeparam name="T">Task/Method return type</typeparam>
        /// <param name="task">Task/Method to execute</param>
        /// <returns>Task/Method result</returns>
        public static T Run<T>(Func<Task<T>> task) =>
            Factory.StartNew(task)
                   .Unwrap()
                   .GetAwaiter()
                   .GetResult();

    }

}
