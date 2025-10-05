using Godot;

namespace DMultiplayerCourse.Scripts.UI.MainMenu;

public partial class MainMenu : Control {
    private const int Port = 3000;
    
    private PackedScene _mainScene;
    private Button _hostButton;
    private Button _joinButton;
    
    public override void _Ready() {
        _mainScene = ResourceLoader.Load<PackedScene>("uid://j2rtu51dup7t");
        _hostButton = GetNode<Button>("%HostButton");
        _joinButton = GetNode<Button>("%JoinButton");

        _hostButton.Pressed += OnHostPressed;
        _joinButton.Pressed += OnJoinPressed;
        Multiplayer.ConnectedToServer += OnConnectedToServer;
    }

    private void OnHostPressed() {
        var serverPeer = new ENetMultiplayerPeer();
        serverPeer.CreateServer(Port);
        Multiplayer.MultiplayerPeer = serverPeer;
        GetTree().ChangeSceneToPacked(_mainScene);
    }

    private void OnJoinPressed() {
        var clientPeer = new ENetMultiplayerPeer();
        clientPeer.CreateClient("127.0.0.1", Port);
        Multiplayer.MultiplayerPeer = clientPeer;
        
    }

    private void OnConnectedToServer() {
        GetTree().ChangeSceneToPacked(_mainScene);
    }
}
