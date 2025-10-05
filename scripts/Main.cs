using Godot;

namespace DMultiplayerCourse.Scripts;

public partial class Main : Node {
    private MultiplayerSpawner _multiplayerSpawner;
    private PackedScene _playerScene;
    
    public override void _Ready() {
        _multiplayerSpawner = GetNode<MultiplayerSpawner>("MultiplayerSpawner");
        _playerScene = ResourceLoader.Load<PackedScene>("uid://bwthgwr6h4l62");
        
        _multiplayerSpawner.SpawnFunction = Callable.From<Variant, Node>((data) => {
            var player = _playerScene.Instantiate();
            player.Name = data.AsInt64().ToString();
            return player;
        });
        
        RpcId(1, MethodName.PeerReady);
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    private void PeerReady() {
        var senderId = Multiplayer.GetRemoteSenderId();
        _multiplayerSpawner.Spawn(senderId);
    }
}
