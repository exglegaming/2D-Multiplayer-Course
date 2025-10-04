using Godot;

namespace DMultiplayerCourse.Scripts.Player;

public partial class Player : CharacterBody2D {
    public override void _Process(double delta) {
        var movementVector = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");
        Velocity = movementVector * 100;
        MoveAndSlide();
    }
}
