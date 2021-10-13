using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Interfaces;

namespace WillyNet.Charlas.Infraestructure.Shared.Services.SignalRServices
{
    public class BroadcastHub : Hub<IHubClient>
    {
    }
}
