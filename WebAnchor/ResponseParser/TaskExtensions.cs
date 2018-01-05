using System;
using System.Threading.Tasks;

namespace WebAnchor
{
    public static class TaskExtensions
    {
        public static async Task<TOut> Then<TIn, TOut>(this Task<TIn> tin, Func<TIn, Task<TOut>> tout)
        {
            TaskCompletionSource<TOut> tcs = new TaskCompletionSource<TOut>();
            var lastTask = tin.ContinueWith(delegate
            {
                if (tin.IsFaulted)
                {
                    tcs.TrySetException(tin.Exception.InnerExceptions);
                }
                else if (tin.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else if (tin.IsCompleted)
                {
                    try
                    {
                        var next = tout(tin.Result);
                        if (next == null)
                        {
                            tcs.TrySetCanceled();
                        }
                        else
                        {
                            next.ContinueWith(delegate
                              {
                                  if (next.IsFaulted)
                                  {
                                      tcs.TrySetException(next.Exception.InnerExceptions);
                                  }
                                  else if (next.IsCanceled)
                                  {
                                      tcs.TrySetCanceled();
                                  }
                                  else
                                  {
                                      tcs.TrySetResult(next.Result);
                                  }
                              }, TaskContinuationOptions.ExecuteSynchronously);
                        }
                    }
                    catch (Exception exc)
                    {
                        tcs.TrySetException(exc);
                    }
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            var result = await tcs.Task;
            return result;
        }

        public static async Task Then<TIn>(this Task<TIn> tin, Func<TIn, Task> tout)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            var lastTask = tin.ContinueWith(delegate
            {
                if (tin.IsFaulted)
                {
                    tcs.TrySetException(tin.Exception.InnerExceptions);
                }
                else if (tin.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else if (tin.IsCompleted)
                {
                    try
                    {
                        var next = tout(tin.Result);
                        if (next == null)
                        {
                            tcs.TrySetCanceled();
                        }
                        else
                        {
                            next.ContinueWith(delegate
                            {
                                if (next.IsFaulted)
                                {
                                    tcs.TrySetException(next.Exception.InnerExceptions);
                                }
                                else if (next.IsCanceled)
                                {
                                    tcs.TrySetCanceled();
                                }
                                else
                                {
                                    tcs.TrySetResult(null);
                                }
                            }, TaskContinuationOptions.ExecuteSynchronously);
                        }
                    }
                    catch (Exception exc)
                    {
                        tcs.TrySetException(exc);
                    }
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            var result = await tcs.Task;
            return;
        }
    }
}