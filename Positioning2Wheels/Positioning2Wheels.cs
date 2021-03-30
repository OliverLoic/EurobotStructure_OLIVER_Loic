using EventArgsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Positioning2WheelsNS
{
    public class Positioning2Wheels
    {
        int robotId;
        Location robotLocation = new Location();

        public Positioning2Wheels(int id)
        {
            robotId = id;
        }

        public void OnOdometryRobotSpeedReceived(object sender, PolarSpeedArgs e)
        {
            /// On fait le calcul du nouveau positionnement du robot
            int fEch = 50;
            //robotLocation.Theta += (e.Vtheta + variablePrece.Vtheta) / (2 * fEch);
            //robotLocation.X += Math.Cos(robotLocation.Theta) * ((e.Vx + variablePrece.Vx) / (2 * fEch));
            //robotLocation.Y += Math.Sin(robotLocation.Theta) * ((e.Vx + variablePrece.Vx) / (2 * fEch));

            robotLocation.Theta += e.Vtheta / fEch;
            robotLocation.X += Math.Cos(robotLocation.Theta) * (e.Vx / fEch);
            robotLocation.Y += Math.Sin(robotLocation.Theta) * (e.Vx / fEch);

            OnCalculatedLocation(robotId, robotLocation);
        }

        /// Output events
        public event EventHandler<LocationArgs> OnCalculatedLocationEvent;
        public virtual void OnCalculatedLocation(int id, Location locationRefTerrain)
        {
            var handler = OnCalculatedLocationEvent;
            if (handler != null)
            {
                handler(this, new LocationArgs { RobotId = id, Location = locationRefTerrain });
            }
        }
    }
}
