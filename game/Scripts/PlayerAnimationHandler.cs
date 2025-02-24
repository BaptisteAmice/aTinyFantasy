using Godot;
using System;

public partial class PlayerAnimationHandler : AnimatedSprite2D
{
    public string currentAnimation { get; set; } = "none";
    //function to update the animation
    public void UpdateAnimation(Vector2 velocity)
    {
        string new_animation = "none";

        if (velocity.Length() > 0)
        {
            if (Mathf.Abs(velocity.X) > Mathf.Abs(velocity.Y))
            {
                if (velocity.X > 0)
                {
                    new_animation = "running";
                }
                else
                {
                    new_animation = "running";
                }
            }
            else
            {
                if (velocity.Y > 0)
                {
                    new_animation = "running";
                }
                else
                {
                    new_animation = "running";
                }
            }
        }
        else
        {
            new_animation = "idle";
        }

        if (new_animation != currentAnimation)
        {
            currentAnimation = new_animation;
            Play(currentAnimation);
        }
        
    }
   



}
