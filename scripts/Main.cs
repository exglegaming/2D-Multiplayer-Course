using Godot;

namespace DMultiplayerCourse.Scripts;

public partial class Main : Node {
    public override void _Ready() {
        RpcId(1, MethodName.PeerReady);
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    private void PeerReady() {
        GD.Print($"Peer {Multiplayer.GetRemoteSenderId()} ready");
    }
}
