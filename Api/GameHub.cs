using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
	public class GameHub : Hub
	{
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await base.OnDisconnectedAsync(exception);
		}

		public override async Task OnConnectedAsync()
		{
			await base.OnConnectedAsync();
		}
	}
}
