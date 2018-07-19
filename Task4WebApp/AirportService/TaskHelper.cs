using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using DALProject.Models;

namespace AirportService
{
	public static class TaskHelper
	{
		public static Task<List<TEntity>> RunAsync<TEntity>(Func<List<TEntity>> function,int delay=5000) where TEntity : class
		{
			if (function == null) throw new ArgumentNullException("TaskHelper");
			var tcs = new TaskCompletionSource<List<TEntity>>();

			Timer timer = new Timer(delay);
			timer.Start();
			timer.Elapsed += (o,e) =>
			{
				try
				{
					List<TEntity> result = function();
					tcs.SetResult(result);
					timer.Stop();
				}
				catch (Exception exc)
				{
					tcs.SetException(exc);
				}

			};



			return tcs.Task;
		}
	}
}
