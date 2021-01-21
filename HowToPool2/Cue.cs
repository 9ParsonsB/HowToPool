using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace HowToPool
{
    class Cue : Entity
    {
        // Prevents power being from being too high
        public float maxPower = 300.0f;
        public Vector2 cuePullSpeed = new Vector2(100);
        public Vector2 defaultPos;
        public int degrees = 0;

        public Vector2 power;
        public bool released = true;
        public bool selected = false;

        public float rotAmount = -0.02f;
        public bool drawCue = false;

        // Wether the cue ball has just stopped moving
        public bool hasStopped = false;

        public void RotateCue(List<Entity> balls)
        {
            //If the white ball is not moving
            if (balls[0].speed.X == 0 && balls[0].speed.Y == 0)
            {
                // Get top right position
                // Vector2 temp = new Vector2(box.Min.X + (box.Max.X - box.Min.X), box.Min.Y + (box.Max.Y - box.Min.Y));

                var wBall = balls[0];
                Vector2 mousePosition = Raylib.GetMousePosition();

                Vector2 dPos = wBall.position - mousePosition;
                angle = MathF.Atan2(-dPos.Y, -dPos.X);
                Vector2 ratio = wBall.position / mousePosition;

                // pos = wBall.pos - new Vector2(texture.Width + 5, 0);

                double degereese = angle * (180.0 / Math.PI);
                double cueAngle = 180.0 - (degereese + 90.0);
                cueAngle = Math.PI * cueAngle / 180.0;
            }
        }

        public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            return Vector2.Transform(point - origin, Matrix4x4.CreateRotationZ(rotation)) + origin;
        }

        public void Update(float dt, List<Entity> balls)
        {
            float twoPi = (2 * MathF.PI);
            float oneRadian = (1 * Raylib.RAD2DEG);

            if (angle < 0)
            {
                degrees = (int)((twoPi - (angle * -1)) * oneRadian);
            }
            else
            {
                degrees = (int)(angle * oneRadian);
            }

            if (balls[0].speed.X == 0 && balls[0].speed.Y == 0)
            {
                // Rotates cue based on mouse position whenever cue ball not moving
                RotateCue(balls);

                //If cue ball just stopped moving
                if (!hasStopped)
                {
                    // Set pos to current pos of cue ball
                    position = new Vector2(balls[0].position.X + balls[0].radius / 2, balls[0].position.Y + balls[0].radius / 2);
                    defaultPos = position;
                    hasStopped = true;
                }
            }
            else
            {
                hasStopped = false;
            }

            // If user clicks on cue
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) && !selected)
            {
                // Cue is now selected
                selected = true;

                // Cue isn't released
                released = false;
            }

            if (selected)
            {
                if (balls[0].speed.X == 0 && balls[0].speed.Y == 0)
                {
                    //If cue on left side of ball
                    if (degrees < 90 || degrees < 360 && degrees > 270)
                    {
                        position.X -= cuePullSpeed.X * dt;

                        if (power.X < maxPower)
                        {
                            power.X += 10 * dt;
                        }

                        // Increase velocity of cue
                        speed.X += power.X;
                    }
                    else
                    {
                        //If cue on right side of ball
                        if (degrees > 90 && degrees < 270)
                        {
                            position.X += cuePullSpeed.X * dt;

                            if (power.X < maxPower)
                            {
                                power.X -= 10 * dt;
                            }

                            // Increase velocity of cue
                            speed.X -= power.X;
                        }
                    }

                    // If cue on top side of ball
                    if (degrees > 0 && degrees < 180)
                    {
                        position.Y -= cuePullSpeed.Y * dt;

                        if (power.Y < maxPower)
                        {
                            power.Y += 10 * dt;
                        }

                        // Increase velocity of cue
                        speed.Y += power.Y;
                    }
                    else
                    {
                        // If cue on bottom side of ball
                        if (degrees > 180)
                        {
                            position.Y += cuePullSpeed.Y * dt;

                            if (power.Y < maxPower)
                            {
                                power.Y -= 10 * dt;
                            }

                            // Increase velocity of cue
                            speed.Y -= power.Y;
                        }
                    }
                }
            }

            // If cue released after cue has been selected
            if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON) && selected == true && balls[0].speed.X == 0 && balls[0].speed.Y == 0)
            {
                released = true;
                selected = false;
            }

            if (released)
            {
                // If cue has not returned to ball
                if (position.X < defaultPos.X || position.Y < defaultPos.Y)
                {
                    // Move cue back towards ball
                    position += speed * dt;
                }
                else
                {
                    Console.WriteLine(power);

                    // Apply power to ball
                    balls[0].speed.X += power.X;
                    balls[0].speed.Y += power.Y;

                    // Reset released
                    released = false;

                    speed = new Vector2(0, 0);
                    power = new Vector2(0, 0);
                }
            }
        }
    }
}
