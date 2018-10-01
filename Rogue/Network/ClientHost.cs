﻿using Microsoft.Xna.Framework;
using Rogue.Network;
using MonoFramework.Network.Protocol;
using Rogue.Scenes;
using MonoFramework.Utils;
using Rogue.Frames;
using Rogue.Protocol.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MonoFramework.Network;
using MonoFramework.Scenes;

namespace Rogue.Network
{
    /// <summary>
    /// Host, cette classe permet de mettre en lien le client réseau et le client unity 
    /// </summary>
    class ClientHost
    {
        public const string SCENE_TO_LOAD_ON_CONN_CLOSED = "LoginScene";

        static Logger logger = new Logger();

        public static bool IsInitialized
        {
            get
            {
                return Client != null;
            }
        }
        public static RogueClient Client
        {
            get;
            private set;
        }

        public static void Initialize()
        {
            ClientHost.Client = new RogueClient(new AuthentificationFrame());
            ClientHost.Client.OnConnectionFailed += OnConnectionFailed;
            ClientHost.Client.OnConnectionSucceed += OnConnectionSucceed;
            ClientHost.Client.OnConnectionLost += OnConnectionLost;
        }


        public static void DestroyClient()
        {
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }
        }
        public static void Connect()
        {
            ClientHost.Client.Connect("130.211.97.242", 555);
        }

        private static void OnConnectionClosed(AbstractClient obj)
        {
            logger.Write("Connection closed!", MessageState.WARNING);
        }

        private static void OnConnectionSucceed()
        {
            logger.Write("Connected to server!", MessageState.INFO);
        }
        private static void OnConnectionFailed()
        {
            logger.Write("Unable to connect to server!", MessageState.WARNING);
        }
        private static void OnConnectionLost()
        {
            Leave();
            logger.Write("We lost connection to the server!", MessageState.WARNING);
        }
        public static void Leave()
        {
            DestroyClient();
            SceneManager.LoadScene(SCENE_TO_LOAD_ON_CONN_CLOSED);
        }
    }
}