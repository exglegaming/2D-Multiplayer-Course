using Godot;

namespace DMultiplayerCourse.Scripts.UI.main_menu;

public partial class MainMenu : Control {
    private const int Port = 3000;
    
    private Button _hostButton;
    private Button _joinButton;
    
    public override void _Ready() {
        _hostButton = GetNode<Button>("%HostButton");
        _joinButton = GetNode<Button>("%JoinButton");

        _hostButton.Pressed += OnHostPressed;
        _joinButton.Pressed += OnJoinPressed;
        Multiplayer.PeerConnected += OnPeerConnected;
    }

    private void OnHostPressed() {
        var serverPeer = new ENetMultiplayerPeer();
        serverPeer.CreateServer(Port);
        Multiplayer.MultiplayerPeer = serverPeer;
    }

    private void OnJoinPressed() {
        var clientPeer = new ENetMultiplayerPeer();
        clientPeer.CreateClient("127.0.0.1", Port);
        Multiplayer.MultiplayerPeer = clientPeer;
    }

    private void OnPeerConnected(long id) {
        GD.Print($"My peer id is: {Multiplayer.GetUniqueId()}");
        GD.Print($"Peer connected {id}");
    }
}