using Godot;
using System;
using aTinyFantasy.InventorySystem.Inventory;

namespace aTinyFantasy.Player
{
    public partial class Player : CharacterBody2D
    {
        [Export]
        public int Speed { get; set; } = 200;

        //get child animation handler
        private PlayerAnimationHandler _playerAnimationHandler;

        [Export]
        public InventoryData InventoryData;

        public Action ToggleInventoryEvent;
        

        public override void _Ready()
        {
            _playerAnimationHandler = GetNodeOrNull<PlayerAnimationHandler>("PlayerAnimationHandler");
        }

        public void GetInput()
        {
            if (Input.IsActionJustPressed("inventory"))
            {
                ToggleInventoryEvent?.Invoke();
            } 

            Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
            Velocity = inputDirection * Speed;
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
}