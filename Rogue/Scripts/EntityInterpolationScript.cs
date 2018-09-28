using Microsoft.Xna.Framework;
using Rogue.Objects;
using Rogue.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rogue.Collisions;
using MonoFramework.Objects;
using MonoFramework.Objects.Abstract;
using MonoFramework.Collisions;

namespace Rogue.Scripts
{
    public class EntityInterpolationScript : IScript
    {
        public static bool UseInterpolation;

        private MovableEntity Target
        {
            get;
            set;
        }
        /// <summary>
        /// Le pourcentage de temps entre td et ta
        /// </summary>
        private float CurrentRatio
        {
            get
            {
                return (float)(CurrentTime / TotalTime);
            }
        }
        /// <summary>
        /// Le temps total qu'a mit l'entitée a se déplacer entre a et b
        /// </summary>
        private float TotalTime
        {
            get
            {
                return (float)(NextPosition.Time - PreviousPosition.Time).TotalMilliseconds;
            }
        }
        /// <summary>
        /// Le temps actuel 
        /// </summary>
        private double CurrentTime
        {
            get;
            set;
        }
        /// <summary>
        /// La position de départ de l'entitée.
        /// </summary>
        public EntityPositionExtended PreviousPosition
        {
            get;
            private set;
        }
        /// <summary>
        /// La position d'arrivée de l'entitée
        /// </summary>
        public EntityPositionExtended NextPosition
        {
            get;
            set;
        }
        /// <summary>
        /// Doit t-on interpoler? 
        /// Seulement si la position d'arrivée envoyée par le serveur est différente de la position actuelle de l'objet.
        /// </summary>
        public bool InterpolationRequired
        {
            get
            {
                return NextPosition.Position.HasValue && (NextPosition.Position.Value != Target.Position || NextPosition.Direction != Target.MovementEngine.Direction);
            }
        }
        public void Initialize(GameObject gameObject)
        {
            this.Target = (MovableEntity)gameObject;
        }
        public void Restore()
        {
            NextPosition = default(EntityPositionExtended);
            PreviousPosition = default(EntityPositionExtended);
        }

        public void Restore(Vector2 position, DirectionEnum direction)
        {
            NextPosition = new EntityPositionExtended(position, direction, DateTime.Now);
            PreviousPosition = NextPosition;
        }

        public void OnPositionReceived(Vector2 position, DirectionEnum direction)
        {
            if (Target.Dashing)
            {
                return;
            }
            if (NextPosition.Position == null) // tout première position reçue. (Previous est donc indéfinie)
            {
                NextPosition = new EntityPositionExtended(position, direction, DateTime.Now);
            }

            this.Target.MovementEngine.Direction = direction;// (position - Target.Position).GetDirection();

            this.Target.Position = NextPosition.Position.Value; // Quoi qu'il arrive avec l'interpolation, l'entitée est téléportée au point d'arrivée
            CurrentTime = 0; // Le compteur de temps repart a 0
            PreviousPosition = NextPosition; // La position précédente devient l'ancienne.
            NextPosition = new EntityPositionExtended(position, direction, DateTime.Now);
            /*  var pos = message.position - entity.Position;

              if (entity.Position != message.position)
              {
                  pos *= new Microsoft.Xna.Framework.Vector2(10);
                  pos.Normalize();
                  entity.Position = message.position;
                  ((MovableEntity)entity).MovementEngine.Direction = pos.GetDirection();
              } */
        }
        public void Update(GameTime time)
        {
            if (Target.Dashing)
            {
                return;
            }
            if (InterpolationRequired)
            {
                CurrentTime += (TotalTime / MainPlayerScript.PositionUpdateFrameCount);

                if (float.IsNaN(CurrentRatio))
                {
                    return;
                }

                var result = Vector2.Lerp(PreviousPosition.Position.Value, NextPosition.Position.Value, CurrentRatio);

                if (float.IsNaN(result.X))
                {
                    return;
                }

                Target.Position = result;
            }
        }

        public void Dispose()
        {
            Restore();
        }

        public void OnRemove()
        {

        }
    }
    /// <summary>
    /// Structure représentant une position reçue par le client
    /// </summary>
    public struct EntityPositionExtended
    {
        /// <summary>
        /// Position de l'entitée
        /// </summary>
        public Vector2? Position;
        /// <summary>
        /// Date a laquelle cette position a été reçue.
        /// </summary>
        /// 
        public DirectionEnum Direction;

        public DateTime Time;

        public EntityPositionExtended(Vector2 position, DirectionEnum direction, DateTime time)
        {
            this.Position = position;
            this.Direction = direction;
            this.Time = time;
        }

    }
}
