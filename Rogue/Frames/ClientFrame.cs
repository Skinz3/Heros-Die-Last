using Rogue.Network;
using Rogue.Scenes;
using Rogue.Core.Utils;
using Rogue.Protocol.Enums;
using Rogue.Protocol.Messages.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Core.Network;
using Rogue.Core.Scenes;

namespace Rogue.Frames
{
    public abstract class ClientFrame : Frame
    {
        private Logger logger = new Logger();

        public abstract FrameEnum FrameEnum
        {
            get;
        }
        protected Scene Scene
        {
            get;
            private set;
        }
        public bool IsLoading
        {
            get;
            private set;
        }
        public ClientFrame(string sceneName = "") : base(sceneName)
        {
            IsLoading = false;
        }
        public override void OnLeave()
        {
            logger.Write("Leave frame: " + FrameEnum);
        }

        public override void Enter()
        {
            logger.Write("Enter in frame: " + FrameEnum);

            if (UseSceneManager)
            {
                IsLoading = true;
                SceneManager.OnSceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(SceneName);
            }
            else
            {
                OnFrameLoaded();
            }
        }

        private void OnSceneLoaded(Scene scene)
        {
            if (scene.GetType().Name == SceneName)
            {
                IsLoading = false;
                Scene = scene;
                OnFrameLoaded();
                SceneManager.OnSceneLoaded -= OnSceneLoaded;
            }
        }
        protected virtual void OnFrameLoaded()
        {
            if (UseSceneManager)
            {
                ClientHost.Client.Send(new FrameLoadedMessage(FrameEnum));
            }
        }
    }
}
