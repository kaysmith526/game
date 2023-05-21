using System.Collections;
using System.Collections.Generic;

namespace StarterGame.Commands
{
    public class ClockCommand : Command
    {
        private Timer _timer;

        // Designated Constructor
        public ClockCommand() : base()
        {
            Name = "time";
            Usage = "time";
            NotificationCenter.Instance.AddObserver("TimerStarted_Game", notification =>
            {
                _timer = notification.Object as Timer;
            });
        }

        override
        public bool Execute(Player player)
        {
            if (_timer != null)
            {
                player.OutputMessage("You have " + _timer.RemainingSeconds + " seconds remaining!");
            }
            else //should never happen, timer starts when game starts, and by this point it should have definitely have started
            {
                player.OutputMessage("The timer has not started!");
            }
            return false;
        }
    }
}
