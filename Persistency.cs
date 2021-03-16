using UniRx;
using FlowCanvas;
using FlowCanvas.Nodes;
using ParadoxNotion.Design;

namespace DH.FCExtensions.GameFoundations
{
    [Category("Functions/Extensions/Game Foundations")]
    public class SaveGameFoundations : FlowControlNode, IGameFoundationNode
    {
        protected override void RegisterPorts()
        {
            var output = AddFlowOutput("Completed");

            var input = AddFlowInput("In", delegate(Flow flow) { this.Save().Subscribe(unit => output.Call(flow)); });
        }
    }
}