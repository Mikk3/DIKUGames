using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;

namespace Galaga {
    public class Enemy : Entity {
        private static float GlobalSpeed = 1;
        public int hitpoints { get; private set; }
        public float Speed { get; private set; }
        private IBaseImage alternativeImage;

        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage alternativeImage, float speed) :
        base(shape, image) {
            hitpoints = 2;
            Speed = speed * GlobalSpeed;
            this.alternativeImage = alternativeImage;    
        }

        public void Damage() {
            hitpoints--;
        }
        
        public bool isDead() {
            return hitpoints < 1;
        }

        public void Enrage() {
            changeStride();
            Speed *= 1.50f;
        }

        public void changeStride() {
            base.Image = alternativeImage;
        }

        public static void IncreaseDifficulty() {
            GlobalSpeed *= 1.25f;
        }
    }
}