﻿using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.SignalR;
using ReseauEntreprise.Session;
using ReseauEntreprise.Hubs;

[assembly: OwinStartup(typeof(SignalRChat.Startup))]
namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new SessionUserProvider());
        }


    }
}