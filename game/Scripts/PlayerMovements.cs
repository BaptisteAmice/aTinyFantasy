using Godot;
using System;

public partial class PlayerMovements : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 200;

    //get child animation handler
    private PlayerAnimationHandler _playerAnimationHandler;

    public void GetInput()
    {

        Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
        Velocity = inputDirection * Speed;
        if (HasNode("PlayerAnimationHandler"))
        {
            _playerAnimationHandler = GetNode<PlayerAnimationHandler>("PlayerAnimationHandler");
        }
        else
        {
            GD.PrintErr("Error: PlayerAnimationHandler node not found!");
        }
        
    }

    public override void _PhysicsProcess(double delta)
    {
        GetInput();

        //flip left or right
        if (Velocity.X != 0)
        {
            _playerAnimationHandler.FlipH = Velocity.X < 0;
        }
        _playerAnimationHandler.UpdateAnimation(Velocity);

        var collisionInfo = MoveAndCollide(Velocity * (float)delta);
        if (collisionInfo != null)
            Velocity = Velocity.Bounce(collisionInfo.GetNormal());
        

        //print if collision is detected with collisionlayer of tilemap
        if (IsOnWall())
        {
            GD.Print("Collision detected");
        }
    }
}
