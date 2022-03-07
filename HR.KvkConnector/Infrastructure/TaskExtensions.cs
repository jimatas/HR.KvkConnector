using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HR.KvkConnector.Infrastructure
{
    internal static class TaskExtensions
    {
        /// <summary>
        /// Adds cancellation to a task that wraps an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task">The asynchronous operation.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<TResult> WithCancellation<TResult>(this Task<TResult> task, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<TResult>();
            cancellationToken.Register(() => taskCompletionSource.TrySetCanceled(), useSynchronizationContext: false);
            var cancellationTask = taskCompletionSource.Task;

            var completedTask = await Task.WhenAny(task, cancellationTask).WithoutCapturingContext();
            if (completedTask == cancellationTask)
            {
                _ = task.ContinueWith(_ => task.Exception,
                    TaskContinuationOptions.OnlyOnFaulted |
                    TaskContinuationOptions.ExecuteSynchronously);
            }

            return await completedTask.WithoutCapturingContext();
        }

        /// <summary>
        /// Attempt to make configuring an awaiter so as not to continue on the captured context more readable.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static ConfiguredTaskAwaitable<TResult> WithoutCapturingContext<TResult>(this Task<TResult> task)
            => task.ConfigureAwait(continueOnCapturedContext: false);
    }
}
