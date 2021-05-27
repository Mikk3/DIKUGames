using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using OpenTK.Graphics.OpenGL;

namespace Breakout.PowerUps {

    public interface IPowerUp {

        //Entity Entity {get; set;}
        Shape shape {get; set;}
        //void Create(Vec2F position);

        void Activate();
    }
}