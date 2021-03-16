using FlowCanvas;
using FlowCanvas.Nodes;
using ParadoxNotion.Design;
using UnityEngine.GameFoundation;

namespace DH.FCExtensions.GameFoundations
{
    [Category("Functions/Extensions/Game Foundations")]
    public class OnGameFoundationInit : EventNode
    {
        private FlowOutput output;

        protected override void RegisterPorts()
        {
            output = AddFlowOutput("Initialized");
        }

        public override void OnPostGraphStarted()
        {
            GameFoundationSdk.initialized += GameFoundationSdkOninitialized;
        }

        public override void OnGraphStoped()
        {
            GameFoundationSdk.initialized -= GameFoundationSdkOninitialized;
        }

        private void GameFoundationSdkOninitialized()
        {
            output.Call(new Flow());
        }
    }
}